using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Models;
using TestApplication.ViewModels;

namespace TestApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Test
        public String GetString()
        {
            return "Hello World is old now. It&rsquo;s time for wassup bro ;)";
        }

        public ActionResult Index()
        {
            //Employee emp = new Employee();

            //emp.FirstName = "Md.";
            //emp.LastName = "Asif";
            //emp.Salary = 100;

            //ViewData["Employee"] = emp;
            //ViewBag.Employee = emp;

            /***************** ViewModel *****************/
            EmployeeListViewModel lvmEmp = new EmployeeListViewModel();

            EmployeeBusinessLayer empBL = new EmployeeBusinessLayer();
            List<Employee> employees = empBL.GetEmployees();

            List<EmployeeViewModel> vmEmployees = new List<EmployeeViewModel>();

            foreach (Employee emp in employees)
            {
                EmployeeViewModel vmEmp = new EmployeeViewModel();

                vmEmp.EmployeeName = emp.FirstName + " " + emp.LastName;
                vmEmp.Salary = emp.Salary.ToString("C");

                if (emp.Salary > 1500) vmEmp.SalaryColor = "yellow";
                else vmEmp.SalaryColor = "green";

                vmEmployees.Add(vmEmp);
            }


            lvmEmp.Employees = vmEmployees;

            return View("Index", lvmEmp);
        }

        public ActionResult AddNew()
        {
            return View("CreateEmployee");
        }

        public string SaveEmployee(Employee e)
        {
            return e.FirstName + " " + e.LastName + "<br /> Salary: " + e.Salary;
        }
    }
}