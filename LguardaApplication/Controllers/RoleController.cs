using LguardaApp.RBAC.Action_Filters;
using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Controllers
{
    public class RoleController : Controller
    {
        #region Class Level Variable
        List<LG_FNR_ROLE_MAP> LIST_LG_FNR_ROLE_MAP = new List<LG_FNR_ROLE_MAP>();
        LG_FNR_ROLE_MAP OBJ_LG_FNR_ROLE_MAP = new LG_FNR_ROLE_MAP();
        #endregion

        [RBAC]
        public ActionResult Index()
        {
            string url = Utility.AppSetting.getLgardaServer() + "/Get_Roles" + "?format=json";
            LIST_LG_FNR_ROLE_MAP = HttpWcfRequest.GetObjectCollection<LG_FNR_ROLE_MAP>(url);
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View(LIST_LG_FNR_ROLE_MAP);
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Role_ByRoleId/" + id + "?format=json";
                OBJ_LG_FNR_ROLE_MAP = HttpWcfRequest.GetObject<LG_FNR_ROLE_MAP>(url);
                if (OBJ_LG_FNR_ROLE_MAP != null)
                {
                    OBJ_LG_FNR_ROLE_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_ROLE_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show role details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show role details.";
                }
            }
            return View(OBJ_LG_FNR_ROLE_MAP);
        }


        [RBAC]
        public ActionResult Create()
        {
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View();
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_FNR_ROLE_MAP pLG_FNR_ROLE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Role/" + pLG_FNR_ROLE_MAP.ROLE_NAME + "/" + pLG_FNR_ROLE_MAP.ROLE_DESCRIP + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);


                        if (result.ToLower().Contains("already exists"))
                        {
                            ModelState.AddModelError("ROLE_NAME", "Role Name Already Exists");
                            return View(pLG_FNR_ROLE_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            return View(pLG_FNR_ROLE_MAP);
                        }
                    }
                    else
                    {
                        return View(pLG_FNR_ROLE_MAP);
                    }

                }
                return View(pLG_FNR_ROLE_MAP);
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Role_ByRoleId/" + id + "?format=json";
                OBJ_LG_FNR_ROLE_MAP = HttpWcfRequest.GetObject<LG_FNR_ROLE_MAP>(url);
                if (OBJ_LG_FNR_ROLE_MAP != null)
                {
                    OBJ_LG_FNR_ROLE_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_ROLE_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit role info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit role info.";
                }
            }
            return View(OBJ_LG_FNR_ROLE_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit(LG_FNR_ROLE_MAP pLG_FNR_ROLE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_Role/" + pLG_FNR_ROLE_MAP.ROLE_ID + "/" + pLG_FNR_ROLE_MAP.ROLE_NAME + "/" + pLG_FNR_ROLE_MAP.ROLE_DESCRIP + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View(pLG_FNR_ROLE_MAP);
                        }
                    }
                    else
                    {
                        return View(pLG_FNR_ROLE_MAP);
                    }
                }
                return View(pLG_FNR_ROLE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update role. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update role.";
                }
                return View(pLG_FNR_ROLE_MAP);
            }
        }


        [RBAC]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Role_ByRoleId/" + id + "?format=json";
                OBJ_LG_FNR_ROLE_MAP = HttpWcfRequest.GetObject<LG_FNR_ROLE_MAP>(url);
                if (OBJ_LG_FNR_ROLE_MAP != null)
                {
                    OBJ_LG_FNR_ROLE_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_ROLE_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_ROLE_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
                TempData["OBJ_LG_FNR_ROLE_MAP"] = OBJ_LG_FNR_ROLE_MAP;
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
            }
            return View(OBJ_LG_FNR_ROLE_MAP);
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
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_Role/" + id + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result == "True")
                        {
                            TempData["Success"] = "Deleted Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View(TempData["OBJ_LG_FNR_ROLE_MAP"]);
                        }
                    }
                    else
                    {
                        return View(TempData["OBJ_LG_FNR_ROLE_MAP"]);
                    }
                }
                return View(TempData["OBJ_LG_FNR_ROLE_MAP"]);
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
    }
}
