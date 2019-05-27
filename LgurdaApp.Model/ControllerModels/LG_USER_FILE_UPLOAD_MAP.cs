using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LgurdaApp.Model.ValidationAttribute;
using System.Drawing;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_USER_FILE_UPLOAD_MAP
    {

        public string USER_SESSION_ID { get; set; }

        public string USER_NAME { get; set; }

        public short FILE_ID { get; set; }

        public short FILE_TYPE { get; set; }

        public short USER_TYPE { get; set; }

        public string USER_ID { get; set; }

        public string APPLICATION_ID { get; set; }

        //[ValidateUploadingFileAttribute(ErrorMessage = "Please select a file smaller than 2MB")]
        public HttpPostedFileBase File { get; set; }

        public byte[] imageByte { get; set; }
        
        public string ERROR { get; set; }
        
        public List<SelectListItem> LIST_ALL_USER_FOR_DD { get; set; }

        public System.Drawing.Bitmap THUMBPIC { get; set; }

        public string CLASSIFICATION_ID { get; set; }
        public string AREA_ID { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string AGENT_ID { get; set; }
        public string ACC_NO { get; set; }
    }
}
