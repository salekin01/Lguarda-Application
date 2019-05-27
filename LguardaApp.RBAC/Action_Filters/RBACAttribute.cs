using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LguardaApp.RBAC.Action_Filters
{
    public class RBACAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);
            //RBACUser requestingUser = new RBACUser(filterContext.RequestContext.HttpContext.User.Identity.Name);

            HttpContext context = HttpContext.Current;
            string userId = (string)context.Session["currentUserID"];
            string appId = (string)context.Session["appID"];

            if (userId != "Admin1" && userId != "Admin2" && userId != "Admin3" && userId != "Admin4")
            {
                RBACUser requestingUser = new RBACUser(userId, appId);
                if (!requestingUser.HasPermission(requiredPermission) || !requestingUser.IsSysAdmin)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Unauthorised" } });
                }
            }
        }
    }
}
