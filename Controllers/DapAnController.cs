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
        public async Task<ActionResult> Index()
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
                ViewBag.listcn = listchinhanh;
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
        // GET: DapAn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DapAn/Create
        public async Task<ActionResult> Create()
        {
            var user = Session["userid"] as SessionUser;
            DataContext<Tbltieuchi> data = new DataContext<Tbltieuchi>();
            string api = "values/TieuChiByChiNhanh";
            var a = await data.GetListBy(user.Idchinhanh.Value,api);
            ViewBag.listtieuchi = new SelectList(a, "idtieuchi", "tentieuchi");
            return View();
        }

        // POST: DapAn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Idloaitc,Tenloaitc,Trangthaitc")]Tbldapan item)
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
                return RedirectToAction("Index", "LoaiTieuChi");
            }
            var listchinhanh = await data.GetList(api);
            return View(listchinhanh);
        }

        // GET: DapAn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DapAn/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
