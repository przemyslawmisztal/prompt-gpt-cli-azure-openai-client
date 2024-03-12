## .NET 8 Console Application with Azure OpenAI API

This is a console application written in .NET 8 that uses the Azure OpenAI API to send and receive chat completions. The chat history is saved in the program directory. You can also add multiple prompts and decide which one to use on the runtime.

# Prerequisites

- .NET 8.0 or later
- Azure account
- OpenAI API key
  
# Getting Started

  1. Clone the repository Use the following command to clone this repository:

```bash
git clone https://github.com/yourusername/yourrepository.git
```

  2. Set up Azure OpenAI
     
     Follow the instructions on the Azure OpenAI Documentation to set up the OpenAI service on Azure and get your API      key.
     
  3. Update the API key Replace YourOpenAIKey in appsettings.json with your OpenAI API key:

 ```bash    
{
  "OpenAIKey": "YourOpenAIKey"
}
```

  4. **Add prompts to `prompts.json`**

     You can add multiple prompts to the `prompts.json` file and select the one to use during runtime.
    
# Running the Application

  1. Navigate to the project directory

```bash
cd yourrepository
```

  2. Run the application
     
```bash
dotnet run
```

# Usage

  After running the application, you will be prompted to enter a message. The application will then send this message   to the OpenAI API and display the chat completion. The chat history will be saved in the program directory.

#Contributing

  Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

# License

MIT

  Please replace yourusername and yourrepository with your GitHub username and repository name, respectively. Also,     make sure to replace YourOpenAIKey with your actual OpenAI API key.
