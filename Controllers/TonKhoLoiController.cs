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
        public async Task<ActionResult> Index()
        {
            if (Session["userid"] == null)
            {
                Response.Redirect("~/DanhGia/Login");
            }
            else
            {
                DataContext<Tbltonkholoi> data = new DataContext<Tbltonkholoi>();
                string api = "values/TonKhoLoi";
                var a = await data.GetList(api);
                var b = a.Where(x=>x.Daxuly ==false);

                // int pageSize = 8;
                //  int pageNumber = (page ?? 1);
                //return View(a.OrderBy(x=>x.Idchinhanh).ToPagedList(pageNumber, pageSize));
                return View(b);
            }
           
            return View();
        }

    }
}