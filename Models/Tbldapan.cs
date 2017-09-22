using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KPIKietHong.Models
{
    public class Tbldapan
    {
        [Key]
        [Display(Name = "ID Đáp Án")]
        public int Iddapan { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Chưa nhập chọn tiêu chí")]
        [Display(Name = "Tiêu chí")]
        
        public int? Idtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập tên dáp án")]
        [Display(Name = "Tên đáp án")]
        public string Tendapan { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập số điểm quy định")]
        [Display(Name = "Số điểm quy định")]
        [Range(0, double.MaxValue, ErrorMessage = "Vui lòng chọn số điểm lớn hơn hoặc bằng 0")]
        public double? Diemdapan { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Trangthaidapan { get; set; }

       
    }
}
