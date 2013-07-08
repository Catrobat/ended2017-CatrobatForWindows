using System.Runtime.Serialization;

namespace Catrobat.Core.Misc.JSON
{
    public enum StatusCodes
    {
        SERVER_RESPONSE_TOKEN_OK = 200,
        SERVER_RESPONSE_REGISTER_OK = 201
    };

    [DataContract]
// ReSharper disable ClassNeverInstantiated.Global
    public class JSONStatusResponse
// ReSharper restore ClassNeverInstantiated.Global
    {
        [DataMember(Name = "statusCode")]
        public StatusCodes StatusCode { get; set; }

        [DataMember(Name = "answer")]
        public string StatusMessage { get; set; }

        [DataMember(Name = "preHeaderMessages")]
        public string PreHeaderMessage { get; set; }
    }
}