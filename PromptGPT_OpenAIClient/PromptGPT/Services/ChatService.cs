using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromptGPT.Clients;
using PromptGPT.Models;

namespace PromptGPT.Services
{
    public class ChatService
    {
        private readonly AzureOpenAIClient _client;
        private readonly ChatPromptService _chatPromptService;
        private readonly ModelDeploymentService _modelDeploymentService;
        private readonly ChatSettingsService _chatSettingsService;
        private readonly AppSettings _appSettings;
        private readonly PromptGptRepository _promptGptRepository;
        private Chat _chat = null!;

        public ChatService(AzureOpenAIClient client,
            ChatPromptService chatPromptService,
            ModelDeploymentService modelDeploymentService,
            ChatSettingsService chatSettingsService,
            AppSettings appSettings,
            PromptGptRepository promptGptRepository)
        {
            this._client = client;
            this._chatPromptService = chatPromptService;
            this._modelDeploymentService = modelDeploymentService;
            this._chatSettingsService = chatSettingsService;
            this._appSettings = appSettings;
            this._promptGptRepository = promptGptRepository;
        }

        public void StartNewChat()
        {
            _chat = new Chat
            {
                DateStarted = DateTime.Now,
                ChatPrompt = _chatPromptService.GetCurrentPrompt(),
                ModelDeployment = _modelDeploymentService.GetDefaultModelDeployment(),
                Id = Guid.NewGuid()
            };
        }

        public async Task<PostChatMessageResponse> PostMessageAsync(string message)
        {
            if (message.Length == 0)
                throw new ArgumentException(message, nameof(message));

            if (_chat == null)
                StartNewChat();

            var request = new PostChatMessageRequest
            {
                ChatPrompt = _chat.ChatPrompt,
                ModelDeployment = _chat.ModelDeployment,
                Temperature = _chatSettingsService.GetChatSettings().Temperature
            };

            foreach (var chatMessage in _chat.ChatMessages.OrderBy(p => p.DatePosted))
            {
                request.ChatMessages.Add(new ChatMessageDto()
                {
                    ChatMessageRole = chatMessage.ChatMessageRole,
                    Message = chatMessage.Message
                });
            }

            _chat.ChatMessages.Add(new ChatMessage(message, ChatMessageRole.User));
            request.CurrentChatMessage = new ChatMessageDto() { Message = message, ChatMessageRole = ChatMessageRole.User };

            var response = await _client.PostAsync(request);

            _chat.ChatMessages.Add(new ChatMessage(response.Message, ChatMessageRole.Assistant));
            _promptGptRepository.SaveChat(_chat);

            return response;
        }
    }
}
