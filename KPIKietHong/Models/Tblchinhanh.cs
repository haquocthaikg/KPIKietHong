using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tblchinhanh
    {
        [Key]
        [Display(Name = "ID")]
        public int Idchinhanh { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Bạn chưa nhập mã chi nhánh")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Tối đa 10 ký tự, tối thiểu 3 ký tự")]
        [Display(Name = "Mã chi nhánh")]
        public string Machinhanh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập tên chi nhánh")]
        [StringLength(150, MinimumLength = 3,ErrorMessage ="Tối đa 150 ký tự")]
        [Display(Name = "Tên chi nhánh")]
        public string Tenchinhanh { get; set; }
        [Required(ErrorMessage ="Vui lòng chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Tragnthaicn { get; set; }

      
    }
}
