﻿using System;
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
        public string GetString()
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

        public ActionResult SaveEmployee(Employee e, string BtnSubmit)
        {
            switch (BtnSubmit)
            {
                case "Save Employee":
                    if (ModelState.IsValid)
                    {
                        //return Content(e.FirstName + " " + e.LastName + "<br /> Salary: " + e.Salary);
                        EmployeeBusinessLayer empBL = new EmployeeBusinessLayer();
                        empBL.SaveEmployee(e);

                        return RedirectToAction("Index");
                    }

                    else
                    {
                        return View("CreateEmployee");
                    }          

                case "Cancel":
                    return RedirectToAction("Index");
            }

            return new EmptyResult();
        }
    }
}