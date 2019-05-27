using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LgurdaApp.Model.ValidationAttribute
{
    public class ValidateUploadingFileAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }

            if (file.ContentLength > 2 * 1024 * 1024)
            {
                return false;
            }

            return true;
        }
    }
}
