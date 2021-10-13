using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
using System;
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
            ResponseViewModel<string> result = new ResponseViewModel<string>();

            try
            {
                //連接資料庫
                var memberdb = new SocialNetworkTest2021Entities();

                //會員註冊資料參數定義
                var nickName = singupViewModel.nickName; //會員名稱
                var account = singupViewModel.Account; //會員帳號
                var password = singupViewModel.Password; //會員密碼
                var passwordCheck = singupViewModel.PasswordCheck; //密碼確認
                var mail = singupViewModel.Mail; //會員Mail
                var vCode = singupViewModel.VCode; //驗證碼

                //檢查是否輸入空值
                if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(mail) || string.IsNullOrEmpty(nickName) || string.IsNullOrEmpty(passwordCheck) || string.IsNullOrEmpty(vCode))
                {
                    result.ResultCode = 0;
                    result.Message = "尚有資料未輸入完成!";
                    return result;
                }

                //檢查會員密碼與密碼確認是否相符
                if ( password != passwordCheck) {
                    result.ResultCode = 0;
                    result.Message = "確認密碼與密碼不相符!";
                    return result;
                }

                //檢查輸入資料是否符合規則


                //檢查帳號和信箱是否已註冊過(兩者都不能重複)

                //註冊資料寫入DB
                var newMember = new Member()
                {
                    Account = account,
                    NickName = nickName,
                    Password = password,
                    Mail = mail,
                    Birthday = null,
                    Interest = null,
                    Job = null,
                    Education = null,
                    InfoStatus = 15,//預設值
                    Status = 1,//預設值
                    CreateDate = DateTime.Now
                };
                memberdb.Member.Add(newMember);
                memberdb.SaveChanges();

                //檢查是否已註冊成功
                if (newMember != null)
                {
                    result.ResultCode = 1;
                    result.Message = "註冊成功";

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = 0;
                result.Message = "註冊失敗";
            }
            return result;
        }

        //測試日期傳值
        [HttpPost, Route(nameof(MemberAPIController.Datetest))]
        public ResponseViewModel<string> Datetest(DateViewModel DateViewModel)
        {
            return new ResponseViewModel<string>();
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