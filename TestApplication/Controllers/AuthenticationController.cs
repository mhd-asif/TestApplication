using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoLogin(UserDetails u)
        {
            if (ModelState.IsValid)
            {
                EmployeeBusinessLayer empBLayer = new EmployeeBusinessLayer();

                if (empBLayer.IsValidUser(u))
                {
                    FormsAuthentication.SetAuthCookie(u.UserName, false);
                    return RedirectToAction("Index", "Employee");
                }

                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid username or password");
                    return View("LogIn");
                }
            }
            else
            {
                return View("LogIn");
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }
    }
}