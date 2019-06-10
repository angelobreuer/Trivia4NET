# Trivia4NET

[![Trivia4NET](https://img.shields.io/nuget/vpre/Trivia4NET.svg)](https://www.nuget.org/packages/Trivia4NET/)

A wrapper for the Open Trivia Database (https://opentdb.com/).

## `Getting Started`

1. First install [.NET Core >= 2.0](https://dotnet.microsoft.com/download/dotnet-core/2.0) for your platform.
2. Install the [Trivia4NET](https://www.nuget.org/packages/Trivia4NET/) NuGet-Package.

___

###### Requesting a session token 

A session token is optional, but recommended. It is used to identify which questions from the Trivia API were already returned. This means the session
token is used to remove duplicate questions responses. In the most request methods an optional session token can be given.

```csharp
var service = new TriviaService();

var response = await service.RequestTokenAsync();
var token = sessionResponse.SessionToken; // <- The Session Token
```

___

###### Requesting questions and answers

```csharp
// request 50 easy-rated questions from the API
var questions = await service.GetQuestionsAsync(token, 
    amount: 50, difficulty: Difficulty.Easy);

// print out questions and their answers in the console
foreach (var question in response.Questions)
{
    Console.WriteLine("Question: " + question.Content);
    Console.WriteLine("Answer: " + question.Answer);
    Console.WriteLine();
}
```

___

###### Paginating requests

Imagine your creating an application, that prints out a question and its answer every time the user presses any key (except shutdown or reset).
Then a question enumerator can be used as the following:

```csharp
// this creates an enumerator that enumerates through 
// the questions with the difficulty easy.
var enumerator = service.PaginateQuestions(token, difficulty: Difficulty.Easy);

// enumerate through questions
while (await enumerator.MoveNext(CancellationToken.None))
{
    // wait the key press
    Console.ReadKey(true);

    var question = enumerator.Current;
    Console.WriteLine("Question: " + question.Content);
    Console.WriteLine("Answer: " + question.Answer);
    Console.WriteLine();
}
```