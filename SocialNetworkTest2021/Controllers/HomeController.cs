﻿using SocialNetworkTest2021.Model;
using SocialNetworkTest2021.ViewModel;
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
            var memberData = memberdb.Member.Where(w => w.Account == "Leo").Select(s => new IndexViewModel()
            {
                MemberID = s.MemberID,
                NickName = s.NickName
            }).FirstOrDefault();

            TempData["Message"] = "TempData測試資料";
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

        public ActionResult Login()
        {
            return View();
        }
    }
}