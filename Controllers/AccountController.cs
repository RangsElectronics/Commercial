using Commercial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Commercial.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        public AccountController()
        {
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModels _login)
        {
            if (ModelState.IsValid) //validating the user inputs
            {
                bool isExist = false;
                using (dbCommercialEntities _entity = new dbCommercialEntities())  // out Entity name is "SampleMenuMasterDBEntites"
                {
                    isExist = _entity.tblUsers.Where(x => x.UserName.Trim().ToLower() == _login.UserName.Trim().ToLower()
                    && x.Passward.Trim().ToLower() == _login.Password.Trim().ToLower()).Any(); //validating the user name in tblLogin table whether the user name is exist or not
                    if (isExist)
                    {
                        LoginModels _loginCredentials = _entity.tblUsers.Where(x => x.UserName.Trim().ToLower() == _login.UserName.Trim().ToLower()).Select(x => new LoginModels
                        {
                            UserName = x.UserName,
                            UserRoleId = x.RolesId,
                            UserId = x.Id
                        }).FirstOrDefault();  // Get the login user details and bind it to LoginModels class
                        FormsAuthentication.SetAuthCookie(_loginCredentials.UserName, false); // set the formauthentication cookie
                        Session["LoginCredentials"] = _loginCredentials; // Bind the _logincredentials details to "LoginCredentials" session
                        Session["UserName"] = _loginCredentials.UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Please enter the valid credentials!...";
                        return View();
                    }
                }
            }
            return View();
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }
    }
}