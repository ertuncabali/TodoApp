using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Api.Models
{
    public static class ApiHelper
    {
        public static string Domain = "http://localhost:49616/api/";
        public static string Token = "";
        public static class controllers
        {
            public static string Users = "User/";
            public static string Todo = "Todo/";
            public static string Company = "Company/";
            public static string Status = "Status/";
        }

        public static T Get<T>(string method)
        {
            using (var client = new HttpClient())
            {
                var UseToken = !string.IsNullOrWhiteSpace(Token);
                var url = Domain + method;
                if (UseToken)
                {
                    var byteArray = Encoding.ASCII.GetBytes(string.Concat(Token));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", Token);
                }
                var response = client.GetAsync(url).Result;
                var data = response.Content.ReadAsStringAsync().Result;
                var rrr = new JavaScriptSerializer().Deserialize<T>(data); ;
                return rrr;
            }
        }
        public static K Post<T, K>(string method, T data)
        {
            var UseToken = !string.IsNullOrWhiteSpace(Token);
            using (var client = new HttpClient())
            {
                var url = Domain + method;
                if (UseToken)
                {
                    var byteArray = Encoding.ASCII.GetBytes(string.Concat(Token));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", Token);
                }
                var response = client.PostAsJsonAsync<T>(url, data).Result;
                var statcode = response.StatusCode;
                var dataResult = response.Content.ReadAsStringAsync().Result;
                var rrr = new JavaScriptSerializer().Deserialize<K>(dataResult);
                return rrr;
            }
        }

    }
}