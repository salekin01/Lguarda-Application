using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_FNR_ROLE_MAP
    {
        public string ROLE_ID { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        public string ROLE_NAME { get; set; }
        [Required(ErrorMessage = "Role desciption is required")]
        public string ROLE_DESCRIP { get; set; }
        public short IS_SYS_ADMIN { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public DateTime? LAST_UPDATE_DT { get; set; }
        public DateTime MAKE_DT { get; set; }
    }
}
