using System.Runtime.Serialization;

namespace Catrobat.IDE.Core.Utilities.JSON
{
    public enum StatusCodes
    {
        ServerResponseTokenOk = 200,
        ServerResponseRegisterOk = 201,
        ServerResponseTokenInvalid = 601,
        ServerResponseRegistrationFailed = 602
    };

    public class JSONStatusResponse
    {
        public string token { get; set; }
        public StatusCodes statusCode { get; set; }
        public string answer { get; set; }
        public string preHeaderMessages { get; set; }
    }

}