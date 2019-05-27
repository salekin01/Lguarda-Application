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
    public class RoleDefineController : Controller
    {
        #region Class Level Variable
        List<LG_FNR_ROLE_DEFINE_MAP> LIST_LG_FNR_ROLE_DEFINE_MAP = new List<LG_FNR_ROLE_DEFINE_MAP>();
        LG_FNR_ROLE_DEFINE_MAP OBJ_LG_FNR_ROLE_DEFINE_MAP = new LG_FNR_ROLE_DEFINE_MAP();
        LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP = new LG_FNR_ROLE_MAP();
        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_AllRoleDefinedInfo" + "?format=json";
                LIST_LG_FNR_ROLE_DEFINE_MAP = LguardaApplication.Utility.HttpWcfRequest.GetObjectCollection<LG_FNR_ROLE_DEFINE_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_FNR_ROLE_DEFINE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show role define info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show role define info.";
                }
                return View(LIST_LG_FNR_ROLE_DEFINE_MAP);
            }
        }
        [RBAC]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_RoleDefineInfo_ByRoleId/" + id + "?format=json";
                OBJ_LG_FNR_ROLE_DEFINE_MAP = HttpWcfRequest.GetObject<LG_FNR_ROLE_DEFINE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show role define info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show role define info.";
                }
            }
            return View(OBJ_LG_FNR_ROLE_DEFINE_MAP);
        }


        [RBAC]
        public ActionResult Create()
        {
            string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
            OBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);

            
            TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD;
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }

            Session["SelectedFunctionDetails"] = null;
            return View(OBJ_LG_FNR_ROLE_DEFINE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_FNR_ROLE_DEFINE_MAP pOBJ_LG_FNR_ROLE_DEFINE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_RoleDefine?format=json";
                        //string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Test?format=json";
                        Uri uri = new Uri(url);
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS = (List<LG_FNR_ROLE_DEFINE_MAP>)Session["SelectedFunctionDetails"];
                        Session["SelectedFunctionDetails"] = null;
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.ROLE_DEFINE_COMMAND = command;
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.MAKE_BY = (string)Session["currentUserID"];

                        /*Test Obj_Test = new Test();
                          Obj_Test.p1 = "Sirajus";
                          Obj_Test.p2 = "Salekin";
                          result = HttpWcfRequest.PostObject_WithReturningString<Test>(uri, Obj_Test);*/
                        result = HttpWcfRequest.PostObject_WithReturningString<LG_FNR_ROLE_DEFINE_MAP>(uri, pOBJ_LG_FNR_ROLE_DEFINE_MAP);


                        if (result.ToLower().Contains("already defined"))
                        {
                            OBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            ModelState.AddModelError("ROLE_NM", "Role already defined.");
                            return View(OBJ_LG_FNR_ROLE_DEFINE_MAP);
                        }

                        if (result == "True")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            pOBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pOBJ_LG_FNR_ROLE_DEFINE_MAP);

                        }
                    }
                    else
                    {
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pOBJ_LG_FNR_ROLE_DEFINE_MAP);
                    }

                }
                pOBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pOBJ_LG_FNR_ROLE_DEFINE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't save role. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't Save role.";
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_RoleDefineInfo_ByRoleId/" + id + "?format=json";
                OBJ_LG_FNR_ROLE_DEFINE_MAP = HttpWcfRequest.GetObject<LG_FNR_ROLE_DEFINE_MAP>(url);
                TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {

                    ViewData["Error"] = "Can't edit role define info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit role define info.";
                }
            }
            return View(OBJ_LG_FNR_ROLE_DEFINE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit(LG_FNR_ROLE_DEFINE_MAP pOBJ_LG_FNR_ROLE_DEFINE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_RoleDefine?format=json";
                        Uri uri = new Uri(url);
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.LIST_SELECTED_FUNCTION_DETAILS = (List<LG_FNR_ROLE_DEFINE_MAP>)Session["UpdatedFunctionDetails"];
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.ROLE_DEFINE_COMMAND = command;
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.MAKE_BY = (string)Session["currentUserID"];
                        result = HttpWcfRequest.PostObject_WithReturningString<LG_FNR_ROLE_DEFINE_MAP>(uri, pOBJ_LG_FNR_ROLE_DEFINE_MAP);

                        if (result == "True")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            pOBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pOBJ_LG_FNR_ROLE_DEFINE_MAP);
                        }
                    }
                    else
                    {
                        pOBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pOBJ_LG_FNR_ROLE_DEFINE_MAP);
                    }
                }
                pOBJ_LG_FNR_ROLE_DEFINE_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pOBJ_LG_FNR_ROLE_DEFINE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update role define. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update role define.";
                }
                return View();
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_OtpConfig_ByAppId/" + id + "?format=json";
                OBJ_LG_FNR_ROLE_DEFINE_MAP = HttpWcfRequest.GetObject<LG_FNR_ROLE_DEFINE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show role details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show role details.";
                }
            }
            return View(OBJ_LG_FNR_ROLE_DEFINE_MAP);
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
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_OtpConfig/" + id + session_user + "?format=json";
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
                    TempData["Error"] = "Can't delete role. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't delete role.";
                }
                return View();
            }

        }




        #region Create-View custom methods
        public ActionResult GetRoleInfoByRoleName(string pROLE_NAME)
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Role_ByRoleName/" + pROLE_NAME + "?format=json";
                OBJ_LG_FNR_ROLE_MAP = HttpWcfRequest.GetObject<LG_FNR_ROLE_MAP>(url);
                if (OBJ_LG_FNR_ROLE_MAP != null)
                {
                    return Json(OBJ_LG_FNR_ROLE_MAP, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        [HttpPost]
        public JsonResult GetFunctionsForGrid(string sidx, string sord, int page, int rows, string app_id, string service_id, string module_id, string item_type)
        {
            try
            {
                //List<SelectListItem> List = new List<SelectListItem>();
                List<LG_FNR_ROLE_DEFINE_MAP> LIST_LG_FNR_ROLE_DEFINE_MAP = new List<LG_FNR_ROLE_DEFINE_MAP>();
                if (module_id.Any(char.IsLetterOrDigit) && item_type.Any(char.IsLetterOrDigit))
                {
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Functions_ByModuleIdAndItemtype/" + app_id + "/" + service_id + "/" + module_id + "/" + item_type + "?format=json";
                    LIST_LG_FNR_ROLE_DEFINE_MAP = HttpWcfRequest.GetObjectCollection<LG_FNR_ROLE_DEFINE_MAP>(url);
                }

                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
                //var todoListResults = new List<SelectListItem>();
                //todoListResults.Add(new SelectListItem
                //{
                //    Value = "Salekin",
                //    Text = "Grid Test"
                //});


                //int totalRecords = List.Count();
                int totalRecords = LIST_LG_FNR_ROLE_DEFINE_MAP.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    //rows = List
                    rows = LIST_LG_FNR_ROLE_DEFINE_MAP
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Service is unable to process the request.";
                return null;
            }
        }
        [HttpPost]
        public void GetSelectedFunctionDetailsFromGrid(List<LG_FNR_ROLE_DEFINE_MAP> selected_rowData)
        {
            if (selected_rowData != null)
            {
                Session["SelectedFunctionDetails"] = selected_rowData;
            }
            //return Json("5", JsonRequestBehavior.AllowGet);
            //return Json(selected_rowData);
        }
        #endregion

        #region Edit-View custom methods
        [HttpPost]
        public JsonResult GetSelectedFunctionsForGridByRoleId(string sidx, string sord, int page, int rows, string app_id, string service_id, string module_id, string item_type, string role_id)
        {
            try
            {
                List<LG_FNR_ROLE_DEFINE_MAP> LIST_MODIFIED_FUNCTIONS = new List<LG_FNR_ROLE_DEFINE_MAP>();

                string papp_id = string.IsNullOrEmpty(app_id) ? " " : app_id;
                string pservice_id = string.IsNullOrEmpty(service_id) ? " " : service_id;
                string pmodule_id = string.IsNullOrEmpty(module_id) ? " " : module_id;
                string pitem_type = string.IsNullOrEmpty(item_type) ? " " : item_type;



                if (app_id.Any(char.IsLetterOrDigit) && service_id.Any(char.IsLetterOrDigit) && module_id.Any(char.IsLetterOrDigit) && item_type.Any(char.IsLetterOrDigit) && role_id.Any(char.IsLetterOrDigit))
                {
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_AllAndSelectedFunctions_ByRoleId/" + app_id  + "/" + service_id + "/" + module_id + "/" + item_type + "/" + role_id + "?format=json";
                    LIST_MODIFIED_FUNCTIONS = HttpWcfRequest.GetObjectCollection<LG_FNR_ROLE_DEFINE_MAP>(url);
                }

                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
                int totalRecords = LIST_MODIFIED_FUNCTIONS.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = LIST_MODIFIED_FUNCTIONS
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Service is unable to process the request.";
                return null;
            }
        }
        [HttpPost]
        public void GetUpdatedFunctionDetailsFromGrid(List<LG_FNR_ROLE_DEFINE_MAP> selected_rowData)
        {
            if (selected_rowData != null)
            {
                Session["UpdatedFunctionDetails"] = selected_rowData;
            }
        }
        public ActionResult GetFunctionIdsByRoleID(string role_id)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_FunctionIds_ByRoleID/" + role_id + "?format=json";
                List<string> Arr_FunctionIds = HttpWcfRequest.GetStringObjectCollection(url);
                return Json(Arr_FunctionIds, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Service is unable to process the request.";
                return null;
            }
        }

        
        [HttpPost] //For 2nd grid
        public JsonResult GetExistingFunctionsForGridByRoleId(string sidx, string sord, int page, int rows, string role_id)
        {
            try
            {
                List<LG_FNR_ROLE_DEFINE_MAP> LIST_MODIFIED_FUNCTIONS = new List<LG_FNR_ROLE_DEFINE_MAP>();

                if (role_id.Any(char.IsLetterOrDigit))
                {
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_SelectedFunctions_ByRoleId/" + role_id + "?format=json";
                    LIST_MODIFIED_FUNCTIONS = HttpWcfRequest.GetObjectCollection<LG_FNR_ROLE_DEFINE_MAP>(url);
                }

                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
                int totalRecords = LIST_MODIFIED_FUNCTIONS.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = LIST_MODIFIED_FUNCTIONS
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Service is unable to process the request.";
                return null;
            }
        }
        #endregion
    }
}
