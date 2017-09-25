using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIKietHong.Models
{
    public class BoKPI
    {
        public DateTime NgayDG { get; set; }
        public int? SoKPI { get; set; }
        public bool? HoanThanh { get; set; }
        public int? DaCham { get; set; }
        public int? Idchinhanh { get; set; }
    }
}