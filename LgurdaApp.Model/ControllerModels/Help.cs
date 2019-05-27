using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LgurdaApp.Model.ControllerModels
{
    public class Help
    {
        public string agentBankMenu { get; set; }
        public string menu { get; set; }
       
        public string cntrlName { get; set; }
        public string actionName { get; set; }
        public string text { get; set; }
    }
}
