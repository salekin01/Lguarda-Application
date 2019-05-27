using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LguardaApplication.Utility
{
    public class HttpWcfRequest
    {
        public static T GetObject<T>(string url) where T : class, new()
        {
            T objTarget = new T();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = 0;

                var response = (HttpWebResponse)request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {

                    StreamReader sr = new StreamReader(responseStream);
                    string jsonStringRaw = sr.ReadToEnd();                                                       //Reading JSON String
                    var serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = int.MaxValue;
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);   //Json string is deserialized to Dictionary
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]];                                      //Only value (key is discarded from dictionary) is extracted from dictionary according to index
                    string jsonObject = serializer.Serialize((object)nthValue);                                  // that value is serializes to json string
                    objTarget = serializer.Deserialize<T>(jsonObject);                                           //Finally json string is deserialized to required object
                    sr.Close();

                }
                response.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return objTarget;
        }
        public static List<T> GetObjectCollection<T>(string url) where T : class, new()
        {
            List<T> objTarget = new List<T>();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream);
                    string jsonStringRaw = sr.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    serializer.MaxJsonLength = int.MaxValue;
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]];
                    string jsonObject = serializer.Serialize((object)nthValue);
                    objTarget = serializer.Deserialize<List<T>>(jsonObject);
                    sr.Close();
                }
                response.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return objTarget;
        }
        public static string GetString(string url)
        {
            string result = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = 0;

                var response = (HttpWebResponse)request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream);
                    string jsonStringRaw = sr.ReadToEnd();                                                       //Reading JSON String
                    var serializer = new JavaScriptSerializer();
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);   //Json string is deserialized to Dictionary
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]];                                      //Only value (key is discarded from dictionary) is extracted from dictionary according to index
                    string jsonObject = serializer.Serialize((object)nthValue);                                  // that value is serializes to json string
                    result = serializer.Deserialize<string>(jsonObject);                                         //Finally json string is deserialized to required format
                    sr.Close();
                }
                response.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string PostParameter(string url)
        {
            string result = string.Empty;
            try
            {
                Uri uri = new Uri(url);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentLength = 0;
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream);
                    string jsonStringRaw = sr.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);   //Json string is deserialized to Dictionary
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]];                                      //Only value (key is discarded from dictionary) is extracted from dictionary according to index
                    string jsonObject = serializer.Serialize((object)nthValue);                                  // that value is serializes to json string
                    result = serializer.Deserialize<string>(jsonObject);                                         //Finally json string is deserialized to required format
                    sr.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public static T PostObject_WithReturningObject<T>(Uri url, T obj_Generic) where T : class,new()
        {
            T objTarget = new T();

            try
            {
                var serilizer = new DataContractJsonSerializer(typeof(T));
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";

                using (var requestStream = request.GetRequestStream())
                {
                    serilizer.WriteObject(requestStream, obj_Generic);

                    var response = (HttpWebResponse)request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    var dcs = new DataContractJsonSerializer(typeof(T));
                    objTarget = (T)dcs.ReadObject(responseStream);
                    response.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return objTarget;
        }
        public static string PostObject_WithReturningString<T>(Uri url, T obj_Generic) where T : class,new()
        {
            string result = string.Empty;
            try
            {
                var serilizer = new DataContractJsonSerializer(typeof(T));
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";


                using (var requestStream = request.GetRequestStream())
                {
                    serilizer.WriteObject(requestStream, obj_Generic);

                    var response = (HttpWebResponse)request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    var dcs = new DataContractJsonSerializer(typeof(string));
                    result = (string)dcs.ReadObject(responseStream);
                    response.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return result;
        }


        // method added by naiem for file uploading
        public static string PostObjectFile_WithReturningString<T>(Uri url, T obj_Generic) where T : class,new()
        {
            string result = string.Empty;
            try
            {
                var serilizer = new DataContractJsonSerializer(typeof(T));
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
               // request.ContentType = "application/json; charset=utf-8";
                request.Timeout = 2139999999;
                //request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1000;

                using (var requestStream = request.GetRequestStream())
                {
                    serilizer.WriteObject(requestStream, obj_Generic);

                    var response = (HttpWebResponse)request.GetResponse();
                    var responseStream = response.GetResponseStream();
                    var dcs = new DataContractJsonSerializer(typeof(string));
                    result = (string)dcs.ReadObject(responseStream);
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        public static List<T> GetObjectCollection_GET<T>(string url, string key) where T : class, new()
        {
            List<T> targerObjects = new List<T>();
            T targetObject = new T();

            try
            {
                var request = (HttpWebRequest) WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse) request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream);
                    string jsonString = sr.ReadToEnd();
                    JObject jsonObject = JObject.Parse(jsonString);
                    IList<JToken> results = jsonObject[key].ToList();

                    foreach (JToken result in results)
                    {
                        targetObject = JsonConvert.DeserializeObject<T>(result.ToString());
                        targerObjects.Add(targetObject);
                    }
                }
            }

                //Exception From JObject.Parse 
                //(Internet=ok, client.GetAsync()=ok but returned object is not valid Json object )
            catch (System.InvalidOperationException e)
            {
                // ServiceInvoke_Failure("Invalid operation");
                targetObject.GetType().GetProperty("errorCode").SetValue(targetObject, 1, null);
                targetObject.GetType()
                    .GetProperty("errorMessage")
                    .SetValue(targetObject, "Unable to Connect to the Remote Server", null);

                targerObjects.Add(targetObject);
            }

                //Exception From JObject.Parse 
                //(Internet=ok, client.GetAsync()=ok but returned object is not valid Json object )
            catch (JsonReaderException e)
            {
                // ServiceInvoke_Failure("Unable to parse returned json object");
                targetObject.GetType().GetProperty("errorCode").SetValue(targetObject, 2, null);
                targetObject.GetType()
                    .GetProperty("errorMessage")
                    .SetValue(targetObject, "App Error, please try later", null);

                targerObjects.Add(targetObject);
            }

                //Only Json Serialization and Deserialization Exception
            catch (JsonSerializationException e)
            {
                // ServiceInvoke_Failure("Unable to deserialize returned json object");
                targetObject.GetType().GetProperty("errorCode").SetValue(targetObject, 3, null);
                targetObject.GetType()
                    .GetProperty("errorMessage")
                    .SetValue(targetObject, "App Error, please try later", null);
                targerObjects.Add(targetObject);
            }

                //Exception from JsonConvert.DeserializeObject
                //Json Serialization and Deserialization Exception Plus Super Type
            catch (JsonException e)
            {
                //  ServiceInvoke_Failure("Supter type json error");
                targetObject.GetType().GetProperty("errorCode").SetValue(targetObject, 4, null);
                targetObject.GetType()
                    .GetProperty("errorMessage")
                    .SetValue(targetObject, "App Error, please try later", null);
                targerObjects.Add(targetObject);
            }

                //Exception from client.GetAsync
                //When unable to connect to service (such as No Interner connection)
            catch (System.AggregateException e)
            {
                //  ServiceInvoke_Failure("Please check internet connection");
                targetObject.GetType().GetProperty("errorCode").SetValue(targetObject, 5, null);
                targetObject.GetType()
                    .GetProperty("errorMessage")
                    .SetValue(targetObject, "App Error, please try later", null);
                targerObjects.Add(targetObject);
            }

                //Exception from client.GetAsync
            catch (HttpRequestException e)
            {
                targetObject.GetType().GetProperty("errorCode").SetValue(targetObject, 6, null);
                targetObject.GetType()
                    .GetProperty("errorMessage")
                    .SetValue(targetObject, "Access Denied by Remote Server", null);

                targerObjects.Add(targetObject);
            }

            catch (Exception e)
            {
                //  ServiceInvoke_Failure("Contact with provider");
                targetObject.GetType().GetProperty("errorCode").SetValue(targetObject, 99, null);
                targetObject.GetType()
                    .GetProperty("errorMessage")
                    .SetValue(targetObject, "App Error, please try later", null);
                targerObjects.Add(targetObject);
            }

            return targerObjects;
        }
        public static List<string> GetStringObjectCollection(string url)
        {
            List<string> objTarget = new List<string>();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";
                var response = (HttpWebResponse)request.GetResponse();

                using (var responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream);
                    string jsonStringRaw = sr.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]];
                    string jsonObject = serializer.Serialize((object)nthValue);
                    objTarget = serializer.Deserialize<List<string>>(jsonObject);
                    sr.Close();
                }
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objTarget;
        }

    }
}