using System.Runtime.Serialization;

namespace Catrobat.IDE.Core.Utilities.JSON
{
    public enum StatusCodes
    {
        HTTPRequestFailed = 11,
        JSONSerializationFailed = 12,
        UnknownError = 13,
        ServerResponseTokenOk = 200,
        ServerResponseRegisterOk = 201,
        ServerResponseLoginFailed = 601,
        ServerResponseRegistrationFailed = 602,
        ServerResponseMissingEmail = 765
    };

    public class JSONStatusResponse
    {
        public JSONStatusResponse() { }
 
        public JSONStatusResponse(StatusCodes status)
        {
            statusCode = status;
        }
              
        public string token { get; set; }
        public StatusCodes statusCode { get; set; }
        public string answer { get; set; }
        public string preHeaderMessages { get; set; }
    }

}