using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Model.EntityModel.LGModel
{
    public class LG_AA_NFT_AUTH_LOG_DTLS_MAP
    {
        #region Properties

        [DataMember]
        [Required(ErrorMessage = "Log Details ID is required")]
        public Int64 LOG_DETAILS_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Log ID is required")]
        public Int64 LOG_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth By is required")]
        public string AUTH_OR_DEC_BY { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Date is required")]
        public DateTime AUTH_OR_DEC_DT { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Level is required")]
        public int AUTH_LEVEL { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Status ID is required")]
        public string AUTH_STATUS_ID { get; set; }
        
        #endregion
    }
}
