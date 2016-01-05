using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApplication.DataAccessLayer;

namespace TestApplication.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Employee> GetEmployees()
        {
            SalesERPDAL salesDAL = new SalesERPDAL();

            return salesDAL.Employees.ToList();
            /*
            List<Employee> employees = new List<Employee>();

            Employee emp1 = new Employee();
            emp1.FirstName = "Md.";
            emp1.LastName = "Asif";
            emp1.Salary = 100;
            employees.Add(emp1);

            Employee emp2 = new Employee();
            emp2.FirstName = "Shadek.";
            emp2.LastName = "Rahman";
            emp2.Salary = 1000;
            employees.Add(emp2);

            Employee emp3 = new Employee();
            emp3.FirstName = "Muhib";
            emp3.LastName = "Khan";
            emp3.Salary = 2000;
            employees.Add(emp3);

            return employees;
            */
        }

        public void SaveEmployee(Employee e)
        {
            SalesERPDAL salesDAL = new SalesERPDAL();
            salesDAL.Employees.Add(e);
            salesDAL.SaveChanges();
        }

        public UserStatus GetUserValidity(UserDetails u)
        {
            if (u.UserName == "Admin" && u.Password == "Admin")
            {
                return UserStatus.AuthenticatedAdmin;
            }
            else if (u.UserName == "Asif" && u.Password == "Asif")
            {
                return UserStatus.AuthenticatedUser;          
            }
            else
            {
                return UserStatus.NonAuthenticatedUser;
            }
        }
    }
}