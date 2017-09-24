using KPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using KPIKietHong.Models;

namespace KPIKietHong.Controllers
{
    public class LoaiTieuChiController : Controller
    {
        private readonly DataContext<Tblloaitieuchi> data;
        private readonly string api;
        public LoaiTieuChiController()
        {
            data = new DataContext<Tblloaitieuchi>();
            api = "values/LoaiTieuChi";
           
            
        }
        // GET: LoaiTieuChi
        public ActionResult Index()
        {
            return View();
        }

        static IEnumerable<Tblloaitieuchi> listLoaiTieuChi = null;
        public async Task<ActionResult> LoaiTieuChiAsync()
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("~/DanhGia/Login");
            }
            else
            {
                var a = await data.GetList(api);
                // listchinhanh = await GetListChiNhanh();
                return View(a);
            }
            return View();
        }//

        public ActionResult LoaiTieuChiCreate()
        {
            //listchinhanh = await GetListChiNhanh();
            return View();
        }

        public async Task<ActionResult> LoaiTieuChiEdit(int id)
        {
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

        [HttpPost]
        public async Task<ActionResult> LoaiTieuChiDelete([Bind(Include = "Idloaitc")]System.Int32? Idloaitc)
        {

            var model = Idloaitc;
            if (Idloaitc != null)
            {
                try
                {
                    var test = await data.Delete(Idloaitc, api);
                    //if (test)
                    //{
                    //    TempData["msg"] = "Thêm mới dữ liệu thành công')";

                    //}
                    //else
                    //{
                    //    TempData["msg"] = "Thao tác không thực hiện";
                    //}
                    return RedirectToAction("LoaiTieuChiAsync");
                }
                catch (Exception e)
                {
                    TempData["msg"] = e.Message;
                }
            }

            listLoaiTieuChi = await data.GetList(api);
            return View(listLoaiTieuChi);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoaiTieuChiCreate([Bind(Include = "Tenloaitc,Trangthaitc")]Tblloaitieuchi item)
        {
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
                return RedirectToAction("LoaiTieuChiAsync");
            }
            listLoaiTieuChi = await data.GetList(api);
            return View(listLoaiTieuChi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoaiTieuChiEdit(int id, [Bind(Include = "Idloaitc,Tenloaitc,Trangthaitc")]Tblloaitieuchi item)
        {
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
                return RedirectToAction("LoaiTieuChiAsync");
            }
            var list = await data.GetList(api);
            return View(list);
        }
    }
}