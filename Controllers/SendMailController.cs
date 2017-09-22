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
    public class SendMailController : Controller
    {
        // GET: SendMail
        public ActionResult Index()
        {
            return View();
        }
        private readonly string UrlApi = ConfigurationManager.ConnectionStrings["ApiConnection"].ToString();
        public async Task<bool> Update(int id, Tblnhanvien item)
        {
            bool check = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("token", "quenmk");
                HttpResponseMessage response = await client.PutAsJsonAsync($"values/NhanVien/{id}", item);
                if (!response.IsSuccessStatusCode)
                {
                    check = false;
                }
            }
            return check;
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


        [HttpPost]
        public async Task<ActionResult> SendMailForgetPass(FormCollection fc)
        {
            try
            {
               
                //Gửi mail
                string maKhauMoi = MailHelper.RandomString(7, false);
                string gmail = fc.Get("Email").ToString();
           
            string api = "values/NhanVien";
            var a = await GetList(api);

            var user = a.Where(x => x.Email == gmail).FirstOrDefault();
                user.Password = maKhauMoi;
                var kqct = await Update(user.Iduser, user);
                if (kqct)
                {

                    string content = System.IO.File.ReadAllText(Server.MapPath("~/template/SendMailForgetPass.html"));
                    content = content.Replace("{{TenNV}}", user.Tennhanvien);
                    //content = content.Replace("{{UserName}}", user.Username);
                    content = content.Replace("{{Code}}", maKhauMoi);
                    MailHelper.SendMail(gmail, "Thông tin đổi mật khẩu", content);
                    return Redirect("/Succesful/Index");
                }
                else
                {
                    return Redirect("/error/index.html");
                }
            
             
            }
            catch
            {

                return Redirect("/loi");
            }
            //return Redirect("/thanh-cong");
        }
    }
}