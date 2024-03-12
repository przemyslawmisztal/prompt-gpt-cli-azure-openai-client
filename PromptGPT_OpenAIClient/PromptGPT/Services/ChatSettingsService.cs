using PromptGPT.Clients;
using PromptGPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Services
{
    public class ChatSettingsService
    {
        private readonly PromptGptRepository _promptGptRepository;

        public ChatSettingsService(PromptGptRepository promptGptRepository)
        {
            this._promptGptRepository = promptGptRepository;
        }
        public GetChatSettingsResponse GetChatSettings()
        {
            return new GetChatSettingsResponse()
            {
                Temperature = _promptGptRepository.ReadChatSettings().Temperature
            };
        }
    }
}
