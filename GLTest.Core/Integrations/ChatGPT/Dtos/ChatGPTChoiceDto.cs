using System.Text.Json.Serialization;

namespace GLTest.Core.Integrations.ChatGPT.Dtos
{
    public class ChatGPTChoiceDto
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("message")]
        public ChatGPTMessageDto Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }
}
