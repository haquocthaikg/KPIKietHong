using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KPIKietHong.Models
{
    public class Tblnhomtieuchi
    {

        [Key]
        [Display(Name = "ID")]
        public int Idnhomtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập tên nhóm tiêu chí")]
        [Display(Name = "Tên nhóm tiêu chí")]
        public string Tennhomtieuchi { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public bool? Trangthaintc { get; set; }

       
    }
}
