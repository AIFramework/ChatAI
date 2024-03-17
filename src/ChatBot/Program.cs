using AI.ChatBotLib;
using AI.ChatBotLib.BaseLogic.GenerativeBot;
using AI.ChatBotLib.BaseLogic.RetrievalBot;
using AI.ChatBotLib.Context;
using AI.ChatBotLib.MainLogic;
using AI.ChatBotLib.RetrievalBot.BaseLogic.TextRetri;
using AI.ChatBotLib.Utilites;

Console.Title = "[Chat Bot]";

Env.Load("data.json");
var searchBot = new NgramJaccardTextSearchBot();

PersonaLocalBot personaLocalBot = new PersonaLocalBot("http://192.168.0.101:8080/");
personaLocalBot.BaseFacts = new List<string>() { "Я дружелюбный помощник.", "Меня зовут Андрей." };
ChatBot bot = new ChatBot(new List<IRetryBot>() { searchBot }, personaLocalBot);
bot.GenerativeAnswer += Bot_GenerativeAnswer;


InputMess();

while (true) { Thread.Sleep(100); }

void InputMess() 
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("User: ");
    Console.ForegroundColor = ConsoleColor.White;
    string textQ = Console.ReadLine();
    bot.GetGenerariveAnswer(textQ);
}


void Bot_GenerativeAnswer(string obj)
{
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.Write("Bot:  ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(obj);
    InputMess();
}
