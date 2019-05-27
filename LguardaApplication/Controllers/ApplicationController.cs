//using LguardaApplication.Action_Filters;
using LguardaApplication.Utility;
using LgurdaApp.Model;
using LgurdaApp.Model.ControllerModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using LguardaApp.RBAC.Action_Filters;

namespace LguardaApplication.Controllers
{
    
    public class ApplicationController : Controller
    {
        #region Class Level Variable
        List<LG_FNR_APPLICATION_MAP> LIST_LG_FNR_APPLICATION_MAP = new List<LG_FNR_APPLICATION_MAP>();
        LG_FNR_APPLICATION_MAP OBJ_LG_FNR_APPLICATION_MAP = new LG_FNR_APPLICATION_MAP();

        #endregion

        [RBAC]
        public ActionResult Index()
        {
            try
            {
                //LguardaApp.Session.LguardaSession.LguardaSessionContainer.APPLICATION_ID = "01";

                string url = Utility.AppSetting.getLgardaServer() + "/Get_Applications" + "?format=json";
                LIST_LG_FNR_APPLICATION_MAP = HttpWcfRequest.GetObjectCollection<LG_FNR_APPLICATION_MAP>(url);
                if (TempData["Success"] != null)
                { ViewData["Success"] = TempData["Success"].ToString(); }
                if (TempData["Error"] != null)
                { ViewData["Error"] = TempData["Error"].ToString(); }
                return View(LIST_LG_FNR_APPLICATION_MAP);

                #region Salekin Test(don't delete)
                #region WAY-1  Retrive xml Data(solved) [web reference]
                /*
                LgardaServiceRef.LgardaServiceClient Obj_Service = new LgardaServiceRef.LgardaServiceClient();
                LgardaServiceRef.Lg_Sys_Application[] Obj_Lg_Sys_Application = Obj_Service.GetApplications();
                //Session["SYS_APPLICATION"] = Obj_Lg_Sys_Application;
                return View(Obj_Lg_Sys_Application);
                */
                #endregion

                #region Test Purpose var.0.2(Fetching object & string)

                //string url = Utility.AppSetting.getLgardaServer() + "/Get_Applications" + "?format=json";
                //var objTarget = GetObjectCollection<Lg_Sys_Application>(url);
                //List<Lg_Sys_Application> List_Lg_Sys_Application = new List<Lg_Sys_Application>();
                //List_Lg_Sys_Application = objTarget;
                //return null;


                //string url = Utility.AppSetting.getLgardaServer() + "/Get_String" + "?format=json";
                //string result = GetString(url);
                //return null;
                #endregion

                #region POST & GET Using HTTP Request For Test Purpose
                /*
                string id1 = "1";
                string id2 = "2";
                string url = Utility.AppSetting.getLgardaServer() + "/Post_String/" + id1 + "/" + id2 + "?format=json";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentLength = 0;
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream);
                    string jsonStringRaw = sr.ReadToEnd(); //Reading JSON String
                    var serializer = new JavaScriptSerializer();
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);  //Json string is deserialized to Dictionary
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]]; //Only value (key is discarded from dictionary) is extracted from dictionary according to index
                    string jsonObject = serializer.Serialize((object)nthValue); // that value is serializes to json string
                    string a = serializer.Deserialize<string>(jsonObject);  //Finally json string is deserialized to required object
                    sr.Close();
                }

                */
                #endregion

                #region WAY-2 Get xml Data(unsolved) ****Don't Delete****
                /*
            //var url = new Uri("http://localhost/LgardaService/LgardaService.svc/Get_Applications?format=xml");
            string url = AppSetting.getLgardaServer() + "/Get_Applications?format=xml";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/xml; charset=utf-8";
            var response = (HttpWebResponse)request.GetResponse();

            IEnumerable<LgardaServiceRef.Lg_Sys_Application> Obj_SYS_APPLICATION_List;
            var dataContractSerializer = new DataContractSerializer(typeof(IEnumerable<LgardaServiceRef.Lg_Sys_Application>));
            using (var responseStream = response.GetResponseStream())
            {
                //StreamReader reader = new StreamReader(responseStream);
                //XmlSerializer serializer = new XmlSerializer(typeof(LgardaServiceRef.SYS_APPLICATION));
                //Obj_SYS_APPLICATION_List = (IEnumerable<LgardaServiceRef.SYS_APPLICATION>)serializer.Deserialize(reader);

                Obj_SYS_APPLICATION_List = (IEnumerable<LgardaServiceRef.Lg_Sys_Application>)dataContractSerializer.ReadObject(responseStream);
            }
            return View(Obj_SYS_APPLICATION_List);  */
                #endregion

                #region WAY-3 Get json Data(solved) ****Don't Delete****
                //WAY-3 Retrive json data      ****Don't Delete****
                /*
                string url = AppSetting.getLgardaServer() + "/Get_String?format=json";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();

                IEnumerable<LgardaServiceRef.Lg_Sys_Application> Obj_SYS_APPLICATION_List;
                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream);
                    string jsonStringRaw = sr.ReadToEnd(); //Reading JSON String
                    var serializer = new JavaScriptSerializer();
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);  //Json string is deserialized to Dictionary
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]]; //Only value (key is discarded from dictionary) is extracted from dictionary according to index
                    string jsonObject = serializer.Serialize((object)nthValue); // that value is serializes to json string
                    Obj_SYS_APPLICATION_List = serializer.Deserialize<IEnumerable<LgardaServiceRef.Lg_Sys_Application>>(jsonObject);  //Finally json string is deserialized to required object
                    sr.Close();

                }
                return View(Obj_SYS_APPLICATION_List); */
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show application info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show application info.";
                }
                return View(LIST_LG_FNR_APPLICATION_MAP);
            }
        }
        [RBAC]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_ByAppId/" + id + "?format=json";
                OBJ_LG_FNR_APPLICATION_MAP = HttpWcfRequest.GetObject<LG_FNR_APPLICATION_MAP>(url);

