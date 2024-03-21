using AI.ChatBotLib;
using AI.ChatBotLib.BaseLogic.GenerativeBot;
using AI.ChatBotLib.BaseLogic.RetrievalBot;
using AI.ChatBotLib.MainLogic;
using AI.ChatBotLib.RetrievalBot.BaseLogic.TextRetri;

Console.Title = "[Chat Bot]";

Settings.Load("data.json");
var searchBot = new NgramJaccardTextSearchBot();

//PersonaLocalBot GBot = new PersonaLocalBot("http://192.168.0.101:8080/");
PersonaChatGPT GBot = new PersonaChatGPT(File.ReadAllText("key.txt"), proxyPath:"proxy.json");
GBot.BaseFacts = new List<string>() { "Я дружелюбный помощник." };
ChatBot bot = new ChatBot(new List<IRetryBot>() { searchBot }, GBot);
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
