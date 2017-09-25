using KPIKietHong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KPIKietHong.Controllers
{
    public class TonKhoLoiController : Controller
    {
        // GET: TonKhoLoi
        [ChildActionOnly]
        public async Task<PartialViewResult> Index()
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("~/DanhGia/Login");
            }
            else
            {
                DataContext<Tbltonkholoi> data = new DataContext<Tbltonkholoi>();
                string api = "values/TonKhoLoi";
                DataContext<Tbltieuchi> datatieuchi = new DataContext<Tbltieuchi>();
                string apitc = "values/TieuChi";

                DataContext<Tblchinhanh> datacn = new DataContext<Tblchinhanh>();
                string apicn = "values"; 

                var tonkholoi = await data.GetList(api);
                var tieuchi = await datatieuchi.GetList(apitc);
                var chinhanh = await datacn.GetList(apicn); 
                
                var a = from x in tonkholoi
                        join y in tieuchi on x.Idtieuchi equals y.Idtieuchi
                        join z in chinhanh on y.Idchinhanh equals z.Idchinhanh
                        select new TieuChiTonKhoLoiCN
                        {
                            Idtonkholoi = x.Idtonkholoi,
                            Idtieuchi = x.Idtieuchi,
                            Ngaychamdiem = x.Ngaychamdiem,
                            Daxuly = x.Daxuly,
                            Decen = x.Decen,
                            Tentieuchi = y.Tentieuchi,
                           Tenchinhanh = z.Tenchinhanh
                        };

                var b = a.Where(x => x.Daxuly == false);
                return PartialView(b);


            }

            return PartialView();
        }


       
    }
}