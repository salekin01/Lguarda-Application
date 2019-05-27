using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LguardaApp.RBAC.Models
{
    public class LG_FNR_MODULE_MAP
    {
        public string MODULE_ID { get; set; }

        public string MODULE_NM { get; set; }

        public string MODULE_SH_NM { get; set; }

        public string MAKE_BY { get; set; }

        public DateTime? MAKE_DT { get; set; }

        public string APPLICATION_ID { get; set; }

        public string APPLICATION_NAME { get; set; }

        public string SERVICE_ID { get; set; }

        public string SERVICE_NM { get; set; }

        public string ERROR { get; set; }

        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        public List<SelectListItem> SERVICE_LIST_FOR_DD { get; set; }
        public string AUTH_STATUS_ID { get; set; }
    }
}
