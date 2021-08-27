using System.Collections.Generic;

namespace SocialNetworkTest2021.ViewModel
{
    // Index ViewModel
    public class IndexViewModel
    {
        public MemberViewModel memberViewModel { get; set; }
        public List<FriendViewModel> friendViewModel { get; set; }
    }

    public class MemberViewModel
    {
        //會員編號
        public int MemberID { get; set; }

        //暱稱
        public string NickName { get; set; }

    }

    public class FriendViewModel
    {
        public string FriendName { get; set; }
    }
}