using AI.ChatBotLib.BaseLogic.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.ChatBotLib.BaseLogic
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
        public string NoAnswer { get; set; }
        = "Нет ответа";

        /// <summary>
        /// Массив вопросов
        /// </summary>
        protected T[] Q { get; set; }
        /// <summary>
        /// Массив ответов
        /// </summary>
        protected string[] A { get; set; }

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
        public void LoadTxt(string path)
        {
            string[] lines = File.ReadAllLines(path);
            A = new string[lines.Length / 2];
            Q = new T[lines.Length / 2];

            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 2 == 0)
                    Q[i / 2] = TextTransform(lines[i]);
                else A[i / 2] = lines[i];
            }
        }

        /// <summary>
        /// Поиск ответа на вопрос
        /// </summary>
        /// <param name="q">Вопрос</param>
        /// <param name="simTreshold">Порог близости (по умолчанию 0.5)</param>
        public virtual string GetAnswer(string q, double simTreshold = 0.5)
        {
            T qFeatures = TextTransform(q);
            double maxValue = double.MinValue;
            int maxQIndex = -1;

            for (int i = 0; i < Q.Length; i++)
            {
                double s = SimFunc(qFeatures, Q[i]);
                if (maxValue < s && s >= simTreshold)
                {
                    maxQIndex = i;
                    maxValue = s;
                }
            }

            return maxQIndex != -1 ? A[maxQIndex] : NoAnswer;
        }

        /// <summary>
        /// Поиск ответа на вопрос
        /// </summary>
        /// <param name="q">Контекст</param>
        /// <param name="simTreshold">Порог близости (по умолчанию 0.5)</param>
        public virtual string GetAnswer(BotContext context, double simTreshold = 0.5) 
        {
            var dim = context.GetContextPart(1);
            string q = dim[0]["text"];
            return GetAnswer(q, simTreshold);

        }
    }
}
