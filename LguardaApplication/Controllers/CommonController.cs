using LguardaApplication.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult DDLServicLoadbyAppId(string app_id)
        {
            if (!string.IsNullOrEmpty(app_id) || app_id != "0" || app_id != string.Empty || app_id != null || app_id != " " || app_id != "")
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Service_By_AppId_For_DD/" + app_id + "?format=json";
                IEnumerable<SelectListItem> data = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        public ActionResult DDLModuleLoadbyServiceId(string service_id, string app_id)
        {
            if (!string.IsNullOrEmpty(service_id) || service_id != "0")
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Module_By_ServiceId_For_DD/" + service_id + "/" + app_id + "?format=json";
                IEnumerable<SelectListItem> data = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        public ActionResult DDLFunctionLoadbyModuleId(string module_id)
        {
            if (!string.IsNullOrEmpty(module_id) || module_id != "0")
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Function_By_ModuleId_For_DD/" + module_id + "?format=json";
                IEnumerable<SelectListItem> data = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        public ActionResult DDLFunctionLoadbyModuleIdAndItemtype(string module_id, string item_type)
        {
            if (!string.IsNullOrEmpty(module_id) || module_id != "0" || !string.IsNullOrEmpty(item_type))
            {
                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Function_ByModuleIdAndItemtype_For_DD/" + module_id + "/" + item_type + "?format=json";
                IEnumerable<SelectListItem> data = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
        public ActionResult DDLItemType()
        {
            string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Get_Item_type_For_DD" + "?format=json";
            IEnumerable<SelectListItem> data = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DDLUserAreaLoadbyUserClassification(string classification_id)
        {
            //LgardaServiceRef.LgardaServiceClient Obj_Service = new LgardaServiceRef.LgardaServiceClient();
            //IEnumerable<SelectListItem> SelectList = Obj_Service.GetUserAreaIdForDD(classification_id);
            //return Json(SelectList, JsonRequestBehavior.AllowGet);
            string url = string.Empty;

            if (!string.IsNullOrWhiteSpace(classification_id))
                url = Utility.AppSetting.getLgardaServer() + "/Get_User_AreaId_For_DD/" + classification_id + "?format=json";
            else
                url = Utility.AppSetting.getLgardaServer() + "/Get_All_User_AreaId_For_DD" + "?format=json";

            IEnumerable<SelectListItem> SelectList = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            return Json(SelectList, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllCalendarType()
        {
            string url = Utility.AppSetting.getLgardaServer() + "/GetCalendarNameForDD" + "?format=json";
            IEnumerable<SelectListItem> data = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}