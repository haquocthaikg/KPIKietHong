using KPIKietHong.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Configuration;

namespace KPIKietHong.Models
{
    public class DataContext<T> : IDataContext<T>
    {
        private readonly string UrlApi = ConfigurationManager.ConnectionStrings["ApiConnection"].ToString();
        public async Task<bool> Create(T item, string api)
        {
            bool check = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
                HttpResponseMessage response = await client.GetAsync($"{api}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return  response.Content.ReadAsAsync<T>().Result;
                }
                return default(T);
            }
            //return product;
        }

        public async Task<bool> Update(int id, T item, string api)
        {
            bool check = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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