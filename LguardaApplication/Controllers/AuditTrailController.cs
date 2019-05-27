using LguardaApp.RBAC.Action_Filters;
using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Controllers

{
    public class AuditTrailController : Controller
    {
        #region Class Level Variable
        List<LG_USER_ACTIVITY_LOG_MAP> LIST_LG_USER_ACTIVITY_LOG_MAP = new List<LG_USER_ACTIVITY_LOG_MAP>();
        LG_USER_ACTIVITY_LOG_MAP OBJ_LG_USER_ACTIVITY_LOG_MAP = new LG_USER_ACTIVITY_LOG_MAP();
        #endregion

        [RBAC]
        public ActionResult Index()
        {
            //LIST_LG_USER_ACTIVITY_LOG_MAP = GetLogData("5", "9/04/2016", "10/04/2016").ToList();
            //LIST_LG_USER_ACTIVITY_LOG_MAP = null;

            return View();
        }
        [RBAC]
        [HttpPost]
        public ActionResult Index(LG_USER_ACTIVITY_LOG_MAP pLG_USER_ACTIVITY_LOG_MAP)
        {
            if (ModelState.IsValid)
            {
                DateTime sDate = Convert.ToDateTime(pLG_USER_ACTIVITY_LOG_MAP.START_DATE);
                DateTime eDate = Convert.ToDateTime(pLG_USER_ACTIVITY_LOG_MAP.END_DATE);

                TimeSpan ts = new TimeSpan(00, 00, 0);
                TimeSpan te = new TimeSpan(23, 59, 59);
                sDate = sDate.Date + ts;
                eDate = eDate.Date + te;

                string startDate = Convert.ToString(sDate);
                string endDate = Convert.ToString(eDate);

                startDate = startDate.Replace("/", "-").Replace(":", ";");
                endDate = endDate.Replace("/", "-").Replace(":", ";");

                string url = Utility.AppSetting.getLgardaServer() + "/Get_User_Activity_Log/" + pLG_USER_ACTIVITY_LOG_MAP.USER_ID + "/" + startDate + "/" + endDate + "?format=json";

                var model = new List<LG_USER_ACTIVITY_LOG_MAP>();

                LIST_LG_USER_ACTIVITY_LOG_MAP = HttpWcfRequest.GetObjectCollection<LG_USER_ACTIVITY_LOG_MAP>(url);

                TempData["userId"] = pLG_USER_ACTIVITY_LOG_MAP.USER_ID.ToString();
                TempData["listItems"] = LIST_LG_USER_ACTIVITY_LOG_MAP;

                return RedirectToAction("LogList");
            }
            return View();
        }

        public ActionResult LogList()
        {

            LIST_LG_USER_ACTIVITY_LOG_MAP = (List<LG_USER_ACTIVITY_LOG_MAP>)TempData["listItems"];
            
            if (LIST_LG_USER_ACTIVITY_LOG_MAP != null)
            {
                ViewBag.U_Id = TempData["userId"].ToString();
                return View(LIST_LG_USER_ACTIVITY_LOG_MAP);
            }
            else
                return View();
        }
    }
}