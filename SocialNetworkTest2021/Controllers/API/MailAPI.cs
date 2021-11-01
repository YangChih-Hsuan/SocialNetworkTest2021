using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
using System;
using System.Linq;
using System.Net.Mail;
using System.Web.Http;

namespace SocialNetworkTest2021.Controllers.API
{
    [RoutePrefix("API/MailAPI")]
    public class MailAPIController : ApiController
    {
        //驗證Mail是否註冊過 & 驗證碼寫入DB
        [HttpPost, Route(nameof(MailAPIController.CheckMail))]
        public ResponseViewModel<string> CheckMail(MailModels mailAccount)
        {
            var memberdb = new SocialNetworkTest2021Entities();

            //從DB撈出相同的Mail
            var accountMail = memberdb.Member.Where(w => w.Mail == mailAccount.Mail).FirstOrDefault();

            ResponseViewModel<string> result = new ResponseViewModel<string>();

            //用來接MailSend回傳值 使用MailSend回傳相同型別
            var mailSendResult = new ResponseViewModel<string>();

            //檢查是否輸入空值
            if (string.IsNullOrEmpty(mailAccount.Mail))
            {
                result.ResultCode = 0;
                result.Message = "請輸入電子郵件!";
                return result;
            }
            //檢查是否已註冊過
            if (accountMail != null) {
                result.ResultCode = 0;
                result.Message = "此電子郵件已註冊過!";
                return result;
            }

            if (accountMail == null)
            {
                //MailSend呼叫&回傳給mailSendResult變數
                mailSendResult = this.MailSend(mailAccount);
                result.ResultCode = 1;
                result.Message = mailSendResult.Message;

                //驗證碼寫入DB
                var newVCode = new VerificationCode()
                {
                    VCode = int.Parse(mailSendResult.Data), //轉int
                    Status = 0, //預設狀態(未驗證註冊)
                    CreateDate = DateTime.Now
                };
                memberdb.VerificationCode.Add(newVCode);
                memberdb.SaveChanges();
            }
            else
            {
                result.ResultCode = 0;
                result.Message = "註冊失敗!";
            }

            return result;
        }

        //Mail發送
        [HttpPost, Route(nameof(MailAPIController.MailSend))]
        public ResponseViewModel<string> MailSend(MailModels mailviewmodels)
        {
            ResponseViewModel<string> result = new ResponseViewModel<string>();
            var mailString = mailviewmodels.Mail;
            MailMessage mail = new MailMessage();

            //檢查是否輸入空值
            if (string.IsNullOrEmpty(mailString))
            {
                result.ResultCode = 0;
                result.Message = "請輸入電子郵件!";
                return result;
            }

            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress("shanna615615615.sy@gmail.com", "IKKON");

            //收信者email
            mail.To.Add(mailString);

            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "IKKON 註冊驗證";

            //Mail內容
            //Random為虛擬亂數產生器
            //Random.Next傳回非負值的隨機整數
            string vCode = new Random().Next(10000).ToString();
            mail.Body = "<h1>驗證碼:" + vCode + "</h1>";

            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("shanna615615615.sy@gmail.com", "freioxiokmbggeog");

            //開啟ssl
            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的MySmtp
            MySmtp = null;

            //放掉宣告出來的mail
            mail.Dispose();

            result.ResultCode = 1;
            result.Message = "已發送";
            result.Data = vCode;

            return result;

            /*
              (2021/09/28) 待補發送失敗判斷
              (2021/10/09) 待補驗證碼到期時間(點註冊打API檢查驗證碼到期時間)
            */
        }
    }
}