
using System.Text.Json.Serialization;

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
        public Result Status { get; set; } = Result.Success;
        public string Message { get; set; } = "Başarılı!";
        public object Data { get; set; }
    }
}