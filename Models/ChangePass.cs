using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KPIKietHong.Models
{
    public class ChangePass
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Chưa nhập mật khẩu")]
        [Remote("CheckOldPass", "ChangePass", ErrorMessage = "Mật khẩu cũ không đúng")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
       

        [Required(AllowEmptyStrings = false, ErrorMessage = "Chưa nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Mật khẩu nhập lại không trùng")]

        [Display(Name = "Nhập lại mật khẩu")]

        public string PasswordConfirm { get; set; }
    }
}