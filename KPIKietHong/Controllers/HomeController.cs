using KPI.Models;
using KPIKietHong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

//using PagedList;

namespace KPIKietHong.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext<Tblchinhanh> data;
        private readonly DataContext<Tblnhomtieuchi> datanhomtieuchi;
        private readonly DataContext<Tbltieuchi> datatieuchi;
        private readonly string api;
        public static int tonloi = 0;
        public static int loidaxuly = 0;
        public static int sochinhanh = 0;
        public HomeController()
        {
           

            System.Web.HttpContext.Current.Session["userid"] = new SessionUser() { Iduser = 1, Email = @"abc@gmail.com", Idchinhanh = 1, Isadmin = false, Sodienthoai = "0123456789", Tennhanvien = "Cao Huong", Tolken = "12345", Username = "caohuong" };
            data = new DataContext<Tblchinhanh>();        
            datanhomtieuchi = new DataContext<Tblnhomtieuchi>();
            datatieuchi = new DataContext<Tbltieuchi>();
            api = "values";
          //  api = "values/DanhGia";
          //  api = "values/NhomTieuchi";
          //  api = "values/tieuChi";
           // api = "values/chinhanh";
           // api = "values/NhanVien";



        }
        // GET: Home
        public async Task<ActionResult> Index()
        {
            DataContext<Tbltieuchi> dtt = new DataContext<Tbltieuchi>();
            string apik = "values/TieuChi";
            var a = await dtt.GetList(apik);
            ViewBag.Count = a.Count();
            DataContext<Tbltonkholoi> tkl = new DataContext<Tbltonkholoi>();
            string apik1 = "values/TonKhoLoi";
            var b = await tkl.GetList(apik1);
            tonloi = b.Where(x => x.Daxuly == false).Count();
            loidaxuly = b.Where(x => x.Daxuly == true).Count();

            DataContext<Tbltieuchi> dtt2 = new DataContext<Tbltieuchi>();
            string apik2 = "values/NhanVien";
            var c = await dtt2.GetList(apik2);
            ViewBag.nhanvien = c.Count();
            //Đếm chi nhánh
            
            
            var d = await data.GetList(api);
            ViewBag.sochinhanh = d.Count();


            return View();

           
        }
       
        static IEnumerable<Tblchinhanh> listchinhanh = null;
        public async Task<ActionResult> ChiNhanhAsync()
        {
            var a =await data.GetList(api);

            // int pageSize = 8;
            //  int pageNumber = (page ?? 1);
            //return View(a.OrderBy(x=>x.Idchinhanh).ToPagedList(pageNumber, pageSize));
            return View(a);
        }
        
        public async Task<ActionResult> ChiNhanhDetails(int id)
        {
            var a = await data.GetList(id,api);
            // listchinhanh = await GetListChiNhanh();
            return View(a);
        }
        public ActionResult ChiNhanhCreate()
        {
            //listchinhanh = await GetListChiNhanh();
            return View();
        }
        public async Task<ActionResult> ChiNhanhEdit(int id)
        {
            var item = await data.GetList(id,api);
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
        public async Task<ActionResult> ChiNhanhDelete([Bind(Include = "Idchinhanh")]System.Int32? Idchinhanh)
        {
           
            var model = Idchinhanh;
            if (Idchinhanh != null)
            {
                try
                {
                    var test = await data.Delete(Idchinhanh,api);
                 
                    return RedirectToAction("ChiNhanhAsync");
                }
                catch (Exception e)
                {
                    TempData["msg"] = e.Message;
                }
            }

            listchinhanh = await data.GetList(api);
            return View(listchinhanh);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChiNhanhCreate([Bind(Include = "Machinhanh,Tenchinhanh,Tragnthaicn")]Tblchinhanh item)
        {
            if (ModelState.IsValid)
            {
                var value = new Tblchinhanh() { Machinhanh = item.Machinhanh, Tenchinhanh = item.Tenchinhanh, Tragnthaicn = item.Tragnthaicn };
                var test = await data.Create(value, api);
                if (test)
                {
                    TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                }
                return RedirectToAction("ChiNhanhAsync");
            }
            listchinhanh = await data.GetList(api);
            return View(listchinhanh);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChiNhanhEdit(int id,[Bind(Include = "Idchinhanh,Machinhanh,Tenchinhanh,Tragnthaicn")]Tblchinhanh item)
        {
            if(ModelState.IsValid)
            {
                var test = await data.Update(id,item,api);
                if (test)
                {
                    TempData["msg"] = "<script>alert('Cập nhật dữ liệu thành công');</script>";
                   
                }
                else
                {
                    TempData["msg"] = "<script>alert('Dữ liệu đã thay đổi, cập nhật không thành công');</script>";

                }
                return RedirectToAction("ChiNhanhAsync");
            }
            var list = await data.GetList(api);
            return View(list);
        }
      
        //Thống kê
        public ActionResult NhanV()
        {          
            return PartialView("NhanV");
        }

        //Tồn kho lỗi

        public ActionResult TonKhoLoiPartial()
        {           
            return PartialView("TonKhoLoiPartial",tonloi);
        }

        public ActionResult LoiDaXuPartial()
        {
            return PartialView("LoiDaXuPartial", loidaxuly);
        }
        public ActionResult ChiNhanhPartial()
        {
            return PartialView("ChiNhanhPartial", sochinhanh);
        }




    }
 
}
