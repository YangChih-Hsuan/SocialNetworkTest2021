using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SocialNetworkTest2021.Controllers.API
{
    [RoutePrefix("API/MemberAPI")]
    public class MemberAPIController : ApiController
    {
        [HttpPost, Route(nameof(MemberAPIController.Login))]
        public ResponseViewModel<string> Login(LoginViewModel loginViewModel)
        {
            var memberdb = new SocialNetworkTest2021Entities();
            var account = memberdb.Member.Where(w => w.Account == loginViewModel.Account && w.Password == loginViewModel.Password).FirstOrDefault();
            string response = "";
            string b = "";
            string a = "";
            if (account != null)
            {
                a = "登入成功";
                response = "帳號:" + account.Account + "\n密碼:" + account.Password;
            }
            else
            {
                b = "帳號或密碼錯誤";
            }
            ResponseViewModel<string> result = new ResponseViewModel<string>
            {
                ResultCode = 1,
                Message = a,
                Data = response
            };
            if (account != null)
            {
                return result;
            }
            result.ResultCode = 0;
            result.Message = b;
            return result;
        }

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

        
    }
}