using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tblnhomtieuchi
    {

        [Key]
        [Display(Name = "ID Nhóm Tiêu Chí")]
        public int Idnhomtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Tên Nhóm Tiêu Chí")]
        [Display(Name = "Tên Nhóm Tiêu Chí")]
        public string Tennhomtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa Chọn Trạng Thái")]
        [Display(Name = "Trạng Thái")]
        public bool? Trangthaintc { get; set; }

       
    }
}
