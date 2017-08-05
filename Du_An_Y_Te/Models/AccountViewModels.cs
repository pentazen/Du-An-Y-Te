using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Du_An_Y_Te.Models
{
    public class AccountViewModels
    {
        public class ForgetPasswordViewModel
        {
            public string TaiKhoan { get; set; }
        }

        public class LoginViewModel
        {
            public string TaiKhoan { get; set; }
            public string MatKhau { get; set; }
            public string Email { get; set; }
        }
        public class SignUpViewModel
        {
            [Required]
            [Display(Name = "TaiKhoan")]
            public string TaiKhoan { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} phải có ít nhất {2} ký tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật Khẩu")]
            public string MatKhau { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "MatKhauXacNhan")]
            [Compare("MatKhau", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không trùng nhau!")]
            public string MatKhauXacNhan { get; set; }

            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}