
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

//string prompt = "What is AI ? explain max 20 word";

//Console.WriteLine($"user >> {prompt}");

//var responseStream = client.GetStreamingResponseAsync(prompt);
//await foreach (var response in responseStream)
//{
//    Console.Write(response.Text);
//}

#endregion

#region Classification

//var classificationPrompt = """
//    Please classify the following sentences into categories: 
//    - 'complaint' 
//    - 'suggestion' 
//    - 'praise' 
//    - 'other'.

//    1) "I love the new layout!"
//    2) "You should add a night mode."
//    3) "When I try to log in, it keeps failing."
//    4) "This app is decent."
//    """;

//Console.WriteLine($"user >> {classificationPrompt}");

//ChatResponse classificationResponse = await client.GetResponseAsync(classificationPrompt);

//Console.WriteLine($"assistant >> {classificationResponse}");

#endregion

#region Sentiment Analysis

var analysisPrompt = """
        You will analyze the sentiment of the following product reviews. 
        Each line is its own review. Output the sentiment of each review in a bulleted list and then provide a generate sentiment of all reviews.

        I bought this product and it's amazing. I love it!
        This product is terrible. I hate it.
        I'm not sure about this product. It's okay.
        I found this product based on the other reviews. It worked for a bit, and then it didn't.
        """;

Console.WriteLine($"user >>> {analysisPrompt}");

ChatResponse responseAnalysis = await client.GetResponseAsync(analysisPrompt);

Console.WriteLine($"assistant >>> \n{responseAnalysis}");

#endregion