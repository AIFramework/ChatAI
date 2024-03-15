using AI.ChatBotLib.Context;
using AI.ChatBotLib.Utilites;

namespace AI.ChatBotLib.BaseLogic.RetrievalBot
{
    /// <summary>
    /// Бот
    /// </summary>
    public interface IRetryBot
    {
        /// <summary>
        /// Поиск индекса ответа на вопрос
        /// </summary>
        /// <param name="q">Вопрос</param>
        int GetAnswerIndex(string q);

        /// <summary>
        /// Поиск ответа на вопрос
        /// </summary>
        /// <param name="q">Вопрос</param>
        QASample GetAnswer(string q);

        /// <summary>
        /// Поиск ответа на вопрос
        /// </summary>
        /// <param name="context">Контекст</param>
        QASample GetAnswer(BotContext context);
    }
}
