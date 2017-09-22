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
    public class LoaiTieuChiController : Controller
    {

       
        // GET: LoaiTieuChi
        public async Task<ActionResult> Index()
        {
            if (Session["userid"] == null)
            {

                return RedirectToAction("Login", "DanhGia");
            }
            else
            {
                DataContext<Tblloaitieuchi> dtt = new DataContext<Tblloaitieuchi>();
                string apik = "values/LoaiTieuChi";
                var a = await dtt.GetList(apik);

                return View(a);

            }


        }

        // GET: LoaiTieuChi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoaiTieuChi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiTieuChi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Idloaitc,Tenloaitc,Trangthaitc")]Tblloaitieuchi item)
        {
            DataContext<Tblloaitieuchi> data = new DataContext<Tblloaitieuchi>();
            string api = "values/LoaiTieuChi";
            if (ModelState.IsValid)
            {
                var value = new Tblloaitieuchi() { Tenloaitc = item.Tenloaitc, Trangthaitc = item.Trangthaitc };
                var test = await data.Create(value, api);
                if (test)
                {
                    TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                }
                return RedirectToAction("Index","LoaiTieuChi");
            }
            var listchinhanh = await data.GetList(api);
            return View(listchinhanh);
        }

        // GET: LoaiTieuChi/Edit/5
        public async Task<ActionResult>  Edit(int id)
        {
            DataContext<Tblloaitieuchi> data = new DataContext<Tblloaitieuchi>();
            string api = "values/LoaiTieuChi";
            var item = await data.GetList(id, api);
            if (item != null)
            {
                return View(item);
            }
            else
            {
                return View();
            }
        }

        // POST: LoaiTieuChi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Include = "Idloaitc,Tenloaitc,Trangthaitc")]Tblloaitieuchi item)
        {
            try
            {
                // TODO: Add update logic here
                DataContext<Tblloaitieuchi> data = new DataContext<Tblloaitieuchi>();
                string api = "values/LoaiTieuChi";
                if (ModelState.IsValid)
                {
                    var test = await data.Update(id, item, api);
                    if (test)
                    {
                        TempData["msg"] = "<script>alert('Cập nhật dữ liệu thành công');</script>";

                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Dữ liệu đã thay đổi, cập nhật không thành công');</script>";

                    }
                    return RedirectToAction("Index", "LoaiTieuChi");
                }
                var list = await data.GetList(api);
                return View(list);
               
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete([Bind(Include = "Idloaitc")]System.Int32? Idloaitc)
        {
            DataContext<Tblchinhanh> data = new DataContext<Tblchinhanh>();
            string api = "values/LoaiTieuChi";
            var model = Idloaitc;
            if (Idloaitc != null)
            {
                try
                {
                    var test = await data.Delete(Idloaitc, api);

                    return RedirectToAction("Index", "LoaiTieuChi");
                }
                catch (Exception e)
                {
                    TempData["msg"] = e.Message;
                }
            }

           var listchinhanh = await data.GetList(api);
            return View(listchinhanh);
        }

        // POST: LoaiTieuChi/Delete/5
       
        public async Task<JsonResult> CheckTenLoaiTC(string Tenloaitc)
        {
            bool result = true;
            DataContext<Tblloaitieuchi> dataNhanVien = new DataContext<Tblloaitieuchi>();
            string apinv = "values/LoaiTieuChi";
            var a = await dataNhanVien.GetList(apinv);
            var email = a.Where(x => x.Tenloaitc == Tenloaitc).FirstOrDefault();

            if (email != null)
                result = false;

            // return JsonType(result, JsonRequestBehavior.AllowGet);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
