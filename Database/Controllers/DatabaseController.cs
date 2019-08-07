using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using Database_Project.ViewModels;
using System.Data.Entity;

namespace Database_Project.Controllers
{
    public class DatabaseController : Controller
    {
        // GET: Database
        public ActionResult Index()
        {
            return View();
        }


        //SQL string that connects to the database
        private SqlConnection MyConnection = new SqlConnection("Data Source=DESKTOP-E47T82S;Initial Catalog=MyShop;Integrated Security=True");

       //Create a new instance of the list of employees
        public static List<EmployeeViewModel> EmployeeList = new List<EmployeeViewModel>();


        // GET: Data
        //Home page
        public ActionResult Main()
        {
            //displays objects in the table
            try
            {
                EmployeeList.Clear();
                SqlCommand mySelectCommand = new SqlCommand("Select [EmployeeID], [EmployeeName],[EmployeeSurname],[EmployeeSalary],[EmployeeAge] FROM [Employee]", MyConnection);
                MyConnection.Open();
                SqlDataReader myReader = mySelectCommand.ExecuteReader();
                while (myReader.Read())
                {
                    //create a new object and assign values from the table to it and adds the employee object to list of objects
                    EmployeeViewModel Employee = new EmployeeViewModel();
                    Employee.mID = (int)myReader["EmployeeID"];
                    Employee.mName = myReader["EmployeeName"].ToString();
                    Employee.mSurname = myReader["EmployeeSurname"].ToString();
                    Employee.mSalary = (double)myReader["EmployeeSalary"];
                    Employee.mAge = (int)myReader["EmployeeAge"];
                    EmployeeList.Add(Employee);
                }
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                MyConnection.Close();
            }
            return View(EmployeeList);
        }

        //Displays add form
        public ActionResult Add()
        {

            return View();
        }

        // Method to insert values from form to database
        public ActionResult InsertDatabase(string EmployeeName, string EmployeeSurname, double EmployeeSalary, int EmployeeAge)
        {
            try
            {
                SqlCommand myInsertCommand = new SqlCommand("Insert into Employee Values('" + EmployeeName + "', '" + EmployeeSurname + "','" + EmployeeSalary + "','" + EmployeeAge + "')", MyConnection);
                MyConnection.Open();
                int rowsAffected = myInsertCommand.ExecuteNonQuery();
                ViewBag.Message = "Success: " + rowsAffected + " rows added.";
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                MyConnection.Close();
            }

            return View("Add");
        }

        //Get ID of the employee that is being updated from the list and accepts it as a parameter
        public ActionResult Update(int ID)
        {
            //searches in the list for a object with a matching id and assigns it to a new instance of a object
            //Object is used to populate fields
            EmployeeViewModel UpdateEmployee = new EmployeeViewModel();
            UpdateEmployee = EmployeeList.Where(p => p.mID == ID).FirstOrDefault();
           return View(UpdateEmployee);
        }

        //Does the update query
        public ActionResult DoUpdate(int ID, string EmployeeName, string EmployeeSurname, double EmployeeSalary, int EmployeeAge)
        {

            try
            {

                SqlCommand MyUpdateCommand = new SqlCommand("Update Employee Set EmployeeName='" + EmployeeName + "',EmployeeSurname='" + EmployeeSurname + "',EmployeeSalary='" + EmployeeSalary + "',EmployeeAge='" + EmployeeAge + "' WHERE EmployeeID =" + ID, MyConnection);
                MyConnection.Open();
                int rowsAffected = MyUpdateCommand.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                MyConnection.Close();
            }

            return RedirectToAction("Main");
        }

        //Delete query
        public ActionResult Delete(int ID)
        {
            // recieves ID as a parameter and updates the object(row) in the database with the same ID
            try
            {
                MyConnection.Open();
                SqlCommand myDelete = new SqlCommand("Delete From Employee WHERE EmployeeID=" + ID, MyConnection);
                myDelete.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                ViewBag.Message = "Error: " + err.Message;
            }
            finally
            {
                MyConnection.Close();
            }

            return RedirectToAction("Main");
        }
    }
}
//Andile Maphalala