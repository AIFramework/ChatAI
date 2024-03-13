using System;
using System.Text.Json.Serialization;

namespace AI.ChatBotLib.Utilites
{
    /// <summary>
    /// Элемент QA
    /// </summary>
    [Serializable]
    public class QASample
    {
        /// <summary>
        /// Идентификатор QA
        /// </summary>
        [JsonPropertyName("id")]
        public int ID { get; set; }

        /// <summary>
        /// Вопрос
        /// </summary>
        [JsonPropertyName("question")]
        public string Question { get; set; }

        /// <summary>
        /// Ответ
        /// </summary>
        [JsonPropertyName("answer")]
        public string Answer { get; set; }

        /// <summary>
        /// Число "лайков"
        /// </summary>
        [JsonPropertyName("reward")]
        public double Reward { get; set; } = 0;

        /// <summary>
        /// Число оценок
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; } = 0;

        /// <summary>
        /// Оценка
        /// </summary>
        [JsonIgnore]
        public double Score => Reward / (Count + double.Epsilon);

        /// <summary>
        /// Элемент QA
        /// </summary>
        public QASample() { }

        /// <summary>
        /// Элемент QA
        /// </summary>
        /// <param name="question">Вопрос</param>
        /// <param name="answer">Ответ</param>
        public QASample(string question, string answer)
        {
            Question = question; Answer = answer;
        }
    }
}
