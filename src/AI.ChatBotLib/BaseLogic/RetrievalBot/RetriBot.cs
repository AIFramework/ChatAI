using AI.ChatBotLib.Context;
using AI.ChatBotLib.Utilites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI.ChatBotLib.RetrievalBot.BaseLogic
{
    /// <summary>
    /// Бот для поиска ответов на базе совпадения слов
    /// </summary>
    [Serializable]
    public abstract class RetriBot<T>
    {
        /// <summary>
        /// Когда ответ не найден
        /// </summary>
        public QASample NoAnswer { get; set; }
        = DefaultSamples.NoAnswerRus;
        /// <summary>
        /// Массив вопросов
        /// </summary>
        protected List<T> Question { get; set; }
        /// <summary>
        /// Массив список ворос-ответ
        /// </summary>
        public List<QASample> DataQA { get; set; }
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
        public void LoadData(string path)
        {
            var data = QAManager.LoadData(path);
            LoadSamples(data);
        }
        /// <summary>
        /// Загрузка вопрос ответ из файла
        /// </summary>
        /// <param name="path">Путь до файла</param>
        public void LoadSamples(IEnumerable<QASample> data)
        {
            Question = new List<T>();
            foreach (QASample sample in data)
                Question.Add(TextTransform(sample.Question));

            var qa = new QASample[Question.Count];
            Array.Copy(data.ToArray(), qa, Question.Count);
            DataQA = qa.ToList();
        }
        /// <summary>
        /// Поиск индекса ответа на вопрос
        /// </summary>
        /// <param name="q">Вопрос</param>
        /// <param name="simTreshold">Порог близости (по умолчанию 0.5)</param>
        public virtual int GetAnswerIndex(string q, double simTreshold = 0.5)
        {
            T qFeatures = TextTransform(q);
            double maxValue = double.MinValue;
            int maxQIndex = -1;

            for (int i = 0; i < Question.Count; i++)
            {
                double s = SimFunc(qFeatures, Question[i]);
                if (maxValue < s && s >= simTreshold)
                {
                    maxQIndex = i;
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
        public virtual QASample GetAnswer(string q, double simTreshold = 0.5)
        {
            int maxQIndex = GetAnswerIndex(q, simTreshold);
            return maxQIndex != -1 ? DataQA[maxQIndex] : NoAnswer;
        }
        /// <summary>
        /// Поиск ответа на вопрос
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="simTreshold">Порог близости (по умолчанию 0.5)</param>
        public virtual QASample GetAnswer(BotContext context, double simTreshold = 0.5) 
        {
            var dim = context.GetContextPart(1);
            string q = dim[0]["text"];
            return GetAnswer(q, simTreshold);
        }
    }
}
