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
    public class CalendarTypeController : Controller
    {
        List<LG_SYS_CLD_TYPE_MAP> LIST_LG_SYS_CLD_TYPE_MAP = new List<LG_SYS_CLD_TYPE_MAP>();
        LG_SYS_CLD_TYPE_MAP OBJ_LG_SYS_CLD_TYPE_MAP = new LG_SYS_CLD_TYPE_MAP();

        // GET: /CalendarType/
        public ActionResult Index()
        {
            try
            {

                string url = Utility.AppSetting.getLgardaServer() + "/Get_AllCalendarType" + "?format=json";
                LIST_LG_SYS_CLD_TYPE_MAP = HttpWcfRequest.GetObjectCollection<LG_SYS_CLD_TYPE_MAP>(url);
                return View(LIST_LG_SYS_CLD_TYPE_MAP);

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
                return View(LIST_LG_SYS_CLD_TYPE_MAP);
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_CalendarByCalendarlId/" + id + "?format=json";
                OBJ_LG_SYS_CLD_TYPE_MAP = HttpWcfRequest.GetObject<LG_SYS_CLD_TYPE_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show Calendar details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show Calendar details.";
                }
            }
            return View(OBJ_LG_SYS_CLD_TYPE_MAP);
        }


        public ActionResult Create()
        {

            //string url = Utility.AppSetting.getLgardaServer() + "/Get_AllCalendarType" + "?format=json";
            //OBJ_LG_SYS_CLD_TYPE_MAP.LIST_OF_ALL_CALENDAR = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            //TempData["LIST_OF_ALL_CALENDAR"] = OBJ_LG_SYS_CLD_TYPE_MAP.LIST_OF_ALL_CALENDAR;

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

        // POST: /CalendarType/Create
        [HttpPost]
        public ActionResult Create(LG_SYS_CLD_TYPE_MAP pLG_SYS_CLD_TYPE, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    //  if (ModelState.IsValid)
                    //  {
                    string session_user = Session["currentUserID"].ToString();
                    if (pLG_SYS_CLD_TYPE.BASED_ON_CLD == null)
                    {
                        pLG_SYS_CLD_TYPE.BASED_ON_CLD = " ";
                    }
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/AddCalendarType/" + pLG_SYS_CLD_TYPE.CLD_TYPE_NM + "/" + pLG_SYS_CLD_TYPE.DEFAULT_CLD_B + "/" + pLG_SYS_CLD_TYPE.BASED_ON_CLD + "/" + session_user + "?format=json";
                    result = HttpWcfRequest.PostParameter(url);


                    if (result.ToLower().Contains("already exists"))
                    {
                        ModelState.AddModelError("CLD_TYPE_NM", "Calendar Type Already Exists");
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

                        pLG_SYS_CLD_TYPE.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                        return View(pLG_SYS_CLD_TYPE);
                    }
                    // }
                    //else
                    //{

                    //    pLG_SYS_CLD_TYPE.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                    //    return View(pLG_SYS_CLD_TYPE);
                    //}

                }

                pLG_SYS_CLD_TYPE.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                return View(pLG_SYS_CLD_TYPE);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't save Calendar. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't Save Calendar.";
                }
                return View(pLG_SYS_CLD_TYPE);
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
                string url = Utility.AppSetting.getLgardaServer() + "/Get_CalendarByCalendarlId/" + id + "?format=json";
                OBJ_LG_SYS_CLD_TYPE_MAP = HttpWcfRequest.GetObject<LG_SYS_CLD_TYPE_MAP>(url);


                TempData["LIST_OF_ALL_CALENDAR"] = OBJ_LG_SYS_CLD_TYPE_MAP.LIST_OF_ALL_CALENDAR;
                OBJ_LG_SYS_CLD_TYPE_MAP.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                if (OBJ_LG_SYS_CLD_TYPE_MAP.DEFAULT_CLD == 1)
                {
                    OBJ_LG_SYS_CLD_TYPE_MAP.DEFAULT_CLD_B = true;
                }
                else
                {
                    OBJ_LG_SYS_CLD_TYPE_MAP.DEFAULT_CLD_B = false;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit Calendar Type. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit Calendar Type.";
                }
            }
            return View(OBJ_LG_SYS_CLD_TYPE_MAP);
        }


        // POST: /CalendarType/Edit/5
        [HttpPost]
        public ActionResult Edit(LG_SYS_CLD_TYPE_MAP pLG_SYS_CLD_TYPE, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {

                    //if (pLG_SYS_CLD_TYPE.DEFAULT_CLD == null)
                    //{
                    //    //pLG_SYS_CLD_TYPE.DEFAULT_CLD = " ";
                    //}
                    //if (pLG_SYS_CLD_TYPE.DEFAULT_CLD == 1)
                    //{
                    //    pLG_SYS_CLD_TYPE.DEFAULT_CLD_B = true;
                    //}
                    //else
                    //{
                    //    pLG_SYS_CLD_TYPE.DEFAULT_CLD_B = false;
                    //}

                    string session_user = Session["currentUserID"].ToString();
                    string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_Calendar/" + pLG_SYS_CLD_TYPE.CLD_TYPE_ID + "/" + pLG_SYS_CLD_TYPE.CLD_TYPE_NM + "/" + pLG_SYS_CLD_TYPE.DEFAULT_CLD_B + "/" + pLG_SYS_CLD_TYPE.BASED_ON_CLD + "/" + "/" + session_user + "?format=json";
                    result = HttpWcfRequest.PostParameter(url);



                    if (result == "True")
                    {
                        TempData["Success"] = "Updated Successfully.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Unable to Update.";
                        pLG_SYS_CLD_TYPE.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                        return View(pLG_SYS_CLD_TYPE);

                    }

                    //else
                    //{

                    //    pLG_SYS_CLD_TYPE.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                    //    return View(pLG_SYS_CLD_TYPE);

                    //}
                }

                pLG_SYS_CLD_TYPE.LIST_OF_ALL_CALENDAR = (List<SelectListItem>)TempData["LIST_OF_ALL_CALENDAR"];
                return View(pLG_SYS_CLD_TYPE);

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
                return View(pLG_SYS_CLD_TYPE);
            }
        }



        public ActionResult Delete(int id)
        {
            return View();
        }


        // POST: /CalendarType/Delete/5
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
