using AI.ChatBotLib.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AI.ChatBotLib.BaseLogic.GenerativeBot
{
    /// <summary>
    /// Интерфейс генеративного бота
    /// </summary>
    public interface IGenerativeBot
    {
        /// <summary>
        /// Ответ на вопрос
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<string> GetAnswer(BotContext context);
    }
}
