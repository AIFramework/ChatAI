using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.ChatBotLib.RetrievalBot.BaseLogic.TextRetri
{
    /// <summary>
    /// Простой бот на базе полнотекстового поиска
    /// </summary>
    [Serializable]
    public class StringTextSearchBot : RetriBot<string>
    {
        /// <summary>
        /// Простой бот на базе полнотекстового поиска
        /// </summary>
        public StringTextSearchBot()
        {
            LoadSamples();
        }

        /// <summary>
        /// Метод для опредления сходства текстов
        /// </summary>
        /// <param name="text1">Текст 1</param>
        /// <param name="text2">Текст 2</param>
        /// <returns></returns>
        public override double SimFunc(string text1, string text2)
        => text1 == text2 ? 1.0 : 0.0;

        /// <summary>
        /// Преобразование текста в новое представление
        /// для поиска
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns></returns>
        public override string TextTransform(string text) =>
            text.ToLower().Trim(new[] { '!', '?', '.', ')', ' ' });

    }

}


