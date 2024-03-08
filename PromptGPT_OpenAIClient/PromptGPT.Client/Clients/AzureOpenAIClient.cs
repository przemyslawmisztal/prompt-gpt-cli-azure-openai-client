using Azure;
using Azure.AI.OpenAI;
using PromptGPT.Models;
using PromptGPT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Clients
{
    public class AzureOpenAIClient
    {
        private readonly HttpClient _client;

        public AzureOpenAIClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<PostChatMessageResult> PostAsync(PostChatMessageRequest request)
        {
            var uri = Environment.GetEnvironmentVariable("AZURE_OPEN_AI:RESOURCE_URI");

            if (uri == string.Empty || uri == null)
                throw new ArgumentNullException(nameof(uri));

            Uri azureOpenAIResourceUri = new(uri);
            var apiKey = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY");

            if (apiKey == string.Empty || apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            AzureKeyCredential azureOpenAIApiKey = new(apiKey);

            OpenAIClient client = new(azureOpenAIResourceUri, azureOpenAIApiKey);

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = request.ModelDeployment.Name,
                Messages =
                {
                    new ChatRequestSystemMessage(request.ChatPrompt.Prompt),
                    new ChatRequestUserMessage(request.ChatMessages.Last<ChatMessageDto>().Message),
                }
            };

            foreach(var message in request.ChatMessages)
            {
                if (message.ChatMessageRole == ChatMessageRole.User)
                    chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(message.Message));
                else
                    chatCompletionsOptions.Messages.Add(new ChatRequestAssistantMessage(message.Message));
            }

            Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
            ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
            Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");

            return new PostChatMessageResult();
        }
    }
}
