using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AI.ChatBotLib.RetrievalBot.BaseLogic.TextRetri
{
    /// <summary>
    /// Бот на базе сравнения N-грамм текстов с использованием коэффициента Жаккара
    /// </summary>
    [Serializable]
    public class NgramJaccardTextSearchBot : RetriBot<HashSet<string>>
    {
        /// <summary>
        /// Нормализовать ли на длинну вопроса
        /// </summary>
        public bool QNorm { get; set; } = true;
        /// <summary>
        /// Размер N-граммы
        /// </summary>
        public int NGrammSize { get; protected set; } = 2;
        /// <summary>
        /// Удалять ли пробелы и переносы
        /// </summary>
        public bool SpaceDel { get; protected set; }
        /// <summary>
        /// Удалять ли знаки пунктуации
        /// </summary>
        public bool PDel { get; protected set; }
        /// <summary>
        /// Бот на базе сравнения N-грамм текстов с использованием коэффициента Жаккарда
        /// </summary>
        /// <param name="path">Путь до файла с вопросами и ответами</param>
        /// <param name="nGSize">Размер n-граммы</param>
        /// <param name="spaceDel">Удалять ли пробелы и переносы</param>
        /// <param name="pDel">Удалять ли знаки пунктуации</param>
        public NgramJaccardTextSearchBot(string path, int nGSize = 2, bool spaceDel = true, bool pDel = true)
        {
            NGrammSize = nGSize;
            SpaceDel = spaceDel;
            PDel = pDel;
            LoadData(path);
            PDel = pDel;
        }

        /// <summary>
        /// Метод для опредления сходства текстов
        /// </summary>
        /// <param name="set1">Текст 1 (множество n-грамм)</param>
        /// <param name="set2">Текст 2 (множество n-грамм)</param>
        public override double SimFunc(HashSet<string> set1, HashSet<string> set2) =>
          QNorm? JaccardCoefficientQ(set1, set2): JaccardCoefficient(set1, set2);        

        /// <summary>
        /// Преобразование текста в новое представление
        /// для поиска
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns></returns>
        public override HashSet<string> TextTransform(string text)
        {
            string textData = text.ToLower();
            if (PDel) textData = Regex.Replace(textData, @"[\p{P}]", "");
            if(SpaceDel) textData = Regex.Replace(textData, @"[\s]", "");
            return TextToNGrammSet(textData);
        }

        // Разбивавает текст на n-граммы
        private HashSet<string> TextToNGrammSet(string text)
        {
            int n = NGrammSize - 1;
            var bigrams = new HashSet<string>();
            for (int i = 0; i < text.Length - n; i++)
                bigrams.Add(text.Substring(i, NGrammSize));

            return bigrams;
        }

        //Симметричный коэффициент Жаккара
        private double JaccardCoefficient(HashSet<string> set1, HashSet<string> set2) =>
            set1.Intersect(set2).Count() / set1.Union(set2).Count();

        //Коэффициент Жаккара нормированный на длинну вопроса
        private double JaccardCoefficientQ(HashSet<string> set1, HashSet<string> set2) =>
            set1.Intersect(set2).Count()/ set1.Count;
         
    }

}


