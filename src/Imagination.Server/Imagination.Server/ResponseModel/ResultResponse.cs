using System.IO;
using System.Net;

namespace Imagination.ResponseModel
{
    public class ResultResponse : BaseResponse
    {
        public MemoryStream Stream { get; private set; }

        public ResultResponse(HttpStatusCode code, string message, MemoryStream stream) : base(code, message)
        {
            Stream = stream;
        }

        public ResultResponse(MemoryStream stream) : this(HttpStatusCode.OK, "Converted Successfully", stream)
        {

        }

        public ResultResponse(string message) : this(HttpStatusCode.BadRequest, message, null)
        {

        }
    }
}