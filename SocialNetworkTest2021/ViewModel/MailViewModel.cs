using System.ComponentModel.DataAnnotations;

namespace SocialNetworkTest2021.ViewModel
{
    public class MailViewModel
    {
        [Required(ErrorMessage = "請輸入電子郵件")]
        public string Mail { get; set; }
    }
}