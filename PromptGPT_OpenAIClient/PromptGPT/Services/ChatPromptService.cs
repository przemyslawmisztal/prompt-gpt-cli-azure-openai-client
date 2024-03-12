using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromptGPT.Clients;
using PromptGPT.Models;

namespace PromptGPT.Services
{
    public class ChatPromptService
    { 
        private readonly PromptGptRepository _promptGptRepository;

        public ChatPromptService(PromptGptRepository promptGptRepository)
        {
            this._promptGptRepository = promptGptRepository;
        }

        public ChatPrompt GetCurrentPrompt()
        {
            var prompts = ReadPrompts();

            var defaultPrompt = prompts.ChatPrompts.Where(p => p.Default).First();

            return new ChatPrompt()
            {
                Prompt = defaultPrompt.Prompt,
                Default = defaultPrompt.Default,
                Name = defaultPrompt.Name
            };
        }

        public void ChangeDefaultPrompt(string name)
        {
            var prompts = ReadPrompts();

            var chatPrompts = new List<ChatPrompt>();

            foreach (var prompt in prompts.ChatPrompts)
            {
                chatPrompts.Add(new ChatPrompt()
                {
                    Default = prompt.Name == name,
                    Name = prompt.Name,
                    Prompt = prompt.Prompt
                });
            }

            _promptGptRepository.SavePrompts(chatPrompts);
        }

        public GetChatPromptsRequest ReadPrompts()
        {
            var listOfPrompts = _promptGptRepository.ReadPrompts();

            var result = new GetChatPromptsRequest();

            foreach (var prompt in listOfPrompts)
            {
                result.ChatPrompts.Add(new ChatPromptDto()
                {
                    Name = prompt.Name,
                    Prompt = prompt.Prompt,
                    Default = prompt.Default
                });
            }

            return result;
        }
    }
}
