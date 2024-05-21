using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CRUDUsingEFUsingMVCApi
{
    public class GlobalConstants
    {
        public static string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["apiAddress"].ToString();
            }
        }
    }
}