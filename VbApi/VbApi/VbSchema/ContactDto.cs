using System.Text.Json.Serialization;
using VbApi.VbBase.Schema;
using VbApi.VbSchema;


namespace VbApi.VbSchema;

public class ContactRequest : BaseRequest
{
    [JsonIgnore]
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public string ContactType { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}


public class ContactResponse : BaseResponse
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string ContactType { get; set; }
    public string Information { get; set; }
    public bool IsDefault { get; set; }
}