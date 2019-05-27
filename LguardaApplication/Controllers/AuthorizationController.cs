using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using Model.EntityModel.LGModel;
using LguardaApp.RBAC.Action_Filters;

namespace LguardaApplication.Controllers
{  
    public class AuthorizationController : Controller
    {
        #region Class Level Variable
        List<LG_AA_NFT_AUTH_LOG_MAP> LIST_LG_AA_NFT_AUTH_LOG_MAP;
        LG_AA_NFT_AUTH_LOG_MAP OBJ_LG_AA_NFT_AUTH_LOG_MAP;
        #endregion Class Level Variable

        [RBAC]
        public ActionResult Index()
        {
            try
            {
                ModelState.Clear();

                OBJ_LG_AA_NFT_AUTH_LOG_MAP = new LG_AA_NFT_AUTH_LOG_MAP();
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Functions_From_Auth_Log_For_DD" + "?format=json";
                OBJ_LG_AA_NFT_AUTH_LOG_MAP.FUNCTION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show authorization info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show authorization info.";
                }
                return View(OBJ_LG_AA_NFT_AUTH_LOG_MAP);
            }
        }

        [HttpPost]
        public JsonResult IndexWithFunctionID(string functionID, string _search, string nd, string rows, string page, string sidx, string sord)
        {
            try
            {
                functionID = functionID == "" ? "0" : functionID;

                string session_user = Session["currentUserID"].ToString();

                string urlForNftLogs = AppSetting.getLgardaServer() + "/Get_Nft_Auth_Logs_By_FunctionID/" + functionID + "/" + session_user + "?format=json";
                
                LIST_LG_AA_NFT_AUTH_LOG_MAP = HttpWcfRequest.GetObjectCollection<LG_AA_NFT_AUTH_LOG_MAP>(urlForNftLogs);
                //LIST_LG_AA_NFT_AUTH_LOG_MAP =
                //    HttpWcfRequest.GetObjectCollection_GET<LG_AA_NFT_AUTH_LOG_MAP>(urlForNftLogs, "GetNftAuthLogsByFunctionIDResult");

                if (LIST_LG_AA_NFT_AUTH_LOG_MAP.Count == 0)
                {
                    return Json(LIST_LG_AA_NFT_AUTH_LOG_MAP, JsonRequestBehavior.DenyGet);    
                }
                else
                {
                    string urlForFunctionList = AppSetting.getLgardaServer() + "/Get_Functions_From_Auth_Log_For_DD" + "?format=json";
                    LIST_LG_AA_NFT_AUTH_LOG_MAP[0].FUNCTION_LIST_FOR_DD =
                        HttpWcfRequest.GetObjectCollection<SelectListItem>(urlForFunctionList);

                    return Json(LIST_LG_AA_NFT_AUTH_LOG_MAP, JsonRequestBehavior.DenyGet);    
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show authorization info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show authorization info.";
                }
                return Json(LIST_LG_AA_NFT_AUTH_LOG_MAP, JsonRequestBehavior.DenyGet);
            }
        }

