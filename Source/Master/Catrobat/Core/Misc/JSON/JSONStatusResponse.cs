using System.Runtime.Serialization;

namespace Catrobat.Core.Misc.JSON
{
    public enum StatusCodes
    {
        ServerResponseTokenOk = 200,
        ServerResponseRegisterOk = 201
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