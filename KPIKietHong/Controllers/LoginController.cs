﻿using KPIKietHong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}