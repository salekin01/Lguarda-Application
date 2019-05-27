using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LgurdaApp.Model.ControllerModels;
using LguardaApplication.Utility;
using System.Configuration;
using LguardaApp.RBAC.Action_Filters;

namespace LguardaApplication.Controllers
{
    public class PasswordPolicyController : Controller
    {
        #region Class Level Variable

        List<LG_CRD_PASSWORD_POLICY_MAP> LIST_LG_CRD_PASSWORD_POLICY_MAP = new List<LG_CRD_PASSWORD_POLICY_MAP>();
        LG_CRD_PASSWORD_POLICY_MAP OBJ_LG_CRD_PASSWORD_POLICY_MAP = new LG_CRD_PASSWORD_POLICY_MAP();

        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Password_Policy" + "?format=json";
                LIST_LG_CRD_PASSWORD_POLICY_MAP = HttpWcfRequest.GetObjectCollection<LG_CRD_PASSWORD_POLICY_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_CRD_PASSWORD_POLICY_MAP);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show application info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show application info.";
                }
                return View(LIST_LG_CRD_PASSWORD_POLICY_MAP);
            }
        }
        [RBAC]
        public ActionResult Details(string id)
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Password_Policy_ByAppId/" + id + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP = HttpWcfRequest.GetObject<LG_CRD_PASSWORD_POLICY_MAP>(url);
                if (OBJ_LG_CRD_PASSWORD_POLICY_MAP != null)
                {
                    OBJ_LG_CRD_PASSWORD_POLICY_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_CRD_PASSWORD_POLICY_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
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
            return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
        }


        [RBAC]
        public ActionResult Create()
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);

                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
            }

            catch (Exception ex)
            {
                ViewBag.Color = "red";
                ViewBag.Message = "Service is unable to process the request.";
                return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
            }
            //   return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);

            //return View();
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_CRD_PASSWORD_POLICY_MAP pLG_CRD_PASSWORD_POLICY_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        if (pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN_B == true)
                        {
                            pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN = "1";
                        }
                        else
                        {
                            pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN = "0";
                        }

                        if (pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION_B == true)
                        {
                            pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION = "1";
                        }
                        else
                        {
                            pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION = "0";
                        }

                        if (pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN_B == true)
                        {
                            pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN = "1";
                        }
                        else
                        {
                            pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN = "0";
                        }


                        string userId = "";

                        if (Session["currentUserID"] != null)
                        {
                            userId = Session["currentUserID"].ToString();
                        }

                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Password_Policy/" + userId + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_MAX_LENGTH + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_MIN_LENGTH + "/" + pLG_CRD_PASSWORD_POLICY_MAP.NUMERIC_CHAR_MIN + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_HIS_PERIOD + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_CAPS_LETTER + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_SMALL_LETTER + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_NUMERIC_CHAR + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_CONS_USE_PASS + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_REPEAT + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGED_EXPIRY + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN + "/" + pLG_CRD_PASSWORD_POLICY_MAP.APPLICATION_ID + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_REUSE_MAX + "/" + pLG_CRD_PASSWORD_POLICY_MAP.FAILED_LOGIN_ATTEMT + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_PERIOD + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_ALERT + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result.ToLower().Contains("already exists"))
                        {
                            ModelState.AddModelError("APPLICATION_ID", "Password Policy Already Exists");
                            string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                            OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                            return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                        }

                        if (result == "True")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Save.";
                            string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                            OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                            return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                        }
                    }
                    else
                    {
                        string url2 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url2);
                        return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                    }
                }
                string url3 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url3);
                return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
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



                string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                // return View("Create");
            }
        }


        [RBAC]
        public ActionResult Edit(string id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            string url = Utility.AppSetting.getLgardaServer() + "/Get_Password_Policy" + "?format=json";
            LIST_LG_CRD_PASSWORD_POLICY_MAP = HttpWcfRequest.GetObjectCollection<LG_CRD_PASSWORD_POLICY_MAP>(url);

            //if (OBJ_LG_CRD_PASSWORD_POLICY_MAP != null)
            //{
            //    OBJ_LG_CRD_PASSWORD_POLICY_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_CRD_PASSWORD_POLICY_MAP.MAKE_DT).AddHours(6);
            //    if (OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT != null)
            //    {
            //        OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT).AddHours(6);
            //    }
            //}

            for (int i = 0; i < LIST_LG_CRD_PASSWORD_POLICY_MAP.Count; i++)
            {
                OBJ_LG_CRD_PASSWORD_POLICY_MAP = LIST_LG_CRD_PASSWORD_POLICY_MAP[i];

                string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);

                if (id == OBJ_LG_CRD_PASSWORD_POLICY_MAP.APPLICATION_ID)
                {
                    if (OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION == "1")
                    {
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION_B = true;
                    }
                    if (OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN == "1")
                    {
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN_B = true;
                    }
                    if (OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN == "1")
                    {
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN_B = true;
                    }

                    return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                }
            }



            return View();
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit(LG_CRD_PASSWORD_POLICY_MAP pLG_CRD_PASSWORD_POLICY_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN_B == true)
                    {
                        pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN = "1";
                    }
                    else
                    {
                        pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN = "0";
                    }

                    if (pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION_B == true)
                    {
                        pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION = "1";
                    }
                    else
                    {
                        pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION = "0";
                    }

                    if (pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN_B == true)
                    {
                        pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN = "1";
                    }
                    else
                    {
                        pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN = "0";
                    }

                    string userId = "";

                    if (Session["currentUserID"] != null)
                    {
                        userId = Session["currentUserID"].ToString();
                    }

                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_Password_Policy/" + userId + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_MAX_LENGTH + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_MIN_LENGTH + "/" + pLG_CRD_PASSWORD_POLICY_MAP.NUMERIC_CHAR_MIN + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_HIS_PERIOD + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_CAPS_LETTER + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_SMALL_LETTER + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_NUMERIC_CHAR + "/" + pLG_CRD_PASSWORD_POLICY_MAP.MIN_CONS_USE_PASS + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_REPEAT + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGED_EXPIRY + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_AUTO_CREATION + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANGE_BY_ADMIN + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_CHANG_AT_FIRST_LOGIN + "/" + pLG_CRD_PASSWORD_POLICY_MAP.APPLICATION_ID + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_REUSE_MAX + "/" + pLG_CRD_PASSWORD_POLICY_MAP.FAILED_LOGIN_ATTEMT + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_PERIOD + "/" + pLG_CRD_PASSWORD_POLICY_MAP.PASS_EXP_ALERT + "?format=json";
                    result = HttpWcfRequest.PostParameter(url);


                    #region Way-1 Post Json Data (Final)
                    /* 
                    string url = Utility.AppSetting.getLgardaServer() + "/Update_Application?format=json";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    //var objTarget = Obj_Service.AddApplication(url, Obj_SYS_APPLICATION);
                    var serilizer = new DataContractJsonSerializer(typeof(LgardaServiceRef.SYS_APPLICATION));
                    
                    request.Method = "POST";
                    request.ContentType = "application/json; charset=utf-8";

                    LgardaServiceRef.Bool_Check Obj_Bool_Check;
                    using (var requestStream = request.GetRequestStream())
                    {
                        serilizer.WriteObject(requestStream, Obj_SYS_APPLICATION);

                        var response = (HttpWebResponse)request.GetResponse();
                        var responseStream = response.GetResponseStream();
                        var dcs = new DataContractJsonSerializer(typeof(LgardaServiceRef.Bool_Check));
                        Obj_Bool_Check = (LgardaServiceRef.Bool_Check)dcs.ReadObject(responseStream);

                        response.Close();
                    }  */
                    #endregion

                    if (result == "True")
                    {
                        TempData["Success"] = "Updated Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Unable to Update.";
                        string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                        return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
                        // return View("Edit");
                    }
                }


                return View();
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


                string url1 = Utility.AppSetting.getLgardaServer() + "/Get_Application_For_DD" + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP.LIST_APPLICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);

            }
        }


        [RBAC]
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Password_Policy_ByAppId/" + id + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP = HttpWcfRequest.GetObject<LG_CRD_PASSWORD_POLICY_MAP>(url);

                if(OBJ_LG_CRD_PASSWORD_POLICY_MAP != null)
                {
                    OBJ_LG_CRD_PASSWORD_POLICY_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_CRD_PASSWORD_POLICY_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_CRD_PASSWORD_POLICY_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }

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
            return View(OBJ_LG_CRD_PASSWORD_POLICY_MAP);
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
                        string userId = "";

                        if (Session["currentUserID"] != null)
                        {
                            userId = Session["currentUserID"].ToString();
                        }

                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_PasswordPolicy/" + userId + "/" + id + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result == "True")
                        {
                            TempData["Success"] = "Deleted Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Delete";
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
                    ViewData["Error"] = "Can't delete password policy. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't delete password policy.";
                }
                return View();
            }
        }
    }
}
