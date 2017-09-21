using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KPIKietHong.Models
{
    public class CheckEmail
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn chưa nhập Email")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Sai định dạng Email")]
        [Remote("CheckEmailForgetPass", "Login", ErrorMessage = "Email chưa có trong hệ thống")]
        public string Email { get; set; }
    }
}