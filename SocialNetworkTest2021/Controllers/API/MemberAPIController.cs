using SocialNetworkTest2021.Models;
using System.Web.Http;

namespace SocialNetworkTest2021.Controllers.API
{
    [RoutePrefix("API/MemberAPI")]
    public class MemberAPIController : ApiController
    {
        [HttpPost, Route(nameof(MemberAPIController.Login))]
        public ResponseViewModel<string> Login(LoginViewModel loginViewModel)
        {
            string response = "帳號:" + loginViewModel.Account + "\n密碼:" + loginViewModel.Password;
            ResponseViewModel<string> result = new ResponseViewModel<string>
            {
                ResultCode = 1,
                Message = response,
            };
            return result;
        }
    }
}