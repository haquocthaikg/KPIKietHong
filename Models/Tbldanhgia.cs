using System;
using System.Collections.Generic;

namespace KPIKietHong.Models
{
    public class Tbldanhgia
    {
        public int Iddanhgia { get; set; }
        public int? Idtieuchi { get; set; }
        public int? Iddapan { get; set; }
        public double? Diemdanhgia { get; set; }
        public DateTime? Ngaydanhgia { get; set; }
        public int? Iduser { get; set; }
        public string Ghichu { get; set; }
        public string Noidungdanhgia { get; set; }
        public bool? Trangthaidanhgia { get; set; }

    
    }
}
