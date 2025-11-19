
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;

// get credentials from user secrets
IConfigurationRoot config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

var credintial=new ApiKeyCredential(config["GitHubModels:Token"]??throw new InvalidOperationException("Missing configuration: GitHubModel:Token."));

var options=new OpenAIClientOptions()
{
    Endpoint = new Uri("https://models.github.ai/inference")
};

// create chat client
var client = new OpenAIClient(credintial, options).GetChatClient("openai/gpt-4.1-mini").AsIChatClient();

#region Basic Completion

// Send propmt to chat model and get response

//string prompt = "What is AI ? explain max 20 word";

//Console.WriteLine($"user >> {prompt}");

//ChatResponse response = await client.GetResponseAsync(prompt);

//Console.WriteLine($"assistant >> {response}");
//Console.WriteLine($"Token use: in={response.Usage?.InputTokenCount}, out={response.Usage?.OutputTokenCount}");

#endregion

#region Streaming Completion

string prompt = "What is AI ? explain max 20 word";

Console.WriteLine($"user >> {prompt}");

var responseStream = client.GetStreamingResponseAsync(prompt);
await foreach (var response in responseStream)
{
    Console.Write(response.Text);
}

#endregion