using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tbltieuchi
    {

        [Key]
        [Display(Name = "ID User")]
        public int Idtieuchi { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Mã Tiêu Chí")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Mã Tiêu Chí tối thiểu 3 ký tự và tối đa 10 ký tự")]
        [Display(Name = "Mã Tiêu Chí")]
        public string Matieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Tên Tiêu Chí")]
        [Display(Name = "Tên Tiêu Chí")]
        public string Tentieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa Chọn Chi nhánh")]
        [Display(Name = "Chi Nhánh")]
        public int? Idchinhanh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa Chọn Nhóm Tiêu Chí")]
        [Display(Name = "Nhóm Tiêu Chí")]
        public int? Idnhomtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa Chọn Trạng Thái")]
        [Display(Name = "Trạng Thái")]
        public bool? Trangthaitc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Điểm Tiêu Chí")]
        [Display(Name = "Điểm Tiêu Chí")]
        public double? Diemtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa Chọn Loại Tiêu Chí")]
        [Display(Name = "Loại Tiêu Chí")]
        public int? Idloaitc { get; set; }

       
    }
}
