using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KPI.Models
{
    public class SessionUser
    {
        public int Iduser { get; set; }
        public string Tennhanvien { get; set; }
        public string Username { get; set; }
       
        public string Tolken { get; set; }
        public string Email { get; set; }
        public string Sodienthoai { get; set; }
       
       
      
        
       
        public int? Idchinhanh { get; set; }
        public bool? Isadmin { get; set; }
    }
}
