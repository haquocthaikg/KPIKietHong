using KPIKietHong.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;
using KPI.Models;

namespace KPIKietHong.Models
{
    public class DataContext<T> : IDataContext<T>
    {
        private readonly string UrlApi = ConfigurationManager.ConnectionStrings["ApiConnection"].ToString();
        private readonly SessionUser user= (SessionUser) System.Web.HttpContext.Current.Session["userid"];
       
        public async Task<bool> Create(T item, string api)
        {
            bool check = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token",user.Tolken);
                HttpResponseMessage response = await client.PostAsJsonAsync(api, item);
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var a = response.Content.ReadAsAsync<T>().Result;
                        check = a == null ? false : true;
                    }
                    catch
                    {
                        check = false;
                    }
                }
            }
            return check;
        }

        public async Task<bool> Delete(int? id, string api)
        {
            bool check = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", user.Tolken);
                HttpResponseMessage response = await client.DeleteAsync($"{api}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    check = false;
                }
            }
            return check;
        }

        public async Task<IEnumerable<T>> GetList(string api)
        {
            IEnumerable<T> product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", user.Tolken);
                HttpResponseMessage response = await client.GetAsync(api);
                if (response.IsSuccessStatusCode)
                {
                    product = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                }
            }
            return product;
        }

        public async Task<T> GetList(int id, string api)
        {
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", user.Tolken);
                HttpResponseMessage response = await client.GetAsync($"{api}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return  response.Content.ReadAsAsync<T>().Result;
                }
                return default(T);
            }
            
        }

        public async Task<IEnumerable<T>> GetListBy(int id, string api)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", user.Tolken);
                HttpResponseMessage response = await client.GetAsync(api+"/"+id);
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                }
                return default(IEnumerable<T>);
            }
        }

        public async Task<bool> Update(int id, T item, string api)
        {
            bool check = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", user.Tolken);
                HttpResponseMessage response = await client.PutAsJsonAsync($"{api}/{id}", item);
                if (!response.IsSuccessStatusCode)
                {
                    check = false;
                }
            }
            return check;
        }
    }
}