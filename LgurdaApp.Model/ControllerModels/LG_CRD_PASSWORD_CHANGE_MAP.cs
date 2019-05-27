using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LgurdaApp.Model.ControllerModels
{
    [Serializable]
    public class LG_CRD_PASSWORD_CHANGE_MAP
    {

        #region Properties
        
        public string APPLICATION_ID { get; set; }

        public string USER_ID { get; set; }
        
        public string USER_ACCOUNT_NO { get; set; }

        public string USER_NAME { get; set; }


        [Required(ErrorMessage = "Current Password is required")]
        public string CURRENT_PASSWORD { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NEW_PASSWORD { get; set; }

        [Required(ErrorMessage = "Confirmation of New Password is required")]
        public string CONFIRM_NEW_PASSWORD { get; set; }

        #endregion

        #region Events

        #region Validate

        //public static string ValidatePasswordPolicyOnCreation(string pAPPLICATION_ID, string pPASSWORD)
        //{

        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities();
        //    LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();

        //    string result = string.Empty;

        //    #region FetchSomePasswordProperty

        //    string passwordString = pPASSWORD;

        //    byte[] pass_byte = Encoding.ASCII.GetBytes(passwordString);

        //    int numberOfUpperCaseAlphabet = 0;
        //    int numberOfLowerCaseAlphabet = 0;
        //    int numberOfNumericCharacter = 0;
        //    int lengthOfString = passwordString.Length;
        //    int numberOfNonAlphaNumericCharacter = 0;


        //    foreach (int val in pass_byte)
        //    {
        //        int flag = 0;
        //        if (val > 64 && val < 91) { numberOfUpperCaseAlphabet++; flag = 1; }
        //        if (val > 96 && val < 123) { numberOfLowerCaseAlphabet++; flag = 1; }
        //        if (val > 47 && val < 58) { numberOfNumericCharacter++; flag = 1; }
        //        if (flag == 0) { numberOfNonAlphaNumericCharacter++; }
        //    }

        //    #endregion

        //    try
        //    {
        //        OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
        //                                      where p.APPLICATION_ID == pAPPLICATION_ID
        //                                      select p).First();

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_MAX_LENGTH) < lengthOfString)
        //        {
        //            string template = "Password maximum length should be within {0} characters.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_MAX_LENGTH.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_MIN_LENGTH) > lengthOfString)
        //        {
        //            string template = "Password minimum length should be at least {0} characters.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_MIN_LENGTH.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }


        //        //if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_HIS_PERIOD) < Convert.ToInt16(pPASS_HIS_PERIOD))
        //        //{
        //        //    string template = "Password History Period should be within {0} characters.";
        //        //    string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_HIS_PERIOD.ToString();
        //        //    result = string.Format(template, data);
        //        //    return result;
        //        //}

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_CAPS_LETTER) > numberOfUpperCaseAlphabet)
        //        {
        //            string template = "Password should contain at least {0} capital letters.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_CAPS_LETTER.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_SMALL_LETTER) > numberOfLowerCaseAlphabet)
        //        {
        //            string template = "Password should contain at least {0} small letters.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_SMALL_LETTER.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_NUMERIC_CHAR) > numberOfNumericCharacter)
        //        {
        //            string template = "Password should contain at least {0} numeric characters.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_NUMERIC_CHAR.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.NUMERIC_CHAR_MIN) > numberOfNonAlphaNumericCharacter)
        //        {
        //            string template = "Password should contain at least {0} non alpha numeric characters.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.NUMERIC_CHAR_MIN.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        //if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGED_EXPIRY) < Convert.ToInt16(pPASS_CHANGED_EXPIRY))
        //        //{
        //        //    string template = "(..........................)";
        //        //    string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_CHANGED_EXPIRY.ToString();
        //        //    result = string.Format(template, data);
        //        //    return result;
        //        //}

        //    }

        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                result = validationError.ErrorMessage.ToString();
        //                return result;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //        return result;
        //    }


        //    result = "Valid";
        //    return result;
        //}

        

        //public static string ValidatePasswordPolicyOnLogin(string pUSER_ID, string pAPPLICATION_ID, string pPASSWORD)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities();
        //    LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();

        //    string result = string.Empty;

        //    try
        //    {
        //        OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
        //                                      where p.APPLICATION_ID == pAPPLICATION_ID
        //                                      select p).First();



        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.FAILED_LOGIN_ATTEMT) < Convert.ToInt16(pFAILED_LOGIN_ATTEMT))
        //        {
        //            string template = "After {0} failed attempt you can't log in.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.FAILED_LOGIN_ATTEMT.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_PERIOD) < Convert.ToInt16(pPASS_EXP_PERIOD))
        //        {
        //            string template = "Password is expired.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_PERIOD.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT) < Convert.ToInt16(pPASS_EXP_ALERT))
        //        {
        //            string template = "Password will be expired soon";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_EXP_ALERT.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }


        //    }

        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                result = validationError.ErrorMessage.ToString();
        //                return result;
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //        return result;
        //    }


        //    return result;
        //}


        //public static string ValidatePasswordPolicyOnChangeOrResetPassword(string pUSER_ID, string pAPPLICATION_ID, string pPASSWORD)
        //{
        //    DBModelEntities Obj_DBModelEntities = new DBModelEntities();
        //    LG_CRD_PASSWORD_POLICY OBJ_LG_CRD_PASSWORD_POLICY = new LG_CRD_PASSWORD_POLICY();

        //    string result = string.Empty;

        //    try
        //    {
        //        OBJ_LG_CRD_PASSWORD_POLICY = (from p in Obj_DBModelEntities.LG_CRD_PASSWORD_POLICY
        //                                      where p.APPLICATION_ID == pAPPLICATION_ID
        //                                      select p).First();



        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.MIN_CONS_USE_PASS) < Convert.ToInt16(pMIN_CONS_USE_PASS))
        //        {
        //            string template = "Minimum consequtive of using password should be within {0}";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.MIN_CONS_USE_PASS.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_REPEAT) < Convert.ToInt16(pPASS_REPEAT))
        //        {
        //            string template = "Same password shouldn't be repeated {0} times.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_REPEAT.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }

        //        if (Convert.ToInt16(OBJ_LG_CRD_PASSWORD_POLICY.PASS_REUSE_MAX) < Convert.ToInt16(pPASS_REUSE_MAX))
        //        {
        //            string template = "Same password shouldn't be reused {0} times.";
        //            string data = OBJ_LG_CRD_PASSWORD_POLICY.PASS_REUSE_MAX.ToString();
        //            result = string.Format(template, data);
        //            return result;
        //        }


        //    }

        //    catch (DbEntityValidationException dbEx)
        //    {
        //        foreach (var validationErrors in dbEx.EntityValidationErrors)
        //        {
        //            foreach (var validationError in validationErrors.ValidationErrors)
        //            {
        //                Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
        //                result = validationError.ErrorMessage.ToString();
        //                return result;
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        result = ex.Message;
        //        return result;
        //    }

        //    return result;
        //}



        #endregion


        #endregion




    }
}
