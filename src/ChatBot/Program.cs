using AI.ChatBotLib.Context;
using AI.ChatBotLib.RetrievalBot.BaseLogic.TextRetri;
using AI.ChatBotLib.Utilites;

Console.Title = "[Chat Bot]";

var searchBot = new NgramJaccardTextSearchBot("data.json");
BotContext context = new BotContext();

while (true)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User: ");
    Console.ForegroundColor = ConsoleColor.White;
    string textQ = Console.ReadLine();

    if (textQ == "(К)") break;
    
    context.AddUserMessage(textQ);
    string answer = GetAnswer(context);
    context.AddAssistantMessage(answer);

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("Bot:  ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(answer);

}


string GetAnswer(BotContext context) =>
    searchBot.GetAnswer(context, 0.7).Answer;

