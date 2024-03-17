using System;

namespace AI.ChatBotLib.RetrievalBot.BaseLogic
{
    /// <summary>
    /// Представление вопроса
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class QData<T> 
    {
        
        public int ID { get; set; }
        public T Question { get; set; }

        public QData(int id, T question)
        {
            ID = id;
            Question = question;
        }
    }
}
