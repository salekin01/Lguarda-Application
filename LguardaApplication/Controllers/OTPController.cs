using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using LguardaApplication.Utility;
using LgurdaApp.Model;
using LgurdaApp.Model.ControllerModels;
using LguardaApp.RBAC.Action_Filters;


namespace LguardaApplication.Controllers
{
    public class OTPController : Controller
    {

        #region Class Level Variable
        List<LG_2FA_OTP_CONFIG_MAP> LIST_LG_2FA_OTP_CONFIG_MAP = new List<LG_2FA_OTP_CONFIG_MAP>();
        LG_2FA_OTP_CONFIG_MAP OBJ_LG_2FA_OTP_CONFIG_MAP = new LG_2FA_OTP_CONFIG_MAP();
        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {

                string url = Utility.AppSetting.getLgardaServer() + "/Get_OtpConfig" + "?format=json";
                LIST_LG_2FA_OTP_CONFIG_MAP = HttpWcfRequest.GetObjectCollection<LG_2FA_OTP_CONFIG_MAP>(url);
                return View(LIST_LG_2FA_OTP_CONFIG_MAP);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show otp info. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show otp info.";
                }
                return View(LIST_LG_2FA_OTP_CONFIG_MAP);
            }
        }


        [RBAC]
        public ActionResult Create()
        {
            string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Application_For_DD" + "?format=json";
            string url1 = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_OTP_Format_For_DD" + "?format=json";
            OBJ_LG_2FA_OTP_CONFIG_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);

            TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_2FA_OTP_CONFIG_MAP.APPLICATION_LIST_FOR_DD;
            TempData["OTP_FORMATE_LIST_FOR_DD"] = OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD;
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View(OBJ_LG_2FA_OTP_CONFIG_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_2FA_OTP_CONFIG_MAP pLG_2FA_OTP_CONFIG_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        string pMAKE_BY = (string)Session["currentUserID"];
                        Int16 mail_flag = Convert.ToInt16(pLG_2FA_OTP_CONFIG_MAP.MAIL_FLAG_B);
                        Int16 sms_flag = Convert.ToInt16(pLG_2FA_OTP_CONFIG_MAP.SMS_FLAG_B);

                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_OtpConfig/" + pLG_2FA_OTP_CONFIG_MAP.APPLICATION_ID + "/" + mail_flag + "/" + sms_flag + "/" + pLG_2FA_OTP_CONFIG_MAP.VALIDITY_PERIOD + "/" + pLG_2FA_OTP_CONFIG_MAP.NO_OF_OTP_DIGIT + "/" + pLG_2FA_OTP_CONFIG_MAP.OTP_FORMAT_ID + "/" + pMAKE_BY + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result.ToLower().Contains("already exists"))
                        {
                            pLG_2FA_OTP_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            pLG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = (List<SelectListItem>)TempData["OTP_FORMATE_LIST_FOR_DD"];
                            ModelState.AddModelError("APPLICATION_ID", "OTP configuation already exists for this application");
                            return View(pLG_2FA_OTP_CONFIG_MAP);
                        }
                        if (result == "True")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            pLG_2FA_OTP_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            pLG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = (List<SelectListItem>)TempData["OTP_FORMATE_LIST_FOR_DD"];
                            return View("Create");
                        }
                    }
                    else
                    {
                        pLG_2FA_OTP_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        pLG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = (List<SelectListItem>)TempData["OTP_FORMATE_LIST_FOR_DD"];
                        return View();
                    }

                }
                pLG_2FA_OTP_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                pLG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = (List<SelectListItem>)TempData["OTP_FORMATE_LIST_FOR_DD"];
                return View(pLG_2FA_OTP_CONFIG_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't save otp. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't Save otp.";
                }
                return View("Create");
            }
        }


        [RBAC]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_OtpConfig_ByAppId/" + id + "?format=json";
                OBJ_LG_2FA_OTP_CONFIG_MAP = HttpWcfRequest.GetObject<LG_2FA_OTP_CONFIG_MAP>(url);
                TempData["OTP_FORMATE_LIST_FOR_DD"] = OBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewBag["Error"] = "Can't edit otp info. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't edit otp info.";
                }
            }
            return View(OBJ_LG_2FA_OTP_CONFIG_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit(LG_2FA_OTP_CONFIG_MAP pOBJ_LG_2FA_OTP_CONFIG_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string pMAKE_BY = (string)Session["currentUserID"];
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_OtpConfig/" + pOBJ_LG_2FA_OTP_CONFIG_MAP.APPLICATION_ID + "/" + pOBJ_LG_2FA_OTP_CONFIG_MAP.MAIL_FLAG_B + "/" + pOBJ_LG_2FA_OTP_CONFIG_MAP.SMS_FLAG_B + "/" + pOBJ_LG_2FA_OTP_CONFIG_MAP.VALIDITY_PERIOD + "/" + pOBJ_LG_2FA_OTP_CONFIG_MAP.NO_OF_OTP_DIGIT + "/" + pOBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMAT_ID + "/" + pMAKE_BY + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result == "True")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            pOBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = (List<SelectListItem>)TempData["OTP_FORMATE_LIST_FOR_DD"];
                            return View(pOBJ_LG_2FA_OTP_CONFIG_MAP);
                        }
                    }
                    else
                    {
                        pOBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = (List<SelectListItem>)TempData["OTP_FORMATE_LIST_FOR_DD"];
                        return View(pOBJ_LG_2FA_OTP_CONFIG_MAP);
                    }
                }
                pOBJ_LG_2FA_OTP_CONFIG_MAP.OTP_FORMATE_LIST_FOR_DD = (List<SelectListItem>)TempData["OTP_FORMATE_LIST_FOR_DD"];
                return View(pOBJ_LG_2FA_OTP_CONFIG_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't update otp. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't update otp.";
                }
                return View();
            }
        }


        [RBAC]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_OtpConfig_ByAppId/" + id + "?format=json";
                OBJ_LG_2FA_OTP_CONFIG_MAP = HttpWcfRequest.GetObject<LG_2FA_OTP_CONFIG_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show otp details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show otp details.";
                }
            }
            return View(OBJ_LG_2FA_OTP_CONFIG_MAP);
        }


        [RBAC]
        ActionResult Delete(int id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_OtpConfig_ByAppId/" + id + "?format=json";
                OBJ_LG_2FA_OTP_CONFIG_MAP = HttpWcfRequest.GetObject<LG_2FA_OTP_CONFIG_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show otp details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show otp details.";
                }
            }
            return View(OBJ_LG_2FA_OTP_CONFIG_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Delete(int id, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Delete")
                {
                    if (ModelState.IsValid)
                    {
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_OtpConfig/" + id + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result == "True")
                        {
                            TempData["Success"] = "Deleted Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("Delete");
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't delete otp. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't delete otp.";
                }
                return View();
            }
        }







    }
}
