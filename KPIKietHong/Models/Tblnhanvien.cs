using System;
using System.Collections.Generic;

namespace KPIKietHong.Models
{
    public class Tblnhanvien
    {
       

        public int Iduser { get; set; }
        public string Tennhanvien { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Tolken { get; set; }
        public string Email { get; set; }
        public string Sodienthoai { get; set; }
        public string Secrect { get; set; }
        public string Keychung { get; set; }
        public string Keyrieng { get; set; }
        public bool? Khoa { get; set; }
        public string Giatrixacthuc { get; set; }
        public int? Idchinhanh { get; set; }

       
    }
}
