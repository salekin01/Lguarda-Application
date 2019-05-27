using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace LguardaApp.RBAC.Utility
{
    public class HttpWcfRequest
    {
        public static T GetObject1<T>(string url) where T : class, new()
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
                    var dictionary = (IDictionary<string, object>)serializer.DeserializeObject(jsonStringRaw);   //Json string is deserialized to Dictionary
                    var nthValue = dictionary[dictionary.Keys.ToList()[0]];                                      //Only value (key is discarded from dictionary) is extracted from dictionary according to index
                    string jsonObject = serializer.Serialize((object)nthValue);                                  // that value is serializes to json string
                    objTarget = serializer.Deserialize<T>(jsonObject);                                           //Finally json string is deserialized to required object
                    sr.Close();

                }
                response.Close();
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            return objTarget;
        }

        public static string GetString1(string url)
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
            catch (Exception ex)
            {
                //throw ex;
            }
            return result;
        }
    }
}
