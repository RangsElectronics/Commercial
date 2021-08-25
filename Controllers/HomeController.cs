using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Commercial.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return Redirect("/Account/Login");
            }
            return View();
        }
    }
}