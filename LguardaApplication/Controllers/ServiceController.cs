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
    public class ServiceController : Controller
    {
        #region Class Level Variable
        List<LG_FNR_SERVICE_MAP> LIST_LG_FNR_SERVICE_MAP = new List<LG_FNR_SERVICE_MAP>();
        LG_FNR_SERVICE_MAP OBJ_LG_FNR_SERVICE_MAP = new LG_FNR_SERVICE_MAP();

        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {

                string url = Utility.AppSetting.getLgardaServer() + "/Get_Services" + "?format=json";
                LIST_LG_FNR_SERVICE_MAP = HttpWcfRequest.GetObjectCollection<LG_FNR_SERVICE_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_FNR_SERVICE_MAP);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show service info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show service info.";
                }
                return View(LIST_LG_FNR_SERVICE_MAP);
            }
        }
        //[RBAC]
        public ActionResult Details(string app_id, string service_id)
        {
            if (app_id == null || service_id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Service_ByServiceId/" + service_id + "/" + app_id + "?format=json";
                OBJ_LG_FNR_SERVICE_MAP = HttpWcfRequest.GetObject<LG_FNR_SERVICE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show application details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show application details.";
                }
            }
            return View(OBJ_LG_FNR_SERVICE_MAP);
        }


        [RBAC]
        public ActionResult Create()
        {
            string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
            OBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD;
            if(TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View(OBJ_LG_FNR_SERVICE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create([Bind(Include = "SERVICE_NM,SERVICE_SH_NM,APPLICATION_ID")] LG_FNR_SERVICE_MAP pLG_FNR_SERVICE_MAP, string command)
          {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Service/" + pLG_FNR_SERVICE_MAP.SERVICE_NM + "/" + pLG_FNR_SERVICE_MAP.SERVICE_SH_NM + "/" + pLG_FNR_SERVICE_MAP.APPLICATION_ID + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);


                        if (result.ToLower().Contains("already exists"))
                        {
                            ModelState.AddModelError("SERVICE_NM", "Service Name Already Exists");
                            pLG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_FNR_SERVICE_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Save.";
                            pLG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_FNR_SERVICE_MAP);
                        }
                    }
                    else
                    {
                        pLG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pLG_FNR_SERVICE_MAP);
                    }

                }

                pLG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pLG_FNR_SERVICE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't save application. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't Save application.";
                }
                return View(pLG_FNR_SERVICE_MAP);                
            }
        }


        [RBAC]
        public ActionResult Edit(string app_id, string service_id)
        {
            if (app_id == null || service_id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Service_ByServiceId/" + service_id + "/" + app_id + "?format=json";
                OBJ_LG_FNR_SERVICE_MAP = HttpWcfRequest.GetObject<LG_FNR_SERVICE_MAP>(url);
                TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit service info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit service info.";
                }
            }
            return View(OBJ_LG_FNR_SERVICE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "SERVICE_ID,SERVICE_NM,SERVICE_SH_NM,APPLICATION_ID")] LG_FNR_SERVICE_MAP pOBJ_LG_FNR_SERVICE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_Service/" + pOBJ_LG_FNR_SERVICE_MAP.SERVICE_ID + "/" + pOBJ_LG_FNR_SERVICE_MAP.SERVICE_NM + "/" + pOBJ_LG_FNR_SERVICE_MAP.SERVICE_SH_NM + "/" + pOBJ_LG_FNR_SERVICE_MAP.APPLICATION_ID + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result.ToLower().Contains("no changes"))
                        {
                            ModelState.AddModelError("SERVICE_NM", "No changes made.");
                            pOBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pOBJ_LG_FNR_SERVICE_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Update.";
                            pOBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pOBJ_LG_FNR_SERVICE_MAP);
                        }
                    }
                    else
                    {
                        pOBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pOBJ_LG_FNR_SERVICE_MAP);
                    }
                }

                pOBJ_LG_FNR_SERVICE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pOBJ_LG_FNR_SERVICE_MAP);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update service. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update service.";
                }
                return View(pOBJ_LG_FNR_SERVICE_MAP);
            }
        }


        [RBAC]
        ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Service_ByServiceId/" + id + "?format=json";
                OBJ_LG_FNR_SERVICE_MAP = HttpWcfRequest.GetObject<LG_FNR_SERVICE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show service details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show service details.";
                }
            }
            return View(OBJ_LG_FNR_SERVICE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Delete(string id, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Delete")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_Service/" + id + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Deleted Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Delete.";
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
                    ViewData["Error"] = "Can't delete service. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't delete service.";
                }
                return RedirectToAction("Delete");
            }
        }
    }
}
