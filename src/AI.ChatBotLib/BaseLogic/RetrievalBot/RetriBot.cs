using AI.ChatBotLib.BaseLogic.RetrievalBot;
using AI.ChatBotLib.Context;
using AI.ChatBotLib.Utilites;
using AI.ChatBotLib.Utilites.QAEnv;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.ChatBotLib.RetrievalBot.BaseLogic
{
    /// <summary>
    /// Бот для поиска ответов на базе совпадения слов
    /// </summary>
    [Serializable]
    public abstract class RetriBot<T> : IRetryBot
    {
        /// <summary>
        /// Порог схожести для выдачи ответа
        /// </summary>
        public double SimTreshold { get; set; } = 0.6;

        /// <summary>
        /// Когда ответ не найден
        /// </summary>
        public QASample NoAnswer { get; set; }
        = DefaultSamples.NoAnswerRus;
        /// <summary>
        /// Массив вопросов
        /// </summary>
        protected List<QData<T>> Questions { get; set; }
        /// <summary>
        /// Массив список ворос-ответ
        /// </summary>
        public QAEnvironment DataQA { get; set; } = Env.QAData;
        /// <summary>
        /// Метод для опредления сходства текстов
        /// </summary>
        /// <param name="text1">Текст 1</param>
        /// <param name="text2">Текст 2</param>
        /// <returns></returns>
        public abstract double SimFunc(T text1, T text2);
        /// <summary>
        /// Преобразование текста в новое представление
        /// для поиска
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns></returns>
        public abstract T TextTransform(string text);
        /// <summary>
        /// Загрузка вопрос ответ из файла
        /// </summary>
        /// <param name="path">Путь до файла</param>
        public void LoadSamples()
        {
            List<QASample> data = Env.QAData.Registry.Values.ToList();
            Questions = new List<QData<T>>();
            
            foreach (QASample sample in data)
            {
                T features = TextTransform(sample.Question);
                QData<T> qData = new QData<T>(sample.ID, features);
                Questions.Add(qData);
            }
        }
        /// <summary>
        /// Поиск индекса ответа на вопрос
        /// </summary>
        /// <param name="q">Вопрос</param>
        /// <param name="simTreshold">Порог близости (по умолчанию 0.5)</param>
        public virtual int GetAnswerIndex(string q)
        {
            T qFeatures = TextTransform(q);
            double maxValue = double.MinValue;
            int maxQIndex = -1;

            for (int i = 0; i < Questions.Count; i++)
            {
                double s = SimFunc(qFeatures, Questions[i].Question);
                if (maxValue < s && s >= SimTreshold)
                {
                    maxQIndex = Questions[i].ID;
                    maxValue = s;
                }
            }

            return maxQIndex;
        }
        /// <summary>
        /// Поиск ответа на вопрос
        /// </summary>
        /// <param name="q">Вопрос</param>
        /// <param name="simTreshold">Порог близости (по умолчанию 0.5)</param>
        public virtual QASample GetAnswer(string q)
        {
            int maxQIndex = GetAnswerIndex(q);
            return maxQIndex != -1 ? DataQA.Registry[maxQIndex] : NoAnswer;
        }
        /// <summary>
        /// Поиск ответа на вопрос
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="simTreshold">Порог близости (по умолчанию 0.5)</param>
        public virtual QASample GetAnswer(BotContext context) 
        {
            var dim = context.GetContextPart(1);
            string q = dim[0]["text"];
            return GetAnswer(q);
        }
    }
}
