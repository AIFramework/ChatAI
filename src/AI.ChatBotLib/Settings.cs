using AI.ChatBotLib.Utilites;
using AI.ChatBotLib.Utilites.QAEnv;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AI.ChatBotLib
{
    /// <summary>
    /// Глобальные переменные окружения
    /// </summary>
    public static class Settings
    {

        public const string StartConvFREDT5 = "Недавно, у меня был следующий диалог: ";

        public static APIConnect Connect { get; set; }


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

    /// <summary>
    /// Подключение к api
    /// </summary>
    [Serializable]
    public class APIConnect 
    {
        /// <summary>
        /// url - для локальных моделей
        /// </summary>
        [JsonPropertyName("llm_host")]
        public string LocalLLMHost { get; set; }
        /// <summary>
        /// Ключ OpenAI
        /// </summary>
        [JsonPropertyName("openai_key")]
        public string OpenAIApi { get; set; }
        /// <summary>
        /// Ключ апи от проекта Fractal Gpt
        /// </summary>
        [JsonPropertyName("fractal_gpt_key")]
        public string FractalGPTApi { get; set; }
        /// <summary>
        /// Путь до файла с набором прокси
        /// </summary>
        [JsonPropertyName("proxy_path")]
        public static string ProxyPath { get; set; }
    }
}
