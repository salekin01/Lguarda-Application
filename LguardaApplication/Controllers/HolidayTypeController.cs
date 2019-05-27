using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using LguardaApplication.Utility;
using LgurdaApp.Model;
using LgurdaApp.Model.ControllerModels;
using LguardaApp.RBAC.Action_Filters;
using System.Collections.Generic;
using System;

namespace LguardaApplication.Controllers
{
    public class HolidayTypeController : Controller
    {
        LG_SYS_HOLIDAY_TYPE_MAP OBJ_LG_SYS_HOLIDAY_TYPE_MAP = new LG_SYS_HOLIDAY_TYPE_MAP();
        List<LG_SYS_HOLIDAY_TYPE_MAP> LIST_LG_SYS_HOLIDAY_TYPE_MAP = new List<LG_SYS_HOLIDAY_TYPE_MAP>();
        public ActionResult Index()
        {

            try
            {

                string url = Utility.AppSetting.getLgardaServer() + "/Get_AllHoliday" + "?format=json";
                LIST_LG_SYS_HOLIDAY_TYPE_MAP = HttpWcfRequest.GetObjectCollection<LG_SYS_HOLIDAY_TYPE_MAP>(url);
                return View(LIST_LG_SYS_HOLIDAY_TYPE_MAP);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show Calendar info. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show Calendar info.";
                }
                return View(LIST_LG_SYS_HOLIDAY_TYPE_MAP);
            }
           
        }

      
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_HolidayById/" + id + "?format=json";
                OBJ_LG_SYS_HOLIDAY_TYPE_MAP = HttpWcfRequest.GetObject<LG_SYS_HOLIDAY_TYPE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show Holiday details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show Holiday details.";
                }
            }
            return View(OBJ_LG_SYS_HOLIDAY_TYPE_MAP);
        }

       
        public ActionResult Create()
        {
          
             
            if (TempData["Success"] != null)
            
            {
                ViewData["Success"] = TempData["Success"].ToString();
            }
            if (TempData["Error"] != null)
            { 
                ViewData["Error"] = TempData["Error"].ToString(); 
            }
            
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(LG_SYS_HOLIDAY_TYPE_MAP pLG_SYS_HOLIDAY_TYPE_MAP, string command)
        {
            try
            {
            string result = string.Empty;
                if (command == "Save")
                {
                  //  if (ModelState.IsValid)
                  //  {
                     pLG_SYS_HOLIDAY_TYPE_MAP.MAKE_BY = Session["currentUserID"].ToString();

                     string url = ConfigurationManager.AppSettings["lgarda_server"] + "/AddHolidayType/" + pLG_SYS_HOLIDAY_TYPE_MAP.HOLIDAY_TYPE_NM + "/" + pLG_SYS_HOLIDAY_TYPE_MAP.WEEKEND_B + "/" + pLG_SYS_HOLIDAY_TYPE_MAP.MAKE_BY + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);


                        if (result.ToLower().Contains("already exists"))
                        {
                            ModelState.AddModelError("HOLIDAY_TYPE_NM", "Holiday Name  Already Exists");
                            return View();
                        }
                        if (result == "True")
                        {
                            TempData["Success"] = "Saved Successfully.";
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            ViewData["Error"] = "Unable to Save.";

                           
                            return View(pLG_SYS_HOLIDAY_TYPE_MAP);
                        }
                   // }
                    //else
                    //{

                    //    pLG_SYS_CLD_TYPE.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                    //    return View(pLG_SYS_CLD_TYPE);
                    //}

                }

               
                return View(pLG_SYS_HOLIDAY_TYPE_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't save Holiday. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't Save Holiday.";
                }
                return View(pLG_SYS_HOLIDAY_TYPE_MAP);
            }
        }

     
        public ActionResult Edit(int id)
        {
            if (id == null)
            {

                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_HolidayById/" + id + "?format=json";
                OBJ_LG_SYS_HOLIDAY_TYPE_MAP = HttpWcfRequest.GetObject<LG_SYS_HOLIDAY_TYPE_MAP>(url);


                   if (OBJ_LG_SYS_HOLIDAY_TYPE_MAP.WEEKEND == 1)
                {
                    OBJ_LG_SYS_HOLIDAY_TYPE_MAP.WEEKEND_B = true;
                }
                else
                {
                    OBJ_LG_SYS_HOLIDAY_TYPE_MAP.WEEKEND_B = false;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit Holiday Type. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit Holiday Type.";
                }
            }
            return View(OBJ_LG_SYS_HOLIDAY_TYPE_MAP);
        }

      
        [HttpPost]
        public ActionResult Edit(LG_SYS_HOLIDAY_TYPE_MAP pLG_SYS_HOLIDAY_TYPE_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {

                   

                    string session_user = Session["currentUserID"].ToString();
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/UpdateHolidayType/" + pLG_SYS_HOLIDAY_TYPE_MAP.HOLIDAY_TYPE_ID + "/" + pLG_SYS_HOLIDAY_TYPE_MAP.HOLIDAY_TYPE_NM + "/" + pLG_SYS_HOLIDAY_TYPE_MAP.WEEKEND_B + "/" + session_user + "?format=json";
                    result = HttpWcfRequest.PostParameter(url);

                    if (result == "True")
                    {
                        TempData["Success"] = "Updated Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Unable to Update.";

                        return View(pLG_SYS_HOLIDAY_TYPE_MAP);

                    }

                  
                }


                return View(pLG_SYS_HOLIDAY_TYPE_MAP);

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't update Calendar. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update Calendar.";
                }
                return View(pLG_SYS_HOLIDAY_TYPE_MAP);
            }
        }

        
        public ActionResult Delete(int id)
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
