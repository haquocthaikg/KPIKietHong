using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIKietHong.Models
{
    public class TieuChiTonKhoLoiCN
    {
        public int Idtonkholoi { get; set; }
        public int? Idtieuchi { get; set; }
        public DateTime? Ngaychamdiem { get; set; }
        public bool? Daxuly { get; set; }
        public bool? Decen { get; set; }
        public string Tentieuchi { get; set; }
    }
}