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
    public class ChamKPIController : Controller
    {
        /* GET: ChamKPI*/
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
                string apik1 = "Values/QuanLyChiNhanh";

                var listcn = await dtt1.GetListBy(user.Iduser, apik1);//lay ds cn
                List<Tblchinhanh> listchinhanh = new List<Tblchinhanh>();
                if (listcn != null || listcn.Count() > 0)
                {
                    DataContext<Tblchinhanh> datacn = new DataContext<Tblchinhanh>();
                    string apikcn = "values";
                    foreach (var cn in listcn.Where(c => c.Trangthaiquanly == true))
                    {
                        var item = await datacn.GetList(cn.Idchinhanh, apikcn);
                        if (item != null)
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


                //var listbokpi = new List<BoKPI>();
                //listbokpi = await GetTCList1(ViewBag.indexlistcn);
                //ViewBag.bokpi = listbokpi;
                return View();
            }
        }

        // GET: ChamKPI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Idchinhanh")]int? Idchinhanh)
        {
            DataContext<Tblchinhanh> datacn = new DataContext<Tblchinhanh>();
            string apikcn = "values";

            DataContext<Tbldanhgia> datadg = new DataContext<Tbldanhgia>();
            string apidg = "values/DanhGia";

            DataContext<Tbltieuchi> datatc = new DataContext<Tbltieuchi>();
            string apitc = "values/TieuChiByChiNhanh";

            DataContext<Tblnhomtieuchi> datantc = new DataContext<Tblnhomtieuchi>();
            string apintc = "values/NhomTieuChi";

            DataContext<Tblloaitieuchi> dataltc = new DataContext<Tblloaitieuchi>();
            string apiltc = "values/LoaiTieuChi";

            var listcnfull = await datacn.GetList(apikcn);//ds chi nhanh

            var listtc = await datatc.GetList(apitc);//lay ds tc
          //  var listtcbycntemp = listtcbycn.Select(p1 => p1.Idtieuchi).ToArray();

            var listdg = await datadg.GetList(apidg);//lay ds danh gia
         //   var listdgtemp = listdg.Where(p => p.Ngaydanhgia.Value.ToString("dd/MM/yyyy") == value.Value.ToString("dd/MM/yyyy") && listtcbycntemp.Contains(p.Idtieuchi.Value));


            var listnhomtc = await datantc.GetList(apintc);//lay ds nhom tc
            var listloaitc = await dataltc.GetList(apiltc);//lay ds loai tc

            var listdgfull = (from dg in listdg
                              join tc in listtc on dg.Idtieuchi equals tc.Idtieuchi
                              join nh in listnhomtc on tc.Idnhomtieuchi equals nh.Idnhomtieuchi
                              join cn in listcnfull on tc.Idchinhanh equals cn.Idchinhanh
                              join ltc in listloaitc on tc.Idloaitc equals ltc.Idloaitc
                              select new Danhgia
                              {
                                  Iddanhgia = dg.Iddanhgia,
                                  Idtieuchi = dg.Idtieuchi,
                                  Tentieuchi = tc.Tentieuchi,
                                  Iddapan = dg.Iddapan,
                                  Diemdanhgia = dg.Diemdanhgia,
                                  Ngaydanhgia = dg.Ngaydanhgia,
                                  Iduser = dg.Iduser,
                                  Ghichu = dg.Ghichu,
                                  Noidungdanhgia = dg.Noidungdanhgia,
                                  Trangthaidanhgia = dg.Trangthaidanhgia,
                                  Idchinhanh = tc.Idchinhanh,
                                  Tenchinhanh = cn.Tenchinhanh,
                                  Idnhomtieuchi = tc.Idnhomtieuchi,
                                  Tennhomtieuchi = nh.Tennhomtieuchi,
                                  Idloaitc = tc.Idloaitc,
                                  Tenloaitc = ltc.Tenloaitc
                              }).Where(p=>p.Idchinhanh== Idchinhanh).OrderByDescending(p=>p.Ngaydanhgia);

            var test1 = listdgfull.Select(p => p.Ngaydanhgia).Distinct().FirstOrDefault();
            var listfull = listdgfull.Where(p => p.Ngaydanhgia.Value.ToString("dd/MM/yyyy") == test1.Value.ToString("dd/MM/yyyy"));
            var booksGrouped = from b in listfull
                               orderby b.Idnhomtieuchi
                               group b by b.Tennhomtieuchi into g
                               select new Group<Danhgia, string> { Key = g.Key, Values = g };
            return View(booksGrouped.ToList());
            
        }

        // POST: ChamKPI/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ChamKPI/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChamKPI/Edit/5
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

        // GET: ChamKPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChamKPI/Delete/5
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
