using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Filters;
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

        [Authorize]
        [HeaderFooterFilter]
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
                vmEmp.Salary = emp.Salary.ToString();

                if (emp.Salary > 1500) vmEmp.SalaryColor = "yellow";
                else vmEmp.SalaryColor = "green";

                vmEmployees.Add(vmEmp);
            }


            lvmEmp.Employees = vmEmployees;
//            lvmEmp.UserName = User.Identity.Name;

            //Add Footer's information too
//            lvmEmp.FooterData = new FooterViewModel();
//            lvmEmp.FooterData.CompnayName = "ASIF";
//            lvmEmp.FooterData.Year = DateTime.Now.Year.ToString();

            return View("Index", lvmEmp);
        }

        [ChildActionOnly]
        public ActionResult GetAddNewLink()
        {
            if (Convert.ToBoolean(Session["IsAdmin"]))
            {
                return PartialView("AddNewLink");
            }
            else
            {
                return new EmptyResult();
            }
        }

        [AdminFilter]
        [HeaderFooterFilter]
        public ActionResult AddNew()
        {
            CreateEmployeeViewModel createEmpVeiwModel = new CreateEmployeeViewModel();
//            createEmpVeiwModel.FooterData = new FooterViewModel();
//            createEmpVeiwModel.FooterData.CompnayName = "Asif";
//            createEmpVeiwModel.FooterData.Year = DateTime.Now.Year.ToString();
//            createEmpVeiwModel.UserName = User.Identity.Name;

            return View("CreateEmployee", createEmpVeiwModel);
        }

        [AdminFilter]
        [HeaderFooterFilter]
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
                        CreateEmployeeViewModel vm = new CreateEmployeeViewModel();

                        vm.FirstName = e.FirstName;
                        vm.LastName = e.LastName;

                        if (e.Salary.HasValue)
                        {
                            vm.Salary = e.Salary.ToString();
                        }
                        else
                        {
                            vm.Salary = ModelState["Salary"].Value.AttemptedValue;
                        }

                        //Add Hear & Footer
//                        vm.FooterData = new FooterViewModel();
//                        vm.FooterData.CompnayName = "Asif";
//                        vm.FooterData.Year = DateTime.Now.Year.ToString();
//                        vm.UserName = User.Identity.Name;
                        return View("CreateEmployee", vm);
                    }          

                case "Cancel":
                    return RedirectToAction("Index");
            }

            return new EmptyResult();
        }
    }
}