using SocialNetworkTest2021.Enum;
using SocialNetworkTest2021.Helper;
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
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 發送驗證碼
        /// </summary>
        [HttpPost, Route(nameof(MailAPIController.SendVCode))]
        public ResponseViewModel<string> SendVCode(MailViewModel mailAccount)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ModelState.AsFailResponse();

                using (var memberdb = new SocialNetworkTest2021Entities())
                {
                    var mailSendResult = new ResponseViewModel<string>();

                    //檢查是否已註冊過
                    if (memberdb.Member.Where(w => w.Mail == mailAccount.Mail).Any())
                        return "此電子郵件已註冊過!".AsFailResponse();

                    string vCode = MailSend(mailAccount.Mail);

                    var verificationCodeEntity = memberdb.VerificationCode.FirstOrDefault(a => a.Mail == mailAccount.Mail);

                    if (verificationCodeEntity != null)
                        memberdb.VerificationCode.Remove(verificationCodeEntity);

                    var newVCode = new VerificationCode()
                    {
                        Mail = mailAccount.Mail,
                        VCode = vCode,
                        Status = (int)VerificationEnum.NotAuth,
                        CreateDate = DateTime.Now
                    };
                    memberdb.VerificationCode.Add(newVCode);

                    memberdb.SaveChanges();
                }

                return CommonHelper.AsSuccessResponse("已發送");
            }
            catch (Exception ex)
            {
                logger.Error($"發送驗證碼 發生錯誤, ex:{ex}");
                return "發送驗證碼失敗".AsFailResponse();
            }
        }

        //Mail發送
        private string MailSend(string mailAddress)
        {
            //檢查是否輸入空值
            if (string.IsNullOrEmpty(mailAddress))
                return string.Empty;

            MailMessage mail = new MailMessage();

            //前面是發信email後面是顯示的名稱
            mail.From = new MailAddress("leozheng0417@gmail.com", "IKKON");

            //收信者email
            mail.To.Add(mailAddress);

            //設定優先權
            mail.Priority = MailPriority.Normal;

            //標題
            mail.Subject = "IKKON 註冊驗證";

            //Mail內容
            string vCode = new Random().Next(10000).ToString();
            mail.Body = "<h1>驗證碼:" + vCode + "</h1>";

            //內容使用html
            mail.IsBodyHtml = true;

            //設定gmail的smtp (這是google的)
            SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);

            //您在gmail的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("leozheng0417@gmail.com", "qxhcjoppdbsoynmz");

            MySmtp.EnableSsl = true;

            //發送郵件
            MySmtp.Send(mail);

            //放掉宣告出來的mail
            mail.Dispose();

            return vCode;

            /*
              (2021/09/28) 待補發送失敗判斷
              (2021/10/09) 待補驗證碼到期時間(點註冊打API檢查驗證碼到期時間)
            */
        }
    }
}