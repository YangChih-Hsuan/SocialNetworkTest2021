using SocialNetworkTest2021.Enum;
using SocialNetworkTest2021.ViewModel;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace SocialNetworkTest2021.Helper
{
    public static class CommonHelper
    {
        public static ResponseViewModel<string> AsSuccessResponse(this string message)
        {
            return new ResponseViewModel<string>()
            {
                ResultCode = ResultCodeEnum.Success,
                Message = message,
                Data = string.Empty
            };
        }

        public static ResponseViewModel<T> AsSuccessResponse<T>(string message, T data)
        {
            return new ResponseViewModel<T>()
            {
                ResultCode = ResultCodeEnum.Success,
                Message = message,
                Data = data
            };
        }

        public static ResponseViewModel<string> AsFailResponse(this string message)
        {
            return new ResponseViewModel<string>()
            {
                ResultCode = ResultCodeEnum.Error,
                Message = message
            };
        }

        public static ResponseViewModel<string> AsFailResponse(this ModelStateDictionary modelState)
        {
            return new ResponseViewModel<string>
            {
                ResultCode = ResultCodeEnum.Error,
                Message = modelState.Keys.SelectMany(key => modelState[key].Errors).Select(x => x.ErrorMessage).FirstOrDefault()
            };
        }
    }
}