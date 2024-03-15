using AI.ChatBotLib.BaseLogic.RetrievalBot;
using AI.ChatBotLib.Context;
using AI.ChatBotLib.MainLogic;
using AI.ChatBotLib.RetrievalBot.BaseLogic.TextRetri;
using AI.ChatBotLib.Utilites;

Console.Title = "[Chat Bot]";

var searchBot = new NgramJaccardTextSearchBot("data.json");

ChatBot bot = new ChatBot(new List<IRetryBot>() { searchBot});

//QAManager.SaveToJson("data.json", searchBot.DataQA);

while (true)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User: ");
    Console.ForegroundColor = ConsoleColor.White;
    string textQ = Console.ReadLine();

    if (textQ == "(К)") break;
    
    string answer = bot.GetRetriAnswer(textQ);

    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("Bot:  ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(answer);

}

