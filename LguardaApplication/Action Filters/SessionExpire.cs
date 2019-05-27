﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace LguardaApplication.Action_Filters
{




    public class SessionExpireAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["currentUserID"] == null)
            {
                FormsAuthentication.SignOut();
                filterContext.Result =
               new RedirectToRouteResult(new RouteValueDictionary   
            {  
             { "action", "Login" },  
            { "controller", "Login" },  
            { "returnUrl", filterContext.HttpContext.Request.RawUrl}  
             });

                return;
            }
        }

    }
}