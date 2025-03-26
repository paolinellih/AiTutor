using System.Text;
using System.Text.Json;
using AiTutor.Application;
using Microsoft.Extensions.Configuration;

namespace AiTutor.Infrastructure.Clients
{
    public class OllamaAiClient : IAiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _ollamaBaseUrl;

        public OllamaAiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _ollamaBaseUrl =
                configuration["OLLAMA_BASE_URL"] ?? "http://ollama-ml:11434"; // Default to container service name
        }

        public async Task<string> GetResponseAsync(string prompt)
        {
            var requestBody = new
            {
                model = "tinyllama",
                prompt = prompt,
                stream = false
            };

            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8,
                    "application/json");

                var response = await _httpClient.PostAsync($"{_ollamaBaseUrl}/api/generate", jsonContent);

                // Ensure the request was successful
                response.EnsureSuccessStatusCode();

                // Read the JSON response
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Parse the JSON response
                var jsonElement = JsonDocument.Parse(jsonResponse).RootElement;

                // Check if 'response' exists in the JSON response and return it
                if (jsonElement.TryGetProperty("response", out var responseText))
                {
                    return responseText.GetString();
                }
                else
                {
                    // Handle the case where 'response' is missing
                    throw new KeyNotFoundException("'response' key not found in the response.");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP errors
                throw new Exception("An error occurred while sending the request.", ex);
            }
            catch (KeyNotFoundException ex)
            {
                // Handle missing key in the response
                throw new Exception("The expected key was not found in the response.", ex);
            }
            catch (Exception ex)
            {
                // General error handling
                throw new Exception("An unexpected error occurred.", ex);
            }
        }
    }
}