using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_FNR_MODULE_MAP
    {
        //[Required(ErrorMessage = "Module ID is required.")]
        //[StringLength(4, MinimumLength = 1)]
        public string MODULE_ID { get; set; }
        
        //[Required(ErrorMessage = "Module Name is required")]
        public string MODULE_NM { get; set; }
        
        public string MODULE_SH_NM { get; set; }
  
        //[Range(1, int.MaxValue, ErrorMessage = "Application ID is required.")]
        public string APPLICATION_ID { get; set; }
       
        public string APPLICATION_NAME { get; set; }
        
        [Required(ErrorMessage = "Service ID is required.")]
        [StringLength(2, MinimumLength = 1)]
        public string SERVICE_ID { get; set; }
      
        public string SERVICE_NM { get; set; }
       
        public string ERROR { get; set; }

        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }
        public DateTime? MAKE_DT { get; set; }
        public string MAKE_BY { get; set; }
       
        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; } 
        public List<SelectListItem> SERVICE_LIST_FOR_DD { get; set; }  
    }
}
