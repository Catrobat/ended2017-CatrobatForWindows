using System.Runtime.Serialization;

namespace Catrobat.IDE.Core.Utilities.JSON
{
    public enum StatusCodes
    {
        ServerResponseTokenOk = 200,
        ServerResponseRegisterOk = 201
    };

    [DataContract]
    public class JSONStatusResponse
    {
        [DataMember(Name = "statusCode")]
        public StatusCodes StatusCode { get; set; }

        [DataMember(Name = "answer")]
        public string StatusMessage { get; set; }

        [DataMember(Name = "preHeaderMessages")]
        public string PreHeaderMessage { get; set; }
    }
}