using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_AA_NFT_AUTH_LOG_VAL_MAP
    {
        #region Properties
        [DataMember]
        [Required(ErrorMessage = "Log ID is required")]
        public Int64 LOG_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Log Value ID is required")]
        public Int64 LOG_VAL_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Column Name is required")]
        public string COLUMN_NAME { get; set; }

        [DataMember]
        public string OLD_VALUE { get; set; }

        [DataMember]
        [Required(ErrorMessage = "New Value is required")]
        public string NEW_VALUE { get; set; }

        [DataMember]
        public string ERROR { get; set; }
        #endregion Properties
    }
}
