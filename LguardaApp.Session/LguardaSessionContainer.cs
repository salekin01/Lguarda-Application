using System;
using System.Web;

namespace LguardaApp.Session
{
    public class LguardaSession
    {
        //Updated By salekin - 06.02.2018

        #region Private Variables
        private string _APPLICATION_ID;
        private string _USER_ID;
        private string _USER_NAME;
        private string _USER_TYPE;
        private string _TRANS_DATE;
        private string _BRANCH_ID;
        private string _BRANCH_NM;
        private string _BANK_ID;
        private string _BANK_NM;
        private string _FUNCTION_ID;
        private string _FUNCTION_NAME;
        private string _SESSION_ID;
        private string _MOBILE_IME;
        private string _IP_ADDRESS;
        private object _LGURDA_OBJECT_CLASS;
        #endregion

        #region GET SET
        public string APPLICATION_ID
        {
            get { return _APPLICATION_ID; }
            set
            {
                _APPLICATION_ID = value;
                LguardaSessionContainer = this;
            }
        }
        public string USER_ID
        {
            get { return _USER_ID; }
            set
            {
                _USER_ID = value;
                LguardaSessionContainer = this;
            }
        }
        public string USER_NAME
        {
            get { return _USER_NAME; }
            set
            {
                _USER_NAME = value;
                LguardaSessionContainer = this;
            }
        }
        public string USER_TYPE
        {
            get { return _USER_TYPE; }
            set
            {
                _USER_TYPE = value;
                LguardaSessionContainer = this;
            }
        }
        public string BRANCH_ID
        {
            get { return _BRANCH_ID; }
            set
            {
                _BRANCH_ID = value;
                LguardaSessionContainer = this;
            }
        }
        public string BRANCH_NM
        {
            get { return _BRANCH_NM; }
            set
            {
                _BRANCH_NM = value;
                LguardaSessionContainer = this;
            }
        }
        public string FUNCTION_ID
        {
            get { return _FUNCTION_ID; }
            set
            {
                _FUNCTION_ID = value;
                LguardaSessionContainer = this;
            }
        }
        public string FUNCTION_NAME
        {
            get { return _FUNCTION_NAME; }
            set
            {
                _FUNCTION_NAME = value;
                LguardaSessionContainer = this;
            }
        }
        public string BANK_ID
        {
            get { return _BANK_ID; }
            set
            {
                _BANK_ID = value;
                LguardaSessionContainer = this;
            }
        }
        public string BANK_NM
        {
            get { return _BANK_NM; }
            set
            {
                _BANK_NM = value;
                LguardaSessionContainer = this;
            }
        }
        public string TRANS_DATE
        {
            get { return _TRANS_DATE; }
            set
            {
                _TRANS_DATE = value;
                LguardaSessionContainer = this;
            }
        }
        public string SESSION_ID
        {
            get { return _SESSION_ID; }
            set
            {
                _SESSION_ID = value;
                LguardaSessionContainer = this;
            }
        }
        public string MOBILE_IME
        {
            get { return _MOBILE_IME; }
            set
            {
                _MOBILE_IME = value;
                LguardaSessionContainer = this;
            }
        }
        public string IP_ADDRESS
        {
            get { return _IP_ADDRESS; }
            set
            {
                _IP_ADDRESS = value;
                LguardaSessionContainer = this;
            }
        }
        public object LGURDA_OBJECT_CLASS
        {
            get { return _LGURDA_OBJECT_CLASS; }
            set
            {
                _LGURDA_OBJECT_CLASS = value;
                LguardaSessionContainer = this;
            }
        }
        #endregion

