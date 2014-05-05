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

    //[DataContract]
    //public class JSONStatusResponse
    //{
    //    [DataMember(Name = "statusCode")]
    //    public StatusCodes StatusCode { get; set; }

    //    [DataMember(Name = "answer")]
    //    public string StatusMessage { get; set; }

    //    [DataMember(Name = "preHeaderMessages")]
    //    public string PreHeaderMessage { get; set; }
    //}


    public class JSONStatusResponse
    {
        private string _token;
        public string token
        {
            get
            {
                return _token;
            }
            set
            {
                //_token = value.Replace("/", "\\/");
                _token = value;
            }
        }
        public StatusCodes statusCode { get; set; }
        public string answer { get; set; }
        public string preHeaderMessages { get; set; }
    }

}