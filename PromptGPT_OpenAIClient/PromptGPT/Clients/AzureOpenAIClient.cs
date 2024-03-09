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
        private readonly AppSettings _appSettings;

        public AzureOpenAIClient(HttpClient client,
            AppSettings appSettings)
        {
            _client = client;
            this._appSettings = appSettings;
        }
        public async Task<PostChatMessageResponse> PostAsync(PostChatMessageRequest request)
        {
            Uri azureOpenAIResourceUri = new(_appSettings.AzureOpenAIResourceUri);

            AzureKeyCredential azureOpenAIApiKey = new(_appSettings.AzureOpenAiAPIKey);

            OpenAIClient client = new(azureOpenAIResourceUri, azureOpenAIApiKey);

            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                DeploymentName = request.ModelDeployment.Name,
                Messages =
                {
                    new ChatRequestSystemMessage(request.ChatPrompt.Prompt)
                }
            };

            foreach (var message in request.ChatMessages)
            {
                if (message.ChatMessageRole == ChatMessageRole.User)
                    chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(message.Message));
                else
                    chatCompletionsOptions.Messages.Add(new ChatRequestAssistantMessage(message.Message));
            }

            chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(request.CurrentChatMessage.Message));

            try
            {
                Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
                ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
                Console.WriteLine($"[{responseMessage.Role.ToString().ToUpperInvariant()}]: {responseMessage.Content}");

                return new PostChatMessageResponse()
                {
                    ChatMessageRole = responseMessage.Role.ToString(),
                    Message = responseMessage.Content,
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
