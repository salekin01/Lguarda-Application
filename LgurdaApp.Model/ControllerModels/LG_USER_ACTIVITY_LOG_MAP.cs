using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_USER_ACTIVITY_LOG_MAP
    {
        [Required(ErrorMessage = "User Id Field is required")]
        public string USER_ID { get; set; }

        public string ACCOUNT_NO { get; set; }

        public string BRANCH_ID { get; set; }

        public string ACTION { get; set; }

        public string PARAMETERS { get; set; }

        public string CURRENT_PAGE { get; set; }

        public string IP_ADDRESS { get; set; }

        [Required(ErrorMessage = "Start Date Field is required")]
        public DateTime? START_DATE { get; set; }

        [Required(ErrorMessage = "End Date Field is required")]
        public DateTime? END_DATE { get; set; }

        public DateTime? DATE_TIME { get; set; }
        public string APPLICATION_ID { get; set; }
    }
}