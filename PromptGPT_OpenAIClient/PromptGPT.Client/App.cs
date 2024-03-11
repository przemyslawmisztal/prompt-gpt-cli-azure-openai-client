using PromptGPT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Client
{
    public class App
    {
        private readonly AzureOpenAIService azureOpenAIService;

        public App(AzureOpenAIService azureOpenAIService)
        {
            this.azureOpenAIService = azureOpenAIService;
        }
        public void Run(string[] args)
        {
            Console.Title = "PromptGPT";
            Console.WriteLine("PromptGPT");

            string defaultPrompt = "default";
            string defaultModel = "model";

            Console.WriteLine($"1 - Start chat with the {defaultPrompt} and the {defaultModel} model.");
            Console.WriteLine($"Quit - Quit application.");

            var menuChoice = Console.ReadLine();

            if (menuChoice == "1")
            {
                string? question;
                Console.WriteLine("Write your question (Quit to quit): ");

                Console.ForegroundColor = ConsoleColor.Blue;
                while ((question = Console.ReadLine()) != "Quit")
                {

                    if (!string.IsNullOrEmpty(question))
                    {
                        var result = azureOpenAIService.PostMessageAsync(question);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("");
                        Console.WriteLine($"[{result.Result.ChatMessageRole.ToUpperInvariant()}]: {result.Result.Message}");
                        Console.WriteLine("");

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("USER - type a followup question or Quit to quit");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("No question asked");
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Program ended.");

            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
    }
}
