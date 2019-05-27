using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace LgurdaApp.Model.ControllerModels
{
  
      public class LG_2FA_OTP_CONFIG_MAP
    {
        
        [Required(ErrorMessage = "Application ID is required")]
        public string APPLICATION_ID { get; set; }
        
        public int MAIL_FLAG { get; set; }
        
        public int SMS_FLAG { get; set; }
        
        [Required(ErrorMessage = "Validity Period is required")]
        //[Range(1, 3, ErrorMessage = "Enter a value between 1 to 3")]  it's workable, commented for demo purpose.
        public string VALIDITY_PERIOD { get; set; }
        
        [Required(ErrorMessage = "No of OTP Digits is required")]
        [Range(6, 9, ErrorMessage = "Enter a value between 6 to 9")]
        public string NO_OF_OTP_DIGIT { get; set; }
        
        [Required(ErrorMessage = "OTP Formate is required")]
        public string OTP_FORMAT_ID { get; set; }
        
        public string MAKE_BY { get; set; }
        
        public System.DateTime MAKE_DT { get; set; }
        
        public string OTP_ID { get; set; }
        
        public string ERROR { get; set; }



        
        public string APPLICATION_NAME { get; set; }
        
        public bool MAIL_FLAG_B { get; set; }
        
        public bool SMS_FLAG_B { get; set; }
        
        public List<SelectListItem> OTP_FORMATE_LIST_FOR_DD { get; set; }
        
        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }

        public string AUTH_STATUS_ID { get; set; }
    }
}