        //[HttpGet]
        //public ViewResult Details(string functionID)
        //{
        //    if (functionID == null)
        //    {
        //        return View();
        //    }
        //    try
        //    {
        //        string url = Utility.AppSetting.getLgardaServer() + "/Get_Nft_Auth_Logs_By_FunctionID/" + functionID + "?format=json";
        //        LIST_LG_AA_NFT_AUTH_LOG_MAP = HttpWcfRequest.GetObjectCollection<LG_AA_NFT_AUTH_LOG_MAP>(url);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Service"))
        //        {
        //            TempData["Error"] = "Can't show authorization details. Service is unable to process the request.";
        //        }
        //        else
        //        {
        //            TempData["Error"] = "Can't show authorization details.";
        //        }
        //        return View();
        //    }
        //    return View("Details", LIST_LG_AA_NFT_AUTH_LOG_MAP[0]);
        //}
        [RBAC]
        [HttpGet]
        public ViewResult Details(string logID)
        {
            if (logID == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Nft_Auth_Logs_By_LogID/" + logID + "?format=json";
                LIST_LG_AA_NFT_AUTH_LOG_MAP = HttpWcfRequest.GetObjectCollection<LG_AA_NFT_AUTH_LOG_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show authorization details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show authorization details.";
                }
            }
            return View("Details", LIST_LG_AA_NFT_AUTH_LOG_MAP[0]);
        }
 
        [HttpGet]
        public ViewResult LogValDetails(string logID)
        {
            List<LG_AA_NFT_AUTH_LOG_VAL_MAP> LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP = new List<LG_AA_NFT_AUTH_LOG_VAL_MAP>();
            string session_user = Session["currentUserID"].ToString();
            if (logID == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Nft_Auth_Log_Val_By_LogID/" + logID + "/" + session_user + "/" + "pFUNCTION_ID" + "?format=json";
                LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP = HttpWcfRequest.GetObjectCollection<LG_AA_NFT_AUTH_LOG_VAL_MAP>(url);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show authorization details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show authorization details.";
                }
            }
            return View("LogValDetails", LIST_LG_AA_NFT_AUTH_LOG_VAL_MAP);
        }

        [HttpPost]
        public ActionResult Create(LG_AA_NFT_AUTH_LOG_MAP pLG_AA_NFT_AUTH_LOG_MAP)
        {
            try
            {
                string result = string.Empty;

                if (ModelState.IsValid)
                {
                    /*[WebInvoke(Method = "POST", 
                     * UriTemplate = "/Add_Nft_Auth_Log/{pFUNCTION_ID}/{pTABLE_NAME}/{pTABLE_PK_COL_NM}/
                     * {pTABLE_PK_COL_VAL}/{pOLD_VALUE}/{pNEW_VALUE}/{pACTION_STATUS}/{pREMARKS}/{pPRIMARY_TABLE_FLAG}/
                     * {pPARENT_TABLE_PK_VAL}/{pAUTH_STATUS_ID}/{pAUTH_LEVEL_MAX}/{pAUTH_LEVEL_PENDING}/{pREASON_DECLINE}/
                     * {pMAKE_BY}/{pMAKE_DT}", BodyStyle = WebMessageBodyStyle.Wrapped)]*/
                    string url =
                        ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Nft_Auth_Log/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.FUNCTION_ID + "/" + pLG_AA_NFT_AUTH_LOG_MAP.TABLE_NAME + "/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_NM + "/" + pLG_AA_NFT_AUTH_LOG_MAP.TABLE_PK_COL_VAL + "/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.ACTION_STATUS + "/" + pLG_AA_NFT_AUTH_LOG_MAP.REMARKS + "/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.PRIMARY_TABLE_FLAG + "/" + pLG_AA_NFT_AUTH_LOG_MAP.PARENT_TABLE_PK_VAL + "/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.REMARKS + "/" + pLG_AA_NFT_AUTH_LOG_MAP.AUTH_STATUS_ID + "/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_MAX + "/" + pLG_AA_NFT_AUTH_LOG_MAP.AUTH_LEVEL_PENDING + "/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE + "/" + pLG_AA_NFT_AUTH_LOG_MAP.MAKE_BY + "/" +
                        pLG_AA_NFT_AUTH_LOG_MAP.MAKE_DT +
                        "?format=json";

                    result = HttpWcfRequest.PostParameter(url);

                    if (result == "True")
                    {
                        TempData["Success"] = "Authorized Successfully.";
                        return RedirectToAction("Create");
                    }
                    if (result.Contains("Maker"))
                    {
                        TempData["Error"] = "Maker Checker can't be same";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        ViewData["Error"] = "Unable to Authorized.";
                        return View("Index");
                    }
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't authorize. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't authorize.";
                }
                return RedirectToAction("Create");
            }
        }


        [HttpPost]
        public ActionResult Edit(LG_AA_NFT_AUTH_LOG_MAP pLG_AA_NFT_AUTH_LOG_MAP)
        {
            string AUTH_BY = string.Empty;
            AUTH_BY = (string)Session["currentUserID"];
            try
            {
                string result = string.Empty;

                //UriTemplate ="/Add_Nft_Auth_Log_Dtls/{pLOG_ID}/{pAUTH_BY}/{pAUTH_DT}/{pAUTH_STATUS_ID}/{preasonDecline}"
                //http://localhost/LgurdaService/LgurdaService.svc/Add_Nft_Auth_Log_Dtls/1/salekin1/2016-03-14/A/0?format=json
                string url =
                    ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Nft_Auth_Log_Dtls/" +
                    pLG_AA_NFT_AUTH_LOG_MAP.LOG_ID + "/" + AUTH_BY + "/" +
                    DateTime.Now.ToString("yyyy-MM-dd") + "/" + "A" + "/" +
                    "0" +
                    "?format=json";

                result = HttpWcfRequest.PostParameter(url);

                if (result == "True")
                {
                    TempData["Success"] = "Authorized successfully.";
                    return RedirectToAction("Index");
                }
                if (result.Contains("Maker"))
                {
                    TempData["Error"] = "Maker Checker can't be same";
                    return RedirectToAction("Edit");
                }
                else
                {
                    TempData["Error"] = "Unable to authorized.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't authorize. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't authorize.";
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditDecline(LG_AA_NFT_AUTH_LOG_MAP pLG_AA_NFT_AUTH_LOG_MAP)
        {
            string AUTH_BY = string.Empty;
            AUTH_BY = (string)Session["currentUserID"];
            try
            {
                if (pLG_AA_NFT_AUTH_LOG_MAP.LOG_ID == 0)
                    return RedirectToAction("Index");

                if (string.IsNullOrEmpty(pLG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE))
                {
                    return RedirectToAction("Index");
                }

                string result = string.Empty;

                //UriTemplate ="/Add_Nft_Auth_Log_Dtls/{pLOG_ID}/{pAUTH_BY}/{pAUTH_DT}/{pAUTH_STATUS_ID}/{preasonDecline}"
                //http://localhost/LgurdaService/LgurdaService.svc/Add_Nft_Auth_Log_Dtls/1/salekin1/2016-03-14/D/not selected?format=json
                string url =
                    ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Nft_Auth_Log_Dtls/" +
                    pLG_AA_NFT_AUTH_LOG_MAP.LOG_ID + "/" + AUTH_BY + "/" +
                    DateTime.Now.ToString("yyyy-MM-dd") + "/" + "D" + "/" +
                    pLG_AA_NFT_AUTH_LOG_MAP.REASON_DECLINE +
                    "?format=json";

                result = HttpWcfRequest.PostParameter(url);

                if (result == "True")
                {
                    TempData["Success"] = "Declined Successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Unable to Decline.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't decline. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't decline.";
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public JsonResult History(string pLOG_ID)
        {
            if (string.IsNullOrEmpty(pLOG_ID))
            {
                pLOG_ID = "0";
            }
            List<LG_AA_NFT_AUTH_LOG_DTLS_MAP> LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP = new List<LG_AA_NFT_AUTH_LOG_DTLS_MAP>();
            try
            {
                string urlForHistory = AppSetting.getLgardaServer() + "/Get_Auth_History/" + pLOG_ID + "?format=json";

                LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP = HttpWcfRequest.GetObjectCollection<LG_AA_NFT_AUTH_LOG_DTLS_MAP>(urlForHistory);
                //LIST_LG_AA_NFT_AUTH_LOG_MAP =
                //    HttpWcfRequest.GetObjectCollection_GET<LG_AA_NFT_AUTH_LOG_MAP>(urlForNftLogs, "GetNftAuthLogsByFunctionIDResult");


                return Json(LIST_LG_AA_NFT_AUTH_LOG_DTLS_MAP, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show authorization info. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show authorization info.";
                }
                return Json(LIST_LG_AA_NFT_AUTH_LOG_MAP, JsonRequestBehavior.DenyGet);
            }
        }

        [HttpGet]
        public JsonResult GetFunctions()
        {
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Functions_From_Auth_Log_For_DD" + "?format=json";
                List<SelectListItem> FUNCTION_LIST_FOR_DD = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);

                return Json(FUNCTION_LIST_FOR_DD, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show authorization info. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show authorization info.";
                }
                return Json(LIST_LG_AA_NFT_AUTH_LOG_MAP, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
