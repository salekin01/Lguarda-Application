using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LguardaApplication.Utility
{
    public static class AppSetting
    {
        public static string getLgardaServer()
        {
            return ConfigurationManager.AppSettings["lgarda_server"].ToString();
        }
    }
}