using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LgurdaApp.Model.ControllerModels
{
  public  class LG_SYS_CLD_TYPE_MAP
    {
        #region Properties

     
        public int CLD_TYPE_ID { get; set; }
       
        public string CLD_TYPE_NM { get; set; }
    
        public short DEFAULT_CLD { get; set; }
        public bool DEFAULT_CLD_B { get; set; }
        
        public string BASED_ON_CLD { get; set; }
     
        public string MAKE_BY { get; set; }
       
        public Nullable<System.DateTime> MAKE_DT { get; set; }
       
        public string AUTH_STATUS_ID { get; set; }
        
        public string LAST_ACTION { get; set; }
      
        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }

        public List<SelectListItem> LIST_OF_ALL_CALENDAR { get; set; }
        #endregion

    }
}
