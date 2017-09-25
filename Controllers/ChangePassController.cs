using KPI.Models;
using KPIKietHong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPIKietHong.Controllers
{
    public class ChangePassController : Controller
    {
        // GET: ChangePass
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult>ChangePass(FormCollection fc)
        {
            string userName = Session["username"].ToString();
            string PassWord = fc.Get("Password").ToString();
            DataContext<Tblnhanvien> datanhanvien = new DataContext<Tblnhanvien>();
            string api = "values/NhanVien";
            var a = await datanhanvien.GetList(api);
            var user = a.Where(x => x.Username == userName).FirstOrDefault();

            user.Password = PassWord;
            var kq = await datanhanvien.Update(user.Iduser, user, api);

            if (kq)
            {
                ViewBag.Message = "Đổi mật khẩu thành công.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return Redirect("/error/index.html");
            }

                 
            
        }

      
        public async Task<JsonResult> CheckOldPass(string OldPassword)
        {
            bool result = false;
            DataContext<Tblnhanvien> datanhanvien = new DataContext<Tblnhanvien>();
            string apinv = "values/NhanVien";
            var a = await datanhanvien.GetList(apinv);

         
            var Pass = a.Where(x => x.Password == Hashing.MD5Hash(OldPassword)).FirstOrDefault();

            if (Pass != null)
                result = true;

            // return JsonType(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}