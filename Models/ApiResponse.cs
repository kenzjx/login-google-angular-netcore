using Newtonsoft.Json;

namespace SignalR.Models
{
    public class ApiResponse
    {
        public int StatusCode {get;} // trả về trạn thái code là gì

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message {get;} // Message hiển thị lỗi;

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
           switch(statusCode)
           {
               case 404:
               return "Resource not found";
               case 500:
               return "An unhandler error occurred";
               default:
               return null;
           }
        }
    }
}