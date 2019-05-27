using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LgurdaApp.Model.ControllerModels
{
     public  class LG_SYS_HOLIDAY_MARKING_MAP
    {
     
        public short SL_NO { get; set; }
       
        public short CLD_TYPE_ID { get; set; }
       
        public short HOLIDAY_ID { get; set; }
       
        public DateTime? DATE_FROM { get; set; }
       
        public DateTime? DATE_TO { get; set; }
       
        public string MAKE_BY { get; set; }
       
       public DateTime? MAKE_DT { get; set; }
      
        public string AUTH_STATUS_ID { get; set; }
       
        public string LAST_ACTION { get; set; }
       
        public DateTime? LAST_UPDATE_DT { get; set; }

        public List<SelectListItem> CALENDAR_LIST_FOR_DD { get; set; }
        public List<SelectListItem> HOLIDAY_LIST_FOR_DD { get; set; }



    }
}

