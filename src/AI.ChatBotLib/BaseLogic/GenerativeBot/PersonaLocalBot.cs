using AI.ChatBotLib.Context;
using FractalGPT.SharpGPTLib.API.LocalServer;
using FractalGPT.SharpGPTLib.Prompts.PersonaChat;
using FractalGPT.SharpGPTLib.Task.DialogTasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AI.ChatBotLib.BaseLogic.GenerativeBot
{
    /// <summary>
    /// Локальный бот решает задачу (PersonaChat)
    /// </summary>
    [Serializable]
    public class PersonaLocalBot : IGenerativeBot
    {
        private BaseLLMServerAPI _baseAPI;

        /// <summary>
        /// Размер части контекста
        /// </summary>
        public int ContextPart { get; set; } = 3;
        
        /// <summary>
        /// Базовые факты
        /// </summary>
        public List<string> BaseFacts { get; set; } = new List<string>();

        /// <summary>
        /// Система для работы с PersonaChat
        /// </summary>
        public PersonaBot PersonaBotLLM { get; set; }


        /// <summary>
        /// Локальный бот решает задачу (PersonaChat)
        /// </summary>
        public PersonaLocalBot(string host, string startConv = Settings.StartConvFREDT5) 
        {
            _baseAPI = new BaseLLMServerAPI(host);
            PersonaChat personaChat = new PersonaChat() 
            { 
                StartConversation = startConv
            };

            PersonaBotLLM = new PersonaBot(personaChat);
            PersonaBotLLM.AnswerGenAsync = GetAns;
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

            PersonaBotLLM.SetFacts(facts);
            string answer = await PersonaBotLLM.GetAnswerContext(contextPart);
            return answer;
        }


        private async Task<string> GetAns(string input)
            =>  await _baseAPI.TextGeneration(input, 150);
        

        // Индексы
        private List<string> IdsToStrings(List<int> ids)
        {
            List<string> strings = new List<string>();

            foreach (var id in ids)
                if(id >= 0) strings.Add(Settings.QAData.Registry[id].Answer);

            return strings;
        }
    }
}
