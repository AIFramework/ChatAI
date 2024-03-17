using AI.ChatBotLib.Utilites;
using AI.ChatBotLib.Utilites.QAEnv;
using System;
using System.Collections.Generic;
using System.Text;

namespace AI.ChatBotLib
{
    /// <summary>
    /// Глобальные переменные окружения
    /// </summary>
    public static class Env
    {

        public const string StartConvFREDT5 = "Недавно, у меня был следующий диалог: ";

        /// <summary>
        /// QA 
        /// </summary>
        public static QAEnvironment QAData { get; set; } = new QAEnvironment();

        public static void Load(string path) 
        {
            QAData = new QAEnvironment();
            var list = QAManager.LoadData(path);

            foreach ( var item in list )
                QAData.AddQAPart(item);
        }
    }
}
