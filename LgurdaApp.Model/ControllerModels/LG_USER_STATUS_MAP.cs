using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_USER_STATUS_MAP
    {

        #region Property

        [Required(ErrorMessage = "User Id is required.")]
        public string USER_ID { get; set; }

        public string USER_ACCOUNT_NO { get; set; }

        public string USER_NAME { get; set; }

        #endregion


    }
}
