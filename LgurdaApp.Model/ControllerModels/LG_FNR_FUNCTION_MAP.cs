using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Foolproof;
using DataAnnotationsExtensions;


namespace LgurdaApp.Model.ControllerModels
{

    public class LG_FNR_FUNCTION_MAP
    {       
        public string FUNCTION_ID { get; set; }       
        [Required(ErrorMessage = "Function Name is required")]
        public string FUNCTION_NM { get; set; }
        [Required(ErrorMessage = "Service ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Service ID is required")]
        public string SERVICE_ID { get; set; }       
        [Required(ErrorMessage = "Module ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Module ID is required")]
        public string MODULE_ID { get; set; }

        public int MAINT_CRT_FLAG { get; set; }
        public int MAINT_EDT_FLAG { get; set; }
        public int MAINT_DEL_FLAG { get; set; }
        public int MAINT_DTL_FLAG { get; set; }
        public int MAINT_INDX_FLAG { get; set; }
        public int? MAINT_AUTH_FLAG { get; set; }

        public int MAINT_OTP_FLAG { get; set; }
        public int? MAINT_BIO_FLAG { get; set; }
        public int MAINT_2FA_FLAG { get; set; }
        public int MAINT_2FA_HARD_FLAG { get; set; }
        public int MAINT_2FA_SOFT_FLAG { get; set; }
        
        public int REPORT_VIEW_FLAG { get; set; }       
        public int REPORT_PRINT_FLAG { get; set; }    
        public int REPORT_GEN_FLAG { get; set; }

        public short? PROCESS_FLAG { get; set; }
        public short? ENABLED_FLAG { get; set; }
        public short? HO_FUNCTION_FLAG { get; set; }

        public string FAST_PATH_NO { get; set; }
        public string TARGET_PATH { get; set; }
        public string DB_ROLE_NAME { get; set; }
   
        public string MAKE_BY { get; set; }       
        public DateTime MAKE_DT { get; set; }       
        [Required(ErrorMessage = "Item Type is required")]
        [RegularExpression("^(F|R)$", ErrorMessage = "Item Type is required")]
        public string ITEM_TYPE { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        [Required(ErrorMessage = "Application ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Application ID is required")]
        public string APPLICATION_ID { get; set; }       
        public string APPLICATION_NAME { get; set; }       
        public string MODULE_NM { get; set; }        
        public string SERVICE_NM { get; set; }             
        public string ERROR { get; set; }
        [RequiredIf("MAINT_AUTH_FLAG_B", true, ErrorMessage = "Authorize level is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Authorize level must be greater than 0")]
        [Integer(ErrorMessage = "Please enter valid Number")]
        public string AUTH_LEVEL { get; set; }
        public string LAST_ACTION { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }
        public int SELECTED_FLAG { get; set; }

        public IEnumerable<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> SERVICE_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> MODULE_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> FUNCTION_GROUP_LIST_FOR_DD { get; set; }
        public IEnumerable<SelectListItem> ITEM_TYPE_LIST_FOR_DD { get; set; }

        //additional parameter for Boolen
                
        public bool MAINT_CRT_FLAG_B { get; set; }        
        public bool MAINT_EDT_FLAG_B { get; set; }       
        public bool MAINT_DEL_FLAG_B { get; set; }
        public bool MAINT_DTL_FLAG_B { get; set; }      
        public bool MAINT_INDX_FLAG_B { get; set; }      
        public bool MAINT_AUTH_FLAG_B { get; set; }

        public bool MAINT_OTP_FLAG_B { get; set; }
        public bool MAINT_BIO_FLAG_B { get; set; }
        public bool MAINT_2FA_FLAG_B { get; set; }
        public bool MAINT_2FA_HARD_FLAG_B { get; set; }
        public bool MAINT_2FA_SOFT_FLAG_B { get; set; }
     
        public bool REPORT_VIEW_FLAG_B { get; set; }    
        public bool REPORT_PRINT_FLAG_B { get; set; }       
        public bool REPORT_GEN_FLAG_B { get; set; }
        public bool SELECTED_FLAG_B { get; set; }

        public bool PROCESS_FLAG_B { get; set; }
        public bool ENABLED_FLAG_B { get; set; }
        public bool HO_FUNCTION_FLAG_B { get; set; }

        public int? APP_TYPE_ID { get; set; }
    }

}
