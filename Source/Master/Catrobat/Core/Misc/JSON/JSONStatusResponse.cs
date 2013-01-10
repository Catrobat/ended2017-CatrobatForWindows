using System.Runtime.Serialization;

namespace Catrobat.Core.Misc.JSON
{
  public enum StatusCodes { SERVER_RESPONSE_TOKEN_OK = 200, SERVER_RESPONSE_REGISTER_OK = 201 };

  [DataContract]
  public class JSONStatusResponse
  {
    [DataMember(Name = "statusCode")]
    public StatusCodes StatusCode { get; set; }
  }
}
