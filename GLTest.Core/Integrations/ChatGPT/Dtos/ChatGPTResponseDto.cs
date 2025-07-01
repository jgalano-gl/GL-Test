using System.Text.Json.Serialization;

namespace GLTest.Core.Integrations.ChatGPT.Dtos
{
    public class ChatGPTResponseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("choices")]
        public List<ChatGPTChoiceDto> Choices { get; set; } = new List<ChatGPTChoiceDto>();
    }
}
