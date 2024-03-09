namespace PromptGPT
{
    public class AppSettings
    {
        public string AzureOpenAiAPIKey { get; private set; } = null!;
        public string AzureOpenAIResourceUri { get; private set; } = null!;

        public AppSettings(string azureOpenAIResourceUri, string azureOpenAiAPIKey)
        {
            AzureOpenAIResourceUri = azureOpenAIResourceUri;
            AzureOpenAiAPIKey = azureOpenAiAPIKey;

            if (string.IsNullOrEmpty(AzureOpenAIResourceUri))
                throw new ArgumentNullException("AzureOpenAIResourceUri");

            if (string.IsNullOrEmpty(AzureOpenAiAPIKey))
                throw new ArgumentNullException("AzureOpenAiAPIKey");
        }
    }
}
