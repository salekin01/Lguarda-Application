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
    public class MailServerController : Controller
    {
        #region Class Level Variable


        List<LG_SYS_MAIL_SERVER_CONFIG_MAP> LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP = new List<LG_SYS_MAIL_SERVER_CONFIG_MAP>();
        LG_SYS_MAIL_SERVER_CONFIG_MAP OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP = new LG_SYS_MAIL_SERVER_CONFIG_MAP();


        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_MailServerConfigs" + "?format=json";
                LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP = HttpWcfRequest.GetObjectCollection<LG_SYS_MAIL_SERVER_CONFIG_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show mail server info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show mail server info.";
                }
                return View(LIST_LG_SYS_MAIL_SERVER_CONFIG_MAP);
            }
        }

        [RBAC]
        public ActionResult Create()
        {
            string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
            OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD;

            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View(OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create([Bind(Include = "APPLICATION_ID,MAIL_SENDER_IP,MAIL_SENDER_ADDRESS,MAIL_SENDER_PASSWORD,MAIL_SENDER_NAME,SESSION_USER_ID")] LG_SYS_MAIL_SERVER_CONFIG_MAP pLG_SYS_MAIL_SERVER_CONFIG_MAP, string command)
        {
            try
            {
                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.SESSION_USER_ID=Session["currentUserID"].ToString();;
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/AddMailServerConfig/" + pLG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_ID + "/" + pLG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_IP + "/" + pLG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_ADDRESS + "/" + pLG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_PASSWORD + "/" + pLG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_NAME + "/" + OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.SESSION_USER_ID + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);


                        if (result.ToLower().Contains("already exists"))
                        {
                            ModelState.AddModelError("APPLICATION_ID", "Application Id Already Exists");
                            pLG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_SYS_MAIL_SERVER_CONFIG_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            pLG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_SYS_MAIL_SERVER_CONFIG_MAP);
                        }
                    }
                    else
                    {
                        pLG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pLG_SYS_MAIL_SERVER_CONFIG_MAP);
                    }
                }
                pLG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pLG_SYS_MAIL_SERVER_CONFIG_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't save mail server. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't Save mail server.";
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
                return View();
            }
            try
            {
                string url1 = Utility.AppSetting.getLgardaServer() + "/GetMailServerConfigById/" + id + "?format=json";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP = HttpWcfRequest.GetObject<LG_SYS_MAIL_SERVER_CONFIG_MAP>(url1);

                string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);

                TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD;
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit mail server info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit mail server info.";
                }
            }
            return View(OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [RBAC]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "MAIL_ID,APPLICATION_ID,MAIL_SENDER_IP,MAIL_SENDER_ADDRESS,MAIL_SENDER_PASSWORD,MAIL_SENDER_NAME")] LG_SYS_MAIL_SERVER_CONFIG_MAP pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP, string command)
        {

            pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_PASSWORD = Base64Encode(pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_PASSWORD);

            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/UpdateMailServerConfig/" + pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_ID + "/" + pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_ID + "/" + pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_IP + "/" + pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_ADDRESS + "/" +  pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_PASSWORD + "/" + pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.MAIL_SENDER_NAME + "?format=json";
                       
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            //string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                            //OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                            pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
                        }
                    }
                    else
                    {
                        pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
                    }
                }
                pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pOBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update mail server. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update mail server.";
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
                string url = Utility.AppSetting.getLgardaServer() + "/GetMailServerConfigById/" + id + "?format=json";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP = HttpWcfRequest.GetObject<LG_SYS_MAIL_SERVER_CONFIG_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show mail server details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show mail server details.";
                }
            }
            return View(OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
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
                string url = Utility.AppSetting.getLgardaServer() + "/GetMailServerConfigById/" + id + "?format=json";
                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP = HttpWcfRequest.GetObject<LG_SYS_MAIL_SERVER_CONFIG_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show mail server details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show mail server details.";
                }
            }
            return View(OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Delete(int id, string command)
        {
            try
            {
                OBJ_LG_SYS_MAIL_SERVER_CONFIG_MAP.SESSION_USER_ID = Session["currentUserID"].ToString(); ;
                string result = string.Empty;
                if (command == "Delete")
                {
                    if (ModelState.IsValid)
                    {
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/DeleteMailServer/" + id + "?format=json";
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
                    TempData["Error"] = "Can't delete mail server. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't delete mail server.";
                }
                return View();
            }
        }
    }
}
