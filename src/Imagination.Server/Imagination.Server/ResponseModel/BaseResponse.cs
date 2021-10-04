using System.Net;

namespace Imagination.ResponseModel
{
    public abstract class BaseResponse
    {
        public HttpStatusCode Code { get; protected set; }

        public string Message { get; protected set; }


        public BaseResponse(HttpStatusCode code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}