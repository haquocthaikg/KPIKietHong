using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KPIKietHong.Models
{
    public class Tblloaitieuchi
    {
        [Key]
        [Display(Name = "ID Loại")]
        public int Idloaitc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Chưa nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        [Remote("CheckTenLoaiTC", "LoaitieuChi", ErrorMessage = "Loại tiêu chí này đã có trong hệ thống. Xin chọn tên khác")]
        public string Tenloaitc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Trangthaitc { get; set; }


    }
}