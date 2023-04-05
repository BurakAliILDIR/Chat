namespace Chat.API.CQRS.Base
{
    public enum Result
    {
        Success = 1,
        Error = 2,
        Warning = 3,
    }

    public class BaseResponse
    {
        public Result Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}