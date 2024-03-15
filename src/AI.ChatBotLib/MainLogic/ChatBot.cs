using AI.ChatBotLib.BaseLogic.RetrievalBot;
using AI.ChatBotLib.Context;
using AI.ChatBotLib.Utilites;
using AI.DataStructs.Algebraic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AI.ChatBotLib.MainLogic
{
    /// <summary>
    /// Чат-бот
    /// </summary>
    [Serializable]
    public class ChatBot
    {
        /// <summary>
        /// Ретривер-боты
        /// </summary>      
        [JsonIgnore]
        public List<IRetryBot> RetriBots { get; set; } = new List<IRetryBot>();

        /// <summary>
        /// Контекст
        /// </summary>
        [JsonPropertyName("context")]
        public BotContext Context { get; set; } = new BotContext();

        /// <summary>
        /// Чат-бот
        /// </summary>
        /// <param name="retryBots">Ретривер-боты</param>
        public ChatBot(IEnumerable<IRetryBot> retryBots) 
        {
            RetriBots = retryBots.ToList();
        }

        /// <summary>
        /// Получение поддерживающих текстов (С добавлением вопроса в контекст)
        /// </summary>
        /// <param name="q">Вопрос</param>
        public List<QASample> GetSupport(string q) 
        {
            Context.AddUserMessage(q);
            var dat = GetSupportBase();
            // Добавление в контекст поддерживающих индексов
            Context.SupportDataIds.Add(dat.Item2);

            return dat.Item1;
        }

        /// <summary>
        /// Получение случайного примера из найденых
        /// </summary>
        /// <param name="q">Вопрос</param>
        public QASample GetRandomSampleWithQ(string q) 
        {
            Random rnd = new Random();

            Context.AddUserMessage(q);
            var data = GetSupportBase().Item1;
            var outSample = QAManager.GetAnswerFromIndexes(data, rnd);
            // Добавление в контекст выбранного поддерживающего индекса
            Context.SupportDataIds.Add(new List<int>() { outSample.ID });
            return outSample;
        }

        /// <summary>
        /// Получение ответа из ретривер бота
        /// </summary>
        /// <param name="q">Вопрос</param>
        public string GetRetriAnswer(string q) 
        {
            string answer = GetRandomSampleWithQ(q).Answer;
            Context.AddAssistantMessage(answer);
            return answer;
        }

        /// <summary>
        /// Генеративный ответ
        /// </summary>
        /// <param name="q">Вопрос</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetGenerariveAnswer(string q) 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение поддерживающих текстов (базовый класс)
        /// </summary>
        private Tuple<List<QASample>, List<int>> GetSupportBase()
        {
            List<QASample> lQA = new List<QASample>();
            List<int> idsQA = new List<int>(); // Индексы QA

            for (int i = 0; i < RetriBots.Count; i++)
            {
                QASample sample = RetriBots[i].GetAnswer(Context);
                lQA.Add(sample);
                idsQA.Add(sample.ID);
            }

            return new Tuple<List<QASample>, List<int>>(lQA, idsQA);
        }
    }
}
