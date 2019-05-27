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
using LgurdaApp.Model.Common;


namespace LguardaApplication.Controllers
{

    public class FunctionController : Controller
    {
        #region Class Level Variable
        List<LG_FNR_FUNCTION_MAP> LIST_LG_FNR_FUNCTION_MAP = new List<LG_FNR_FUNCTION_MAP>();
        LG_FNR_FUNCTION_MAP OBJ_LG_FNR_FUNCTION_MAP = new LG_FNR_FUNCTION_MAP();
        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Functions" + "?format=json";
                LIST_LG_FNR_FUNCTION_MAP = HttpWcfRequest.GetObjectCollection<LG_FNR_FUNCTION_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_FNR_FUNCTION_MAP);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show function info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show function info.";
                }
                return View(LIST_LG_FNR_FUNCTION_MAP);
            }
        }


        [RBAC]
        public ActionResult Create()
        {
            string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Application_For_DD" + "?format=json";
            OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD;
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View(OBJ_LG_FNR_FUNCTION_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_FNR_FUNCTION_MAP pLG_FNR_FUNCTION_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        if(string.IsNullOrEmpty(pLG_FNR_FUNCTION_MAP.AUTH_LEVEL))
                        {
                            pLG_FNR_FUNCTION_MAP.AUTH_LEVEL = "0";
                        }

                        if (string.IsNullOrWhiteSpace(pLG_FNR_FUNCTION_MAP.FAST_PATH_NO))
                        {
                            pLG_FNR_FUNCTION_MAP.FAST_PATH_NO = "null";
                        }
                        if (string.IsNullOrWhiteSpace(pLG_FNR_FUNCTION_MAP.TARGET_PATH))
                        {
                            pLG_FNR_FUNCTION_MAP.TARGET_PATH = "null";
                        }
                        if (string.IsNullOrWhiteSpace(pLG_FNR_FUNCTION_MAP.DB_ROLE_NAME))
                        {
                            pLG_FNR_FUNCTION_MAP.DB_ROLE_NAME = "null";
                        }
                        string session_user = Session["currentUserID"].ToString();
                        string encryptedTargetPath = CryptorEngine.ConvertStringToHex(pLG_FNR_FUNCTION_MAP.TARGET_PATH, System.Text.Encoding.Unicode);

                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Function/" +
                                  pLG_FNR_FUNCTION_MAP.FUNCTION_NM + 
                            "/" + pLG_FNR_FUNCTION_MAP.APPLICATION_ID + 
                            "/" + pLG_FNR_FUNCTION_MAP.SERVICE_ID +
                            "/" + pLG_FNR_FUNCTION_MAP.MODULE_ID +
                            "/" + pLG_FNR_FUNCTION_MAP.ITEM_TYPE +
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG_B +
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG_B +
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG_B +
                            "/" + pLG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.AUTH_LEVEL + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG_B + 
                            "/" + session_user + 
                            "/" + pLG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.PROCESS_FLAG_B + 
                            "/" + pLG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG_B +
                            "/" + pLG_FNR_FUNCTION_MAP.FAST_PATH_NO +
                            "/" + encryptedTargetPath +
                            "/" + pLG_FNR_FUNCTION_MAP.DB_ROLE_NAME +
                            "/" + pLG_FNR_FUNCTION_MAP.ENABLED_FLAG_B + "?format=json"; //salekin added 17.01.2018
                            
                       
                        result = HttpWcfRequest.PostParameter(url);


                        if (result.ToLower().Contains("already exists"))
                        {
                            pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            ModelState.AddModelError("FUNCTION_NM", "Function Name Already Exists");
                            return View(pLG_FNR_FUNCTION_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Save.";
                            pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_FNR_FUNCTION_MAP);
                        }
                    }
                    else
                    {
                        pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pLG_FNR_FUNCTION_MAP );
                    }

                }
                pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pLG_FNR_FUNCTION_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                    ViewData["Error"] = "Can't save function. Service is unable to process the request.";
                }
                else
                {
                    pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                    ViewData["Error"] = "Can't Save function.";
                }
                return RedirectToAction("Create");
            }
        }


        

        [RBAC]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Function_ByFunctionId/" + id + "?format=json";
                OBJ_LG_FNR_FUNCTION_MAP = HttpWcfRequest.GetObject<LG_FNR_FUNCTION_MAP>(url);
                if (OBJ_LG_FNR_FUNCTION_MAP != null)
                {
                    OBJ_LG_FNR_FUNCTION_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_FUNCTION_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
                TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit function info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit function info.";
                }
            }
            return View(OBJ_LG_FNR_FUNCTION_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit(LG_FNR_FUNCTION_MAP pLG_FNR_FUNCTION_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        ValidateObj(pLG_FNR_FUNCTION_MAP);

                        string encryptedTargetPath = CryptorEngine.ConvertStringToHex(pLG_FNR_FUNCTION_MAP.TARGET_PATH, System.Text.Encoding.Unicode);
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_Function/" + pLG_FNR_FUNCTION_MAP.FUNCTION_ID + "/" + pLG_FNR_FUNCTION_MAP.FUNCTION_NM + "/" + pLG_FNR_FUNCTION_MAP.APPLICATION_ID + "/" + pLG_FNR_FUNCTION_MAP.SERVICE_ID + "/" + pLG_FNR_FUNCTION_MAP.MODULE_ID + "/" + pLG_FNR_FUNCTION_MAP.ITEM_TYPE + "/" +
                                                                                                            pLG_FNR_FUNCTION_MAP.MAINT_CRT_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.MAINT_EDT_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.MAINT_DEL_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.MAINT_DTL_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.MAINT_INDX_FLAG_B + "/" +
                                                                                                            pLG_FNR_FUNCTION_MAP.MAINT_OTP_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.MAINT_2FA_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.MAINT_2FA_HARD_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.MAINT_2FA_SOFT_FLAG_B + "/" +
                                                                                                            pLG_FNR_FUNCTION_MAP.REPORT_VIEW_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.REPORT_PRINT_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.REPORT_GEN_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.AUTH_LEVEL + "/" + pLG_FNR_FUNCTION_MAP.MAINT_AUTH_FLAG_B + "/" + session_user + "/" + pLG_FNR_FUNCTION_MAP.MAINT_BIO_FLAG_B + "/" +
                                                                                                            pLG_FNR_FUNCTION_MAP.PROCESS_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.HO_FUNCTION_FLAG_B + "/" + pLG_FNR_FUNCTION_MAP.FAST_PATH_NO + "/" + encryptedTargetPath + "/" + pLG_FNR_FUNCTION_MAP.DB_ROLE_NAME + "/" + pLG_FNR_FUNCTION_MAP.ENABLED_FLAG_B + "?format=json";                
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Save.";
                            pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_FNR_FUNCTION_MAP);
                        }
                    }
                    else
                    {
                        pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pLG_FNR_FUNCTION_MAP);
                    }
                }
                pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pLG_FNR_FUNCTION_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                    ViewData["Error"] = "Can't update function. Service is unable to process the request.";
                }
                else
                {
                    pLG_FNR_FUNCTION_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                    ViewData["Error"] = "Can't update function.";
                }
                return View(pLG_FNR_FUNCTION_MAP);
            }
        }


        [RBAC]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Function_ByFunctionId/" + id + "?format=json";
                OBJ_LG_FNR_FUNCTION_MAP = HttpWcfRequest.GetObject<LG_FNR_FUNCTION_MAP>(url);
                if (OBJ_LG_FNR_FUNCTION_MAP != null)
                {
                    OBJ_LG_FNR_FUNCTION_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_FUNCTION_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show function details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show function details.";
                }
            }
            return View(OBJ_LG_FNR_FUNCTION_MAP);
        }


        [RBAC]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Function_ByFunctionId/" + id + "?format=json";
                OBJ_LG_FNR_FUNCTION_MAP = HttpWcfRequest.GetObject<LG_FNR_FUNCTION_MAP>(url);
                if (OBJ_LG_FNR_FUNCTION_MAP != null)
                {
                    OBJ_LG_FNR_FUNCTION_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_FUNCTION_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_FUNCTION_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
                TempData["OBJ_LG_FNR_FUNCTION_MAP"] = OBJ_LG_FNR_FUNCTION_MAP;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't delete function. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't delete function.";
                }
            }
            return View(OBJ_LG_FNR_FUNCTION_MAP);
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
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_Function/" + id + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result == "True")
                        {
                            TempData["Success"] = "Deleted Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Delete.";
                            return View(TempData["OBJ_LG_FNR_FUNCTION_MAP"]);
                        }
                    }
                    else
                    {
                        return View(TempData["OBJ_LG_FNR_FUNCTION_MAP"]);
                    }
                }
                return View(TempData["OBJ_LG_FNR_FUNCTION_MAP"]);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't delete function. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't delete function.";
                }
                return View(TempData["OBJ_LG_FNR_FUNCTION_MAP"]);
            }
            
        }

        private void ValidateObj(LG_FNR_FUNCTION_MAP pLG_FNR_FUNCTION_MAP)
        {
            if (pLG_FNR_FUNCTION_MAP.AUTH_LEVEL == null)
            {
                pLG_FNR_FUNCTION_MAP.AUTH_LEVEL = " ";
            }

            if (string.IsNullOrWhiteSpace(pLG_FNR_FUNCTION_MAP.FAST_PATH_NO))
            {
                pLG_FNR_FUNCTION_MAP.FAST_PATH_NO = "null";
            }
            if (string.IsNullOrWhiteSpace(pLG_FNR_FUNCTION_MAP.TARGET_PATH))
            {
                pLG_FNR_FUNCTION_MAP.TARGET_PATH = "null";
            }
            if (string.IsNullOrWhiteSpace(pLG_FNR_FUNCTION_MAP.DB_ROLE_NAME))
            {
                pLG_FNR_FUNCTION_MAP.DB_ROLE_NAME = "null";
            }
        }
        public int GetAppTypeByAppId(string app_id)
        {
            string url = ConfigurationManager.AppSettings["lgarda_server"] + "/GetAppTypeByAppId" + "/" + app_id + "?format=json";
            var result = HttpWcfRequest.GetString(url);
            return Convert.ToInt32(result);
        }
    }
}
