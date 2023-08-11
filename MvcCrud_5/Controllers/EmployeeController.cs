using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using MvcCrud_5.Models;
using System.Configuration;

namespace MvcCrud_5.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        string ConnectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
         
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {

                con.Open();
                
                SqlCommand cmd = new SqlCommand("ReadStoredProcedure", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);



            }
            return View(dt);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View(new EmployeeModel());
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmployeeModel employeeModel)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                cmd.Parameters.AddWithValue("@Emp_Age", employeeModel.Emp_Age);
                cmd.Parameters.AddWithValue("@Mobile", employeeModel.Mobile);
                cmd.ExecuteNonQuery();
            }
            // TODO: Add insert logic here

            return RedirectToAction("Index");


        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ReadEmployeeByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            if (dt.Rows.Count == 1)
            {
                employeeModel.id = Convert.ToInt32(dt.Rows[0][0].ToString());
                employeeModel.FirstName = dt.Rows[0][1].ToString();
                employeeModel.LastName = dt.Rows[0][2].ToString();
                employeeModel.Emp_Age = dt.Rows[0][3].ToString();
                employeeModel.Mobile = dt.Rows[0][4].ToString();
                return View(employeeModel);
            }
            else return RedirectToAction("Index");
        }
        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmployeeModel employeeModel)
            {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EditEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", employeeModel.id);
                cmd.Parameters.AddWithValue("@FirstName", employeeModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employeeModel.LastName);
                cmd.Parameters.AddWithValue("@Emp_Age", employeeModel.Emp_Age);
                cmd.Parameters.AddWithValue("@Mobile", employeeModel.Mobile);
                cmd.ExecuteNonQuery();
            }
            // TODO: Add insert logic here

            return RedirectToAction("Index");
            }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string procedure = "DeleteEmployee";
                SqlCommand cmd = new SqlCommand(procedure, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

       
       
    }
}
