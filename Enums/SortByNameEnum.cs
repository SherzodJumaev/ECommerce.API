using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ECommerce.API.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortByNameEnum
    {
        [EnumMember(Value = "name")]
        Name
    }
}
