using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromptGPT.Models;

namespace PromptGPT.Services
{
    public class ChatPromptService
    {
        public ChatPrompt GetCurrentPrompt()
        {
            return new ChatPrompt()
            {
                Prompt = "You are an assistant to a Head of Microsoft Technology Practice"
            };
        }
    }
}
