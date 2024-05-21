using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace CRUDUsingEFUsingMVCApi
{
    public class Utility
    {
        public static void AddTokenToRequest(HttpClient client)
        {
            string sessionToken = HttpContext.Current.Session["apitoken"] as string;
            char doubleQuote = '"';
            string token = $"Bearer {sessionToken}".Replace($"{doubleQuote}", "");
            client.DefaultRequestHeaders.Add("Authorization", token);
        }


        //JavaScript Ajax call ke Liye
        public static string GetApiTokenFromSession()
        {
            string sessionToken = HttpContext.Current.Session["apitoken"] as string;
            char doubleQuote = '"';
            string token = $"Bearer {sessionToken}".Replace($"{doubleQuote}", "");
            return token;
        }
    }
}