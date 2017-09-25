using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KPIKietHong
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            if (!File.Exists(Server.MapPath("/Count_Visited.txt")))
                File.WriteAllText(Server.MapPath("/Count_Visited.txt"),"0");
            Application["DaTruyCap"] = int.Parse(File.ReadAllText(Server.MapPath("Count_Visited.txt")));


         
        }
        protected void Session_Start()
        {
            Session["userid"] = null;


            // Tăng số đang truy cập lên 1 nếu có khách truy cập
            if (Application["DangTruyCap"] == null)
                Application["DangTruyCap"] = 1;
            else
                Application["DangTruyCap"] = (int)Application["DangTruyCap"] + 1;
            // Tăng số đã truy cập lên 1 nếu có khách truy cập
            Application["DaTruyCap"] = (int)Application["DaTruyCap"] + 1;
            File.WriteAllText(Server.MapPath("/Count_Visited.txt"), Application["DaTruyCap"].ToString());
        }
        protected void Session_End()
        {
            Application["DangTruyCap"] = (int)Application["DangTruyCap"] - 1;
        }
    }
}
