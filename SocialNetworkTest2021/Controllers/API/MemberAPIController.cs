using SocialNetworkTest2021.Enum;
using SocialNetworkTest2021.Helper;
using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace SocialNetworkTest2021.Controllers.API
{
    [RoutePrefix("API/MemberAPI")]
    public class MemberAPIController : ApiController
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //會員登入
        [HttpPost, Route(nameof(MemberAPIController.Login))]
        public ResponseViewModel<string> Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ModelState.AsFailResponse();

                using (var memberdb = new SocialNetworkTest2021Entities())
                {
                    if (memberdb.Member.Where(w => w.Account == loginViewModel.Account && w.Password == loginViewModel.Password).Any())
                    {
                        SetLoginCookie(loginViewModel.Account);
                        return "登入成功!".AsSuccessResponse();
                    }

                    return "帳密錯誤!".AsSuccessResponse();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"會員登入發生錯誤!, ex:{ex}");
                return "會員登入發生錯誤!".AsFailResponse();
            }
        }

        //會員註冊
        [HttpPost, Route(nameof(MemberAPIController.Singup))]
        public ResponseViewModel<string> Singup(SingupViewModel singupViewModel)
        {
            ResponseViewModel<string> result = new ResponseViewModel<string>();

            try
            {
                if (!ModelState.IsValid)
                    return ModelState.AsFailResponse();

                //檢查會員密碼與密碼確認是否相符
                if (singupViewModel.Password != singupViewModel.PasswordCheck)
                    return "確認密碼與密碼不相符!".AsFailResponse();

                using (var memberdb = new SocialNetworkTest2021Entities())
                {

                    if(memberdb.Member.Where(w => w.Account == singupViewModel.Account || w.Mail == singupViewModel.Mail).Any())
                        return "會員帳號或信箱已被註冊!".AsFailResponse();

                    DateTime expiryDate = DateTime.Now.AddMinutes(-10);
                    var singupVCodeCheck = memberdb.VerificationCode.Where(w => w.Mail == singupViewModel.Mail && 
                                                                                w.Status == (int)VerificationEnum.NotAuth &&
                                                                                w.CreateDate > expiryDate &&
                                                                                w.VCode == singupViewModel.VCode).FirstOrDefault();

                    if (singupVCodeCheck == null)
                    {
                        return "驗證碼錯誤!".AsFailResponse();
                    }

                    // 更新驗證碼狀態
                    singupVCodeCheck.Status = (int)VerificationEnum.AuthSuccess;
                    singupVCodeCheck.VerificationDate = DateTime.Now;

                    //註冊資料寫入DB
                    var newMember = new Member()
                    {
                        Account = singupViewModel.Account,
                        NickName = singupViewModel.NickName,
                        Password = singupViewModel.Password,
                        Mail = singupViewModel.Mail,
                        Birthday = null,
                        Interest = null,
                        Job = null,
                        Education = null,
                        InfoStatus = (int)MemberInfoEnum.All,
                        Status = (int)MemberStatusEnum.在線,
                        CreateDate = DateTime.Now
                    };
                    memberdb.Member.Add(newMember);
                    memberdb.SaveChanges();

                    SetLoginCookie(singupViewModel.Account);

                    return "註冊成功!".AsSuccessResponse();
                }
            }
            catch (Exception ex)
            {
                logger.Error($"會員註冊發生錯誤!, ex:{ex}");
                return "會員註冊發生錯誤!".AsFailResponse();
            }
        }

        //測試日期傳值
        [HttpPost, Route(nameof(MemberAPIController.Datetest))]
        public ResponseViewModel<string> Datetest(DateViewModel DateViewModel)
        {
            return new ResponseViewModel<string>();
        }

        /// <summary>
        /// 設定 Cookie
        /// </summary>
        private void SetLoginCookie(string account)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, "AuthCookie", DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30), true, account);

            HttpCookie Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                Expires = ticket.Expiration,
                HttpOnly = true,
                Path = string.IsNullOrWhiteSpace(ticket.CookiePath) ? FormsAuthentication.FormsCookiePath : ticket.CookiePath,
                Secure = FormsAuthentication.RequireSSL
            };

            HttpContext.Current.Response.Cookies.Add(Cookie);
            HttpContext.Current.Session["LoginAccount"] = account;
        }

        //密碼變更

        //個人主頁設定

        /*驗證碼
            DB兩個欄位 驗證碼、註冊狀態(註冊成功1 & 未完成註冊0)
            點取得驗證碼按鈕時，資料已經寫到DB，註冊狀態為0
            1. 驗證碼過期
            2. 多一步檢查是否註冊成功

            定期清理DB的排程(API可寫 但要用執行續每一段時間跑一次)
         */
    }
}