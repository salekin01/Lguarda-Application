using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_SYS_MAIL_SERVER_CONFIG_MAP
    {
        #region Properties

        public string MAIL_ID { get; set; }

        [Required(ErrorMessage = "Mail sender IP is required")]
        public string MAIL_SENDER_IP { get; set; }

        [Required(ErrorMessage = "Mail sender address is required")]
        public string MAIL_SENDER_ADDRESS { get; set; }

        [Required(ErrorMessage = "Mail sender password is required")]
        public string MAIL_SENDER_PASSWORD { get; set; }

        [Required(ErrorMessage = "Mail sender name is required")]
        public string MAIL_SENDER_NAME { get; set; }

        [Required(ErrorMessage = "Application needs to be selected")]
        public string APPLICATION_ID { get; set; }

        public string MAKE_BY { get; set; }

        public DateTime MAKE_DT { get; set; }

        public string APPLICATION_NAME { get; set; }

        public string ERROR { get; set; }

        public string AUTH_STATUS_ID { get; set; }

        public string LAST_ACTION { get; set; }

        public string SESSION_USER_ID { get; set; }

        public DateTime? LAST_UPDATE_DT { get; set; }

        public List<SelectListItem> APPLICATION_LIST_FOR_DD { get; set; }
        #endregion
    }
}
