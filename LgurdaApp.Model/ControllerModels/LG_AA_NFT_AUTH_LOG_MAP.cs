using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Model.EntityModel.LGModel
{
    public class LG_AA_NFT_AUTH_LOG_MAP
    {
        #region Properties

        [DataMember]
        [Required(ErrorMessage = "Log ID is required")]
        public Int64 LOG_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Function ID is required")]
        public string FUNCTION_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Table Name is required")]
        public string TABLE_NAME { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Table Primary Column Name is required")]
        public string TABLE_PK_COL_NM { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Table Primary Column Value is required")]
        public string TABLE_PK_COL_VAL { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Action Status is required")]
        public string ACTION_STATUS { get; set; }

        [DataMember]
        public string REMARKS { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Primary Table Flag is required")]
        public Int16 PRIMARY_TABLE_FLAG { get; set; }

        [DataMember]
        public Nullable<Int64> PARENT_TABLE_PK_VAL { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Status ID is required")]
        public string AUTH_STATUS_ID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Level Max is required")]
        public Int16 AUTH_LEVEL_MAX { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Auth Level Pneding is required")]
        public Int16 AUTH_LEVEL_PENDING { get; set; }

        [DataMember]
        public string REASON_DECLINE { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Make By is required")]
        public string MAKE_BY { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Make Date is required")]
        public DateTime MAKE_DT { get; set; }

        [DataMember]
        public string ERROR { get; set; }

        public List<LG_AA_NFT_AUTH_LOG_DTLS_MAP> LG_AA_NFT_AUTH_LOG_DTLS_MAP;

        public IEnumerable<SelectListItem> FUNCTION_LIST_FOR_DD { get; set; }

        #endregion Properties

    }
}