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
    public class DanhGiaController : Controller
    {
        private readonly DataContext<Tbldanhgia> data;
        private readonly DataContext<Tblnhomtieuchi> datanhomtieuchi;
        private readonly DataContext<Tbltieuchi> datatieuchi;
        private readonly DataContext<Tblchinhanh> datachinhanh;
        private readonly DataContext<Tblnhanvien> datanhanvien;
        private readonly  Login login;
        private readonly string api;
        private readonly string apinhomtieuchi;
        private readonly string apitieuchi;
        private readonly string apichinhanh;
        private readonly string apinhanvien;
        public DanhGiaController()
        {
            //System.Web.HttpContext.Current.Session["userid"] = new SessionUser()
            //{
            //    Iduser = 2,
            //    Email = @"abc@gmail.com",
            //    Idchinhanh = 1020,
            //    Isadmin = false,
            //    Sodienthoai = "0123456789",
            //    Tennhanvien = "Cao Huong",
            //    Tolken = "12345",
            //    Username = "caohuong"
            //};

            //data = new DataContext<Tbldanhgia>();
            //datanhomtieuchi = new DataContext<Tblnhomtieuchi>();
            //datatieuchi = new DataContext<Tbltieuchi>();
            //datachinhanh = new DataContext<Tblchinhanh>();
            //datanhanvien = new DataContext<Tblnhanvien>();
            api = "values/DanhGia";
            apinhomtieuchi = "values/NhomTieuchi";
            apitieuchi = "values/TieuChi";
            apichinhanh = "values/ChiNhanh";
            apinhanvien = "values/NhanVien";
            login = new Models.Login();
        }
        // GET: NhomTieuChi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(string username,string Password)
        {
            
            var chk = await login.LoginApi(username, Password);
            if(chk)
            {
                return RedirectToAction("DanhGiaAsync");
            }
            ViewBag.error = "Đăng nhập sai thông tin hoặc bạn không có quyền vào";
            return View();
        }
        static IEnumerable<Tbldanhgia> listdanhgia = null;
        public async Task<ActionResult> DanhGiaAsync()
        {
       //     var a = await data.GetList(api);
          //  ViewBag.nhanvien = await datanhanvien.GetList(1,apinhanvien);
          //  var userid = System.Web.HttpContext.Current.Session["userid"];
          if(Session["userid"] != null)
            {
               var user = Session["userid"] as SessionUser;
                ViewBag.user = user.Tennhanvien;
                Session["TenNV"] = user.Tennhanvien;
            }
            // listchinhanh = await GetListChiNhanh();
            return View();
        }//

        public ActionResult DanhGiaCreate()
        {
            //listchinhanh = await GetListChiNhanh();
            return View();
        }

        public async Task<ActionResult> DanhGiaChiEdit(int id)
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
        public async Task<ActionResult> DanhGiaDelete([Bind(Include = "iddanhgia")]System.Int32? Iddanhgia)
        {

            var model = Iddanhgia;
            if (Iddanhgia != null)
            {
                try
                {
                    var test = await data.Delete(Iddanhgia, api);
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

            listdanhgia = await data.GetList(api);
            return View(listdanhgia);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DanhGiaCreate([Bind(Include = "Idtieuchi,Iddapan,Diemdanhgia,Ngaydanhgia,Iduser,Ghichu,Noidungdanhgia,Trangthaidanhgia")]Tbldanhgia item)
        {
            if (ModelState.IsValid)
            {
                var value = new Tbldanhgia() { Idtieuchi = item.Idtieuchi, Diemdanhgia=item.Diemdanhgia, Ghichu=item.Ghichu,
                        Iddapan=item.Iddapan, Iduser=item.Iduser, Ngaydanhgia=item.Ngaydanhgia, Noidungdanhgia=item.Noidungdanhgia, Trangthaidanhgia=item.Trangthaidanhgia
                };
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
            listdanhgia = await data.GetList(api);
            return View(listdanhgia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DanhGiaChiEdit(int id, [Bind(Include = "Iddanhgia,Idtieuchi,Iddapan,Diemdanhgia,Ngaydanhgia,Iduser,Ghichu,Noidungdanhgia,Trangthaidanhgia")]Tbldanhgia item)
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