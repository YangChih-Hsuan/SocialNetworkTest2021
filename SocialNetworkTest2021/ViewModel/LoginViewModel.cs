using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkTest2021.ViewModel
{
    //登入
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }
    }

    //測試
    public class DateViewModel
    {
        public DateTime Brithdate { get; set; }
    }
}