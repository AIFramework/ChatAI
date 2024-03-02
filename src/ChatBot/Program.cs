using AI.ChatBotLib.BaseLogic.TextRetri;

Console.Title = "Chat Bot";

var searchBot = new NgramJaccardTextSearchBot("QA.txt", 4);

while (true)
{
    Console.WriteLine();
    Console.Write("User: ");
    string textQ = Console.ReadLine();
    if (textQ == "(К)") break;
    string answer = GetAnswer(q: textQ);
    Console.WriteLine("Bot: " + answer);
}

string GetAnswer(string q)
{
    return searchBot.GetAnswer(q);
}
