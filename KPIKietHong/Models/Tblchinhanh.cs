using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tblchinhanh
    {
        [Key]
        [Display(Name = "ID chi nhánh")]
        public int Idchinhanh { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Bạn chưa nhập mã chi nhánh")]
        [StringLength(10, MinimumLength = 3)]
        [Display(Name = "Mã chi nhanh")]
        public string Machinhanh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập tên chi nhánh")]
        [StringLength(150, MinimumLength = 3,ErrorMessage ="Tối đa 150 ký tự")]
        [Display(Name = "Tên chi nhanh")]
        public string Tenchinhanh { get; set; }
        [Required(ErrorMessage ="Vui lòng check trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Tragnthaicn { get; set; }

      
    }
}
