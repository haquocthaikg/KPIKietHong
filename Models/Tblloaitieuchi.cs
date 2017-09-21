using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tblloaitieuchi
    {
        [Key]
        [Display(Name ="ID")]
        public int Idloaitc { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="Chưa nhập tên loại tiêu chí")]
        [Display(Name = "Tên loại")]
        public string Tenloaitc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Trangthaitc { get; set; }


    }
}