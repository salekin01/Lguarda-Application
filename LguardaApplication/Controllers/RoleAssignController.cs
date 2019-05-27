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
    public class RoleAssignController : Controller
    {
        #region Class Level Variable
        List<LG_USER_ROLE_ASSIGN_MAP> LIST_LG_USER_ROLE_ASSIGN_MAP = new List<LG_USER_ROLE_ASSIGN_MAP>();
        LG_USER_ROLE_ASSIGN_MAP OBJ_LG_USER_ROLE_ASSIGN_MAP = new LG_USER_ROLE_ASSIGN_MAP();
        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_AllRoleAssignedInfo" + "?format=json";
                LIST_LG_USER_ROLE_ASSIGN_MAP = HttpWcfRequest.GetObjectCollection<LG_USER_ROLE_ASSIGN_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_USER_ROLE_ASSIGN_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show role assigned info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show role assigned info.";
                }
                return View(LIST_LG_USER_ROLE_ASSIGN_MAP);
            }
        }


        [RBAC]
        public ActionResult Create()
        {
            //string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Application_For_DD" + "?format=json";
            //OBJ_LG_USER_ROLE_ASSIGN_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);

            TempData["APPLICATION_LIST_FOR_DD"] = OBJ_LG_USER_ROLE_ASSIGN_MAP.APPLICATION_LIST_FOR_DD;
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View();
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_USER_ROLE_ASSIGN_MAP pLG_USER_ROLE_ASSIGN_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                var make_by = Session["currentUserID"].ToString();
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        pLG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER = (string)Session["selected_roleids"];

                        if (!pLG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER.Any(char.IsLetterOrDigit))
                        {
                            pLG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER = " ";
                        }

                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_RoleAssign/" + pLG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER + "/" + command + "/" + pLG_USER_ROLE_ASSIGN_MAP.USER_ID + "/" + make_by + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result.ToLower().Contains("already"))
                        {
                            pLG_USER_ROLE_ASSIGN_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            ModelState.AddModelError("USER_ID", "User role already assigned");
                            return View(pLG_USER_ROLE_ASSIGN_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            pLG_USER_ROLE_ASSIGN_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                            return View(pLG_USER_ROLE_ASSIGN_MAP);
                        }
                    }
                    else
                    {
                        pLG_USER_ROLE_ASSIGN_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                        return View(pLG_USER_ROLE_ASSIGN_MAP);
                    }
                }
                pLG_USER_ROLE_ASSIGN_MAP.APPLICATION_LIST_FOR_DD = (List<SelectListItem>)TempData["APPLICATION_LIST_FOR_DD"];
                return View(pLG_USER_ROLE_ASSIGN_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't save role assigned. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't Save role assigned.";
                }
                return View("Create");
            }
        }


        [RBAC]
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_RoleAssignInfo_ByUserId/" + id + "?format=json";
                OBJ_LG_USER_ROLE_ASSIGN_MAP = HttpWcfRequest.GetObject<LG_USER_ROLE_ASSIGN_MAP>(url);

                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit role assigned info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit role assigned info.";
                }
            }
            return View(OBJ_LG_USER_ROLE_ASSIGN_MAP);
        }
        [RBAC]
        [HttpPost]
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Edit(LG_USER_ROLE_ASSIGN_MAP pOBJ_LG_USER_ROLE_ASSIGN_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                var currentUserID = Session["currentUserID"].ToString();
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        pOBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER = (string)Session["UpdatedRoleIds"];
                        pOBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ASSIGN_COMMAND = command;

                        if (!pOBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER.Any(char.IsLetterOrDigit))
                        {
                            pOBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER = " ";
                        }

                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_RoleAssign/" + pOBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ID_FOR_IND_USER + "/"
                                                                                                               + pOBJ_LG_USER_ROLE_ASSIGN_MAP.ROLE_ASSIGN_COMMAND + "/"
                                                                                                               + pOBJ_LG_USER_ROLE_ASSIGN_MAP.USER_ID + "/" 
                                                                                                               + currentUserID + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View(pOBJ_LG_USER_ROLE_ASSIGN_MAP);
                        }
                    }
                    else
                    {
                        return View(pOBJ_LG_USER_ROLE_ASSIGN_MAP);
                    }
                }
                return View(pOBJ_LG_USER_ROLE_ASSIGN_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update role assigned info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update role assigned info.";
                }
                return RedirectToAction("Edit", "RoleAssign", pOBJ_LG_USER_ROLE_ASSIGN_MAP.USER_ID);
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_RoleAssignInfo_ByUserId/" + id + "?format=json";
                OBJ_LG_USER_ROLE_ASSIGN_MAP = HttpWcfRequest.GetObject<LG_USER_ROLE_ASSIGN_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show role assign info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show role assign info.";
                }
            }
            return View(OBJ_LG_USER_ROLE_ASSIGN_MAP);
        }


        #region Create-View custom methods
        public JsonResult GetRolesAssignForGrid(string sidx, string sord, int page, int rows, string pUser_id, string pFormAction /*, string application_id*/)
        {
            try
            {
                List<SelectListItem> Role_List = new List<SelectListItem>();
                
                /*if (application_id.Any(char.IsLetterOrDigit))
                {
                    //string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Roles_ByApplicationID/" + application_id + "?format=json";
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_AllRoles" + "?format=json";
                    Role_List = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                }*/

                if (pFormAction == "Edit")
                {
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_AllAuthorizedRoles_ByUserId/" + pUser_id + "?format=json";
                    Role_List = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                }
                else
                {
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_AllRoles" + "?format=json";
                    Role_List = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                }

                int pageIndex = Convert.ToInt32(page) - 1;
                int pageSize = rows;
                int totalRecords = Role_List.Count();
                var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
                var jsonData = new
                {
                    total = totalPages,
                    page,
                    records = totalRecords,
                    rows = Role_List
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show roles. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show roles.";
                }
                return null;
            }
        }
        public void GetSelectedRoleFromGrid(string selected_roleids)
        {
            if (selected_roleids != null)
            {
                Session["selected_roleids"] = selected_roleids;
            }
        }
        public ActionResult GetUserInfoByUserId(string pUSER_ID)
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserInfo_ByUserId/" + pUSER_ID + "?format=json";
                OBJ_LG_USER_ROLE_ASSIGN_MAP = HttpWcfRequest.GetObject<LG_USER_ROLE_ASSIGN_MAP>(url);
                if (OBJ_LG_USER_ROLE_ASSIGN_MAP != null)
                {
                    return Json(OBJ_LG_USER_ROLE_ASSIGN_MAP, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Edit-View custom methods
        public void GetUpdatedRoleIdsFromGrid(string selected_roleids)
        {
            if (selected_roleids != null)
            {
                Session["UpdatedRoleIds"] = selected_roleids;
            }
        }
        //[OutputCache(NoStore = true, Duration = 0)]
        public ActionResult GetRoleIdsByUserID(string user_id)
        {
            try
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_RoleIds_ByUserID/" + user_id + "?format=json";
                List<string> List_RoleIds = HttpWcfRequest.GetStringObjectCollection(url);
                return Json(List_RoleIds, JsonRequestBehavior.AllowGet);
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
