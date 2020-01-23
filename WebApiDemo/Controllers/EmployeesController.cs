using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;
using System.Data.SqlClient;
using System.Data;
using System.Text;
//using System.Web.Mvc;

namespace WebApiDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        public static IList<Employee> listEmp = new List<Employee>()
        {
            #region commented code
            //    new Employee()
            //    {
            //        ID =001, FirstName="Sachin", LastName="Kalia",Department="Engineering"
            //    },
            //     new Employee()
            //    {
            //        ID =002, FirstName="Dhnanjay" ,LastName="Kumar",Department="Engineering"
            //    },
            //    new Employee()
            //    {
            //        ID =003, FirstName="Ravish", LastName="Sindhwani",Department="Finance"
            //    },
            //     new Employee()
            //    {
            //        ID =004, FirstName="Amit" ,LastName="Chaudhary",Department="Architect"
            //    },
            //     new Employee()
            //    {
            //        ID =004, FirstName="Anshu" ,LastName="Aggarwal",Department="HR"
            //    },
            #endregion commented code

        };

        [AcceptVerbs("GET")]
        public Employee RPCStyleMethodFetchFirstEmployees()
        {
            return listEmp.FirstOrDefault();
        }

        [HttpGet]
        [ActionName("GetEmployeeByID")]
        public Employee Get(int id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-DA944DO\SQLEXPRESS;Database=DBCompany;Trusted_Connection=True;MultipleActiveResultSets=true";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Employee where EMPLOYEE_ID=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(reader.GetValue(0));
                emp.Name = reader.GetValue(1).ToString();
                emp.ManagerId = Convert.ToInt32(reader.GetValue(3));
            }
            myConnection.Close();
            return emp;
        }


        [HttpPost]
        public void AddEmployee(Employee employee)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-DA944DO\SQLEXPRESS;Database=DBCompany;Trusted_Connection=True;MultipleActiveResultSets=true";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Employee (EmployeeId,Name,ManagerId) Values (@EmployeeId,@Name,@ManagerId)";
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            sqlCmd.Parameters.AddWithValue("@Name", employee.Name);
            sqlCmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }


        [ActionName("DeleteEmployee")]
        public void DeleteEmployeeByID(int id)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-DA944DO\SQLEXPRESS;Database=DBCompany;Trusted_Connection=True;MultipleActiveResultSets=true";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from Employee where EMPLOYEE_ID=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
