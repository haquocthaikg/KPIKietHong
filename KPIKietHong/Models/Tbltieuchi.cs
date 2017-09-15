using System;
using System.Collections.Generic;

namespace KPIKietHong.Models
{
    public class Tbltieuchi
    {
    

        public int Idtieuchi { get; set; }
        public string Matieuchi { get; set; }
        public string Tentieuchi { get; set; }
        public int? Idchinhanh { get; set; }
        public int? Idnhomtieuchi { get; set; }
        public bool? Trangthaitc { get; set; }
        public double? Diemtieuchi { get; set; }
        public int? Idloaitc { get; set; }

       
    }
}
