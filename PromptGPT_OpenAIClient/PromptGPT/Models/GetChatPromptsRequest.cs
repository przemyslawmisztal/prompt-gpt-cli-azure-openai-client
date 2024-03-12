namespace PromptGPT.Models
{
    public class GetChatPromptsRequest
    {
        public List<ChatPromptDto> ChatPrompts { get; set; } = [];
    }

    public class ChatPromptDto
    {
        public string Name { get; set; } = null!;
        public string Prompt { get; set; } = null!;
        public bool Default { get; set; }
    }
}
