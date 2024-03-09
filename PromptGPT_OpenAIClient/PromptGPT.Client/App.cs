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
            Console.WriteLine("Write your question: ");
            var question = Console.ReadLine();

            if (!string.IsNullOrEmpty(question))
            {
                var result = azureOpenAIService.SendMessage(question);

                Console.WriteLine(result);
            }
            else 
            {
                Console.WriteLine("No question asked");
            }
            Console.ReadKey();
        }
    }
}
