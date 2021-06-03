namespace SocialNetworkTest2021.Models
{
    /// <summary>
    /// 回傳共用類別
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseViewModel<T>
    {
        /// <summary>
        /// ResultCode回傳狀態碼(1 成功、0 失敗)
        /// </summary>
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}