using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
using System.Linq;
using System.Web.Http;

namespace SocialNetworkTest2021.Controllers.API
{
    [RoutePrefix("API/MemberAPI")]
    public class MemberAPIController : ApiController
    {
        //會員登入
        [HttpPost, Route(nameof(MemberAPIController.LoginV2))]
        public ResponseViewModel<string> LoginV2(LoginViewModel loginViewModel)
        {
            var memberdb = new SocialNetworkTest2021Entities();
            var account = memberdb.Member.Where(w => w.Account == loginViewModel.Account && w.Password == loginViewModel.Password).FirstOrDefault();
            ResponseViewModel<string> result = new ResponseViewModel<string>();

            if (account != null)
            {
                result.ResultCode = 1;
                result.Message = "登入成功";
                result.Data = "帳號:" + account.Account + "\n密碼:" + account.Password;

                return result;
            }
            result.ResultCode = 0;
            result.Message = "帳號或密碼錯誤";

            return result;
        }

        //會員註冊
        [HttpPost, Route(nameof(MemberAPIController.Singup))]
        public ResponseViewModel<string> Singup(SingupViewModel singupViewModel)
        {
            var memberdb = new SocialNetworkTest2021Entities();
            //var account = memberdb.Member.Where(w => w.Account == loginViewModel.Account && w.Password == loginViewModel.Password).FirstOrDefault();
            ResponseViewModel<string> result = new ResponseViewModel<string>();

            //if (account != null)
            //{
            //    result.ResultCode = 1;
            //    result.Message = "登入成功";
            //    result.Data = "帳號:" + account.Account + "\n密碼:" + account.Password;

            //    return result;
            //}
            //result.ResultCode = 0;
            result.Message = "建置中";

            return result;
        }

        //密碼變更

        //個人主頁設定
    }
}