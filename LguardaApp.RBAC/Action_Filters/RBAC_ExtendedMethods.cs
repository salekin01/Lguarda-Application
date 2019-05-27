using LguardaApp.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LguardaApp.RBAC.Action_Filters
{
    public static class RBAC_ExtendedMethods
    {
        public static bool HasRole(this ControllerBase controller, string role)
        {
            bool bFound = false;
            try
            {
                //Check if the requesting user has the specified role...
                //bFound = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasRole(role);
            }
            catch { }
            return bFound;
        }

        public static bool HasRoles(this ControllerBase controller, string roles)
        {
            bool bFound = false;
            try
            {
                //Check if the requesting user has any of the specified roles...
                //Make sure you separate the roles using ; (ie "Sales Manager;Sales Operator"
                //bFound = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasRoles(roles);
            }
            catch { }
            return bFound;
        }

        public static bool HasPermission(this ControllerBase controller, string permission)
        {
            bool bFound = false;
            try
            {
                //Check if the requesting user has the specified application permission...
                //bFound = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasPermission(permission);
            }
            catch { }
            return bFound;
        }

        public static bool IsSysAdmin(this ControllerBase controller)
        {
            bool bIsSysAdmin = false;
            try
            {
                //Check if the requesting user has the System Administrator privilege...
                //bIsSysAdmin = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).IsSysAdmin;
            }
            catch (Exception ex)
            {

            }
            return bIsSysAdmin;
        }

        public static bool IsSysAdmin()
        {
            bool bIsSysAdmin = false;
            try
            {
                HttpContext context = HttpContext.Current;
                string userId = (string)context.Session["currentUserID"];
                string appId = (string)context.Session["appID"];
                bIsSysAdmin = new RBACUser(userId, appId).IsSysAdmin;
            }
            catch (Exception ex)
            {

            }
            return bIsSysAdmin;
        }

        public static List<LG_MENU_MAP> GetAppMenus()
        {
            return RBACUser.GetAppMenus();
        }
        public static bool MenuPermission(string menu_name)
        {
            bool menu_permisson = false;
            try
            {
                menu_permisson = RBACUser.HasMenuPermission(menu_name);
            }
            catch (Exception ex)
            {

            }
            return menu_permisson;
        }
        public static List<MenuPermission> MenuPermission()
        {
            return RBACUser.HasMenuPermission();
        }
        public static List<SubMenuPermission> SubMenuPermission()
        {
            return RBACUser.HasSubMenuPermission();
        }
        public static bool SysAdmin()
        {
            bool bFound = false;
            HttpContext context = HttpContext.Current;
            string userId = (string)context.Session["currentUserID"];
            string appId = (string)context.Session["appID"];
            bFound = new RBACUser(userId, appId).SysAdmin();
            return bFound;
        }
        //public static List<LguardaApp.RBAC.Models.LG_FNR_MODULE_MAP> MenuPermission()
        //{
        //    return RBACUser.HasMenuPermission();
        //}
    }
}
