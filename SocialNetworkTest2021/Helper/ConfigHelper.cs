using System.Web.Configuration;

namespace SocialNetworkTest2021.Helper
{
    public static class ConfigHelper
    {
        public static string DbConnection { get; set; } = WebConfigurationManager.AppSettings["dbConnection"];
    }
}