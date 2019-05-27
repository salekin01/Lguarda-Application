using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_FNR_ROLE_DEFINE_MAP
    {
        /*
        public string ROLE_ID { get; set; }     
        [Required(ErrorMessage = "Role Name is required")]
        public string ROLE_NM { get; set; }
        [Required(ErrorMessage = "Role Description is required")]
        public string ROLE_DESCRIP { get; set; }    
        public string FUNCTION_ID { get; set; }     
        public string FUNCTION_NM { get; set; }

        public int MAINT_CRT_FLAG { get; set; }
        public int MAINT_EDT_FLAG { get; set; }
        public int MAINT_DEL_FLAG { get; set; }
        public int MAINT_DTL_FLAG { get; set; }
        public int MAINT_INDX_FLAG { get; set; }
        public int MAINT_AUTH_FLAG { get; set; }

        public int MAINT_OTP_FLAG { get; set; }
        public int MAINT_2FA_FLAG { get; set; }
        public int MAINT_2FA_HARD_FLAG { get; set; }
        public int MAINT_2FA_SOFT_FLAG { get; set; }

        public int REPORT_VIEW_FLAG { get; set; }
        public int REPORT_PRINT_FLAG { get; set; }
        public int REPORT_GEN_FLAG { get; set; } 
      
        public string MAKE_BY { get; set; }
        public DateTime? MAKE_DT { get; set; }
        public string FUNCTION_IDs_FOR_IND_ROLE { get; set; }
        public string AUTH_LEVEL { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }

        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }      
        public List<SelectListItem> SERVICE_LIST_FOR_DD { get; set; }     
        public List<SelectListItem> MODULE_LIST_FOR_DD { get; set; }    
        public List<SelectListItem> FUNCTION_GROUP_LIST_FOR_DD { get; set; }    
        public List<SelectListItem> ITEM_TYPE_LIST_FOR_DD { get; set; }     
        public List<SelectListItem> FUNCTION_LIST_FOR_DD { get; set; }    
        public List<SelectListItem> ROLE_LIST { get; set; }
        public List<LG_FNR_ROLE_DEFINE_MAP> LIST_SELECTED_FUNCTION_DETAILS { get; set; }
       
        [Range(1, int.MaxValue, ErrorMessage = "Application ID is required.")]
        public string APPLICATION_ID { get; set; }     
        [Required(ErrorMessage = "Service ID is required")]
        public string SERVICE_ID { get; set; }      
        [Required(ErrorMessage = "Module ID is required")]
        public string MODULE_ID { get; set; }      
        public string FUNCTION_GROUP_ID { get; set; }      
        [Required(ErrorMessage = "Item Type is required")]
        public string ITEM_TYPE { get; set; } 
        public string ERROR { get; set; }      
        public string APPLICATION_NAME { get; set; }      
        public string SERVICE_NM { get; set; }    
        public string MODULE_NM { get; set; }     
        public string FUNCTION_GROUP_NAME { get; set; }
        public string AUTH_STATUS_ID { get; set; }

        public ICollection<LG_FNR_ROLE_PERMISSION_DETAILS_MAP> PERMISSIONS { get; set; }
        public ICollection<LG_USER_SETUP_PROFILE_MAP> USERS { get; set; }

        //additional parameter for Boolen
        public bool MAINT_CRT_FLAG_B { get; set; }
        public bool MAINT_EDT_FLAG_B { get; set; }
        public bool MAINT_DEL_FLAG_B { get; set; }
        public bool MAINT_DTL_FLAG_B { get; set; }
        public bool MAINT_INDX_FLAG_B { get; set; }
        public bool MAINT_AUTH_FLAG_B { get; set; }

        public bool MAINT_OTP_FLAG_B { get; set; }
        public bool MAINT_2FA_FLAG_B { get; set; }
        public bool MAINT_2FA_HARD_FLAG_B { get; set; }
        public bool MAINT_2FA_SOFT_FLAG_B { get; set; }

        public bool REPORT_VIEW_FLAG_B { get; set; }
        public bool REPORT_PRINT_FLAG_B { get; set; }
        public bool REPORT_GEN_FLAG_B { get; set; } */

        public string ROLE_ID { get; set; }
        [Required(ErrorMessage = "Role Name is required")]
        public string ROLE_NM { get; set; }
        public string FUNCTION_ID { get; set; }
        public string FUNCTION_NM { get; set; }
        public short MAINT_CRT_FLAG { get; set; }
        public short MAINT_EDT_FLAG { get; set; }
        public short MAINT_DEL_FLAG { get; set; }
        public short MAINT_DTL_FLAG { get; set; }
        public short MAINT_INDX_FLAG { get; set; }
        public short? MAINT_AUTH_FLAG { get; set; }
        public short? MAINT_OTP_FLAG { get; set; }
        public short? MAINT_2FA_FLAG { get; set; }
        public short? MAINT_2FA_HARD_FLAG { get; set; }
        public short? MAINT_2FA_SOFT_FLAG { get; set; }
        public short REPORT_VIEW_FLAG { get; set; }
        public short REPORT_PRINT_FLAG { get; set; }
        public short REPORT_GEN_FLAG { get; set; }
        public string MAKE_BY { get; set; }
        public DateTime? MAKE_DT { get; set; }
        public string FUNCTION_IDs_FOR_IND_ROLE { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }
        public int? AUTH_LEVEL { get; set; }
        [Required(ErrorMessage = "Role Description is required")]
        public string ROLE_DESCRIP { get; set; }
        public string ROLE_DEFINE_COMMAND { get; set; }


        public IEnumerable<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> SERVICE_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> MODULE_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> FUNCTION_GROUP_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> ITEM_TYPE_LIST_FOR_DD { get; set; }
        public List<LG_FNR_ROLE_DEFINE_MAP> LIST_SELECTED_FUNCTION_DETAILS { get; set; }


        //additional parameter for Boolen
        public bool MAINT_CRT_FLAG_B { get; set; }
        public bool MAINT_EDT_FLAG_B { get; set; }
        public bool MAINT_DEL_FLAG_B { get; set; }
        public bool MAINT_DTL_FLAG_B { get; set; }
        public bool MAINT_INDX_FLAG_B { get; set; }
        public bool MAINT_AUTH_FLAG_B { get; set; }
        public bool MAINT_OTP_FLAG_B { get; set; }
        public bool MAINT_2FA_FLAG_B { get; set; }
        public bool MAINT_2FA_HARD_FLAG_B { get; set; }
        public bool MAINT_2FA_SOFT_FLAG_B { get; set; }
        public bool REPORT_VIEW_FLAG_B { get; set; }
        public bool REPORT_PRINT_FLAG_B { get; set; }
        public bool REPORT_GEN_FLAG_B { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Application ID is required.")]
        public string APPLICATION_ID { get; set; }
        //[Required(ErrorMessage = "Service ID is required")]
        public string SERVICE_ID { get; set; }
        //[Required(ErrorMessage = "Module ID is required")]
        public string MODULE_ID { get; set; }
        public string FUNCTION_GROUP_ID { get; set; }
        //[Required(ErrorMessage = "Item Type is required")]
        public string ITEM_TYPE { get; set; }
        public string ERROR { get; set; }

        public string APPLICATION_NAME { get; set; }
        public string SERVICE_NM { get; set; }
        public string MODULE_NM { get; set; }
        public string FUNCTION_GROUP_NAME { get; set; }

        //public virtual ICollection<LG_FNR_ROLE_PERMISSION_DETAILS_MAP> PERMISSIONS { get; set; }
        //public virtual ICollection<LG_USER_SETUP_PROFILE_MAP> USERS { get; set; }
    }
}
