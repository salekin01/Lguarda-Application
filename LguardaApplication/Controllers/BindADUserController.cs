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
using System.Data.Entity;
using System.DirectoryServices;

namespace LguardaApplication.Controllers
{
    public class BindADUserController : Controller
    {
        #region Class Level Variable
        List<LG_USER_AD_BINDING_MAP> LIST_LG_USER_AD_BINDING_MAP = new List<LG_USER_AD_BINDING_MAP>();
        LG_USER_AD_BINDING_MAP OBJ_LG_USER_AD_BINDING_MAP = new LG_USER_AD_BINDING_MAP();
        #endregion


        // GET: /BindADUser/
        // [RBAC]
        public ActionResult Index()
        {
            string url = Utility.AppSetting.getLgardaServer() + "/Get_allAd_User" + "?format=json";
            LIST_LG_USER_AD_BINDING_MAP = HttpWcfRequest.GetObjectCollection<LG_USER_AD_BINDING_MAP>(url);
            return View(LIST_LG_USER_AD_BINDING_MAP);
            
        }



       // [RBAC]
        public ActionResult Create()
        {
            if (TempData["Success"] != null)
            { ViewData["Success"] = TempData["Success"].ToString(); }
            if (TempData["Error"] != null)
            { ViewData["Error"] = TempData["Error"].ToString(); }
            return View();
        }

        #region Custom method for Ad user info
        public static bool isAdUser(string domain_id)
        {
            string domainName = ConfigurationManager.AppSettings["domain_name"].ToString();
            string valid_domain_userID = ConfigurationManager.AppSettings["valid_domain_userID"].ToString();
            string valid_domain_userPass = ConfigurationManager.AppSettings["valid_domain_userPass"].ToString();
            bool isTrue = false;

            using (var de = new DirectoryEntry(domainName, valid_domain_userID, valid_domain_userPass))
            using (var ds = new DirectorySearcher(de))
            {
                //NetworkCredential credential = new NetworkCredential("mehedi", "mrocky");
                ds.Filter = string.Format("(sAMAccountName={0})", domain_id);
                ds.PropertiesToLoad.AddRange(new[] {
                                                    "sn",  // last name
                                                    "givenName",  // first name
                                                    "mail",  // email
                                                    "telephoneNumber",  // phone number
                                                    // etc - add other properties you need
                                                    });

                var res = ds.FindOne();

                if (res != null)
                    isTrue = true;
            }

            return isTrue;
        }

