using AI.ChatBotLib.BaseLogic.Context;
using AI.ChatBotLib.BaseLogic.TextRetri;

Console.Title = "Chat Bot";

var searchBot = new StringTextSearchBot("QA.txt");
BotContext context = new BotContext();

while (true)
{
    Console.WriteLine();
    Console.Write("User: ");
    string textQ = Console.ReadLine();

    if (textQ == "(К)") break;
    
    context.AddUserMessage(textQ);
    string answer = GetAnswer(context);
    context.AddAssistantMessage(answer);

    Console.WriteLine("Bot: " + answer);
}

string GetAnswer(BotContext context) =>
    searchBot.GetAnswer(context, 0.7);

