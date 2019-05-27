using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LguardaApplication.Filters
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!(filterContext.HttpContext.Request.IsAjaxRequest()))
            {
                string userId = "";
                if (filterContext.HttpContext.Session["currentUserID"] != null)
                {
                   userId = filterContext.HttpContext.Session["currentUserID"].ToString();
                }
                string accountNo = "5";
                //accountNo = Session["accountNo"].ToString();
                string branchId = "5";
                //branchId = Session["branchId"].ToString();

                bool isMobile = filterContext.HttpContext.Request.Browser.IsMobileDevice;

                string LocalUserIp = "";

                if (isMobile)
                {
                    // Fetch the ip of mobile device                                       
                }
                else
                {
                    //LocalUserIp = filterContext.HttpContext.Request.ServerVariables["LOCAL_ADDR"];
                    //if (LocalUserIp == "::1")
                    //{
                    //    LocalUserIp = "127.0.0.1";
                    //}
                    
                    LocalUserIp = filterContext.HttpContext.Request.ServerVariables["REMOTE_ADDR"];

                    if (LocalUserIp == "::1")
                    {
                        LocalUserIp = "127.0.0.1";
                    }


                }

                var param = filterContext.ActionParameters;
               

                //code to get all the parameter field value in variable(dataString here) dynamically 

                string dataString = "";
                if (param.Count != 0)
                {
                    List<string> key = param.Keys.ToList();
                    string abc = key[0];
                    dynamic extractedObj = param.Single(x => x.Key == key[0]).Value;

                    
                    if (abc.Contains("_MAP"))
                    {
                        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(extractedObj))
                        {
                            dataString += prop.Name + ":" + prop.GetValue(extractedObj) + ";" ;
                        }
                    }

                    else
                    {
                        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(extractedObj))
                        {
                            dataString += abc + ":" + prop.GetValue(extractedObj) + ";";
                        }
                                     
                    }
                }

                string actionName = filterContext.ActionDescriptor.ActionName;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string currentPageName = "/" + controllerName + "/" + actionName;
                string ReferenceAddress = "";
                if (filterContext.HttpContext.Request.UrlReferrer != null)
                {                    
                    ReferenceAddress = filterContext.HttpContext.Request.UrlReferrer.LocalPath.ToString();
                    //ReferenceAddress = ReferenceAddress.Replace("/", "-");
                    string action = "From:" + ReferenceAddress + "    " + "To:" + currentPageName;

                    if (HttpContext.Current.Items["u_id"] == null)
                    {
                        filterContext.HttpContext.Items.Add("u_id", userId);
                    }
                    if (HttpContext.Current.Items["account_no"] == null)
                    {
                        filterContext.HttpContext.Items.Add("account_no", accountNo);
                    }
                    if (HttpContext.Current.Items["branch_id"] == null)
                    {
                        filterContext.HttpContext.Items.Add("branch_id", branchId);
                    }
                    if (HttpContext.Current.Items["u_ip"] == null)
                    {
                        filterContext.HttpContext.Items.Add("u_ip", LocalUserIp);
                    }
                    if (HttpContext.Current.Items["params"] == null)
                    {
                        filterContext.HttpContext.Items.Add("params", dataString);
                    }
                    if (HttpContext.Current.Items["reference"] == null)
                    {
                        filterContext.HttpContext.Items.Add("reference", action);
                    }
                    if (HttpContext.Current.Items["curr_page"] == null)
                    {
                        filterContext.HttpContext.Items.Add("curr_page", currentPageName);
                    }
                }


                base.OnActionExecuting(filterContext);
            }
        }


        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (!(filterContext.HttpContext.Request.IsAjaxRequest()))
            {
               LG_USER_ACTIVITY_LOG_MAP pOBJ_LG_USER_ACTIVITY_LOG_MAP = new LG_USER_ACTIVITY_LOG_MAP();

                string result = string.Empty;
                string result1 = string.Empty;

                pOBJ_LG_USER_ACTIVITY_LOG_MAP.USER_ID = (string)filterContext.HttpContext.Items["u_id"];
                pOBJ_LG_USER_ACTIVITY_LOG_MAP.ACCOUNT_NO = (string)filterContext.HttpContext.Items["account_no"];
                pOBJ_LG_USER_ACTIVITY_LOG_MAP.BRANCH_ID = (string)filterContext.HttpContext.Items["branch_id"];
                pOBJ_LG_USER_ACTIVITY_LOG_MAP.IP_ADDRESS = (string)filterContext.HttpContext.Items["u_ip"];
                pOBJ_LG_USER_ACTIVITY_LOG_MAP.PARAMETERS = (string)filterContext.HttpContext.Items["params"];
                pOBJ_LG_USER_ACTIVITY_LOG_MAP.ACTION = (string)filterContext.HttpContext.Items["reference"];
                pOBJ_LG_USER_ACTIVITY_LOG_MAP.APPLICATION_ID = "1";
                pOBJ_LG_USER_ACTIVITY_LOG_MAP.CURRENT_PAGE = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "/" + filterContext.ActionDescriptor.ActionName;

                
                string exceptionMessage = "";
                try
                {

                    if (pOBJ_LG_USER_ACTIVITY_LOG_MAP.USER_ID != null && pOBJ_LG_USER_ACTIVITY_LOG_MAP.USER_ID.Trim().Length > 0 && pOBJ_LG_USER_ACTIVITY_LOG_MAP.ACCOUNT_NO != null && pOBJ_LG_USER_ACTIVITY_LOG_MAP.BRANCH_ID != null)
                    {
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Insert_To_Log?format=json";
                        Uri uri = new Uri(url);
                                                
                        result = HttpWcfRequest.PostObject_WithReturningString<LG_USER_ACTIVITY_LOG_MAP>(uri, pOBJ_LG_USER_ACTIVITY_LOG_MAP);
                                                
                    }

                    if (pOBJ_LG_USER_ACTIVITY_LOG_MAP.USER_ID != null && pOBJ_LG_USER_ACTIVITY_LOG_MAP.USER_ID.Trim().Length > 0)
                    {
                        string session_user = filterContext.HttpContext.Session["currentUserID"].ToString();
                        string Application_id = "01";
                        string url1 = ConfigurationManager.AppSettings["lgarda_server"] + "/Insert_To_session_tracker/" + session_user + "/" + Application_id + "?format=json";
                         result1 = HttpWcfRequest.PostParameter(url1);
                    }

                }


                catch (Exception ex)
                {
                    exceptionMessage = ex.Message.ToString();
                }

                base.OnActionExecuted(filterContext);
            }
        }
    }
}