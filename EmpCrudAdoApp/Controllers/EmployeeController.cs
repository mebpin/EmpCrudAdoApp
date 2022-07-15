using EmpCrudAdoApp.Models;
using EmpCrudAdoApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EmpCrudAdoApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeRepo empRepo = new EmployeeRepo();
        //to addd record
        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();

        }
        [HttpPost]
        public IActionResult CreateEmployee(Employee emp)
        {
            try
            { 
                empRepo.AddEmployee(emp);
                return Content("Data has been inserted successfully!");
            }
            catch (SqlException ex)
            {
                return Content("OOPS!" + ex.Message);
            }
        }
        //to show all records in index page 
        public IActionResult Index()
        {
            IEnumerable<Employee> emplist = empRepo.GetAllEmployee();
            return View(emplist);
        }
        //to show record of individual student

        public IActionResult EmployeeDetail(int id)
        {
            try
            {
                Employee emp = empRepo.GetEmployeeData(id);
                return View(emp);
            }
            catch(SqlException ex)
            {
                return Content("OOPS!" + ex.Message);
            }
            
        }
        //to edit a record
        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            try
            {
                Employee emp = empRepo.GetEmployeeData(id);
                return View(emp);
            }
            catch (SqlException ex)
            {
                return Content("OOPS!" + ex.Message);
            }
        }
        [HttpPost]
        public IActionResult EditEmployee(Employee emp)
        {
            try
            {
                empRepo.UpdateEmployee(emp, emp.Id);
            }
            catch (SqlException ex)
            {
                return Content("OOPS!" + ex.Message);
            }
            return RedirectToAction("Index");
        }

        // to delete the record
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                Employee emp = empRepo.GetEmployeeData(id);
                empRepo.DeleteEmployee(emp, id);

            }
            catch(SqlException ex){
                return Content("OOPS!" + ex.Message);
            }
            return RedirectToAction("Index");
        }

    }
}
