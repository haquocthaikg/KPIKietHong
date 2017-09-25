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
            if (Session["userid"] == null)
            {

                return RedirectToAction("Login", "DanhGia");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
       
        public async Task<ActionResult> Details(int? id, DateTime? value)
        {
          //  var a = id;
          //  var b = value;
            if (id != null && value != null)
            {
                var user = Session["userid"] as SessionUser;
                DataContext<Tblquanlychinhanh> dtt1 = new DataContext<Tblquanlychinhanh>();
                string apik1 = "Values/QuanLyChiNhanh";

                var listcn = await dtt1.GetListBy(user.Iduser, apik1);//lay ds cn
                if (listcn.Where(p => p.Idchinhanh == id).ToList().Count > 0)
                {
                    DataContext<Tblchinhanh> datacn = new DataContext<Tblchinhanh>();
                    string apikcn = "values";

                    DataContext<Tbldanhgia> datadg = new DataContext<Tbldanhgia>();
                    string apidg = "values/DanhGia";

                    DataContext<Tbltieuchi> datatc = new DataContext<Tbltieuchi>();
                    string apitc = "values/TieuChiByChiNhanh";
                    //var a1 = await datatc.GetListBy(id.Value, apitc);//lay ds tc by cn
                    //var b1 = a1.Select(p1 => p1.Idtieuchi).ToArray();

                    DataContext<Tblnhomtieuchi> datantc = new DataContext<Tblnhomtieuchi>();
                    string apintc = "values/NhomTieuChi";

                    DataContext<Tblloaitieuchi> dataltc = new DataContext<Tblloaitieuchi>();
                    string apiltc = "values/LoaiTieuChi";

                    var listcnfull = await datacn.GetList(apikcn);

                    var listtcbycn = await datatc.GetListBy(id.Value, apitc);//lay ds tc by cn
                    var listtcbycntemp = listtcbycn.Select(p1 => p1.Idtieuchi).ToArray();

                    var listdg = await datadg.GetList(apidg);//lay ds danh gia
                    var listdgtemp = listdg.Where(p => p.Ngaydanhgia.Value.ToString("dd/MM/yyyy") == value.Value.ToString("dd/MM/yyyy") && listtcbycntemp.Contains(p.Idtieuchi.Value));
                    
                    
                    var listnhomtc = await datantc.GetList(apintc);//lay ds nhom tc
                    var listloaitc = await dataltc.GetList(apiltc);//lay ds loai tc

                    var listdgfull =( from dg in listdgtemp
                                     join tc in listtcbycn on dg.Idtieuchi equals tc.Idtieuchi
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
                                     });

                    //var nhomtieuchi = await datantc.GetList(apintc);//lay ds nhom tc 
                    //List<Tblnhomtieuchi> listntcbycn = new List<Tblnhomtieuchi>();
                    //if (nhomtieuchi.ToList().Count > 0)
                    //{
                    //    foreach (var itemnhom in nhomtieuchi)
                    //    {
                    //        if (a1.Where(p => p.Idnhomtieuchi == itemnhom.Idnhomtieuchi).Count() > 0)
                    //        {
                    //            listntcbycn.Add(itemnhom);
                    //        }
                    //    }
                    //}
                    //ViewBag.listnhomtc = listntcbycn.Distinct();

                    //List<BoKPI> listda = new List<BoKPI>();


                    //var listdgbyngay = listdg.Where(p => p.Ngaydanhgia.Value.ToString("dd/MM/yyyy") == value.Value.ToString("dd/MM/yyyy") && b1.Contains(p.Idtieuchi.Value));
                    //if (listdg.ToList().Count > 0)//co list dang gia theo nhung tieu chi cua chi nhanh
                    //{

                    //    return View(listdg);


                    //}
                    //return listda;
                    var booksGrouped = from b in listdgfull
                                       orderby b.Idnhomtieuchi
                                       group b by b.Tennhomtieuchi into g
                                       select new Group<Danhgia,string> { Key = g.Key, Values = g };
                    return View(booksGrouped.ToList());
                }
                else
                {
                    return View();//error id chi nhanh khong thuoc quyen quan lý
                }
            }
            else
            {
                return View();//error
            }
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
        public async Task<ActionResult> DanhGiaAsync(int? value)
        {
           
            if (Session["userid"] == null)
            {
               var user = Session["userid"] as SessionUser;
                ViewBag.user = user.Tennhanvien;
            }
            // listchinhanh = await GetListChiNhanh();
            return View();
        }//

                }
                else
                {
                    ViewBag.indexlistcn = listchinhanh?.Count > 0 ? listchinhanh.FirstOrDefault().Idchinhanh : 0;
                }

               
                var listbokpi = new List<BoKPI>();
                listbokpi =await GetTCList1(ViewBag.indexlistcn);
                ViewBag.bokpi = listbokpi;
                return View();
            }
        }
        public async Task<List<BoKPI>> GetTCList1(int Idchinhanh)
        {
            DataContext<Tbltieuchi> datatc = new DataContext<Tbltieuchi>();
            string apitc = "values/TieuChiByChiNhanh";
            var a1 = await datatc.GetListBy(Idchinhanh, apitc);//lay ds tc by cn
            List<BoKPI> listda = new List<BoKPI>();
            DataContext<Tbldanhgia> datadg = new DataContext<Tbldanhgia>();
            string apidg = "values/DanhGia";
            var a = await datadg.GetList(apidg);//lay ds danh gia
            if (a != null)
            {
                
                var b1 = a1.Select(p1 => p1.Idtieuchi).ToArray();
                var b = a.Select(p => p.Ngaydanhgia).OrderByDescending(p => p.Value).Distinct().ToList();
              //  b.ForEach(p => listda.Add(new BoKPI() { NgayDG = p.Value, DaCham = 0, HoanThanh = false, SoKPI = 0 }));
                foreach(var item in b.ToList())
                {
                    int countkpi = 0;
                    var c = a.Where(p => p.Ngaydanhgia.Value.ToString("dd/MM/yyyy") == item.Value.ToString("dd/MM/yyyy") && b1.Contains(p.Idtieuchi.Value));
                    countkpi = c.Count()>0?c.Count():0;
                    if(countkpi >0)
                    {
                        var tongkpidacham = c.Where(p => p.Trangthaidanhgia == true).Count();
                        listda.Add(new BoKPI() { NgayDG=item.Value, SoKPI= countkpi, DaCham=tongkpidacham, HoanThanh=countkpi==tongkpidacham?true:false, Idchinhanh=Idchinhanh });
                    }
                }
              
            }
            return listda;
        }
        public ActionResult DanhGiaCreate()
        {
            
            return View();
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
                DataContext<Tbldanhgia> datadg = new DataContext<Tbldanhgia>();
                string apidg = "values/DanhGia";
                var listdg = await datadg.GetList(apidg);
                var listtccn = a.Select(x => x.Idtieuchi).ToArray();
                var k = listdg.Where(p => p.Ngaydanhgia.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && listtccn.Contains(p.Idtieuchi.Value)).ToList();
                //kiem tra da tao hom nay chua
                if (k.Count > 0)
                {
                    TempData["msg"] = "<script>alert('Bộ KPI của chi nhánh này đã tạo hôm nay rồi');</script>";
                    return RedirectToAction("DanhGiaAsync", "DanhGia"); //tra ve loi
                }
                //kiem tra cai cuoii cung da cap nhat xong chua
                var listck = listdg.Where(p => listtccn.Contains(p.Idtieuchi.Value)).OrderByDescending(p => p.Ngaydanhgia).ToList();//danh sachtieu chi da cham cua chi nhanh 
                var listngaycuoicung = listck.Select(p => p.Ngaydanhgia).Distinct().FirstOrDefault();//ngay cham cuoi
                var k1 = listck.Where(p=>p.Ngaydanhgia.Value.ToString("dd/MM/yyyy")==listngaycuoicung.Value.ToString("dd/MM/yyyy") && p.Trangthaidanhgia!=true).ToList();

                if (k1.Count > 0)
                {
                    TempData["msg"] = "<script>alert('Bộ KPI của chi nhánh này ở kỳ trước bạn chưa hoàn thành');</script>";
                    return RedirectToAction("DanhGiaAsync", "DanhGia"); //tra ve loi
                }
                //kiem tra check ton loi
                DataContext<Tbltonkholoi> datatonloi = new DataContext<Tbltonkholoi>();
                List<Tbltonkholoi> listdatatonloiforcheck = new List<Tbltonkholoi>(); //danh sach data ton loi theo list tieu chi cua chi nhanh trong cac dot cham truoc do
                foreach(var item in a)
                {
                    var listerror = await datatonloi.GetListBy(item.Idtieuchi, "values/TonKhoLoibyTC");
                    var liisttamtonloicuatc = listerror.Where(p => p.Daxuly.Value != true).ToList();//ds ton loi chua xu ly
                    if(liisttamtonloicuatc.Count>0)
                    {
                        listdatatonloiforcheck.AddRange(liisttamtonloicuatc);
                    }
                }
                if (listdatatonloiforcheck.Count > 0)
                {
                    TempData["msg"] = "<script>alert('Trong bộ KPI của chi nhánh này ở các kỳ trước còn  vài tiêu chí tồn lỏi chưa xữ lý');</script>";
                    return RedirectToAction("DanhGiaAsync", "DanhGia"); //tra ve loi
                }

                List<Tbldanhgia> list = new List<Tbldanhgia>();
                if(a.Count()>0)
                {
                    a.OrderBy(p => p.Idtieuchi).ToList().ForEach(p => list.Add(new Tbldanhgia() { Ngaydanhgia=DateTime.Now, Idtieuchi=p.Idtieuchi, Trangthaidanhgia=false }));
                }
                
                int i = 0;
                if (list.Count>0)
                {
                   
                    foreach(var item in list)
                    {
                        var k12 = await datadg.Create(item, apidg);
                        if (k12) i++;
                    }
                }
               if(i>0)
                {
                    TempData["msg"] = "<script>alert('Đã tạo mới bộ KPI ngày "+ DateTime.Now.ToString("dd/MM/yyyy") + " thành công có "+ i.ToString()+" KPI được tại');</script>";
                }
                else
                {
                    TempData["msg"] = "<script>alert('Tạo bộ KPI không thành công');</script>";
                }
              
                return RedirectToAction("DanhGiaAsync", "DanhGia"); //tra ve loi
            }
            else
            {
                return View(); //tra ve loi
            }
        }
       
    }
}