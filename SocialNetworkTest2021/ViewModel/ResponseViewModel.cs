using SocialNetworkTest2021.Enum;

namespace SocialNetworkTest2021.ViewModel
{
    public class ResponseViewModel<T>
    {
        public ResultCodeEnum ResultCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}