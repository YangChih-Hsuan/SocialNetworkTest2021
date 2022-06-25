using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SocialNetworkTest2021.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var memberdb = new SocialNetworkTest2021Entities();
            //Leo換成代入index方法的值，來源是從登入時來
            var memberData = memberdb.Member.Where(w => w.Account == "Leo").Select(s => new MemberViewModel()
            {
                MemberID = s.MemberID,
                NickName = s.NickName
            }).FirstOrDefault();

            var indexViewModel = new IndexViewModel()
            {
                memberViewModel = memberData,
                friendViewModel = new List<FriendViewModel>()
                {
                    new FriendViewModel()
                    {
                        FriendName = "999"
                    }
                }
            };

            TempData["Message"] = memberData.NickName;
            return View(memberData);
        }

        public ActionResult About(int memberID)
        {
            //about取得會員ID
            ViewBag.Message = $"Your memberID is {memberID}.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
    }
}