using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_USER_AD_BINDING_MAP
    {

        #region Properties
                
        public int SL { get; set; }
                
        public string USER_ID { get; set; }
                
        public string DOMAIN_ID { get; set; }
                
        public string DOMAIN { get; set; }
               
        public int AD_ACTIVE_FLAG { get; set; }

        public bool AD_ACTIVE_FLAG_B { get; set; }
              
        public string AUTH_STATUS_ID { get; set; }
                
        public string LAST_ACTION { get; set; }
               
        public DateTime? LAST_UPDATE_DT { get; set; }
              
        public DateTime? MAKE_DT { get; set; }

        #endregion


    }
}
