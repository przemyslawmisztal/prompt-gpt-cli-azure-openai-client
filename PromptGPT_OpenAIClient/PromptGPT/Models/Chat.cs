using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Models
{
    public class Chat
    {
        public ChatPrompt ChatPrompt { get; set; } = null!;
        public List<ChatMessage> ChatMessages { get; set; } = [];
        public ModelDeployment ModelDeployment { get; set; } = null!;
        public DateTime DateStarted { get; set; }
        public Guid Id { get; set; } 
    }
}
