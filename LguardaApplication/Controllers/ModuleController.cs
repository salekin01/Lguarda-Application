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
    public class ModuleController : Controller
    {

        #region Class Level Variable

        List<LG_FNR_MODULE_MAP> LIST_LG_FNR_MODULE_MAP = new List<LG_FNR_MODULE_MAP>();
        LG_FNR_MODULE_MAP OBJ_LG_FNR_MODULE_MAP = new LG_FNR_MODULE_MAP();

        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {

                string url = Utility.AppSetting.getLgardaServer() + "/Get_Modules" + "?format=json";
                LIST_LG_FNR_MODULE_MAP = HttpWcfRequest.GetObjectCollection<LG_FNR_MODULE_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_FNR_MODULE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show module info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show module info.";
                }
                return View(LIST_LG_FNR_MODULE_MAP);
            }
        }


        [RBAC]
        public ActionResult Create()
        {
            string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
            OBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD;
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View(OBJ_LG_FNR_MODULE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create([Bind(Include = "MODULE_NM,MODULE_SH_NM,APPLICATION_ID,SERVICE_ID")] LG_FNR_MODULE_MAP pLG_FNR_MODULE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/AddModule/" + pLG_FNR_MODULE_MAP.MODULE_NM + "/" + pLG_FNR_MODULE_MAP.MODULE_SH_NM + "/" + pLG_FNR_MODULE_MAP.APPLICATION_ID + "/" + pLG_FNR_MODULE_MAP.SERVICE_ID + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result.ToLower().Contains("already exists"))
                        {
                            ModelState.AddModelError("MODULE_NM", "Module Name Already Exists");
                            pLG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_FNR_MODULE_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {  
                            TempData["Success"] = "Saved Successfully";          
                            return RedirectToAction("Create");
                        }
                        else
                        {                           
                            pLG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];                            
                            return View(pLG_FNR_MODULE_MAP);
                        }
                    }
                    else
                    {                       
                        pLG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];                        
                        return View(pLG_FNR_MODULE_MAP);
                    }
                }
                
                pLG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];               
                return View(pLG_FNR_MODULE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't save module. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't Save module.";
                }
                return View("Create");
            }
        }


        [RBAC]
        public ActionResult Edit(string service_id, string module_id, string app_id)
        {
            if (service_id == null || module_id == null || app_id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Module_ByServiceId_AndModuleId/" + service_id + "/" + module_id + "/" + app_id +  "?format=json";
                OBJ_LG_FNR_MODULE_MAP = HttpWcfRequest.GetObject<LG_FNR_MODULE_MAP>(url);

                if (OBJ_LG_FNR_MODULE_MAP != null)
                {
                    OBJ_LG_FNR_MODULE_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_MODULE_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }

                TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD;
                TempData["SERVICE_LIST_FOR_DD"] = OBJ_LG_FNR_MODULE_MAP.SERVICE_LIST_FOR_DD;
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit module info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit module info.";
                }
            }
            return View(OBJ_LG_FNR_MODULE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "MODULE_ID,MODULE_NM,MODULE_SH_NM,APPLICATION_ID,SERVICE_ID")] LG_FNR_MODULE_MAP pOBJ_LG_FNR_MODULE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_Module/" + pOBJ_LG_FNR_MODULE_MAP.MODULE_ID + "/" + pOBJ_LG_FNR_MODULE_MAP.MODULE_NM + "/" + pOBJ_LG_FNR_MODULE_MAP.MODULE_SH_NM + "/" + pOBJ_LG_FNR_MODULE_MAP.APPLICATION_ID + "/" + pOBJ_LG_FNR_MODULE_MAP.SERVICE_ID + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result.ToLower().Contains("no changes"))
                        {
                            ViewData["Error"] = "No changes made";
                            pOBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            pOBJ_LG_FNR_MODULE_MAP.SERVICE_LIST_FOR_DD = (List<SelectListItem>)TempData["SERVICE_LIST_FOR_DD"];
                            return View(pOBJ_LG_FNR_MODULE_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                           string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                           pOBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                           pOBJ_LG_FNR_MODULE_MAP.SERVICE_LIST_FOR_DD = (List<SelectListItem>)TempData["SERVICE_LIST_FOR_DD"];
                           return View(pOBJ_LG_FNR_MODULE_MAP);
                        }
                    }
                    else
                    {
                        pOBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        pOBJ_LG_FNR_MODULE_MAP.SERVICE_LIST_FOR_DD = (List<SelectListItem>)TempData["SERVICE_LIST_FOR_DD"];
                        return View(pOBJ_LG_FNR_MODULE_MAP);
                    }
                }
           
                pOBJ_LG_FNR_MODULE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                pOBJ_LG_FNR_MODULE_MAP.SERVICE_LIST_FOR_DD = (List<SelectListItem>)TempData["SERVICE_LIST_FOR_DD"];
                return View(pOBJ_LG_FNR_MODULE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update module. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update module.";
                }
                return View();
            }
        }


        [RBAC]
        public ActionResult Details(string service_id, string module_id, string app_id)
        {
            if (service_id == null || module_id == null || app_id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Module_ByServiceId_AndModuleId/" + service_id + "/" + module_id + "/" + app_id + "?format=json";
                OBJ_LG_FNR_MODULE_MAP = HttpWcfRequest.GetObject<LG_FNR_MODULE_MAP>(url);

                if (OBJ_LG_FNR_MODULE_MAP != null)
                {
                    OBJ_LG_FNR_MODULE_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_MODULE_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show module details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show module details.";
                }
            }
            return View(OBJ_LG_FNR_MODULE_MAP);
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Module_ByModuleId/" + id + "?format=json";
                OBJ_LG_FNR_MODULE_MAP = HttpWcfRequest.GetObject<LG_FNR_MODULE_MAP>(url);
                if (OBJ_LG_FNR_MODULE_MAP != null)
                {
                    OBJ_LG_FNR_MODULE_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_MODULE_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_MODULE_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show module details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show module details.";
                }
            }
            return View(OBJ_LG_FNR_MODULE_MAP);
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
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_Module/" + id + session_user + "?format=json";
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
                    TempData["Error"] = "Can't delete module. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't delete module.";
                }
                return View();
            }
        }




    }
}
