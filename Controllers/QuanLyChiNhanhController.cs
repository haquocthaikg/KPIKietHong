using KPIKietHong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPIKietHong.Controllers
{
    public class QuanLyChiNhanhController : Controller
    {
        // GET: QuanLyChiNhanh
        public async Task<ActionResult> Index()
        {
            if (Session["userid"] == null)
            {

                return RedirectToAction("Login", "DanhGia");
            }
            else
            {
                DataContext<Tblchinhanh> dtt = new DataContext<Tblchinhanh>();
                string apik = "values";
                var a = await dtt.GetList(apik);
                ViewBag.listcn = a.Where(p => p.Tragnthaicn == true).OrderBy(p => p.Machinhanh);
                DataContext<Tblnhanvien> datanv = new DataContext<Tblnhanvien>();
                string apiknv = "values/NhanVien";
                var b = await datanv.GetList(apiknv);

                ViewBag.listnv = b.OrderBy(p => p.Iduser);
                DataContext<Tblquanlychinhanh> dataql = new DataContext<Tblquanlychinhanh>();
                string apikql = "values/QuanLyChiNhanh";
                var listqla = await dataql.GetList(apikql);
                return View(listqla.OrderBy(p=>p.Iduser));
            }
        }

        // GET: QuanLyChiNhanh/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuanLyChiNhanh/Create
        public async Task<ActionResult> Create()
        {
            DataContext<Tblchinhanh> dtt = new DataContext<Tblchinhanh>();
            string apik = "values";
            var a = await dtt.GetList(apik);
            ViewBag.listcn = new SelectList(a.Where(p => p.Tragnthaicn == true).OrderBy(p => p.Machinhanh), "idchinhanh", "tenchinhanh");
            DataContext<Tblnhanvien> datanhanvien = new DataContext<Tblnhanvien>();
            string apiknhanvien = "values/NhanVien";
            var b = await datanhanvien.GetList(apiknhanvien);
            ViewBag.listnv = new SelectList(b.Where(p => p.Khoa == true).OrderBy(p => p.Tennhanvien), "iduser", "tennhanvien");
            return View();
        }

        // POST: QuanLyChiNhanh/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([Bind(Include = "Iduser,Idchinhanh,Trangthaiquanly")]Tblquanlychinhanh item)
        {
            try
            {
                DataContext<Tblquanlychinhanh> dataql = new DataContext<Tblquanlychinhanh>();
                string apikql = "values/QuanLyChiNhanh";
                if (ModelState.IsValid)
                {
                    
                    var value = new Tblquanlychinhanh() { Iduser = item.Iduser, Trangthaiquanly = item.Trangthaiquanly,
                       Idchinhanh = item.Idchinhanh };
                    var test = await dataql.Create(value, apikql);

                    if (test)
                    {
                        TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                    }
                    return RedirectToAction("Index","QuanLyChiNhanh");
                }
                var listqlcn = await dataql.GetList(apikql);
                return View(listqlcn);
            }
            catch
            {
                return View();
            }
        }
        public async Task<JsonResult> CheckUserCN(int Iduser, int Idchinhanh)
        {
            bool result = true;
            HeThong<Tblquanlychinhanh> dataNhanVien = new HeThong<Tblquanlychinhanh>();
            string apinv = "values/QuanLyChiNhanh";
            var a = await dataNhanVien.GetList(apinv);
            var email = a.Where(x => x.Iduser == Iduser && x.Idchinhanh==Idchinhanh).FirstOrDefault();

            if (email != null)
                result = false;

            // return JsonType(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: QuanLyChiNhanh/Edit/5
        public async Task<ActionResult> Edit(int Iduser,int Idchinhanh)
        {

            DataContext<Tblchinhanh> dtt = new DataContext<Tblchinhanh>();
            string apik = "values";
            var a = await dtt.GetList(apik);
            ViewBag.listcn = a.Where(p => p.Tragnthaicn == true).OrderBy(p => p.Machinhanh);
            DataContext<Tblnhanvien> datanv = new DataContext<Tblnhanvien>();
            string apiknv = "values/NhanVien";
            var b = await datanv.GetList(apiknv);

            ViewBag.listnv = b.OrderBy(p => p.Iduser);

            DataContext<Tblquanlychinhanh> dataqlcn = new DataContext<Tblquanlychinhanh>();
            string apikqlcn = "values/QuanLyChiNhanh";
            var item = await dataqlcn.GetList(apikqlcn);
            var kq = item.Where(p => p.Idchinhanh == Idchinhanh && p.Iduser == Iduser).FirstOrDefault();
            if (kq != null)
            {
                return View(kq);
            }
            else
            {
                return View();
            }
            
        }

        // POST: QuanLyChiNhanh/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Iduser,Idchinhanh,Trangthaiquanly")]Tblquanlychinhanh item)
        {
            try
            {
                HeThong<Tblquanlychinhanh> data = new HeThong<Tblquanlychinhanh>();
                string api = "values/QuanLyChiNhanh";
                if (ModelState.IsValid)
                {
                    var value = new Tblquanlychinhanh() { Iduser=item.Iduser, Idchinhanh=item.Idchinhanh, Trangthaiquanly=item.Trangthaiquanly };
                    var test = await data.Update(value, api);

                    if (test)
                    {
                        TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                    }
                    return RedirectToAction("Index","QuanLyChiNhanh");
                }
                var listNhanVien = await data.GetList(api);
                return View(listNhanVien);
            }
            catch
            {
                return View();
            }
        }

        // GET: QuanLyChiNhanh/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuanLyChiNhanh/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(Tblquanlychinhanh item)
        {
            HeThong<Tblquanlychinhanh> data = new HeThong<Tblquanlychinhanh>();
            string api = "values/QuanLyChiNhanh";
           
            if (item != null)
            {
                try
                {
                    var test = await data.Delete(item.Idchinhanh,item.Iduser, api);

                    return RedirectToAction("Index", "QuanLyChiNhanh");
                }
                catch (Exception e)
                {
                    TempData["msg"] = e.Message;
                }
            }

            var listchinhanh = await data.GetList(api);
            return View(listchinhanh);
        }
    }
}
