using LguardaApplication.Utility;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Management;
using System.Security.Principal;
using System.DirectoryServices;
using System.Net;
using System.Net.Sockets;

//using System.Object;

namespace LguardaApplication.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            // Session Destroy working properly
            Session.Clear();
            Session.Abandon();
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return View();
        }

        [HttpPost]
        public ActionResult Login(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP)
        {
            try
            {
                pLG_USER_SETUP_PROFILE_MAP.USER_SESSION_ID = System.Web.HttpContext.Current.Session.SessionID;
                pLG_USER_SETUP_PROFILE_MAP.APPLICATION_ID = "01";
                pLG_USER_SETUP_PROFILE_MAP.IP_ADDRESS = GetLocalIPv4(); //System.Web.HttpContext.Current.Request.UserHostAddress;

                //if (pLG_USER_SETUP_PROFILE_MAP.IP_ADDRESS == "::1")
                //{
                //    pLG_USER_SETUP_PROFILE_MAP.IP_ADDRESS = 127.0.0.1;
                //}

                string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Verify_User_And_Password_For_login/" + pLG_USER_SETUP_PROFILE_MAP.USER_ID + "/" + pLG_USER_SETUP_PROFILE_MAP.PASSWORD + "/" + pLG_USER_SETUP_PROFILE_MAP.USER_SESSION_ID + "/" + pLG_USER_SETUP_PROFILE_MAP.IP_ADDRESS + "/" + pLG_USER_SETUP_PROFILE_MAP.APPLICATION_ID + "?format=json";
                var result = HttpWcfRequest.PostParameter(url);

                System.Web.HttpContext.Current.Session["currentUserID"] = pLG_USER_SETUP_PROFILE_MAP.USER_ID;
                System.Web.HttpContext.Current.Session["appID"] = "01";
                //System.Web.HttpContext.Current.Session["appID"] = "01";

                if (result == "1")
                {
                    // FormsAuthentication.SetAuthCookie(pLg_Sys_User_Setup_Profile.USER_ID, pLg_Sys_User_Setup_Profile.RememberMe);
                    return RedirectToAction("Index", "Home");
                }

                if (result == "2")   // for "restrict multiple login"
                {
                    ViewData["Error"] = "User already logged in!";
                    return View();
                    //TempData["Error"] = "User already logged in!";
                    //return RedirectToAction("Login", "Login");
                }
                if (result == "3")  // for inactive user
                {
                    ViewData["Error"] = "User id is inactive! Pls Contact with system administrator.";
                    return View();
                }
                if (result == "4")  // for first time login
                {
                    return RedirectToAction("Index", "PasswordChange");
                }
                if (result == "6")  // for if user have no role
                {
                    ViewData["Error"] = "You have no role for this application";
                    return View();
                }
                if (result == "7")  // for if user have no role
                {
                    ViewData["Error"] = "domain user is not binded";
                    return View();
                }
                if (result == "8")  // for if user have no role
                {
                    ViewData["Error"] = "User Locked";
                    return View();
                }

                if (result == "0")
                {
                    loginAttempts();
                    string a = Session["loginAttempts"].ToString();
                    string b = ConfigurationManager.AppSettings["MaxFailedAccessAttemptsBeforeLockout"];

                    if (Convert.ToInt16(a) > Convert.ToInt16(b))
                    {
                        url = ConfigurationManager.AppSettings["lgarda_server"] + "/Lock_User_ID/" + pLG_USER_SETUP_PROFILE_MAP.USER_ID + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result == "5")
                        {
                            ViewData["Error"] = "user id Locked!";
                            Session["loginAttempts"] = null;
                        }
                            
                        return View();
                    }

                    ViewData["Error"] = "The User ID and password you entered don't match.";
                    return View();

                    //*** Mitu commented here...dont delete... *** //

                    //string errorMsg = "The user name or password provided is incorrect.";
                    //if (Roles.IsUserInRole(pLG_USER_SETUP_PROFILE_MAP.USER_ID, "Disabled"))
                    //    errorMsg = "Your account has been disabled. Contact webmaster for more info.";
                    //else if (ModelState.IsValid &&
                    //    !WebSecurity.IsAccountLockedOut(pLG_USER_SETUP_PROFILE_MAP.USER_ID, 3, 180) &&
                    //    WebSecurity.Login(pLG_USER_SETUP_PROFILE_MAP.USER_ID, pLG_USER_SETUP_PROFILE_MAP.PASSWORD, persistCookie: pLG_USER_SETUP_PROFILE_MAP.RememberMe))
                    //{
                    //    return RedirectToLocal(returnUrl);
                    //}
                    //if (!WebSecurity.IsConfirmed(model.UserName))
                    //    errorMsg = "You have not completed the registration process. To complete this process look for the email that provides instructions.";

                    //// If we got this far, something failed, redisplay form
                    //ModelState.AddModelError("", errorMsg);
                    //return View(model);
                }
            }
            catch
            {
                ViewData["Error"] = "Service is unable to process the request.";
            }
            return View();
        }

        public ActionResult Logout(LG_USER_SETUP_PROFILE_MAP pLG_USER_SETUP_PROFILE_MAP)
        {
            pLG_USER_SETUP_PROFILE_MAP.USER_SESSION_ID = System.Web.HttpContext.Current.Session.SessionID;
            pLG_USER_SETUP_PROFILE_MAP.APPLICATION_ID = "01";
            pLG_USER_SETUP_PROFILE_MAP.IP_ADDRESS = GetLocalIPv4();
            if (Session["currentUserID"] != null)
            {
                pLG_USER_SETUP_PROFILE_MAP.USER_ID = Session["currentUserID"].ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }

            string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Logout_user/" + pLG_USER_SETUP_PROFILE_MAP.USER_ID + "/" + pLG_USER_SETUP_PROFILE_MAP.USER_SESSION_ID + "/" + pLG_USER_SETUP_PROFILE_MAP.IP_ADDRESS + "/" + pLG_USER_SETUP_PROFILE_MAP.APPLICATION_ID + "?format=json";
            var result = HttpWcfRequest.PostParameter(url);

            if (result == "1")
            {
                Session["currentUserID"] = null; //it's my session variable
                Session.Clear();
                Session.Abandon();
                FormsAuthentication.SignOut(); //you write this when you use FormsAuthentication
            }
            return RedirectToAction("Login");
        }

        public int loginAttempts()
        {
            if (!(Session["loginAttempts"] == null))
            {
                Session["loginAttempts"] = int.Parse(Session["loginAttempts"].ToString()) + 1;
                return int.Parse(Session["loginAttempts"].ToString());
            }
            else
            {
                Session["loginAttempts"] = 1;
                return 1;
            }
        }

        //Added by salekin - 25.07.2017
        public string GetLocalIPv4()
        {
            string localIP = string.Empty;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }
    }
}