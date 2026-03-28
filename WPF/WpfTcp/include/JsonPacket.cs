using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chat
{
    [Serializable]
    public class JsonPacket
    {
        public string PacketName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string JsonData { get; set; } = string.Empty;

        [JsonIgnore]
        public ChatMessage? Message
        {
            get
            {
                if (Type == nameof(ChatMessage))
                {
                    try
                    {
                        ChatMessage? chatMessage = JsonSerializer.Deserialize<ChatMessage>(JsonData);
                        return chatMessage;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                    return null;
            }
            set
            {
                try
                {
                    JsonData = JsonSerializer.Serialize(value);
                    Type = nameof(ChatMessage);
                }
                catch (Exception)
                {
                    Type = string.Empty;
                    JsonData = string.Empty;
                }
            }
        }
        [JsonIgnore]
        public UserInfo? User
        {
            get
            {
                if (Type == nameof(UserInfo))
                {
                    try
                    {
                        UserInfo? chatMessage = JsonSerializer.Deserialize<UserInfo>(JsonData);
                        return chatMessage;
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else
                    return null;
            }
            set
            {
                try
                {
                    JsonData = JsonSerializer.Serialize(value);
                    Type = nameof(UserInfo);
                }
                catch (Exception)
                {
                    Type = string.Empty;
                    JsonData = string.Empty;
                }
            }
        }

        public JsonPacket()
        {
            PacketName = GetType().Name;
        }
    }
}
