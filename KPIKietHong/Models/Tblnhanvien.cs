using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tblnhanvien
    {
        [Key]
        [Display(Name = "ID User")]
        public int Iduser { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Họ Tên Nhân Viên")]
        [Display(Name = "Họ Tên Nhân Viên")]
        public string Tennhanvien { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Tên Đăng Nhập")]
        [Display(Name = "Tên Đăng Nhập")]
        public string Username { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Mật Khẩu")]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }
        public string Tolken { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Điện Thoại")]
        [Display(Name = "Điện Thoại")]
        public string Sodienthoai { get; set; }
        public string Secrect { get; set; }
        public string Keychung { get; set; }
        public string Keyrieng { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa Chọn Trạng Thái")]
        [Display(Name = "Trạng Thái")]
        public bool? Khoa { get; set; }
        public string Giatrixacthuc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa Chọn Chi Nhánh")]
        [Display(Name = "Chi Nhánh")]
        public int? Idchinhanh { get; set; }

       
    }
}
