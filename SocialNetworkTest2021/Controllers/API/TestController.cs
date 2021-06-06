using Dapper;
using SocialNetworkTest2021.Helper;
using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace SocialNetworkTest2021.Controllers.API
{
    [RoutePrefix("API/Test")]
    public class TestController : ApiController
    {
        [HttpPost, Route("Test")]
        public void Test(LoginViewModel loginViewModel)
        {
            var memberdb = new SocialNetworkTest2021Entities();
            var account = memberdb.Member.Where(w => w.NickName == "測試帳號12345").FirstOrDefault();

            //更新
            //account.NickName = account.NickName + "12345";
            //account.Mail = "QQ@gmail.com";

            //刪除
            //memberdb.Member.Remove(account);

            //新增
            //var newMember = new Member()
            //{
            //    Account = "shanna",
            //    NickName = "瑄拿",
            //    Mail = "shanna@gmail.com",
            //    CreateDate = DateTime.Now,
            //    Password = "123"
            //};
            //memberdb.Member.Add(newMember);

            //新增多筆
            List<Member> memberList = new List<Member>()
            {
                new Member()
                {
                    Account = "Leo",
                    NickName = "庠庠",
                    Mail = "leo@gmail.com",
                    CreateDate = DateTime.Now,
                    Password = "leoPassword"
                },
                new Member()
                {
                    Account = "WOO",
                    NickName = "挖阿",
                    Mail = "WOO@gmail.com",
                    CreateDate = DateTime.Now,
                    Password = "WOO123"
                }
            };
            memberdb.Member.AddRange(memberList);

            memberdb.SaveChanges();
        }

        [HttpPost, Route("DapperTest")]
        public void DapperTest()
        {
            using (SqlConnection conn = new SqlConnection(ConfigHelper.DbConnection))
            {
                //撈資料Query
                string strSql = "SELECT * FROM SocialNetworkTest2021.dbo.Member";
                List<Member> memberList = conn.Query<Member>(strSql).ToList();

                //更新
                //string updateStrSql = @"UPDATE SocialNetworkTest2021.dbo.Member
                //                        SET NickName ='啾啾'
                //                        WHERE MemberID = @MemberID";
                //int updateCount = conn.Execute(updateStrSql,new {@MemberID = 5});

                //刪除
                //string deleteStrSql = @"DELETE SocialNetworkTest2021.dbo.Member
                //                        WHERE MemberID = @MemberID";
                //int deleteCount = conn.Execute(deleteStrSql, new { @MemberID = 5 });

                //新增
                List<Member> newMember = new List<Member>()
                {
                    new Member()
                    {
                        Account = "Leo",
                        NickName = "庠庠",
                        Mail = "leo@gmail.com",
                        CreateDate = DateTime.Now,
                        Password = "leoPassword"
                    },
                    new Member()
                    {
                        Account = "WOO",
                        NickName = "挖阿",
                        Mail = "WOO@gmail.com",
                        CreateDate = DateTime.Now,
                        Password = "WOO123"
                    }
                };
                string insertStrSql = @"INSERT SocialNetworkTest2021.dbo.Member
                                        VALUES ( @Account, @NickName, @Password, @Mail, @CreateDate)";
                int insertCount = conn.Execute(insertStrSql, newMember);
            }
        }
    }
}