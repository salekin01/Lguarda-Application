using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_USER_SETUP_PROFILE_MAP
    {
        #region Properties

        [MaxLength(15, ErrorMessage = "Maximum User ID length should be within 15 characters")]
        [Required(ErrorMessage = "User ID  required")]
        public string USER_ID { get; set; }

        [Required(ErrorMessage = "Classification ID is required")]
        public string USER_CLASSIFICATION_ID { get; set; }

        [Required(ErrorMessage = "Area ID is required")]
        public string USER_AREA_ID { get; set; }

        [Required(ErrorMessage = "Area wise ID is required")]
        [MaxLength(15, ErrorMessage = "ID must be within 15 digits")]
        public string USER_AREA_ID_VALUE { get; set; }

        [MaxLength(255, ErrorMessage = "User Name Maximum length should be within 255 characters")]
        [Required(ErrorMessage = "User Name ID is required")]
        public string USER_NAME { get; set; }

        [MaxLength(255, ErrorMessage = "User Name Maximum length should be within 255 characters")]
        public string USER_DESCRIPTION { get; set; }

        [Required(ErrorMessage = "Branch ID is required")]
        public string BRANCH_ID { get; set; }

        //[Required(ErrorMessage = "Acc No is required")]
        //[RegularExpression(@"^\d{11}$", ErrorMessage = "Acc No must contain 11 digits.")]
        [MaxLength(15, ErrorMessage = "Acc. No must be within 15 digits")]
        public string ACC_NO { get; set; }

        public string FATHERS_NAME { get; set; }

        public string MOTHERS_NAME { get; set; }

        public string DOB { get; set; }

        [Required(ErrorMessage = "Email ID is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not in a valid format.")]
        public string MAIL_ADDRESS { get; set; }

        [RegularExpression(@"^(01[1-9]\d{8})$", ErrorMessage = "Mob No must contain 11 digits, starting with 01")]
        [Required(ErrorMessage = "Mob No is required")]
        public string MOB_NO { get; set; }

        [Required(ErrorMessage = "Authentication ID is required")]
        public string AUTHENTICATION_ID { get; set; }

        public string TERMINAL_IP { get; set; }

        [Required(ErrorMessage = "Working Hour Type ID is required")]
        public string WORKING_HOUR { get; set; }

        public string START_TIME { get; set; }

        public string END_TIME { get; set; }

        public string MAKE_BY { get; set; }

        //[DataMember]
        //public DateTime MAKE_DT { get; set; }

        public string PASSWORD { get; set; }

        public string APPLICATION_ID { get; set; }

        public string APPLICATION_NAME { get; set; }

        public string CLASSIFICATION_NAME { get; set; }

        public string AREA_NAME { get; set; }

        public string BRANCH_NAME { get; set; }

        // for image
        //public HttpPostedFileBase File { get; set; }

        //public byte[] imageByte { get; set; }

        public int? HR { get; set; }

        public string MIN { get; set; }

        public int? AMPM { get; set; }

        public int? E_HR { get; set; }

        public string E_MIN { get; set; }

        public int? E_AMPM { get; set; }

        public int day { get; set; }
        public string month { get; set; }

        public IEnumerable<SelectListItem> Months
        {
            get
            {
                return DateTimeFormatInfo
                       .InvariantInfo
                       .MonthNames
                       .Select((monthName, index) => new SelectListItem
                       {
                           Value = monthName,
                           // (index + 1).ToString(),
                           Text = monthName
                       });
            }
        }

        public int year { get; set; }

        public string AUTHENTICATION_NAME { get; set; }

        public string ERROR { get; set; }

        public string TWO_FA_TYPE_ID { get; set; }

        public string TWO_FA_TYPE_NAME { get; set; }

        public string AUTH_STATU_ID { get; set; }

        public string LAST_ACTION { get; set; }

        // for login validation

        public short ACTIVE_FLAG_INACTV_USER { get; set; }

        public short USER_ID_LOCK_WRNG_ATM { get; set; }

        public short ACTIVE_FLAG_MULTI_LOGIN { get; set; }
        public short LOCK_FLAG_FOR_WRONG_ATTM { get; set; }

        public string USER_SESSION_ID { get; set; }

        public DateTime? USER_START_TIME { get; set; }

        public DateTime? USER_LAST_TIME { get; set; }

        public short FIRST_LOGIN_FLAG { get; set; }

        public string IP_ADDRESS { get; set; }

        public string USER_LOGIN { get; set; }
        //public string APPLICATION_ID { get; set; }

        // END for login validation
        public List<SelectListItem> LIST_USER_CLASSIFICATION_FOR_DD { get; set; }

        public List<SelectListItem> LIST_USER_AREA_FOR_DD { get; set; }

        public List<SelectListItem> LIST_BRANCH_FOR_DD { get; set; }

        public List<SelectListItem> LIST_AUTHENTICATION_TYPE_FOR_DD { get; set; }

        public List<SelectListItem> LIST_WORKING_HOUR_FOR_DD { get; set; }

        public List<SelectListItem> LIST_TWO_FA_TYPE_FOR_DD { get; set; }

        //salekin added bellow
        public ICollection<LG_USER_ROLE_ASSIGN_MAP> ROLES { get; set; }

        public List<LG_FNR_ROLE_PERMISSION_DETAILS_MAP> PERMISSIONS { get; set; }
        public List<LG_FNR_MODULE_MAP> LIST_MODULES_FOR_SELECTED_ROLE { get; set; }

        public IEnumerable<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }

        public List<LG_MENU_MAP> LIST_MENU_MAP { get; set; }
        public string POLICY_ID { get; set; }

        //file upload khaled

        public List<SelectListItem> LIST_USER_FILE_TYPE { get; set; }

        public string FilesToBeUploaded { get; set; }
        public string fileToUpload { get; set; }

        [Display(Name = "FILE TYPE")]
        public short? FILE_TYPE { get; set; }

        public HttpPostedFileBase File { get; set; }
        public List<FILE_INFO> LIST_FILE_INFO { get; set; }
        public byte[] imageByte { get; set; }

        public class FILE_INFO
        {
            public string FILE_NAME { get; set; }
            public string CONTENT_TYPE { get; set; }
            public byte[] FILE_ImageByte { get; set; }
            public short FILE_TYPE { get; set; }
        }
        public int MyProperty { get; set; }
        public string MSG { get; set; }        
        public string LAST_TIME { get; set; }

        public string FAILED_LOGIN_ATTEMPT { get; set; }

        //file upload

        #endregion Properties

        /*

        #region Properties

        [MaxLength(15, ErrorMessage = "Maximum User ID length should be within 15 characters")]
        [Required(ErrorMessage = "User ID  required")]
        public string USER_ID { get; set; }

        [Required(ErrorMessage = "Classification ID is required")]
        public string CLASSIFICATION_ID { get; set; }

        [Required(ErrorMessage = "Area ID is required")]
        public string AREA_ID { get; set; }

        [MaxLength(255, ErrorMessage = "User Name Maximum length should be within 255 characters")]
        [Required(ErrorMessage = "User Name ID is required")]
        public string USER_NAME { get; set; }

        [MaxLength(255, ErrorMessage = "User Name Maximum length should be within 255 characters")]
        public string USER_DESCRIPTION { get; set; }

        //[RegularExpression(@"^\d{6}$", ErrorMessage = "Employee ID must be contain 6 digits.")] //6
        [MaxLength(6, ErrorMessage = "Employee ID length must be within 6 digits")]
        public string EMPLOYEE_ID { get; set; }

        [Required(ErrorMessage = "Branch ID is required")]
        public string BRANCH_ID { get; set; }

        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Customer ID must contain 10 digits.")]  //10
        [MaxLength(10, ErrorMessage = "Customer ID length must be within 10 digits")]
        public string CUSTOMER_ID { get; set; }

        //[RegularExpression(@"^\d{5}$", ErrorMessage = "Agent ID must be contain 5 digits.")]  //5
        [MaxLength(5, ErrorMessage = "Agent ID length must be within 5 digits")]
        public string AGENT_ID { get; set; }

        //[Required(ErrorMessage = "Acc No is required")]
        //[RegularExpression(@"^\d{11}$", ErrorMessage = "Acc No must contain 11 digits.")]
        [MaxLength(15, ErrorMessage = "Acc. No must be within 15 digits")]
        public string ACC_NO { get; set; }

        public string FATHERS_NAME { get; set; }

        public string MOTHERS_NAME { get; set; }

        public string DOB { get; set; }

        [Required(ErrorMessage = "Email ID is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not in a valid format.")]
        public string MAIL_ADDRESS { get; set; }

        [RegularExpression(@"^(01[1-9]\d{8})$", ErrorMessage = "Mob No must contain 11 digits, starting with 01")]
        [Required(ErrorMessage = "Mob No is required")]
        public string MOB_NO { get; set; }

        [Required(ErrorMessage = "Authentication ID is required")]
        public string AUTHENTICATION_ID { get; set; }

        public string TERMINAL_IP { get; set; }

        [Required(ErrorMessage = "Working Hour Type ID is required")]
        public string WORKING_HOUR { get; set; }

        public string START_TIME { get; set; }

        public string END_TIME { get; set; }

        public string MAKE_BY { get; set; }

        //[DataMember]
        //public DateTime MAKE_DT { get; set; }

        public string PASSWORD { get; set; }

        public string APPLICATION_ID { get; set; }

        public string APPLICATION_NAME { get; set; }

        public string CLASSIFICATION_NAME { get; set; }

        public string AREA_NAME { get; set; }

        public string BRANCH_NAME { get; set; }

        // for image
        //public HttpPostedFileBase File { get; set; }

        //public byte[] imageByte { get; set; }

        public int? HR { get; set; }

        public string MIN { get; set; }

        public int? AMPM { get; set; }

        public int? E_HR { get; set; }

        public string E_MIN { get; set; }

        public int? E_AMPM { get; set; }

        public int day { get; set; }
        public string month { get; set; }
        public IEnumerable<SelectListItem> Months
        {
            get
            {
                return DateTimeFormatInfo
                       .InvariantInfo
                       .MonthNames
                       .Select((monthName, index) => new SelectListItem
                       {
                           Value = monthName,
                           // (index + 1).ToString(),
                           Text = monthName
                       });
            }
        }

        public int year { get; set; }

        public string AUTHENTICATION_NAME { get; set; }

        public string ERROR { get; set; }

        public string TWO_FA_TYPE_ID { get; set; }

        public string TWO_FA_TYPE_NAME { get; set; }

        public string AUTH_STATU_ID { get; set; }

        public string LAST_ACTION { get; set; }

        // for login validation

        public short ACTIVE_FLAG_INACTV_USER { get; set; }

        public short USER_ID_LOCK_WRNG_ATM { get; set; }

        public short ACTIVE_FLAG_MULTI_LOGIN { get; set; }

        public string USER_SESSION_ID { get; set; }

        public DateTime? USER_START_TIME { get; set; }

        public DateTime? USER_LAST_TIME { get; set; }

        public short FIRST_LOGIN_FLAG { get; set; }

        public string IP_ADDRESS { get; set; }

        public string USER_LOGIN { get; set; }
        //public string APPLICATION_ID { get; set; }

        // END for login validation
        public List<SelectListItem> LIST_USER_CLASSIFICATION_FOR_DD { get; set; }

        public List<SelectListItem> LIST_USER_AREA_FOR_DD { get; set; }

        public List<SelectListItem> LIST_BRANCH_FOR_DD { get; set; }

        public List<SelectListItem> LIST_AUTHENTICATION_TYPE_FOR_DD { get; set; }

        public List<SelectListItem> LIST_WORKING_HOUR_FOR_DD { get; set; }

        public List<SelectListItem> LIST_TWO_FA_TYPE_FOR_DD { get; set; }

        //salekin added bellow
        public ICollection<LG_USER_ROLE_ASSIGN_MAP> ROLES { get; set; }
        public List<LG_FNR_ROLE_PERMISSION_DETAILS_MAP> PERMISSIONS { get; set; }
        public List<LG_FNR_MODULE_MAP> LIST_MODULES_FOR_SELECTED_ROLE { get; set; }

        public IEnumerable<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }

        public List<LG_MENU_MAP> LIST_MENU_MAP { get; set; }
        public string POLICY_ID { get; set; }

        #endregion Properties

        */
    }
}