using LguardaApp.RBAC.Action_Filters;
using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Controllers
{
    public class PasswordResetController : Controller
    {

        #region Class Level Variable

        List<LG_CRD_PASSWORD_CHANGE_MAP> LIST_LG_CRD_PASSWORD_CHANGE_MAP = new List<LG_CRD_PASSWORD_CHANGE_MAP>();
        LG_CRD_PASSWORD_CHANGE_MAP OBJ_LG_CRD_PASSWORD_CHANGE_MAP = new LG_CRD_PASSWORD_CHANGE_MAP();
        LG_CRD_PASSWORD_POLICY_MAP OBJ_LG_CRD_PASSWORD_POLICY_MAP = new LG_CRD_PASSWORD_POLICY_MAP();
        LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = new LG_USER_SETUP_PROFILE_MAP();

        #endregion

        [RBAC]
        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult Reset(string pUSER_ID)
        {

            string application_Id = "01";
            string result = string.Empty;
            string mesg = string.Empty;

            try
            {
                string userId = "";

                if (Session["currentUserID"] != null)
                {
                    userId = Session["currentUserID"].ToString();
                }

                string url = Utility.AppSetting.getLgardaServer() + "/Reset_Password/" + userId + "/" + pUSER_ID + "/" + application_Id + "?format=json";
                result = HttpWcfRequest.PostParameter(url);

                if (result == "True")
                {                   
                    TempData["Success"] = "Password Reset Successful.";  
                    mesg = TempData["Success"].ToString();
                }
                else
                {
                    TempData["Error"] = result;
                    mesg = TempData["Error"].ToString();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {                   
                    TempData["Error"] = "Can't reset password. Service is unable to process the request.";
                    mesg = TempData["Error"].ToString();
                }
                else
                {                    
                    TempData["Error"] = "Can't reset password.";
                    mesg = TempData["Error"].ToString();
                }
            }

            //return RedirectToAction("Index","PasswordReset");
            return Json(mesg, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailById(string id)
        {
            if (id == "")
            {
                return RedirectToAction("ResetPassword");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserId/" + id + "?format=json";
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



            return PartialView("Partial1", OBJ_LG_USER_SETUP_PROFILE_MAP);

        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailByName(string name)
        {
            if (name == "")
            {
                return RedirectToAction("ResetPassword");
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

            return PartialView("Partial1", OBJ_LG_USER_SETUP_PROFILE_MAP);

        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailByAccountNo(string account_number)
        {
            if (account_number == "")
            {
                return RedirectToAction("ResetPassword");
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

            return PartialView("Partial1", OBJ_LG_USER_SETUP_PROFILE_MAP);

        }

	}
}