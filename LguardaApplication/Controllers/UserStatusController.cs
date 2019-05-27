using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LgurdaApp.Model.ControllerModels;
using LguardaApplication.Utility;
using LguardaApp.RBAC.Action_Filters;

namespace LguardaApplication.Controllers
{
    public class UserStatusController : Controller
    {
        #region Class Level Variable

        private LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = new LG_USER_SETUP_PROFILE_MAP();

        #endregion Class Level Variable

        [RBAC]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailById(string id)
        {
            if (id == "")
            {
                return RedirectToAction("Index");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserId/" + id + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);
                if (OBJ_LG_USER_SETUP_PROFILE_MAP.AUTH_STATU_ID == "U")
                {
                    OBJ_LG_USER_SETUP_PROFILE_MAP = null;
                    ViewData["Success"] = null;
                    ViewData["Error"] = "UserId is UnAuthorised.";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Success"] = null;
                    ViewData["Error"] = "Can't show user details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Success"] = null;
                    ViewData["Error"] = "Can't show user details.";
                }
            }

            return PartialView("PartialView_UserDetails", OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailByName(string name)
        {
            if (name == "")
            {
                return RedirectToAction("Index");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserName/" + name + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show user details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show user details.";
                }
            }

            return PartialView("PartialView_UserDetails", OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailByAccountNo(string account_number)
        {
            if (account_number == "")
            {
                return RedirectToAction("Index");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserAccountNo/" + account_number + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show user details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show user details.";
                }
            }

            return PartialView("PartialView_UserDetails", OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        public ActionResult Active(string pUSER_ID)
        {
            string application_Id = "01";
            OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY = Session["currentUserID"].ToString();
            string result = string.Empty;

            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Activate_User/" + pUSER_ID + "/" + application_Id + "/" + OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY + "?format=json";
                result = HttpWcfRequest.PostParameter(url);

                if (result == "True")
                {
                    ViewData["Success"] = "User Activated Successfully.";
                }
                else
                {
                    ViewData["Error"] = result;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't activate user. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't activate user.";
                }
            }

            return View("Index");
        }

        public ActionResult InActive(string pUSER_ID)
        {
            string application_Id = "01";
            OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY = Session["currentUserID"].ToString();
            string result = string.Empty;

            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/InActivate_User/" + pUSER_ID + "/" + application_Id + "/" + OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY + "?format=json";
                result = HttpWcfRequest.PostParameter(url);

                if (result == "True")
                {
                    ViewData["Success"] = "User InActivated Successfully.";
                }
                else
                {
                    ViewData["Error"] = result;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't inactivate user. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't inactivate user.";
                }
            }

            return View("Index");
        }

        public ActionResult Lock(string pUSER_ID)
        {
            string application_Id = "01";
            string result = string.Empty;
            OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY = Session["currentUserID"].ToString();
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Lock_User/" + pUSER_ID + "/" + application_Id + "/" + OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY + "?format=json";
                result = HttpWcfRequest.PostParameter(url);

                if (result == "True")
                {
                    ViewData["Success"] = "User Locked Successfully.";
                }
                else
                {
                    ViewData["Error"] = result;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't lock user. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't lock user.";
                }
            }

            return View("Index");
        }

        public ActionResult UnLock(string pUSER_ID)
        {
            string application_Id = "01";
            string result = string.Empty;
            OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY = Session["currentUserID"].ToString();

            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Unlock_User/" + pUSER_ID + "/" + application_Id + "/" + OBJ_LG_USER_SETUP_PROFILE_MAP.MAKE_BY + "?format=json";
                result = HttpWcfRequest.PostParameter(url);

                if (result == "True")
                {
                    ViewData["Success"] = "User Unlocked Successfully.";
                }
                else
                {
                    ViewData["Error"] = result;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't unlock user. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't unlock user.";
                }
            }

            return View("Index");
        }

        //public ActionResult Deactivate(string pUSER_ID)
        //{
        //    string application_Id = "1";
        //    string result = string.Empty;

        //    try
        //    {
        //        string url = Utility.AppSetting.getLgardaServer() + "/Deactivate_User/" + pUSER_ID + "/" + application_Id + "?format=json";
        //        result = HttpWcfRequest.PostParameter(url);

        //        if (result == "True")
        //        {
        //            TempData["Success"] = "User Deactivated Successful.";
        //        }
        //        else
        //        {
        //            TempData["Error"] = result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Service"))
        //        {
        //            TempData["Error"] = "Can't deactivate user. Service is unable to process the request.";
        //        }
        //        else
        //        {
        //            TempData["Error"] = "Can't deactivate user.";
        //        }
        //    }

        //    return View("Index");
        //}
    }
}