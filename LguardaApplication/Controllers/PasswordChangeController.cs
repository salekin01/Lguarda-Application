using LguardaApp.RBAC.Action_Filters;
using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Controllers
{
    public class PasswordChangeController : Controller
    {
        #region Class Level Variable

        private List<LG_CRD_PASSWORD_CHANGE_MAP> LIST_LG_CRD_PASSWORD_CHANGE_MAP = new List<LG_CRD_PASSWORD_CHANGE_MAP>();
        private LG_CRD_PASSWORD_CHANGE_MAP OBJ_LG_CRD_PASSWORD_CHANGE_MAP = new LG_CRD_PASSWORD_CHANGE_MAP();
        private LG_CRD_PASSWORD_POLICY_MAP OBJ_LG_CRD_PASSWORD_POLICY_MAP = new LG_CRD_PASSWORD_POLICY_MAP();
        private LG_USER_SETUP_PROFILE_MAP OBJ_LG_USER_SETUP_PROFILE_MAP = new LG_USER_SETUP_PROFILE_MAP();

        #endregion Class Level Variable

        //[RBAC]
        public ActionResult Index()  //Previous: ChangePassword(), salekin change the name
        {
            //return View("ChangePassword");
            return View();
        }

        //[RBAC]
        [HttpPost]
        public ActionResult Index(LG_CRD_PASSWORD_CHANGE_MAP pLG_CRD_PASSWORD_CHANGE_MAP)  //Previous: ChangePassword(), salekin change the name
        {
            if (ModelState.IsValid)
            {
                if (pLG_CRD_PASSWORD_CHANGE_MAP.NEW_PASSWORD == pLG_CRD_PASSWORD_CHANGE_MAP.CONFIRM_NEW_PASSWORD)
                {
                    if (pLG_CRD_PASSWORD_CHANGE_MAP.CURRENT_PASSWORD != pLG_CRD_PASSWORD_CHANGE_MAP.NEW_PASSWORD)
                    {
                        try
                        {
                            string result = string.Empty;

                            string userId = "";

                            if (Session["currentUserID"] != null)
                            {
                                userId = Session["currentUserID"].ToString();
                            }

                            // static application_id
                            string applicationId = "01";

                            // password should be encrypted before sending over http request

                            string url = Utility.AppSetting.getLgardaServer() + "/Change_Password/" + applicationId + "/" + userId + "/" + pLG_CRD_PASSWORD_CHANGE_MAP.NEW_PASSWORD + "/" + pLG_CRD_PASSWORD_CHANGE_MAP.CURRENT_PASSWORD + "?format=json";
                            result = HttpWcfRequest.PostParameter(url);

                            if (result == "True")
                            {
                                ViewData["Success"] = "Password Changed Successfully. U'll be logged out within a moment.";
                                ViewData["Result"] = 1;
                            }
                            else
                            {
                                ViewData["Error"] = result;
                            }

                            //return RedirectToAction("ChangePassword");
                            return View();
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("Service"))
                            {
                                ViewData["Error"] = "Can't change password. Service is unable to process the request.";
                            }
                            else
                            {
                                ViewData["Error"] = "Can't change password.";
                            }
                            //return View("ChangePassword");
                            return View();
                        }
                    }
                    else
                    {
                        ViewData["Error"] = "New Password can't be same as current password.";
                        //return View("ChangePassword");
                        return View();
                    }
                }
                else
                {
                    ViewData["Error"] = "New Password Mismatched.";
                    //return View("ChangePassword");
                    return View();
                }
            }
            else
            {
                ViewData["Error"] = "Can't change password.";
                //return View("ChangePassword");
                return View();
            }
        }

        //public ActionResult ResetPassword()
        //{
        //    return View();
        //}

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailById(string id)
        {
            if (id == "")
            {
                return RedirectToAction("ResetPassword");
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
                    ViewData["Error"] = "Can't show user details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show user details.";
                }
            }

            return PartialView("Partial1", OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailByName(string name)
        {
            if (name == "")
            {
                return RedirectToAction("ResetPassword");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserName/" + name + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show user details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show user details.";
                }
            }

            return PartialView("Partial1", OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        [HttpGet]
        [OutputCache(Duration = 0)]
        public ActionResult FindUserDetailByAccountNo(string account_number)
        {
            if (account_number == "")
            {
                return RedirectToAction("ResetPassword");
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_UserSetupInfoByUserAccountNo/" + account_number + "?format=json";
                OBJ_LG_USER_SETUP_PROFILE_MAP = HttpWcfRequest.GetObject<LG_USER_SETUP_PROFILE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show user details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show user details.";
                }
            }

            return PartialView("Partial1", OBJ_LG_USER_SETUP_PROFILE_MAP);
        }

        //public ActionResult Reset(string pUSER_ID)
        //{
        //    string application_Id = "01";
        //    string result = string.Empty;

        //    try
        //    {
        //        string userId = "";

        //        if (Session["currentUserID"] != null)
        //        {
        //            userId = Session["currentUserID"].ToString();
        //        }

        //        string url = Utility.AppSetting.getLgardaServer() + "/Reset_Password/" + userId + "/" + pUSER_ID + "/" + application_Id + "?format=json";
        //        result = HttpWcfRequest.PostParameter(url);

        //        if (result == "True")
        //        {
        //            TempData["Success"] = "Password Reset Successful.";
        //        }
        //        else
        //        {
        //            TempData["Error"] = result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Service"))
        //        {
        //            TempData["Error"] = "Can't reset password. Service is unable to process the request.";
        //        }
        //        else
        //        {
        //            TempData["Error"] = "Can't reset password.";
        //        }
        //    }

        //    return View("ResetPassword");
        //}

        public string PasswordStrengthCheck(string pPASSWORD)
        {
            // hard coded app id

            string application_id = "01";

            #region FetchSomePasswordProperty

            string passwordString = pPASSWORD;

            byte[] pass_byte = Encoding.ASCII.GetBytes(passwordString);

            int numberOfUpperCaseAlphabet = 0;
            int numberOfLowerCaseAlphabet = 0;
            int numberOfNumericCharacter = 0;
            int lengthOfString = passwordString.Length;
            int numberOfNonAlphaNumericCharacter = 0;

            foreach (int val in pass_byte)
            {
                int flag = 0;
                if (val > 64 && val < 91) { numberOfUpperCaseAlphabet++; flag = 1; }
                if (val > 96 && val < 123) { numberOfLowerCaseAlphabet++; flag = 1; }
                if (val > 47 && val < 58) { numberOfNumericCharacter++; flag = 1; }
                if (flag == 0) { numberOfNonAlphaNumericCharacter++; }
            }

            #endregion FetchSomePasswordProperty

            try
            {
                int score = 0;

                string url = Utility.AppSetting.getLgardaServer() + "/Get_Password_Policy_ByAppId/" + application_id + "?format=json";
                OBJ_LG_CRD_PASSWORD_POLICY_MAP = HttpWcfRequest.GetObject<LG_CRD_PASSWORD_POLICY_MAP>(url);

                if (OBJ_LG_CRD_PASSWORD_POLICY_MAP != null)
                {
                    int policy_max_length = Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_MAX_LENGTH);
                    int policy_min_length = Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY_MAP.PASS_MIN_LENGTH);
                    int policy_min_caps_letter = Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_CAPS_LETTER);
                    int policy_min_small_letter = Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_SMALL_LETTER);
                    int policy_min_numeric_char = Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY_MAP.MIN_NUMERIC_CHAR);
                    int policy_min_non_alpha_numeric_char = Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY_MAP.NUMERIC_CHAR_MIN);

                    if (policy_min_length <= lengthOfString)
                    {
                        if ((lengthOfString == policy_min_length) || (lengthOfString <= (policy_min_length + 1)))
                        {
                            score = score + 1;
                        }
                        else
                        {
                            score = score + 2;
                        }
                    }

                    if (policy_min_caps_letter <= numberOfUpperCaseAlphabet)
                    {
                        if ((numberOfUpperCaseAlphabet == policy_min_caps_letter) || (numberOfUpperCaseAlphabet <= (policy_min_caps_letter + 1)))
                        {
                            score = score + 1;
                        }
                        else
                        {
                            score = score + 2;
                        }
                    }

                    if (policy_min_small_letter <= numberOfLowerCaseAlphabet)
                    {
                        if ((numberOfLowerCaseAlphabet == policy_min_small_letter) || (numberOfLowerCaseAlphabet <= (policy_min_small_letter + 1)))
                        {
                            score = score + 1;
                        }
                        else
                        {
                            score = score + 2;
                        }
                    }

                    if (policy_min_numeric_char <= numberOfNumericCharacter)
                    {
                        if ((numberOfNumericCharacter == policy_min_numeric_char) || (numberOfNumericCharacter <= (policy_min_numeric_char + 1)))
                        {
                            score = score + 1;
                        }
                        else
                        {
                            score = score + 2;
                        }
                    }

                    if (policy_min_non_alpha_numeric_char <= numberOfNonAlphaNumericCharacter)
                    {
                        if ((numberOfNonAlphaNumericCharacter == policy_min_non_alpha_numeric_char) || (numberOfNonAlphaNumericCharacter <= (policy_min_non_alpha_numeric_char + 1)))
                        {
                            score = score + 1;
                        }
                        else
                        {
                            score = score + 2;
                        }
                    }

                    switch (score)
                    {
                        case 0: return "Very Weak";
                        case 1: return "Very Weak";
                        case 2: return "Very Weak";
                        case 3: return "Weak";
                        case 4: return "Weak";
                        case 5: return "Medium";
                        case 6: return "Medium";
                        case 7: return "Strong";
                        case 8: return "Strong";
                        case 9: return "Very Strong";
                        case 10: return "Very Strong";

                        default: return "";
                    }
                }
                else
                {
                    ViewData["Error"] = "No Policy Found!!";
                    return "No_Policy";
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show password strength. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show password strength.";
                }

                return ex.ToString();
            }
        }
    }
}