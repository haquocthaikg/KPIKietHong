using KPIKietHong.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPIKietHong.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        private readonly string UrlApi = ConfigurationManager.ConnectionStrings["ApiConnection"].ToString();
        public ActionResult login()
        {
            return View();
        }

        public async Task<JsonResult> CheckEmail(string Email)
        {
            bool result = true;
            DataContext<Tblnhanvien> dataNhanVien = new DataContext<Tblnhanvien>();
            string apinv = "values/NhanVien";
            var a = await dataNhanVien.GetList(apinv);
            var email = a.Where(x => x.Email == Email).FirstOrDefault();

            if (email != null)
                result = false;

            // return JsonType(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> CheckUser(string UserName)
        {
            bool result = true;
            DataContext<Tblnhanvien> dataNhanVien = new DataContext<Tblnhanvien>();
            string apinv = "values/NhanVien";
            var a = await dataNhanVien.GetList(apinv);
            var email = a.Where(x => x.Username == UserName).FirstOrDefault();

            if (email != null)
                result = false;

            // return JsonType(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        //Kiểm tra mail khi quên mật khẩu

        public async Task<JsonResult> CheckEmailForgetPass(string Email)
        {
            bool result = false;
           
            string apinv = "values/NhanVien";
            var a = await GetList(apinv);
            var email = a.Where(x => x.Email == Email).FirstOrDefault();

            if (email != null)
                result = true;

            // return JsonType(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<IEnumerable<Tblnhanvien>> GetList(string api)
        {
            IEnumerable<Tblnhanvien> product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", "getallnhanvien");
                HttpResponseMessage response = await client.GetAsync(api);
                if (response.IsSuccessStatusCode)
                {
                    product = response.Content.ReadAsAsync<IEnumerable<Tblnhanvien>>().Result;
                }
            }
            return product;
        }

        public ActionResult LogOut()
        {
           
            Session.Abandon();
            return RedirectToAction("Login", "DanhGia");
        }


    }
}