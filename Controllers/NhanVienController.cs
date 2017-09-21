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

    public class NhanVienController : Controller
    {
        private readonly DataContext<Tblnhanvien> data;
        private readonly DataContext<Tblchinhanh> data1;
        private readonly string api;
        private readonly string api1;
        // GET: NhanVien

        public NhanVienController()
        {
            data1 = new DataContext<Tblchinhanh>();
            api1 = "values";

            data = new DataContext<Tblnhanvien>();
            api = "values/NhanVien";
        }
        public ActionResult Index()
        {
            return View();
        }

        static IEnumerable<Tblnhanvien> listNhanVien = null;

        public async Task<ActionResult> NhanVienAsync()
        {
            var a = await data.GetList(api);
            var chinhanh = await data1.GetList(api1);
            ViewBag.ChiNhanhList1 = chinhanh;
            // listchinhanh = await GetListChiNhanh();
            return View(a);
        }//
        //public async Task<string> GetTenChiNhanh(int idchinhanh)
        //{
        //    var chinhanh = await data1.GetList(api1);
        //    return chinhanh.Where(p => p.Idchinhanh == idchinhanh).FirstOrDefault()?.Tenchinhanh??"";

        //}
        public async Task<ActionResult> NhanVienDetails(int id)
        {
            var a = await data.GetList(id, api);
            // listchinhanh = await GetListChiNhanh();
            return View(a);
        }

        public async Task<ActionResult> NhanVienCreate()
        {
            var a = await data1.GetList(api1);
            ViewBag.ChiNhanhList = new SelectList(a, "idchinhanh", "tenchinhanh");
            
            return View();
        }

        public async Task<ActionResult> NhanVienEdit(int id)
        {
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
        public async Task<ActionResult> NhanVienDelete([Bind(Include = "Iduser")]System.Int32? Iduser)
        {

            var model = Iduser;
            if (Iduser != null)
            {
                try
                {
                    var test = await data.Delete(Iduser, api);
                    //if (test)
                    //{
                    //    TempData["msg"] = "Thêm mới dữ liệu thành công')";

                    //}
                    //else
                    //{
                    //    TempData["msg"] = "Thao tác không thực hiện";
                    //}
                    return RedirectToAction("NhanVienAsync");
                }
                catch (Exception e)
                {
                    TempData["msg"] = e.Message;
                }
            }

            listNhanVien = await data.GetList(api);
            return View(listNhanVien);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NhanVienCreate([Bind(Include = "Iduser,Tennhanvien,Username,Password,PasswordConfirm,Tolken,Email,Sodienthoai,Secrect,Keychung,Keyrieng,Khoa,Giatrixacthuc,Idchinhanh")]NhanVien item)
        {
            if (ModelState.IsValid)
            {
                var value = new Tblnhanvien() { Iduser = item.Iduser, Tennhanvien = item.Tennhanvien, Username = item.Username, Password = item.Password, Tolken = item.Tolken, Email = item.Email, Sodienthoai = item.Sodienthoai, Secrect = item.Secrect, Keychung = item.Keychung, Keyrieng = item.Keyrieng, Khoa = item.Khoa, Giatrixacthuc = item.Giatrixacthuc, Idchinhanh = item.Idchinhanh};
                var test = await data.Create(value, api);
                
                if (test)
                {
                    TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                }
                return RedirectToAction("NhanVienAsync");
            }
            listNhanVien = await data.GetList(api);
            return View(listNhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> NhanVienEdit(int id, [Bind(Include = "Iduser,Tennhanvien,Username,Password,Tolken,Email,Sodienthoai,Secrect,Keychung,Keyrieng,Khoa,Giatrixacthuc,Idchinhanh")]Tblnhanvien item)
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
                return RedirectToAction("NhanVienAsync");
            }
            var list = await data.GetList(api);
            return View(list);
        }
    }
}