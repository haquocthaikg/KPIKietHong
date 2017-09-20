using KPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace KPIKietHong.Models
{
    public class Login
    {
        private readonly string UrlApi = ConfigurationManager.ConnectionStrings["ApiConnection"].ToString();
        //private readonly SessionUser user;
        public async Task<bool> LoginApi(string username,string pass)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", "loginkpikiethong");
                HttpResponseMessage response = await client.GetAsync($"values/Login/{username}/{pass}");
                if (response.IsSuccessStatusCode)
                {
                    try {
                        var user =  response.Content.ReadAsAsync<SessionUser>().Result;
                        System.Web.HttpContext.Current.Session["userid"] = user;
                        return true;
                    } catch
                    {
                        return false;
                    }
                    
                }
                return false;
            }
            //return product;
        }
    }
}