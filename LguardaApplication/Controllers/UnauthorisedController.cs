using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Controllers
{
    public class UnauthorisedController : Controller
    {
        public ActionResult Index()
        {
            //Session.Abandon();
            return View();
        }
	}
}