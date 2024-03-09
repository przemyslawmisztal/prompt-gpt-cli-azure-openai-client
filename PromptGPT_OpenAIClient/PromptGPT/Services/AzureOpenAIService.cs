using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromptGPT.Clients;
using PromptGPT.Models;

namespace PromptGPT.Services
{
    public class AzureOpenAIService
    {
        private readonly AzureOpenAIClient _client;
        private readonly ChatPromptService _chatPromptService;
        private readonly ModelDeploymentService _modelDeploymentService;
        private readonly AppSettings _appSettings;
        private Chat _chat = null!;

        public AzureOpenAIService(AzureOpenAIClient client,
            ChatPromptService chatPromptService,
            ModelDeploymentService modelDeploymentService,
            AppSettings appSettings)
        {
            this._client = client;
            this._chatPromptService = chatPromptService;
            this._modelDeploymentService = modelDeploymentService;
            this._appSettings = appSettings;
        }

        public void StartNewChat()
        {
            _chat = new Chat
            {
                DateStarted = DateTime.Now,
                ChatPrompt = _chatPromptService.GetCurrentPrompt(),
                ModelDeployment = _modelDeploymentService.GetDefaultModelDeployment()
            };
        }

        public string SendMessage(string message)
        {
            if (message.Length == 0)
                throw new ArgumentException(message, nameof(message));

            if (_chat == null)
                StartNewChat();

            var request = new PostChatMessageRequest
            {
                ChatPrompt = _chat.ChatPrompt,
                ModelDeployment = _chat.ModelDeployment
            };

            foreach (var chatMessage in _chat.ChatMessages.OrderBy(p => p.DatePosted))
                request.ChatMessages.Add(new ChatMessageDto()
                {
                    ChatMessageRole = chatMessage.ChatMessageRole,
                    Message = chatMessage.Message
                });

            _chat.ChatMessages.Add(new ChatMessage(message, ChatMessageRole.User));
            request.CurrentChatMessage = new ChatMessageDto() { Message = message, ChatMessageRole = ChatMessageRole.User };

            var response = _client.PostAsync(request);

            return response.Result.Message;
        }
    }
}
