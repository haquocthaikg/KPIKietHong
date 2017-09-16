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
    public class HomeController : Controller
    {
        private readonly DataContext<Tblchinhanh> data;
        private readonly string api;
        public HomeController()
        {
            data =new DataContext <Tblchinhanh>();
            api = "values";
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
       
        static IEnumerable<Tblchinhanh> listchinhanh = null;
        public async Task<ActionResult> ChiNhanhAsync()
        {
            var a =await data.GetList(api);
           // listchinhanh = await GetListChiNhanh();
            return View(a);
        }//
        
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
    
    }
}