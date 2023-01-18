using Newtonsoft.Json;
using System;

namespace WebChatClient.model
{
    class Message
    {
        [JsonProperty("username")]
        public string Username;
        [JsonProperty("time")]
        public DateTime Time;
        [JsonProperty("text")]
        public string Text;
        [JsonProperty("messageType")]
        public MessageType MessageType;

        public Message(string username, DateTime time, string text, MessageType messageType)
        {
            Username = username;
            Time = time;
            Text = text;
            MessageType = messageType;
        }
    }
}
