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
    public class NhomTieuChiController : Controller
    {
        private readonly DataContext<Tblnhomtieuchi> data;
        private readonly string api;
        public NhomTieuChiController()
        {
            data = new DataContext<Tblnhomtieuchi>();
            api = "values/NhomTieuChi";
        }
        // GET: NhomTieuChi
        public ActionResult Index()
        {
            return View();
        }

        static IEnumerable<Tblnhomtieuchi> listNhomTieuChi = null;
        public async Task<ActionResult> NhomTieuChiAsync()
        {
            var a = await data.GetList(api);
            // listchinhanh = await GetListChiNhanh();
            return View(a);
        }//

        public ActionResult NhomTieuChiCreate()
        {
            //listchinhanh = await GetListChiNhanh();
            return View();
        }

        public async Task<ActionResult> NhomTieuChiEdit(int id)
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
        public async Task<ActionResult> NhomTieuChiDelete([Bind(Include = "Idnhomtieuchi")]System.Int32? Idnhomtieuchi)
        {

            var model = Idnhomtieuchi;
            if (Idnhomtieuchi != null)
            {
                try
                {
                    var test = await data.Delete(Idnhomtieuchi, api);
                    //if (test)
                    //{
                    //    TempData["msg"] = "Thêm mới dữ liệu thành công')";

                    //}
                    //else
                    //{
                    //    TempData["msg"] = "Thao tác không thực hiện";
                    //}
                    return RedirectToAction("NhomTieuChiAsync");
                }
                catch (Exception e)
                {
                    TempData["msg"] = e.Message;
                }
            }

            listNhomTieuChi = await data.GetList(api);
            return View(listNhomTieuChi);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NhomTieuChiCreate([Bind(Include = "Idnhomtieuchi,Tennhomtieuchi,Trangthaintc")]Tblnhomtieuchi item)
        {
            if (ModelState.IsValid)
            {
                var value = new Tblnhomtieuchi() { Idnhomtieuchi = item.Idnhomtieuchi, Tennhomtieuchi = item.Tennhomtieuchi, Trangthaintc = item.Trangthaintc };
                var test = await data.Create(value, api);
                if (test)
                {
                    TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                }
                return RedirectToAction("NhomTieuChiAsync");
            }
            listNhomTieuChi = await data.GetList(api);
            return View(listNhomTieuChi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NhomTieuChiEdit(int id, [Bind(Include = "Idnhomtieuchi,Tennhomtieuchi,Trangthaintc")]Tblnhomtieuchi item)
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
                return RedirectToAction("NhomTieuChiAsync");
            }
            var list = await data.GetList(api);
            return View(list);
        }
    }
}