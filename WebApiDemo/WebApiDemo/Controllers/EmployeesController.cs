using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        [HttpGet]
        [ActionName("GetEmployeeByID")]
        public Employee Get(int id)
        {
            //return listEmp.First(e => e.ID == id);  
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select idcliente,nombre,numExt from Ctrl_clietnes where IdCliente=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(reader.GetValue(0));
                emp.Name = reader.GetValue(1).ToString();
                emp.ManagerId = Convert.ToInt32(reader.GetValue(2));
            }
            myConnection.Close();
            return emp;
        }
    }
}
