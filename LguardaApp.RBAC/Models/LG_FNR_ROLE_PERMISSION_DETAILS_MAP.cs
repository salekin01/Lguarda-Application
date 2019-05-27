using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LguardaApp.RBAC.Models
{
    public class LG_FNR_ROLE_PERMISSION_DETAILS_MAP
    {
        #region Properties
        public string PERMISSION_ID { get; set; }
        public string PERMISSION_DETAILS { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }
        public DateTime MAKE_DT { get; set; }
        #endregion
    }
}
