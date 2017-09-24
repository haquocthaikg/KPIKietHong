using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIKietHong.Models
{
    public class Danhgia
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
        public string Tentieuchi { get; set; }
        public int? Idchinhanh { get; set; }
        public string Tenchinhanh { get; set; }
        public int? Idnhomtieuchi { get; set; }
        public string Tennhomtieuchi { get; set; }
        public int? Idloaitc { get; set; }
        public string Tenloaitc { get; set; }
    }
}