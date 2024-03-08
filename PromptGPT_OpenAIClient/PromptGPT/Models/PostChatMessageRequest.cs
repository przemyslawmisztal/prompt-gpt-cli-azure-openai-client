using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Models
{
    public class PostChatMessageRequest()
    {
        public ChatPrompt ChatPrompt { get; set; } = null!;
        public List<ChatMessageDto> ChatMessages { get; set; } = [];
        public ModelDeployment ModelDeployment { get; set; } = null!;
        public ChatMessageDto CurrentChatMessage { get; set; } = null!;
    }

    public class ChatMessageDto
    {
        public string Message { get; set; } = null!;
        public string ChatMessageRole { get; set; } = null!;
    }

    public class ModelDeploymentDto
    {
        public string Name { get; set; } = null!;
    }
}
