using PromptGPT.Helpers;
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
        private readonly ChatService _azureOpenAIService;
        private readonly ChatPromptService _chatPromptService;

        public App(ChatService azureOpenAIService,
            ChatPromptService chatPromptService)
        {
            this._azureOpenAIService = azureOpenAIService;
            this._chatPromptService = chatPromptService;
        }
        public void Run(string[] args)
        {
            Console.Title = "PromptGPT";
            Console.WriteLine("PromptGPT");

            string defaultPrompt = "default";
            string defaultModel = "model";

            Console.WriteLine($"1 - Start chat with the {defaultPrompt} and the {defaultModel} model.");
            Console.WriteLine($"2 - Select different prompt.");
            Console.WriteLine($"Quit - Quit application.");

            var menuChoice = Console.ReadLine();

            if (menuChoice == "1")
            {
                RunChat();
            }
            else if (menuChoice == "2") 
            {
                RunPrompts();
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Program ended.");

            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        private void RunPrompts()
        {
            var prompts = _chatPromptService.ReadPrompts();
            var chatPrompts = prompts.ChatPrompts.OrderByDescending(p => p.Default).ThenBy(p => p.Name).ToList();
            Console.WriteLine("List of available prompts: ");
            var index = 0;

            foreach (var prompt in chatPrompts) 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (!prompt.Default)
                {
                    Console.WriteLine($"{index} - [{prompt.Name}] {prompt.Prompt}");
                }
                else
                {
                    Console.WriteLine($"D - [{prompt.Name}] {prompt.Prompt}");
                }

                index++;
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Pick a number to change default. Pick 0 to abort.");
            string? number = Console.ReadLine();

            if (number != null && IntegerHelpers.IsPositiveIntegerGreaterThanZero(number))
            {
                int selection = int.Parse(number);

                string name = chatPrompts[selection].Name;

                _chatPromptService.ChangeDefaultPrompt(name);
                Console.WriteLine($"Default changed to [{chatPrompts[selection].Name}] {chatPrompts[selection].Prompt}");
                RunPrompts();
            }
            else
            {
                Console.WriteLine("Picked 0 or invalid. Aborted.");
                Console.WriteLine();
                Run([]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void RunChat()
        {
            string? question;
            Console.WriteLine("Write your question (Quit to quit): ");

            Console.ForegroundColor = ConsoleColor.Blue;
            while ((question = Console.ReadLine()) != "Quit")
            {

                if (!string.IsNullOrEmpty(question))
                {
                    var busy = new ConsoleBusyIndicator();

                    busy.Start();
                    var result = _azureOpenAIService.PostMessageAsync(question).Result;
                    busy.Stop();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("");
                    Console.WriteLine($"[{result.ChatMessageRole.ToUpperInvariant()}]: {result.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"This answer did cost you {result.TotalTokens} tokens. Prompt tokens: {result.PromptTokens}. Completion tokens: {result.CompletionTokens}");
                    Console.WriteLine("");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("USER - type a followup question or Quit to quit");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("No question asked");
                }
            }
        }
    }
}
