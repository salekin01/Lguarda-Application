using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgurdaApp.Model.ControllerModels
{
   public class LG_SYS_HOLIDAY_TYPE_MAP
    {
        #region Properties


        public int HOLIDAY_TYPE_ID { get; set; }

        public string HOLIDAY_TYPE_NM { get; set; }

        public short WEEKEND { get; set; }
        public bool WEEKEND_B { get; set; }

       
        public string MAKE_BY { get; set; }

        public Nullable<System.DateTime> MAKE_DT { get; set; }

        public string AUTH_STATUS_ID { get; set; }

        public string LAST_ACTION { get; set; }

        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }

        public string WEEKEND_TEXT { get; set; }
        #endregion



    }
}
