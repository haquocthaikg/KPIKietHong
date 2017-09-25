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
    
        public static int tonloi = 0;
        public static int loidaxuly = 0;
        public static int sochinhanh = 0;
     
        // GET: Home
        public async Task<ActionResult> Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("Login", "DanhGia");
            }
            else
            {
                DataContext<Tbltieuchi> dtt = new DataContext<Tbltieuchi>();
                string apik = "values/TieuChi";
                var a = await dtt.GetList(apik);
                ViewBag.Count = a.Count();
                ViewBag.TieuChiAn = a.Where(x => x.Trangthaitc == false).Count();
                ViewBag.TieuChiHien = a.Where(x => x.Trangthaitc == true).Count();

                DataContext<Tbltonkholoi> tkl = new DataContext<Tbltonkholoi>();
                string apik1 = "values/TonKhoLoi";
                var b = await tkl.GetList(apik1);
                tonloi = b.Where(x => x.Daxuly == false).Count();
                loidaxuly = b.Where(x => x.Daxuly == true).Count();
              
            }
            ViewBag.SoNguoiTruyCap = HttpContext.Application["DaTruyCap"].ToString();// lấy số người truy cập từ application
            ViewBag.SoNguoiOnline = HttpContext.Application["DangTruyCap"].ToString();
            return View();
        }
       
        static IEnumerable<Tblchinhanh> listchinhanh = null;
        public async Task<ActionResult> ChiNhanhAsync()
        {
            if (Session["userid"] == null)
            {
               return RedirectToAction("Login","DanhGia");
            }
            else
            {
                DataContext<Tblchinhanh> data = new DataContext<Tblchinhanh>();
                string api = "values";
                var a = await data.GetList(api);


                // int pageSize = 8;
                //  int pageNumber = (page ?? 1);
                //return View(a.OrderBy(x=>x.Idchinhanh).ToPagedList(pageNumber, pageSize));
                return View(a);
            }
            
        }
        
        public async Task<ActionResult> ChiNhanhDetails(int id)
        {
            DataContext<Tblchinhanh> data = new DataContext<Tblchinhanh>();
            string api = "values";
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
            DataContext<Tblchinhanh> data = new DataContext<Tblchinhanh>();
            string api = "values";
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
            DataContext<Tblchinhanh> data = new DataContext<Tblchinhanh>();
            string api = "values";
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
            DataContext<Tblchinhanh> data = new DataContext<Tblchinhanh>();
            string api = "values";
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
            DataContext<Tblchinhanh> data = new DataContext<Tblchinhanh>();
            string api = "values";
            if (ModelState.IsValid)
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
