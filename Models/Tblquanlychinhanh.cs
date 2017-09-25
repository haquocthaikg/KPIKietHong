using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KPIKietHong.Models
{
    public class Tblquanlychinhanh
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn người quản lý")]
        [Display(Name = "Người dùng")]
        [Remote("CheckUserCN", "QuanLyChiNhanh", AdditionalFields= "Idchinhanh", ErrorMessage = "Tài khoản này đã có quản lý chi nhánh này trong hệ thống. Xin chọn Tài khoản khác cho chi nhánh")]
        public int Iduser { get; set; }
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn chi nhánh cho người quản lý")]
        [Display(Name = "Chi nhánh quản lý")]
        [Remote("CheckUserCN", "QuanLyChiNhanh", AdditionalFields = "Iduser", ErrorMessage = "Tài khoản trên đã có quản lý chi nhánh này trong hệ thống. Xin chọn Chi nhánh khác cho Tài khoản")]
        public int Idchinhanh { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Trangthaiquanly { get; set; }

      
    }
}