                if (OBJ_LG_FNR_APPLICATION_MAP != null)
                {
                    OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't show application details. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't show application details.";
                }
            }
            return View(OBJ_LG_FNR_APPLICATION_MAP);
        }


        [RBAC]
        public ActionResult Create()
        {
            //var abc = System.Web.HttpContext.Current.Session["__LGUARDA__LguardaSessionContainer"];

            string url = Utility.AppSetting.getLgardaServer() + "/GetAppTypesForDD" + "?format=json";
            OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = HttpWcfRequest.GetObjectCollection<SelectListItem>(url);
            TempData["LIST_APP_TYPE"] = OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE;

            if (TempData["Success"] != null)
            { 
                ViewData["Success"] = TempData["Success"].ToString(); 
            }
            if (TempData["Error"] != null)
            { 
                ViewData["Error"] = TempData["Error"].ToString(); 
            }
            return View(OBJ_LG_FNR_APPLICATION_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Create(LG_FNR_APPLICATION_MAP pLG_FNR_APPLICATION_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Save")
                {
                    if(ModelState.IsValid)
                    {
                       
                        
                        string session_user= Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Add_Application/" + pLG_FNR_APPLICATION_MAP.APPLICATION_NAME + "/" + session_user + "/" + pLG_FNR_APPLICATION_MAP.APP_TYPE_ID + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        #region Salekin Test(Don't delete)
                        #region Fetch Object by web reference
                        /*
                    LgardaServiceRef.LgardaServiceClient Obj_Service = new LgardaServiceRef.LgardaServiceClient();
                    LgardaServiceRef.Lg_Sys_Application Obj_Lg_Sys_Application = new LgardaServiceRef.Lg_Sys_Application();
                    Obj_Lg_Sys_Application.APPLICATION_ID = pObj_Lg_Sys_Application.APPLICATION_ID;
                    Obj_Lg_Sys_Application.APPLICATION_NAME = pObj_Lg_Sys_Application.APPLICATION_NAME;
                    Obj_Lg_Sys_Application.MAKE_BY = "admin";
                    LgardaServiceRef.Bool_Check Obj_Bool_Check = Obj_Service.AddApplication(Obj_Lg_Sys_Application);  */
                        #endregion

                        #region Test Purpose var.0.2(passing object)
                        /*
                    Bool_Check Obj_Bool_Check = null;

                    Lg_Sys_Application Obj_new_Lg_Sys_Application = new Lg_Sys_Application();
                    Obj_new_Lg_Sys_Application.APPLICATION_ID = pObj_Lg_Sys_Application.APPLICATION_ID;
                    Obj_new_Lg_Sys_Application.APPLICATION_NAME = pObj_Lg_Sys_Application.APPLICATION_NAME;
                    Obj_new_Lg_Sys_Application.MAKE_BY = "admin";
                    Obj_new_Lg_Sys_Application.MAKE_DT = System.DateTime.Now;

                    string url = Utility.AppSetting.getLgardaServer() + "/Add_Application?format=json";
                    Uri uri = new Uri(url);
                    Obj_Bool_Check = PostObject_WithReturningBool<Lg_Sys_Application>(uri, Obj_new_Lg_Sys_Application);*/
                        #endregion
                        #region Test Purpost v.0.1
                        /*
                    string url = Utility.AppSetting.getLgardaServer() + "/Product_Test?format=json";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    //var objTarget = Obj_Service.AddApplication(url, Obj_SYS_APPLICATION);

                    PRODUCT_INFO OBJ_PRODUCT_INFO = new PRODUCT_INFO();
                    OBJ_PRODUCT_INFO.Discontinued = "1";
                    OBJ_PRODUCT_INFO.ProductID = 0;
                    OBJ_PRODUCT_INFO.ProductName = "salekin";
                    OBJ_PRODUCT_INFO.QuantityPerUnit = "7";
                    OBJ_PRODUCT_INFO.UnitPrice = 2000;

                    string postData = OBJ_PRODUCT_INFO.Discontinued + OBJ_PRODUCT_INFO.ProductID + OBJ_PRODUCT_INFO.ProductName + OBJ_PRODUCT_INFO.QuantityPerUnit + OBJ_PRODUCT_INFO.UnitPrice;
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] byte1 = encoding.GetBytes(postData);

                    var serilizer = new DataContractJsonSerializer(typeof(PRODUCT_INFO));

                    request.Method = "POST";
                    request.ContentLength = byte1.Length;
                    request.ContentType = "application/json; charset=utf-8";

                    LgardaServiceRef.Bool_Check Obj_Bool_Check;
                    using (var requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(byte1, 0, byte1.Length);
                        serilizer.WriteObject(requestStream, OBJ_PRODUCT_INFO);

                        var response = (HttpWebResponse)request.GetResponse();
                        var responseStream = response.GetResponseStream();
                        var dcs = new DataContractJsonSerializer(typeof(LgardaServiceRef.Bool_Check));
                        Obj_Bool_Check = (LgardaServiceRef.Bool_Check)dcs.ReadObject(responseStream);

                        response.Close();
                    }  */
                        #endregion

                        #region Way-3 Post Json Data
                        //WAY-3
                        /*
                        string strUri = Utility.AppSetting.getLgardaServer() + "/Add_Application?format=json";
                        var request = (HttpWebRequest)HttpWebRequest.Create(strUri);

                        request.Accept = "application/json";
                        request.ContentType = "application/json";
                        request.Method = "POST";

                        var serializer = new DataContractJsonSerializer(typeof(LgardaServiceRef.SYS_APPLICATION));
                        var requestStream = request.GetRequestStream();
                        serializer.WriteObject(requestStream, Obj_SYS_APPLICATION);

                        requestStream.Close();

                        var response = request.GetResponse();
                        if(response.ContentLength == 0)
                        {
                            response.Close();
                        }

                        var responseStream = response.GetResponseStream();
                        LgardaServiceRef.SYS_APPLICATION responseObject = (LgardaServiceRef.SYS_APPLICATION)serializer.ReadObject(responseStream);

                        responseStream.Close();  */
                        #endregion
                        #region Way-2 Post Json Data
                        /*
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string output = serializer.Serialize(Obj_SYS_APPLICATION);

                    string strUri = Utility.AppSetting.getLgardaServer() + "/Add_Application?format=json";
                    Uri uri = new Uri(strUri);
                    WebRequest request = WebRequest.Create(uri);
                    request.Method = "POST";
                    request.ContentType = "application/json; charset=utf-8";

                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    string serOut = jsonSerializer.Serialize(Obj_SYS_APPLICATION);

                    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(serOut);
                    }

                    WebResponse responce = request.GetResponse();
                    Stream reader = responce.GetResponseStream();

                    StreamReader sReader = new StreamReader(reader);
                    string outResult = sReader.ReadToEnd();
                    sReader.Close();  */
                        #endregion
                        #region Way-1 Post Json Data (Final)
                        /*
                    string url = Utility.AppSetting.getLgardaServer() + "/Add_Application?format=json";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    //var objTarget = Obj_Service.AddApplication(url, Obj_SYS_APPLICATION);
                    var serilizer = new DataContractJsonSerializer(typeof(LgardaServiceRef.SYS_APPLICATION));
                    
                    request.Method = "POST";
                    request.ContentType = "application/json; charset=utf-8";

                    LgardaServiceRef.Bool_Check Obj_Bool_Check;
                    using (var requestStream = request.GetRequestStream())
                    {
                        serilizer.WriteObject(requestStream, Obj_SYS_APPLICATION);

                        var response = (HttpWebResponse)request.GetResponse();
                        var responseStream = response.GetResponseStream();
                        var dcs = new DataContractJsonSerializer(typeof(LgardaServiceRef.Bool_Check));
                        Obj_Bool_Check = (LgardaServiceRef.Bool_Check)dcs.ReadObject(responseStream);

                        response.Close();
                    }  */
                        #endregion
                        #endregion

                        if (result != string.Empty && result.ToLower().Contains("already exists"))
                        {
                            //ModelState.Clear();
                            ModelState.AddModelError("APPLICATION_NAME", "Application name already exists.");
                            OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                            return View(OBJ_LG_FNR_APPLICATION_MAP);
                        }
                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            TempData["Success"] = "Added successfully.";                         
                            return RedirectToAction("Create");
                        }
                        else
                        {
                            ViewData["Error"] = "Can't add application.";
                            OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                            return View(OBJ_LG_FNR_APPLICATION_MAP);
                        }
                    }
                    else
                    {
                        OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                        return View(OBJ_LG_FNR_APPLICATION_MAP);
                    }
                }
                OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                return View(OBJ_LG_FNR_APPLICATION_MAP);
            }
            catch(Exception ex)             
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't add application. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't add application. Please contact with system administrator";
                }
                OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                return View(OBJ_LG_FNR_APPLICATION_MAP);
            }
        }


        [RBAC]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View();
            }
            try
            {
                
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_ByAppId/" + id + "?format=json";
                OBJ_LG_FNR_APPLICATION_MAP = HttpWcfRequest.GetObject<LG_FNR_APPLICATION_MAP>(url);

                if (OBJ_LG_FNR_APPLICATION_MAP != null)
                {
                    OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }

                TempData["LIST_APP_TYPE"] = OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE;
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    ViewData["Error"] = "Can't edit application info. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't edit application info.";
                }
            }
            return View(OBJ_LG_FNR_APPLICATION_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "APPLICATION_ID,APPLICATION_NAME,APP_TYPE_ID")] LG_FNR_APPLICATION_MAP pOBJ_LG_FNR_APPLICATION_MAP, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Update")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Update_Application/" + pOBJ_LG_FNR_APPLICATION_MAP.APPLICATION_ID + "/" + pOBJ_LG_FNR_APPLICATION_MAP.APPLICATION_NAME + "/" + session_user + "/" + pOBJ_LG_FNR_APPLICATION_MAP.APP_TYPE_ID + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        #region Way-1 Post Json Data (Final)
                        /* 
                    string url = Utility.AppSetting.getLgardaServer() + "/Update_Application?format=json";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    //var objTarget = Obj_Service.AddApplication(url, Obj_SYS_APPLICATION);
                    var serilizer = new DataContractJsonSerializer(typeof(LgardaServiceRef.SYS_APPLICATION));
                    
                    request.Method = "POST";
                    request.ContentType = "application/json; charset=utf-8";

                    LgardaServiceRef.Bool_Check Obj_Bool_Check;
                    using (var requestStream = request.GetRequestStream())
                    {
                        serilizer.WriteObject(requestStream, Obj_SYS_APPLICATION);

                        var response = (HttpWebResponse)request.GetResponse();
                        var responseStream = response.GetResponseStream();
                        var dcs = new DataContractJsonSerializer(typeof(LgardaServiceRef.Bool_Check));
                        Obj_Bool_Check = (LgardaServiceRef.Bool_Check)dcs.ReadObject(responseStream);

                        response.Close();
                    }  */
                        #endregion

                        if (result != string.Empty && result.ToLower().Contains("no changes"))
                        {
                            ModelState.AddModelError("APPLICATION_NAME", "No changes made.");
                            OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                            return View(OBJ_LG_FNR_APPLICATION_MAP);
                        }

                        if (result != string.Empty && result.ToLower() == "true")
                        {
                            ViewData["Success"] = "Updated Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                            return View(OBJ_LG_FNR_APPLICATION_MAP);
                        }
                    }
                    else
                    {
                        OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                        return View(OBJ_LG_FNR_APPLICATION_MAP);
                    }
                }
                OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                return View(OBJ_LG_FNR_APPLICATION_MAP);
            }
            catch (Exception ex)      
            {
                if (ex.Message.Contains("Service")) 
                {
                    ViewData["Error"] = "Can't update application. Service is unable to process the request.";
                }
                else
                {
                    ViewData["Error"] = "Can't update application.";
                }
                OBJ_LG_FNR_APPLICATION_MAP.LIST_APP_TYPE = (List<SelectListItem>)TempData["LIST_APP_TYPE"];
                return View(OBJ_LG_FNR_APPLICATION_MAP);
            }
        }

        [RBAC]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return View();
            }
            try
            {
                string url = Utility.AppSetting.getLgardaServer() + "/Get_Application_ByAppId/" + id + "?format=json";
                OBJ_LG_FNR_APPLICATION_MAP = HttpWcfRequest.GetObject<LG_FNR_APPLICATION_MAP>(url);
                if (OBJ_LG_FNR_APPLICATION_MAP != null)
                {
                    OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT = Convert.ToDateTime(OBJ_LG_FNR_APPLICATION_MAP.MAKE_DT).AddHours(6);
                    if (OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT != null)
                    {
                        OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT = Convert.ToDateTime(OBJ_LG_FNR_APPLICATION_MAP.LAST_UPDATE_DT).AddHours(6);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't show application details. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't show application details.";
                }
            }
            return View(OBJ_LG_FNR_APPLICATION_MAP);
        }
        [RBAC]
        [HttpPost]
        public ActionResult Delete(string id, string command)
        {
            try
            {
                string result = string.Empty;
                if (command == "Delete")
                {
                    if (ModelState.IsValid)
                    {
                        string session_user = Session["currentUserID"].ToString();
                        string url = ConfigurationManager.AppSettings["lgarda_server"] + "/Delete_Application/" + id + "/" + session_user + "?format=json";
                        result = HttpWcfRequest.PostParameter(url);

                        if (result != string.Empty && result == "true")
                        {
                            TempData["Success"] = "Deleted Successfully.";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View("Delete");
                        }
                    }
                    else
                    {
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Service"))
                {
                    TempData["Error"] = "Can't delete application. Service is unable to process the request.";
                }
                else
                {
                    TempData["Error"] = "Can't delete application.";
                }
                return View();
            }
        }
    }
}
