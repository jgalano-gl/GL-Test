using System.Text.Json.Serialization;

namespace GLTest.Core.Integrations.ChatGPT.Dtos
{
    public class ChatGPTRequestDto
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-4o";

        [JsonPropertyName("messages")]
        public List<ChatGPTMessageDto> Messages { get; set; } = new List<ChatGPTMessageDto>();

        [JsonPropertyName("max_tokens")]
        public int MaxTokens { get; set; } = 1000;

        [JsonPropertyName("temperature")]
        public double Temperature { get; set; } = 0.2;

        [JsonPropertyName("response_format")]
        public ChatGPTResponseFormatDto? ResponseFormat { get; set; } = new ChatGPTResponseFormatDto { Type = "json_object" };

    }

    public class ChatGPTMessageDto
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        // Use object type to support both string and array content
        [JsonPropertyName("content")]
        public object Content { get; set; }

        // Helper methods to set content
        public void SetTextContent(string text)
        {
            Content = text;
        }
    }

    public class ChatGPTResponseFormatDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
