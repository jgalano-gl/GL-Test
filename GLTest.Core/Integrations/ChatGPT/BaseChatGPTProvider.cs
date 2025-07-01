using GLTest.Core.Common;
using GLTest.Core.Integrations.ChatGPT.Dtos;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GLTest.Core.Integrations.ChatGPT
{
    public class BaseChatGPTProvider
    {
        private readonly HttpClient _httpClient;
        private readonly Configuration _configuration;
        private const string ChatGPTApiUrl = "https://api.openai.com/v1/chat/completions";

        public BaseChatGPTProvider(HttpClient httpClient, Configuration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ResultModel<ChatGPTResponseDto>> SendChatCompletionAsync(ChatGPTRequestDto request)
        {
            try
            {
                // Set authorization header
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _configuration.ChatGPTApiKey);

                // Serialize request
                var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Make API call
                var response = await _httpClient.PostAsync(ChatGPTApiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ResultModel<ChatGPTResponseDto>(false,
                        $"ChatGPT API call failed with status {response.StatusCode}: {errorContent}");
                }

                // Deserialize response
                var responseContent = await response.Content.ReadAsStringAsync();
                var chatGptResponse = JsonSerializer.Deserialize<ChatGPTResponseDto>(responseContent,
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                    });

                return new ResultModel<ChatGPTResponseDto>(chatGptResponse);
            }
            catch (Exception ex)
            {
                return new ResultModel<ChatGPTResponseDto>(false,
                    $"Error calling ChatGPT API: {ex.Message}");
            }
        }
    }
}
