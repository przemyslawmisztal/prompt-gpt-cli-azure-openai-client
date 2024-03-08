using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Models
{
    public class ChatMessage(string message, string chatMessageRole)
    {
        public string Message { get; } = message;
        public string ChatMessageRole { get; } = chatMessageRole;
        public DateTime DatePosted { get; } = DateTime.Now;
    }
}
