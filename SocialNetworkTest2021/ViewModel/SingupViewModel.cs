using System.ComponentModel.DataAnnotations;

namespace SocialNetworkTest2021.ViewModel
{
    //註冊
    public class SingupViewModel
    {
        [Required(ErrorMessage = "請輸入會員名稱")]
        public string NickName { get; set; } //會員名稱

        [Required(ErrorMessage = "請輸入會員帳號")]
        public string Account { get; set; } //會員帳號

        [Required(ErrorMessage = "請輸入會員密碼")]
        public string Password { get; set; } //會員密碼

        [Required(ErrorMessage = "請輸入會員密碼確認")]
        public string PasswordCheck { get; set; } //密碼確認

        [Required(ErrorMessage = "請輸入會員信箱")]
        public string Mail {get; set;} //會員Mail

        [Required(ErrorMessage = "請輸入驗證碼")]
        public string VCode { get; set; } //註冊驗證碼
    }
}