        #region  Session Utility
        private const string SESSION_KEY_PREFIX = "__LGUARDA__";
        public static LguardaSession LguardaSessionContainer
        {
            set
            {
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[SESSION_KEY_PREFIX + "LguardaSessionContainer"] = value;
                }
            }
            get
            {
                if (HttpContext.Current.Session != null)
                {
                    if (HttpContext.Current.Session[SESSION_KEY_PREFIX + "LguardaSessionContainer"] != null)
                    {
                        return (LguardaSession)HttpContext.Current.Session[SESSION_KEY_PREFIX + "LguardaSessionContainer"];
                    }
                    else
                    {
                        return new LguardaSession();
                    }
                }
                else
                    return new LguardaSession();
            }
        }
        #endregion







        /*
        #region Private Variables

    
        private string _APPLICATION_ID;
      
        private string _USER_ID;

        private string _USER_NAME;
        private string _USER_TYPE;
        private string _TRANS_DATE;
        private string _BRANCH_ID;
        private string _BRANCH_NM;
        private string _BANK_ID;
        private string _BANK_NM;
        private string _FUNCTION_ID;
        private string _FUNCTION_NAME;
        private string _SESSION_ID;
        private string _MOBILE_IME;
        private string _IP_ADDRESS;

        private object _LGURDA_OBJECT_CLASS;

        #endregion

        #region GET SET
   
        public string APPLICATION_ID
        {
            get { return _APPLICATION_ID; }
            set
            {
                _APPLICATION_ID = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string USER_ID
        {
            get { return _USER_ID; }
            set
            {
                _USER_ID = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string USER_NAME
        {
            get { return _USER_NAME; }
            set
            {
                _USER_NAME = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string USER_TYPE
        {
            get { return _USER_TYPE; }
            set
            {
                _USER_TYPE = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }

     
        public string BRANCH_ID
        {
            get { return _BRANCH_ID; }
            set
            {
                _BRANCH_ID = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string BRANCH_NM
        {
            get { return _BRANCH_NM; }
            set
            {
                _BRANCH_NM = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string FUNCTION_ID
        {
            get { return _FUNCTION_ID; }
            set
            {
                _FUNCTION_ID = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string FUNCTION_NAME
        {
            get { return _FUNCTION_NAME; }
            set
            {
                _FUNCTION_NAME = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string BANK_ID
        {
            get { return _BANK_ID; }
            set
            {
                _BANK_ID = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string BANK_NM
        {
            get { return _BANK_NM; }
            set
            {
                _BANK_NM = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
     
        public string TRANS_DATE
        {
            get { return _TRANS_DATE; }
            set
            {
                _TRANS_DATE = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }

        public string SESSION_ID
        {
            get { return _SESSION_ID; }
            set
            {
                _SESSION_ID = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }

        public string MOBILE_IME
        {
            get { return _MOBILE_IME; }
            set
            {
                _MOBILE_IME = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public string IP_ADDRESS
        {
            get { return _IP_ADDRESS; }
            set
            {
                _IP_ADDRESS = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        public object LGURDA_OBJECT_CLASS
        {
            get { return _LGURDA_OBJECT_CLASS; }
            set
            {
                _LGURDA_OBJECT_CLASS = value;
                LguardaSessionUtility.LguardaSessionContainer = this;
            }
        }
        #endregion

        #region  Session Utility
        public class LguardaSessionUtility
        {
            private const string SESSION_KEY_PREFIX = "__LGUARDA__";
            public static LguardaSession LguardaSessionContainer
            {
                set
                {
                    if (HttpContext.Current.Session != null)
                    {
                        HttpContext.Current.Session[SESSION_KEY_PREFIX + "LguardaSessionContainer"] = value;
                    }
                }
                get
                {
                    if (HttpContext.Current.Session != null)
                    {
                        if (HttpContext.Current.Session[SESSION_KEY_PREFIX + "LguardaSessionContainer"] != null)
                        {
                            return (LguardaSession)HttpContext.Current.Session[SESSION_KEY_PREFIX + "LguardaSessionContainer"];
                        }
                        else
                        {
                            return new LguardaSession();
                        }
                    }
                    else
                        return new LguardaSession();
                }
            }
        }
        #endregion
        */

    }
}
