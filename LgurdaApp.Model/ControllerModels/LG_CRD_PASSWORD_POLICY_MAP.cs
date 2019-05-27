using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LgurdaApp.Model.ControllerModels
{
    [Serializable]
    
    public class LG_CRD_PASSWORD_POLICY_MAP
    {

        public string SL_NO { get; set; }

        [Required(ErrorMessage = "One Application Need To Be Selected")]
        public string APPLICATION_ID { get; set; }

        [Required(ErrorMessage = "Max Password Length is required")]
        [MaxLength(30, ErrorMessage = "Max Password Length should be within 30 characters")]
        public string PASS_MAX_LENGTH { get; set; }

        [Required(ErrorMessage = "Min Password Length is required")]
        [MaxLength(10, ErrorMessage = "Min Password length should be within 10 characters")]
        public string PASS_MIN_LENGTH { get; set; }


        [Required(ErrorMessage = "Non Alphanumeric Character(min) is required")]
        [Range(0, 9, ErrorMessage = "Value should be number and single character.")]
        public string NUMERIC_CHAR_MIN { get; set; }

        [Required(ErrorMessage = "Password history period is required")]
        [MaxLength(30, ErrorMessage = "Max Password length should be within 30 characters")]
        public string PASS_HIS_PERIOD { get; set; }
            

        [Required(ErrorMessage = "Password reuse max is required")]
        [Range(0, 9999, ErrorMessage = "Value should be number and within 4 characters.")]
        public string PASS_REUSE_MAX { get; set; }

        [Required(ErrorMessage = "Failed Login Attempt is required")]
        [Range(0, 999, ErrorMessage = "Value should be number and within 3 characters.")]
        public string FAILED_LOGIN_ATTEMT { get; set; }


        [Range(0, 999, ErrorMessage = "Value should be number and within 3 characters.")]
        public string PASS_EXP_PERIOD { get; set; }

        [Range(0, 999, ErrorMessage = "Value should be number and within 3 characters.")]
        public string MIN_CAPS_LETTER { get; set; }
        
        [Range(0, 999, ErrorMessage = "Value should be number and within 3 characters.")]
        public string MIN_SMALL_LETTER { get; set; }

        [Range(0, 999, ErrorMessage = "Value should be number and within 3 characters.")]
        public string MIN_NUMERIC_CHAR { get; set; }

        [MaxLength(15, ErrorMessage = "Min Consicutive Of Using Password should be within 15 characters")]
        public string MIN_CONS_USE_PASS { get; set; }

        [MaxLength(15, ErrorMessage = "Same PAssword Repeat After should be within 15 characters")]
        public string PASS_REPEAT { get; set; }

        [Range(0, 9, ErrorMessage = "Value should be number and single character.")]
        public string PASS_CHANGED_EXPIRY { get; set; }


        public string PASS_EXP_ALERT { get; set; }

       
        [Range(0, 9, ErrorMessage = "Value should be number and single character.")]
        public bool PASS_CHANG_AT_FIRST_LOGIN_B { get; set; }


        public string PASS_CHANG_AT_FIRST_LOGIN { get; set; }

        [Range(0, 9, ErrorMessage = "Value should be number and single character.")]    
        public bool PASS_CHANGE_BY_ADMIN_B { get; set; }


        [Range(0, 999, ErrorMessage = "Value should be number and within 3 characters.")]
        public string PASS_CHANGE_BY_ADMIN { get; set; }

        public string AUTH_STATUS_ID { get; set; }

        public string LAST_ACTION { get; set; }

        [Range(0, 9, ErrorMessage = "Value should be number and single character.")]
        public bool PASS_AUTO_CREATION_B { get; set; }

        public string PASS_AUTO_CREATION { get; set; }

        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }


        public Nullable<System.DateTime> MAKE_DT { get; set; }

        public string ERROR { get; set; }




        public IEnumerable<SelectListItem> LIST_APPLICATION_FOR_DD { get; set; }
    }
}