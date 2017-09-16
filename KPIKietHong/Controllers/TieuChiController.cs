using KPIKietHong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPIKietHong.Controllers
{
    public class TieuChiController : Controller
    {
        private readonly DataContext<Tbltieuchi> data;
        private readonly DataContext<Tblchinhanh> data1;
        private readonly DataContext<Tblnhomtieuchi> data2;
        private readonly DataContext<Tblloaitieuchi> data3;
        private readonly string api;
        private readonly string api1;
        private readonly string api2;
        private readonly string api3;
        public TieuChiController()
        {
            data3 = new DataContext<Tblloaitieuchi>();
            api3 = "values/LoaiTieuChi";

            data2 = new DataContext<Tblnhomtieuchi>();
            api2 = "values/NhomTieuChi";
            data1 = new DataContext<Tblchinhanh>();
            api1 = "values";
            data = new DataContext<Tbltieuchi>();
            api = "values/TieuChi";
        }
        // GET: TieuChi
        public ActionResult Index()
        {
            return View();
        }

        static IEnumerable<Tbltieuchi> listTieuChi = null;
        public async Task<ActionResult> TieuChiAsync()
        {
            var a = await data.GetList(api);
            // listchinhanh = await GetListChiNhanh();
            return View(a);
        }//

        public async Task<ActionResult>TieuChiDetails(int id)
        {
            var a = await data.GetList(id, api);
            // listchinhanh = await GetListChiNhanh();
            return View(a);
        }

        public async Task<ActionResult> TieuChiCreate()
        {
            var c = await data3.GetList(api3);
            ViewBag.LoaiTieuChiList = new SelectList(c, "idloaitc", "tenloaitc");
            var b = await data2.GetList(api2);
            ViewBag.NhomTieuChiList = new SelectList(b, "idnhomtieuchi", "tennhomtieuchi");
            var a = await data1.GetList(api1);
            ViewBag.ChiNhanhList = new SelectList(a, "idchinhanh", "tenchinhanh");
            //listchinhanh = await GetListChiNhanh();
            return View();
        }

        public async Task<ActionResult> TieuChiEdit(int id)
        {
            var c = await data3.GetList(api3);
            ViewBag.LoaiTieuChiList = new SelectList(c, "idloaitc", "tenloaitc");
            var b = await data2.GetList(api2);
            ViewBag.NhomTieuChiList = new SelectList(b, "idnhomtieuchi", "tennhomtieuchi");
            var a = await data1.GetList(api1);
            ViewBag.ChiNhanhList = new SelectList(a, "idchinhanh", "tenchinhanh");
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
        public async Task<ActionResult> TieuChiDelete([Bind(Include = "Idtieuchi")]System.Int32? Idtieuchi)
        {

            var model = Idtieuchi;
            if (Idtieuchi != null)
            {
                try
                {
                    var test = await data.Delete(Idtieuchi, api);
                    //if (test)
                    //{
                    //    TempData["msg"] = "Thêm mới dữ liệu thành công')";

                    //}
                    //else
                    //{
                    //    TempData["msg"] = "Thao tác không thực hiện";
                    //}
                    return RedirectToAction("TieuChiAsync");
                }
                catch (Exception e)
                {
                    TempData["msg"] = e.Message;
                }
            }

            listTieuChi = await data.GetList(api);
            return View(listTieuChi);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TieuChiCreate([Bind(Include = "Idtieuchi,Matieuchi,Tentieuchi,Idchinhanh,Idnhomtieuchi,Trangthaitc,Diemtieuchi,Idloaitc")]Tbltieuchi item)
        {
            if (ModelState.IsValid)
            {
                var value = new Tbltieuchi() { Idtieuchi = item.Idtieuchi, Matieuchi = item.Matieuchi, Tentieuchi = item.Tentieuchi, Idchinhanh = item.Idchinhanh, Idnhomtieuchi = item.Idnhomtieuchi, Trangthaitc = item.Trangthaitc, Diemtieuchi = item.Diemtieuchi, Idloaitc = item.Idloaitc };
                var test = await data.Create(value, api);
                if (test)
                {
                    TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                }
                return RedirectToAction("TieuChiAsync");
            }
            listTieuChi = await data.GetList(api);
            return View(listTieuChi);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TieuChiEdit(int id, [Bind(Include = "Idtieuchi,Matieuchi,Tentieuchi,Idchinhanh,Idnhomtieuchi,Trangthaitc,Diemtieuchi,Idloaitc")]Tbltieuchi item)
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
                return RedirectToAction("TieuChiAsync");
            }
            var list = await data.GetList(api);
            return View(list);
        }
    }
}