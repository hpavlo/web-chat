using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebChatClient.model
{
    [JsonConverter(typeof(StringEnumConverter))]
    enum MessageType
    {
        User,
        CreateSession,
        CloseSession
    }
}