        [HttpGet]
        public JsonResult isValidAdUser(string domain_id)
        {
            string domainName = ConfigurationManager.AppSettings["domain_name"].ToString();
            //string domain_id = ConfigurationManager.AppSettings["domain_id"].ToString();
            string valid_domain_userID = ConfigurationManager.AppSettings["valid_domain_userID"].ToString();
            string valid_domain_userPass = ConfigurationManager.AppSettings["valid_domain_userPass"].ToString();
            ADUserInformation adInfo = new ADUserInformation();

            using (var de = new DirectoryEntry(domainName, valid_domain_userID, valid_domain_userPass))
            using (var ds = new DirectorySearcher(de))
            {
                //NetworkCredential credential = new NetworkCredential("mehedi", "mrocky");
                ds.Filter = string.Format("(sAMAccountName={0})", domain_id);
                ds.PropertiesToLoad.AddRange(new[] {
                                                    "sn",  // last name
                                                    "givenName",  // first name
                                                    "mail",  // email
                                                    "telephoneNumber",  // phone number
                                                    // etc - add other properties you need
                                                    });

                var res = ds.FindOne();

                if (res != null)
                {
                    foreach (string propName in res.Properties.PropertyNames)
                    {
                        ResultPropertyValueCollection valueCollection = res.Properties[propName];
                        foreach (Object propertyValue in valueCollection)
                        {
                            //Console.WriteLine("Property: " + propName + ": " + propertyValue.ToString());
                            if (propName.ToUpper() == "SN")
                            {
                                adInfo.LAST_NAME = propertyValue.ToString();
                            }
                            else if (propName.ToUpper() == "GIVENNAME")
                            {
                                adInfo.FIRST_NAME = propertyValue.ToString();
                            }
                            else if (propName.ToUpper() == "MAIL")
                            {
                                adInfo.MAIL = propertyValue.ToString();
                            }
                            else if (propName.ToUpper() == "TELEPHONENUMBER")
                            {
                                adInfo.TL_PHONE = propertyValue.ToString();
                            }
                        }
                    }
                }
            }

            return Json(adInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        // [RBAC]
        [HttpPost]
        public ActionResult Create(LG_USER_AD_BINDING_MAP pLG_USER_AD_BINDING_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        bool isValidADUser = isAdUser(pLG_USER_AD_BINDING_MAP.DOMAIN_ID);

                        if(isValidADUser)
                        {
                            string session_user = Session["currentUserID"].ToString();
                            string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Bind_AD_User/" + pLG_USER_AD_BINDING_MAP.USER_ID + "/" + pLG_USER_AD_BINDING_MAP.DOMAIN_ID + "/" + pLG_USER_AD_BINDING_MAP.DOMAIN + "/" + pLG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG_B + "/" + session_user + "?format=json";
                            result = HttpWcfRequest.PostParameter(url);


                            if (result.ToLower().Contains("already exists"))
                            {
                                ModelState.AddModelError("DOMAIN_ID", "AD Name Already Exists");
                                return View(pLG_USER_AD_BINDING_MAP);
                            }
                            if (result != string.Empty && result.ToLower() == "true")
                            {
                                TempData["Success"] = "Saved Successfully.";
                                return RedirectToAction("Create");
                            }
                            else
                            {
                                return View(pLG_USER_AD_BINDING_MAP);
                            }
                        }
                        else
                        {
                            ViewData["Error"] = "Invalid Domain ID";
                            return View(pLG_USER_AD_BINDING_MAP);
                        }
                    }
                    else
                    {
                        return View(pLG_USER_AD_BINDING_MAP);
                    }

                }
                return View(pLG_USER_AD_BINDING_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't save Ad. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't Save Ad.";
                }
                return View("Create");
            }
        }



        //[RBAC]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View();
            }
            try
            {

                string url = Utility.AppSetting.getLgardaServer() + "/GetBindUser/" + id + "?format=json";
                OBJ_LG_USER_AD_BINDING_MAP = HttpWcfRequest.GetObject<LG_USER_AD_BINDING_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit AD user info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit AD user info.";
                }
            }
            return View(OBJ_LG_USER_AD_BINDING_MAP);
        }
        //[RBAC]
        [HttpPost]
        public ActionResult Edit(/*[Bind(Include = "USER_ID,DOMAIN_ID")] */ LG_USER_AD_BINDING_MAP pOBJ_LG_USER_AD_BINDING_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/UpdateADUser/" + pOBJ_LG_USER_AD_BINDING_MAP.SL + "/" + pOBJ_LG_USER_AD_BINDING_MAP.DOMAIN_ID + "/" + pOBJ_LG_USER_AD_BINDING_MAP.USER_ID + "/" + pOBJ_LG_USER_AD_BINDING_MAP.DOMAIN + "/" + pOBJ_LG_USER_AD_BINDING_MAP.AD_ACTIVE_FLAG_B + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                      

                        if (result != string.Empty && result.ToLower().Contains("no changes"))
                        {
                            ViewData["Error"] = "No changes made.";
                            return View(pOBJ_LG_USER_AD_BINDING_MAP);
                        }

                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View(pOBJ_LG_USER_AD_BINDING_MAP);
                        }
                    }
                    else
                    {
                        return View(pOBJ_LG_USER_AD_BINDING_MAP);
                    }
                }
                return View(pOBJ_LG_USER_AD_BINDING_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update application. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update application.";
                }
                return View();
            }
        }







	}
}