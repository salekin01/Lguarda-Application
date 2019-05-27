using LguardaApp.RBAC.Action_Filters;
using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

//using System.Data.Entity.Validation;

namespace LguardaApplication.Controllers
{
    public class UserProfileSetupController : Controller
    {
        #region CLASS LVEL VARIABLE

        private List<LG_USER_SETUP_PROFILE_MAP> LIST_LG_USER_SETUP_PROFILE_MAP = new List<LG_USER_SETUP_PROFILE_MAP>();
        private LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = new LG_USER_SETUP_PROFILE_MAP();

        #endregion CLASS LVEL VARIABLE

        [RBAC]
        public ActionResult Index()
        {
            string result;
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_AllUserSetupInfo" + "?format=json";
                LIST_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObjectCollection<LG_USER_SETUP_PROFILE_MAP>(url);
                //var a = HttpWcfRequest.GetObjectCollection<LG_USER_SETUP_PROFILE_MAP>(url);
            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
            //            result = "Can't Add Application(Db) " + validationError.ErrorMessage;
            //        }
            //    }

            //}
            catch (Exception ex)
            {
                result = "Can't Add User profile  " + ex.Message;
                //return result;
            }
            return View(LIST_LG_USER_SETUP_PROFILE_MAP);
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserId/" + id + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show user profile details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show user profile details.";
                }

                string exceptionMessage = ex.Message.ToString();
                string innerExceptionMessage = ex.InnerException.ToString();
            }
            return View(OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        [RBAC]
        public ActionResult Create()
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_User_Classification_For_DD" + "?format=json";
                string url1 = Utility.AppSetting.getLgardaServer() + "/Get_All_User_AreaId_For_DD" + "?format=json";
                string url2 = Utility.AppSetting.getLgardaServer() + "/Get_Branch_For_DD" + "?format=json";
                string url3 = Utility.AppSetting.getLgardaServer() + "/Get_Authentication_Type_For_DD" + "?format=json";
                string url4 = Utility.AppSetting.getLgardaServer() + "/Get_Work_Hour_Type_For_DD" + "?format=json";
                string url5 = Utility.AppSetting.getLgardaServer() + "/Get_Two_FA_type_For_DD" + "?format=json";
                string url6 = Utility.AppSetting.getLgardaServer() + "/Get_UserUploadFileType_ForDD" + "?format=json";

                OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                TempData["LIST_USER_CLASSIFICATION_FOR_DD"] = OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD;

                OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
                TempData["LIST_USER_AREA_FOR_DD"] = OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD;

                OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url2);
                TempData["LIST_BRANCH_FOR_DD"] = OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD;

                OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url3);
                TempData["LIST_AUTHENTICATION_TYPE_FOR_DD"] = OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD;

                OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url4);
                TempData["LIST_WORKING_HOUR_FOR_DD"] = OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD;

                OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url5);
                TempData["LIST_TWO_FA_TYPE_FOR_DD"] = OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD;

                OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_FILE_TYPE = HttpWcfRequest.GetObjectCollection<SelectListItem>(url6);
                TempData["LIST_USER_FILE_TYPE"] = OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_FILE_TYPE;

                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(OBJ_LG_USER_SETUP_PROFILE_MAP);
            }
            catch (Exception ex)
            {
                ViewBag.Color = "red";
                ViewBag.Message = "Service is unable to process the request.";
                return View(OBJ_LG_USER_SETUP_PROFILE_MAP);
            }
        }

        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP, string command, List<HttpPostedFileBase> fileUpload, HttpPostedFileBase profile_photo)
        {
            try
            {
                string result = string.Empty;
                OBJ_LG_USER_SETUP_PROFILE_MAP.USER_ID = Session["currentUserID"].ToString();
                if (command == "Save")
                {
                    if (ModelState.IsValid)
                    {
                        string id = pLG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID;

                        if (pLG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE = "null";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.BRANCH_ID == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.BRANCH_ID = "0000";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.ACC_NO == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.ACC_NO = "null";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.TERMINAL_IP == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.TERMINAL_IP = "null";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.USER_DESCRIPTION == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.USER_DESCRIPTION = "null";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.FATHERS_NAME == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.FATHERS_NAME = "null";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.MOTHERS_NAME == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.MOTHERS_NAME = "null";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.WORKING_HOUR == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.WORKING_HOUR = "0";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.START_TIME == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.START_TIME = "null";
                        }
                        if (pLG_USER_SETUP_PROFILE_MAP.END_TIME == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.END_TIME = "null";
                        }

                        if (pLG_USER_SETUP_PROFILE_MAP.day == 0 && pLG_USER_SETUP_PROFILE_MAP.month == null && pLG_USER_SETUP_PROFILE_MAP.year == 0)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.DOB = "null";
                        }
                        else
                        {
                            //branch.BRANCH_CLOSED_DT = Convert.ToDateTime(branch.BRANCH_CLOSED_DT).ToString("dd-MMM-yyyy");
                            string mmm = pLG_USER_SETUP_PROFILE_MAP.month.Substring(0, 3);
                            // string DOB1 = agent.month + "/" + agent.day + "/" + agent.year;
                            string DOB = pLG_USER_SETUP_PROFILE_MAP.day + "-" + mmm + "-" + pLG_USER_SETUP_PROFILE_MAP.year;
                            pLG_USER_SETUP_PROFILE_MAP.DOB = Convert.ToDateTime(DOB).ToString("dd-MMM-yyyy");// Convert.ToDateTime(DOB1).ToString();
                        }

                        if (pLG_USER_SETUP_PROFILE_MAP.HR == null && pLG_USER_SETUP_PROFILE_MAP.MIN == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.START_TIME = "null";
                        }
                        else
                        {
                            pLG_USER_SETUP_PROFILE_MAP.START_TIME = pLG_USER_SETUP_PROFILE_MAP.HR + "-" + pLG_USER_SETUP_PROFILE_MAP.MIN + "-" + "AM";
                        }

                        if (pLG_USER_SETUP_PROFILE_MAP.E_HR == null && pLG_USER_SETUP_PROFILE_MAP.E_MIN == null)
                        {
                            pLG_USER_SETUP_PROFILE_MAP.END_TIME = "null";
                        }
                        else
                        {
                            pLG_USER_SETUP_PROFILE_MAP.END_TIME = pLG_USER_SETUP_PROFILE_MAP.E_HR + "-" + pLG_USER_SETUP_PROFILE_MAP.E_MIN + "-" + "PM";
                        }

                        if (pLG_USER_SETUP_PROFILE_MAP.HR > (pLG_USER_SETUP_PROFILE_MAP.E_HR))
                        {
                            ModelState.AddModelError("HR", "End hour must be greater than start hour");
                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_CLASSIFICATION_FOR_DD"];

                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_AREA_FOR_DD"];

                            pLG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD = (List<SelectListItem>)TempData["LIST_BRANCH_FOR_DD"];

                            pLG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_AUTHENTICATION_TYPE_FOR_DD"];

                            pLG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = (List<SelectListItem>)TempData["LIST_WORKING_HOUR_FOR_DD"];

                            pLG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_TWO_FA_TYPE_FOR_DD"];

                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_FILE_TYPE = (List<SelectListItem>)TempData["LIST_USER_FILE_TYPE"];

                            return View(pLG_USER_SETUP_PROFILE_MAP);
                        }

                        pLG_USER_SETUP_PROFILE_MAP.USER_SESSION_ID = Session["currentUserID"].ToString();
                        string pAPPLICATION_ID = "01";
                        string pAUTH_STATUS_ID = "U"; //For MFS purpose
                        string pROLE_ID = "null"; //For MFS purpose

                        string uri = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_UserProfile/"
                        + pLG_USER_SETUP_PROFILE_MAP.USER_ID + "/" + pLG_USER_SETUP_PROFILE_MAP.USER_SESSION_ID + "/"
                        + pLG_USER_SETUP_PROFILE_MAP.USER_CLASSIFICATION_ID + "/" + pLG_USER_SETUP_PROFILE_MAP.USER_AREA_ID + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.USER_AREA_ID_VALUE + "/" + pLG_USER_SETUP_PROFILE_MAP.USER_NAME + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.USER_DESCRIPTION + "/" + pLG_USER_SETUP_PROFILE_MAP.BRANCH_ID + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.ACC_NO + "/" + pLG_USER_SETUP_PROFILE_MAP.FATHERS_NAME + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.MOTHERS_NAME + "/" + pLG_USER_SETUP_PROFILE_MAP.DOB + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.MAIL_ADDRESS + "/" + pLG_USER_SETUP_PROFILE_MAP.MOB_NO + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.AUTHENTICATION_ID + "/" + pLG_USER_SETUP_PROFILE_MAP.TERMINAL_IP + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.START_TIME + "/" + pLG_USER_SETUP_PROFILE_MAP.END_TIME + "/" +
                        pLG_USER_SETUP_PROFILE_MAP.WORKING_HOUR + "/" + pAPPLICATION_ID + "/" + pAUTH_STATUS_ID + "/" + pROLE_ID + "?format=json";

                        result = HttpWcfRequest.PostParameter(uri);

                        if (result.ToLower().Contains("already"))
                        {
                            ModelState.AddModelError("USER_ID", "User already assigned");
                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_CLASSIFICATION_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_AREA_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD = (List<SelectListItem>)TempData["LIST_BRANCH_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_AUTHENTICATION_TYPE_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = (List<SelectListItem>)TempData["LIST_WORKING_HOUR_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_TWO_FA_TYPE_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_FILE_TYPE = (List<SelectListItem>)TempData["LIST_USER_FILE_TYPE"];

                            if (pLG_USER_SETUP_PROFILE_MAP.WORKING_HOUR == "Fixed")
                            {
                                pLG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = (List<SelectListItem>)TempData["LIST_WORKING_HOUR_FOR_DD"];
                            }
                            return View(pLG_USER_SETUP_PROFILE_MAP);
                        }

                        if (result == "True")
                        {
                            TempData["Success"] = "Saved Successfully.";

                            //try
                            //{
                            //   //profile_photo.imageByte = ReadThumbPic();
                            //}
                            //catch()
                            //{
                            //}

                            return RedirectToAction("Create");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Save.";
                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_CLASSIFICATION_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_AREA_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD = (List<SelectListItem>)TempData["LIST_BRANCH_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_AUTHENTICATION_TYPE_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = (List<SelectListItem>)TempData["LIST_WORKING_HOUR_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_TWO_FA_TYPE_FOR_DD"];
                            pLG_USER_SETUP_PROFILE_MAP.LIST_USER_FILE_TYPE = (List<SelectListItem>)TempData["LIST_USER_FILE_TYPE"];

                            if (pLG_USER_SETUP_PROFILE_MAP.WORKING_HOUR == "Fixed")
                            {
                                pLG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = (List<SelectListItem>)TempData["LIST_WORKING_HOUR_FOR_DD"];
                            }
                            return View(pLG_USER_SETUP_PROFILE_MAP);
                        }
                    }
                    else
                    {
                        pLG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_CLASSIFICATION_FOR_DD"];
                        pLG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_AREA_FOR_DD"];
                        pLG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD = (List<SelectListItem>)TempData["LIST_BRANCH_FOR_DD"];
                        pLG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_AUTHENTICATION_TYPE_FOR_DD"];
                        pLG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = (List<SelectListItem>)TempData["LIST_WORKING_HOUR_FOR_DD"];
                        pLG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_TWO_FA_TYPE_FOR_DD"];
                        pLG_USER_SETUP_PROFILE_MAP.LIST_USER_FILE_TYPE = (List<SelectListItem>)TempData["LIST_USER_FILE_TYPE"];

                        if (pLG_USER_SETUP_PROFILE_MAP.WORKING_HOUR == "Fixed")
                        {
                        }

                        return View(pLG_USER_SETUP_PROFILE_MAP);
                    }
                }
                else
                {
                    pLG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_CLASSIFICATION_FOR_DD"];
                    pLG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD = (List<SelectListItem>)TempData["LIST_USER_AREA_FOR_DD"];
                    pLG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD = (List<SelectListItem>)TempData["LIST_BRANCH_FOR_DD"];
                    pLG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_AUTHENTICATION_TYPE_FOR_DD"];
                    pLG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = (List<SelectListItem>)TempData["LIST_WORKING_HOUR_FOR_DD"];
                    pLG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD = (List<SelectListItem>)TempData["LIST_TWO_FA_TYPE_FOR_DD"];
                    pLG_USER_SETUP_PROFILE_MAP.LIST_USER_FILE_TYPE = (List<SelectListItem>)TempData["LIST_USER_FILE_TYPE"];
                    return View(pLG_USER_SETUP_PROFILE_MAP);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't save application. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't Save User Profile.";
                }

                string exception = ex.Message.ToString();

                return RedirectToAction("Create");
            }
        }

        [RBAC]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Index");
            }

            string url = Utility.AppSetting.getLgardaServer() + "/Get_User_Classification_For_DD" + "?format=json";
            string url1 = Utility.AppSetting.getLgardaServer() + "/Get_All_User_AreaId_For_DD" + "?format=json";
            string url2 = Utility.AppSetting.getLgardaServer() + "/Get_Branch_For_DD" + "?format=json";
            string url3 = Utility.AppSetting.getLgardaServer() + "/Get_Authentication_Type_For_DD" + "?format=json";
            string url4 = Utility.AppSetting.getLgardaServer() + "/Get_Work_Hour_Type_For_DD" + "?format=json";
            string url5 = Utility.AppSetting.getLgardaServer() + "/Get_Two_FA_type_For_DD" + "?format=json";

            OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_CLASSIFICATION_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_USER_AREA_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url1);
            OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_BRANCH_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url2);
            OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_AUTHENTICATION_TYPE_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url3);
            OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_WORKING_HOUR_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url4);
            OBJ_LG_USER_SETUP_PROFILE_MAP.LIST_TWO_FA_TYPE_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url5);

            //LgardaServiceRef.LgardaServiceClient Obj_Service = new LgardaServiceRef.LgardaServiceClient();
            //LgardaServiceRef.Lg_Sys_User_Setup_Profile[] Obj_Lg_Sys_User_Setup_Profile_Arr = Obj_Service.GetAllUserSetupInfo();

            //for (int i = 0; i < Obj_Lg_Sys_User_Setup_Profile_Arr.Length; i++)
            //{
            //    LgardaServiceRef.Lg_Sys_User_Setup_Profile Obj_Lg_Sys_User_Setup_Profile = new LgardaServiceRef.Lg_Sys_User_Setup_Profile();
            //    Obj_Lg_Sys_User_Setup_Profile = (LgardaServiceRef.Lg_Sys_User_Setup_Profile)Obj_Lg_Sys_User_Setup_Profile_Arr[i];
            //    if (id == Convert.ToInt32(Obj_Lg_Sys_User_Setup_Profile.USER_ID))
            //    {
            //        Obj_Lg_Sys_User_Setup_Profile.LIST_USER_CLASSIFICATION_FOR_DD = Obj_Service.GetUserClassificationForDD();
            //        Obj_Lg_Sys_User_Setup_Profile.LIST_USER_AREA_FOR_DD = Obj_Service.GetAllUserAreaForDD();
            //        Obj_Lg_Sys_User_Setup_Profile.LIST_BRANCH_FOR_DD = Obj_Service.GetBranchForDD();
            //        Obj_Lg_Sys_User_Setup_Profile.LIST_AUTHENTICATION_TYPE_FOR_DD = Obj_Service.GetAuthenticationTypeForDD();
            //        Obj_Lg_Sys_User_Setup_Profile.LIST_WORKING_HOUR_FOR_DD = Obj_Service.GetWorkHourTypeForDD();
            //        Obj_Lg_Sys_User_Setup_Profile.LIST_TWO_FA_TYPE_FOR_DD = Obj_Service.GetTwoFAtypeForDD();
            ViewBag.Color = TempData["Color"];
            ViewBag.Message = TempData["Message"];
            return View(OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        //[HttpPost]
        //public ActionResult Edit(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP, string command)
        //{
        //    try
        //    {
        //        if (command == "Save")
        //        {
        //            LgardaServiceRef.LgardaServiceClient Obj_Service = new LgardaServiceRef.LgardaServiceClient();
        //            pLg_Sys_Mail_Server_Config.MAKE_BY = "admin";
        //            //LgardaServiceRef.Bool_Check Obj_Bool_Check = Obj_Service.UpdateMailServerConfig(pLg_Sys_Mail_Server_Config);
        //            LgardaServiceRef.Bool_Check Obj_Bool_Check = null;
        //            if (Obj_Bool_Check.CHECK)
        //            {
        //                TempData["Color"] = "green";
        //                TempData["Message"] = "Updated Successfully.";
        //            }
        //            else
        //            {
        //                TempData["Color"] = "red";
        //                TempData["Message"] = "Can't Update.";
        //            }
        //            return RedirectToAction("Edit");
        //        }
        //        if (btnsubmit == "Refresh")
        //        {
        //            return RedirectToAction("Edit");
        //        }
        //        if (btnsubmit == "Back")
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Service"))     //it'll be removed when angularjs validation is added for client side validation.
        //        {
        //            TempData["Color"] = "red";
        //            TempData["Message"] = ex.Message;
        //            return RedirectToAction("Edit");
        //        }
        //        else
        //        {
        //            TempData["Color"] = "red";
        //            TempData["Message"] = "Can't Update..";
        //            return RedirectToAction("Edit");
        //        }
        //    }
        //}

        [RBAC]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserProfile_ByUserId/" + id + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show application details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show application details.";
                }
            }
            return View(OBJ_LG_USER_SETUP_PROFILE_MAP);
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
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_PasswordPolicy/" + id + "?format=json";
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
                    TempData["Error"] = "Can't delete password policy. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't delete password policy.";
                }
                return View();
            }
        }

        /*
         private IEnumerable<int> GetDays()
         {
             List<int> days = new List<int>();
             for (int i = 0; i < 32; i++)
             {
                 int day = i + 1;
                 days.Add(day);
             }
             return days;
         }
          * */
    }
}