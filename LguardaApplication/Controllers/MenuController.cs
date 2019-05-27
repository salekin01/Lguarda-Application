using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult GetMenu()
        {
            LguardaApp.RBAC.Action_Filters.RBAC_ExtendedMethods.IsSysAdmin();
            var menusource = LguardaApp.RBAC.Action_Filters.RBAC_ExtendedMethods.GetAppMenus();
            var Menus = CreateVM(0, menusource);

            //string userId = (string)Session["currentUserID"];
            //ViewBag.First_login_flag = 0;
            //if (!string.IsNullOrWhiteSpace(userId))
            //{
            //    string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserId/" + userId + "?format=json";
            //    var OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);

            //    if (OBJ_LG_USER_SETUP_PROFILE_MAP != null)
            //    {
            //        if (OBJ_LG_USER_SETUP_PROFILE_MAP.FIRST_LOGIN_FLAG == 0)
            //        {
            //            ViewBag.First_login_flag = 0;
            //        }
            //        else
            //        {
            //            ViewBag.First_login_flag = 1;
            //        }
            //    }
            //}
            Session["MenuRearrange"] = Menus;

            return PartialView("_Menu", Menus);
        }

        public IEnumerable<LG_MENU_MAP> CreateVM(decimal? parentid, List<LguardaApp.RBAC.Models.LG_MENU_MAP> source)
        {
            List<LG_MENU_MAP> LIST_LG_MENU_MAP = new List<LG_MENU_MAP>();
            if (source == null)
                return LIST_LG_MENU_MAP;
            var result = (from menu in source
                          where menu.PARENTID == parentid
                          select new LG_MENU_MAP()
                          {
                              MENU_ID = menu.MENU_ID,
                              NAME = menu.NAME,
                              DESCRIPTION = menu.DESCRIPTION,
                              ACTION = menu.ACTION,
                              CONTROLLER = menu.CONTROLLER,
                              URL = menu.URL,
                              PARENTID = menu.PARENTID,
                              Children = CreateVM(menu.MENU_ID, source)
                          }).ToList();
            return result;
        }

        /*
        menusource.Add(new LG_MENU_MAP { MENU_ID = 1, NAME = "Access Control", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 2, NAME = "User Management", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 3, NAME = "Credential Management", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 4, NAME = "Audit Trail", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 5, NAME = "Configure Settings", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 6, NAME = "Approval Authentication", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Home", URL = null, PARENTID = 0 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 7, NAME = "Application", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Application", URL = null, PARENTID = 1 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 8, NAME = "Service", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Service", URL = null, PARENTID = 1 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 9, NAME = "Module", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Module", URL = null, PARENTID = 1 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 10, NAME = "Function", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Function", URL = null, PARENTID = 1 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 11, NAME = "Role", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Role", URL = null, PARENTID = 1 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 12, NAME = "Role Define", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "RoleDefine", URL = null, PARENTID = 1 });

        menusource.Add(new LG_MENU_MAP { MENU_ID = 13, NAME = "User Profile Setup", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "UserProfileSetup", URL = null, PARENTID = 2 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 14, NAME = "Role Assign", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "RoleAssign", URL = null, PARENTID = 2 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 15, NAME = "User Status", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "UserStatus", URL = null, PARENTID = 2 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 16, NAME = "User File Upload", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "UserFileUpload", URL = null, PARENTID = 2 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 17, NAME = "Session Initialize", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "SessionInitialize", URL = null, PARENTID = 2 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 18, NAME = "Bind AD User", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "BindADUser", URL = null, PARENTID = 2 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 19, NAME = "Password Policy", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "PasswordPolicy", URL = null, PARENTID = 3 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 20, NAME = "Password Change", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "PasswordChange", URL = null, PARENTID = 3 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 21, NAME = "Password Reset", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "PasswordReset", URL = null, PARENTID = 3 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 22, NAME = "Audit Trail", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "AuditTrail", URL = null, PARENTID = 4 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 23, NAME = "Mail Server", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "MailServer", URL = null, PARENTID = 5 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 24, NAME = "OTP", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "OTP", URL = null, PARENTID = 5 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 25, NAME = "Calendar Type", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "CalendarType", URL = null, PARENTID = 5 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 26, NAME = "Holiday Type", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "HolidayType", URL = null, PARENTID = 5 });
        menusource.Add(new LG_MENU_MAP { MENU_ID = 27, NAME = "Authorization", DESCRIPTION = null, ACTION = "Index", CONTROLLER = "Authorization", URL = null, PARENTID = 6 });  */
    }
}