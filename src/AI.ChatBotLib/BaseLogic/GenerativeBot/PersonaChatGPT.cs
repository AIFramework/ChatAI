using AI.ChatBotLib.Context;
using FractalGPT.SharpGPTLib.API.ChatGPT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AI.ChatBotLib.BaseLogic.GenerativeBot
{
    /// <summary>
    /// Бот на базе chatgpt
    /// </summary>
    [Serializable]
    public class PersonaChatGPT : IGenerativeBot
    {
        /// <summary>
        /// Апи для ChatGPT
        /// </summary>
        public ChatGptApi ChatGPT { get; set; }

        /// <summary>
        /// Размер части контекста
        /// </summary>
        public int ContextPart { get; set; } = 5;

        /// <summary>
        /// Базовые факты
        /// </summary>
        public List<string> BaseFacts { get; set; } = new List<string>();


        /// <summary>
        /// Локальный бот решает задачу (PersonaChat)
        /// </summary>
        public PersonaChatGPT(string apiKey, string model="gpt-3.5-turbo", string proxyPath = null)
        {
            ChatGPT = new ChatGptApi(apiKey, proxyPath != null, proxyPath, model);
        }

        /// <summary>
        /// Ответ на вопрос
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> GetAnswer(BotContext context)
        {
            var qaFacts = context.SupportDataIds[context.SupportDataIds.Count - 1];
            List<Dictionary<string, string>> contextPart = context.GetContextPart(ContextPart);
            List<string> facts = new List<string>();

            // Добавление 
            facts.AddRange(BaseFacts);
            facts.AddRange(IdsToStrings(qaFacts));

            string prompt = Facts2Prompt(facts);
            ChatGPT.SetPrompt(prompt);
            var answer = await ChatGPT.SendAsync(contextPart);
            return answer.Choices[0].Message.Content;
        }


        // Индексы
        private List<string> IdsToStrings(List<int> ids)
        {
            List<string> strings = new List<string>();

            foreach (var id in ids)
                if (id >= 0) strings.Add(Settings.QAData.Registry[id].Answer);

            return strings;
        }

        private string Facts2Prompt(List<string> facts) 
        {
            StringBuilder sb = new StringBuilder();
            string append = facts.Count > BaseFacts.Count ? "Answer questions based only on context and facts about yourself" : "";
            sb.Append($"Give short and concise answers. {append}. Here are some facts about you:\n");

            foreach (var fact in facts)
            { 
                sb.Append(fact);
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
