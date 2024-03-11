using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;

namespace AI.ChatBotLib.Context
{
    /// <summary>
    /// Часть диалога с ботом
    /// </summary>
    [Serializable]
    public class BotContext
    {
        /// <summary>
        /// Абсолютный номер сообщения
        /// </summary>
        private int currentIndex;

        /// <summary>
        /// Сообщения (часть диалога)
        /// </summary>
        public List<Message> Messages { get; set; } = new List<Message>();

        /// <summary>
        /// Максимальное число сообщений
        /// </summary>
        public int MaxMess { get; protected set; }

        /// <summary>
        /// Часть диалога с ботом
        /// </summary>
        /// <param name="maxMess">Максимальное число сообщений, по умолчанию 20</param>
        public BotContext(int maxMess = 20)
        {
            MaxMess = maxMess;
            Clear();
        }

        /// <summary>
        /// Очищает текущий список сообщений
        /// </summary>
        public void Clear()
        {
            Messages.Clear();
            currentIndex = 1;
        }

        /// <summary>
        /// Добавляет сообщение от пользователя в список сообщений
        /// </summary>
        /// <param name="text">Текст сообщения пользователя</param>
        public void AddUserMessage(string text)
        {
            AddMessage("user", text);
        }

        /// <summary>
        /// Добавляет сообщение от бота в список сообщений
        /// </summary>
        /// <param name="text">Текст сообщения</param>
        public void AddAssistantMessage(string text)
        {
            AddMessage("bot", text);
        }

        /// <summary>
        /// Отдает часть датасета
        /// </summary>
        /// <param name="count">Число сообщений, которые будут отданы</param>
        public List<Dictionary<string, string>> GetContextPart(int count)
        {
            int countNew = Math.Min(count, Messages.Count);
            List<Dictionary<string, string>> mess = new List<Dictionary<string, string>>();

            for (int i = Messages.Count - countNew; i < Messages.Count; i++)
            {
                mess.Add(new Dictionary<string, string>
                {
                    { "role", Messages[i].Role },
                    { "text", Messages[i].Text }
                });

            }

            return mess;
        }

        /// <summary>
        /// Добавляет сообщение в список
        /// </summary>
        /// <param name="role"></param>
        /// <param name="text"></param>
        private void AddMessage(string role, string text)
        {
            if (currentIndex >= MaxMess)
                Messages.RemoveAt(0);
            else
                currentIndex++;

            Messages.Add(new Message(role, text));
        }



    }
}
