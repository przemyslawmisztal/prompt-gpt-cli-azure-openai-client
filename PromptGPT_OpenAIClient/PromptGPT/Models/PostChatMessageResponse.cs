using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Models
{
    public class PostChatMessageResponse
    {
        public string Message { get; set; } = null!;
        public string ChatMessageRole { get; set; } = null!;
    }
}
