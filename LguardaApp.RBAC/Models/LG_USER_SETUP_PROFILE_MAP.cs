using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LguardaApp.RBAC.Models
{
    public class LG_USER_SETUP_PROFILE_MAP
    {
        #region Properties

        public string USER_ID { get; set; }
        public string USER_CLASSIFICATION_ID { get; set; }
        public string USER_AREA_ID { get; set; }
        public string USER_AREA_ID_VALUE { get; set; }
        public string USER_NAME { get; set; }
        public string USER_DESCRIPTION { get; set; }
        public string BRANCH_ID { get; set; }
        public string ACC_NO { get; set; }
        public string FATHERS_NAME { get; set; }
        public string MOTHERS_NAME { get; set; }
        public string DOB { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string MOB_NO { get; set; }
        public string AUTHENTICATION_ID { get; set; }
        public string TERMINAL_IP { get; set; }
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





        //public int? DAY { get; set; }

        //public string MONTH { get; set; }

        //public int? YEAR { get; set; }

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

        public int ACTIVE_FLAG_FOR_INACTIVE_USER { get; set; }

        public int LOCK_FLAG_FOR_WRONG_ATTM { get; set; }

        public int ACTIVE_FLAG_FOR_MULTI_LOGIN { get; set; }

        public string USER_SESSION_ID { get; set; }

        public string USER_START_TIME { get; set; }

        public string USER_LAST_TIME { get; set; }

        public int FIRST_LOGIN_FLAG { get; set; }

        public string IP_ADDRESS { get; set; }

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
        public List<LG_MENU_MAP> LIST_MENU_MAP { get; set; }

        public string FAILED_LOGIN_ATTEMPT { get; set; }
        #endregion
    }
}
