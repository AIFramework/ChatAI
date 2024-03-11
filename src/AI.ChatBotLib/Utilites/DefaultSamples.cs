using System;

namespace AI.ChatBotLib.Utilites
{
    /// <summary>
    /// Значения фраз QA для определенных сценариев
    /// </summary>
    [Serializable]
    public static class DefaultSamples 
    {
        /// <summary>
        /// Нет ответа (русский)
        /// </summary>
        public static QASample NoAnswerRus = new QASample("","Нет ответа");
    }
}
