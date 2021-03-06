﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KPIKietHong.Models
{
    public class Tblnhanvien
    {
        [Key]
        [Display(Name = "ID User")]
        public int Iduser { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Chưa nhập tên nhân viên")]
        [Display(Name = "Tên nhân viên")]
        public string Tennhanvien { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Chưa nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        [Remote("CheckUser", "Login", ErrorMessage = "Tên đăng nhập đã có trong hệ thống. Xin chọn tên khác")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Chưa nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
   
       


        public string Tolken { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email")]
        [Remote("CheckEmail","Login", ErrorMessage = "Email đã có trong hệ thống")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập điện thoại")]
        [Display(Name = "Điện thoại")]
        public string Sodienthoai { get; set; }
        public string Secrect { get; set; }
        public string Keychung { get; set; }
        public string Keyrieng { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Khoa { get; set; }
        public string Giatrixacthuc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn chi nhánh")]
        [Display(Name = "Chi nhánh")]
        public int? Idchinhanh { get; set; }

       
    }
}
