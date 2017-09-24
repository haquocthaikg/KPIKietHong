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
    public class DapAnController : Controller
    {
        // GET: DapAn
        public async Task<ActionResult> Index(int? value)
        {
            if (Session["userid"] == null)
            {

                return RedirectToAction("Login", "DanhGia");
            }
            else
            {
                var user = Session["userid"] as SessionUser;
                DataContext<Tblquanlychinhanh> dtt1 = new DataContext<Tblquanlychinhanh>();
                string apik1 = "values/QuanLyChiNhanh";

                var listcn = await dtt1.GetListBy(user.Iduser, apik1);//lay ds cn
                List<Tblchinhanh> listchinhanh = new List<Tblchinhanh>();
                if(listcn.Count()>0)
                {
                    DataContext<Tblchinhanh> datacn = new DataContext<Tblchinhanh>();
                    string apikcn = "values";
                    foreach (var cn in listcn.Where(c=>c.Trangthaiquanly==true))
                    {
                        var item = await datacn.GetList(cn.Idchinhanh, apikcn);
                        if(item != null)
                        {
                            listchinhanh.Add(item);
                        }
                    }
                }
                ViewBag.listcn = listchinhanh; //new SelectList(listchinhanh, "idchinhanh", "tenchinhanh");
               
                if (value != null)
                {
                    ViewBag.indexlistcn = value.Value;
                   
                }
                else
                {
                    ViewBag.indexlistcn = listchinhanh?.Count > 0 ? listchinhanh.FirstOrDefault().Idchinhanh : 0;
                }
                 
                ViewBag.listdapancn = await GetTCList1(ViewBag.indexlistcn);
                DataContext<Tbltieuchi> datatc = new DataContext<Tbltieuchi>();
                string apitc = "values/TieuChiByChiNhanh";
                var a = await datatc.GetListBy(ViewBag.indexlistcn, apitc);//lay ds tc
                ViewBag.listtcbycn = a;
                return View();

            }
        }
        public async Task<JsonResult> GetTCList(int Idchinhanh)
        {
            DataContext<Tbltieuchi> datatc = new DataContext<Tbltieuchi>();
            string apitc = "values/TieuChiByChiNhanh";
            var a = await datatc.GetListBy(Idchinhanh, apitc);//lay ds tc
            List<Tbldapan> listda = new List<Tbldapan>();
            DataContext<Tbldapan> dtt = new DataContext<Tbldapan>();
            string apik = "values/DapAnByTC";
            if (a.Count()>0)
            {
                foreach(var item in a.OrderBy(p=>p.Idnhomtieuchi))//chay ds tc lay dap an
                {
                    var listdatemp = await dtt.GetListBy(item.Idtieuchi,apik);
                    listda.AddRange(listdatemp);

                }
            }
            return Json(listda, JsonRequestBehavior.AllowGet);
        }
        public async Task<List<Tbldapan>> GetTCList1(int Idchinhanh)
        {
            DataContext<Tbltieuchi> datatc = new DataContext<Tbltieuchi>();
            string apitc = "values/TieuChiByChiNhanh";
            var a = await datatc.GetListBy(Idchinhanh, apitc);//lay ds tc
            List<Tbldapan> listda = new List<Tbldapan>();
            DataContext<Tbldapan> dtt = new DataContext<Tbldapan>();
            string apik = "values/DapAnByTC";
            if (a.Count() > 0)
            {
                foreach (var item in a.OrderBy(p => p.Idnhomtieuchi))//chay ds tc lay dap an
                {
                    var listdatemp = await dtt.GetListBy(item.Idtieuchi, apik);
                    listda.AddRange(listdatemp);

                }
            }
            return listda;
        }
        // GET: DapAn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpPost]
        // GET: DapAn/Create
        public async Task<ActionResult> Create([Bind(Include = "Idchinhanh")]int? Idchinhanh)
        {
            //lay ds tieu chi cua chi nhanh
            // la loai
            //var user = Session["userid"] as SessionUser;
            if (Idchinhanh != null)
            {
                DataContext<Tbltieuchi> data = new DataContext<Tbltieuchi>();
                string api = "values/TieuChiByChiNhanh";
                var a = await data.GetListBy(Idchinhanh.Value, api);
                ViewBag.listtieuchi = new SelectList(a, "idtieuchi", "tentieuchi");
                DataContext<Tblchinhanh> datacn = new DataContext<Tblchinhanh>();
                string apicn = "values";
                var cn = await datacn.GetList(Idchinhanh.Value, apicn);
                ViewBag.Tencn = "[ " + cn.Tenchinhanh +" ] - ";
                return View(); //tra ve loi
            }
            else
            {
                return View(); //tra ve loi
            }
        }

        // POST: DapAn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "Idtieuchi,Tendapan,Diemdapan,Trangthaidapan")]Tbldapan item)
        {
            DataContext<Tbldapan> data = new DataContext<Tbldapan>();
            string api = "values/DapAn";
            if (ModelState.IsValid)
            {
                var value = new Tbldapan() { Idtieuchi = item.Idtieuchi, Tendapan=item.Tendapan, Diemdapan=item.Diemdapan,Trangthaidapan = item.Trangthaidapan };
                var test = await data.Create(value, api);
                if (test)
                {
                    TempData["msg"] = "<script>alert('Thêm mới dữ liệu thành công');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('Thao tác không thực hiện');</script>";
                }
                
            }
            return RedirectToAction("Index", "DapAn");
            //var listchinhanh = await data.GetList(api);
            //return View(listchinhanh);
        }

        // GET: DapAn/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            DataContext<Tbldapan> datada = new DataContext<Tbldapan>();
            string apida = "values/DapAn";
            var da = await datada.GetList(id, apida);

            DataContext<Tbltieuchi> data = new DataContext<Tbltieuchi>();
            string api = "values/TieuChi";
            var a = await data.GetList(api);
            ViewBag.listtieuchi = new SelectList(a.Where(p=>p.Idtieuchi==da.Idtieuchi), "idtieuchi", "tentieuchi");
           
            //ViewBag.listtieuchi = new SelectList(a, "idtieuchi", "tentieuchi");
            return View(da);
        }

        // POST: DapAn/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Include = "Iddapan,Idtieuchi,Tendapan,Diemdapan,Trangthaidapan")]Tbldapan item)
        {
            try
            {
                DataContext<Tbldapan> datada = new DataContext<Tbldapan>();
                string apida = "values/DapAn";
              if (ModelState.IsValid)
                {
                   
                    var test = await datada.Update(id, item, apida);
                    if (test)
                    {
                        TempData["msg"] = "<script>alert('Cập nhật dữ liệu thành công');</script>";

                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Dữ liệu đã thay đổi, cập nhật không thành công');</script>";

                    }
                    return RedirectToAction("Index","DapAn");
                }
                var listTieuChi = await datada.GetList(apida);
                return View(listTieuChi);
            }
            catch
            {
                return View();
            }
        }

        // GET: DapAn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DapAn/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete([Bind(Include = "Idtieuchi")]System.Int32? Iddapan)
        {
            try
            {
                DataContext<Tbldapan> datada = new DataContext<Tbldapan>();
                string apida = "values/DapAn";
                var model = Iddapan;
                if (Iddapan != null)
                {
                    try
                    {
                        var test = await datada.Delete(Iddapan, apida);
                      
                        return RedirectToAction("Index", "DapAn");
                    }
                    catch (Exception e)
                    {
                        TempData["msg"] = e.Message;
                    }
                }

                var listTieuChi = await datada.GetList(apida);
                return View(listTieuChi);
            }
            catch
            {
                return View();
            }
        }
    }
}
