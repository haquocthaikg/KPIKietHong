using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tbltieuchi
    {

        [Key]
        [Display(Name = "ID")]
        public int Idtieuchi { get; set; }
       
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập mã tiêu chí")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Mã tiêu chí tối thiểu 3 ký tự và tối đa 10 ký tự")]
        [Display(Name = "Mã TC")]
        public string Matieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập tên tiêu chí")]
        [Display(Name = "Tên tiêu chí")]
        public string Tentieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn chi nhánh")]
        [Display(Name = "Chi nhánh")]
        public int? Idchinhanh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn nhóm tiêu chí")]
        [Display(Name = "Nhóm tiêu chí")]
        public int? Idnhomtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Trangthaitc { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập điểm tiêu chí")]
        [RegularExpression(@"(^(\+|\-)(0|([1-9][0-9]*))(\.[0-9]{1,2})?$)|(^(0{0,1}|([1-9][0-9]*))(\.[0-9]{1,2})?$)", ErrorMessage = "Điểm chưa đúng định dạng. Bạn phải nhập dạng: 00.00, VD: 4.5")]
        [Display(Name = "Điểm")]
        public double? Diemtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn loại tiêu chí")]
        [Display(Name = "Loại TC")]
        public int? Idloaitc { get; set; }

       
    }
}
