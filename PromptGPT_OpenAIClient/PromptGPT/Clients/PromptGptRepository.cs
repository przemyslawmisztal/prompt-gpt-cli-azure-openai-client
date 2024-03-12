using PromptGPT.Models;
using System;
using System.Text.Json;

namespace PromptGPT.Clients
{
    public class PromptGptRepository
    {
        private readonly string localStoragePath;
        private readonly string chatDirectory = "/chats/";
        private readonly string promptSearchPattern = "*prompts.json";
        public PromptGptRepository(string localStoragePath)
        {
            this.localStoragePath = localStoragePath;
        }
        public void SaveChat(Chat chat)
        {
            var jsonContent = JsonSerializer.Serialize(chat);
            var chatFileName = $"{localStoragePath}{chatDirectory}{chat.Id}.json";

            try
            {
                var directory = Path.GetDirectoryName(localStoragePath + chatDirectory);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(chatFileName, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the JSON content: {ex.Message}");
            }
        }

        public Chat ReadChat(Guid id)
        {
            try
            {
                var searchPattern = $"*{id}.json";
                var chatFile = Directory.GetFiles(localStoragePath + chatDirectory, searchPattern).First();

                var jsonContent = File.ReadAllText(chatFile);
                Chat chats = JsonSerializer.Deserialize<Chat>(jsonContent);

                return chats;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file was not found.");
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to access this file.");
                return null;
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON format.");
                return null;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public List<ChatPrompt> ReadPrompts()
        {
            try
            {
                var promptName = Directory.GetFiles(localStoragePath, promptSearchPattern).First();

                var jsonContent = File.ReadAllText(promptName);
                List<ChatPrompt> listOfPrompts = JsonSerializer.Deserialize<List<ChatPrompt>>(jsonContent);

                return listOfPrompts;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file was not found.");
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to access this file.");
                return null;
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON format.");
                return null;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public void SavePrompts(List<ChatPrompt> chatPrompts)
        {
            var jsonContent = JsonSerializer.Serialize(chatPrompts);
            var propmtFileName = Directory.GetFiles(localStoragePath, promptSearchPattern).First();

            try
            {
                File.WriteAllText(propmtFileName, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the JSON content: {ex.Message}");
            }
        }

        public ChatSettings ReadChatSettings()
        {
            try
            {
                var settingsFileName = Directory.GetFiles(localStoragePath, "settings.json").First();

                var jsonContent = File.ReadAllText(settingsFileName);
                ChatSettings settings = JsonSerializer.Deserialize<ChatSettings>(jsonContent);

                return settings;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file was not found.");
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("You do not have permission to access this file.");
                return null;
            }
            catch (JsonException)
            {
                Console.WriteLine("Invalid JSON format.");
                return null;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
