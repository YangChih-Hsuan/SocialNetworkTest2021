namespace SocialNetworkTest2021.ViewModel
{
    //註冊
    public class SingupViewModel
    {
        public string nickName { get; set; } //會員名稱
        public string Account { get; set; } //會員帳號
        public string Password { get; set; } //會員密碼
        public string PasswordCheck { get; set; } //密碼確認
        public string Mail {get; set;} //會員Mail
        public string VCode { get; set; } //註冊驗證碼
    }
}