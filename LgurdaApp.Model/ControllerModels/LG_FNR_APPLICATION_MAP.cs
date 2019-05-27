using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_FNR_APPLICATION_MAP
    {
        #region Properties

        public string APPLICATION_ID { get; set; }

        [Required(ErrorMessage = "Application Name is required")]
        public string APPLICATION_NAME { get; set; }

        public string AUTH_STATUS_ID { get; set; }

        public string LAST_ACTION { get; set; }

        public DateTime? LAST_UPDATE_DT { get; set; }

        public DateTime MAKE_DT { get; set; }

        public string ERROR { get; set; }

        [Required(ErrorMessage = "Select App Type")]
        public int? APP_TYPE_ID { get; set; }

        public string APP_TYPE_NM { get; set; }

        public IEnumerable<SelectListItem> LIST_APP_TYPE { get; set; }

        #endregion
    }
}
