using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgurdaApp.Model.ControllerModels
{
    public class LG_MENU_MAP
    {
        //public int MenuId { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public string Action { get; set; }
        //public string Controller { get; set; }
        //public string Url { get; set; }
        //public Nullable<int> ParentId { get; set; }

        //public IEnumerable<LG_MENU_MAP> Children { get; set; }

        public decimal SL_ID { get; set; }
        public string APP_ID { get; set; }
        public Nullable<decimal> MENU_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string CONTROLLER { get; set; }
        public string ACTION { get; set; }
        public string URL { get; set; }
        public Nullable<decimal> PARENTID { get; set; }
        public string FUNCTION_ID { get; set; }

        public IEnumerable<LG_MENU_MAP> Children { get; set; }
    }
}
