﻿using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ECommerce.API.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductEnum
    {
        [EnumMember(Value = "default")]
        Default,

        [EnumMember(Value = "largest")]
        Largest,

        [EnumMember(Value = "smallest")]
        Smallest,
    }
}
