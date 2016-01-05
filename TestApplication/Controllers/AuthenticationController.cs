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
                UserStatus status = empBLayer.GetUserValidity(u);
                bool isAdmin = false;

                if (status == UserStatus.AuthenticatedAdmin)
                {
                    isAdmin = true;                  
                }
                else if (status == UserStatus.AuthenticatedUser)
                {
                    isAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "Invalid username or password");
                    return View("LogIn");
                }

                FormsAuthentication.SetAuthCookie(u.UserName, false);
                Session["IsAdmin"] = isAdmin;
                return RedirectToAction("Index", "Employee");
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