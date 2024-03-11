using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PromptGPT.Models;

namespace PromptGPT.Services
{
    public class ChatPromptService
    { 
        private readonly string localStoragePath;
        public ChatPromptService(string localStoragePath)
        {
            this.localStoragePath = localStoragePath;
        }

        public ChatPrompt GetCurrentPrompt()
        {
            var prompts = ReadPrompts();
            return new ChatPrompt()
            {
                Prompt = prompts.ChatPrompts.First().Prompt
            };
        }

        public GetChatPromptsRequest ReadPrompts()
        {
            var promptNames = Directory.GetFiles(localStoragePath);
            var result = new GetChatPromptsRequest();

            foreach (var promptName in promptNames)
            {
                if (IsTextFile(promptName))
                {
                    result.ChatPrompts.Add(
                        new ChatPromptDto()
                        {
                            Name = Path.GetFileName(promptName),
                            Prompt = ReadTextFileContent(promptName)
                        }
                    );
                }
                
            }

            return result;
        }

        public static bool IsTextFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return !string.IsNullOrEmpty(extension) && extension.Equals(".txt", StringComparison.OrdinalIgnoreCase);
        }

        public static string ReadTextFileContent(string filePath)
        {
            try
            {
                string content = File.ReadAllText(filePath);
                return content;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading {Path.GetFileName(filePath)}: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
