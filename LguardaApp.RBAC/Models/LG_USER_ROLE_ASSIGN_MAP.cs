using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LguardaApp.RBAC.Models
{
    public class LG_USER_ROLE_ASSIGN_MAP
    {
        #region Properties
        public int SL_NO { get; set; }
        public string USER_ID { get; set; }
        public string ROLE_ID { get; set; }
        public string ROLE_NAME { get; set; }
        public string MAKE_BY { get; set; }
        public DateTime MAKE_DT { get; set; }
        public string APPLICATION_ID { get; set; }
        public string ERROR { get; set; }
        public string ROLE_ID_FOR_IND_USER { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }
        public string USER_NM { get; set; }
        public string APPLICATION_NAME { get; set; }
        public string ROLE_ASSIGN_COMMAND { get; set; }

        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        public List<SelectListItem> ROLE_LIST_FOR_IND_USER { get; set; }
        #endregion
    }
}
