using LguardaApplication.Utility;
using LgurdaApp.Model;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using LguardaApp.RBAC.Action_Filters;


namespace LguardaApplication.Controllers
{
    public class SessionInitializeController : Controller
    {
        //
        // GET: /SessionInitialize/

        List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE_MAP = new List<LG_USER_SETUP_PROFILE_MAP>();
        LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = new LG_USER_SETUP_PROFILE_MAP();

    
         [HttpGet]
        public ActionResult Index(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP, string User_command)
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                ViewBag.APPLICATION_LIST_FOR_DD = OBJ_LG_USER_SETUP_PROFILE_MAP.APPLICATION_LIST_FOR_DD;

                if (User_command != null)
                {
                    string session_user = Session["currentUserID"].ToString();
                    string url1 = Utility.AppSetting.getLgardaServer() + "/Get_user_session/" + session_user + "/" + pLG_USER_SETUP_PROFILE_MAP.APPLICATION_ID + "?format=json";
                    LIST_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObjectCollection<LG_USER_SETUP_PROFILE_MAP>(url1);
                }
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_USER_SETUP_PROFILE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show application info. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show application info.";
                }
                return View(LIST_LG_USER_SETUP_PROFILE_MAP);
            }
        }

    
        [HttpPost]
        public ActionResult Index(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP, string User_command, string command)
        {
            try
            {

                if (User_command != "Application")
                {
                    string result = string.Empty;
                    string user_id = null;
                    var tr = User_command.Split(':');
                    if (tr[0] == "Add")
                    {

                        user_id = (tr[1]);
                    }

                    string session_user = Session["currentUserID"].ToString();
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/UpdateUserSession/" + user_id + "/" + session_user + "?format=json";
                    result = HttpWcfRequest.PostParameter(url);



                    if (result == "True")
                    {
                        TempData["Success"] = "Initialized Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(pLG_USER_SETUP_PROFILE_MAP);
                    }
                }

                else
                {
                    try
                    {
                        string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                        OBJ_LG_USER_SETUP_PROFILE_MAP.APPLICATION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                        ViewBag.APPLICATION_LIST_FOR_DD = OBJ_LG_USER_SETUP_PROFILE_MAP.APPLICATION_LIST_FOR_DD;

                        if (User_command != null)
                        {
                            string session_user = Session["currentUserID"].ToString();
                            string url1 = Utility.AppSetting.getLgardaServer() + "/Get_user_session/" + session_user + "/" + pLG_USER_SETUP_PROFILE_MAP.APPLICATION_ID + "?format=json";
                            LIST_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObjectCollection<LG_USER_SETUP_PROFILE_MAP>(url1);

                            
                            if (TempData["Success"] != null)
                            { ViewData["Success"] = TempData["Success"].ToString(); }
                            if (TempData["Error"] != null)
                            { ViewData["Error"] = TempData["Error"].ToString(); }

                        }
                        return View(LIST_LG_USER_SETUP_PROFILE_MAP);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Service"))
                        {
                            TempData["Error"] = "Can't show application info. Service is unable to process the request.";
                        }
                        else
                        {
                            TempData["Error"] = "Can't show application info.";
                        }
                        return View(LIST_LG_USER_SETUP_PROFILE_MAP);
                    }
                }
            }


            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't save role. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't Save role.";
                }
                return View("Create");
            }
        }

     
    
    }
}
