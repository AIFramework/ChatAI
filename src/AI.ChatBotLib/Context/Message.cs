using AI.ChatBotLib.Utilites.MaskedData;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AI.ChatBotLib.Context
{
    /// <summary>
    /// Сообщение
    /// </summary>
    [Serializable]
    public class Message
    {
        /// <summary>
        /// Время отправки
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Замаскированное время отправки, например "вчера"
        /// </summary>
        [JsonIgnore]
        public string TimeMask => DataTimeMask.GetMask(Time);

        /// <summary>
        /// Роль
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// Текст
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public Message() 
        {
            Time = DateTime.Now;
        }

        /// <summary>
        /// Сообщение
        /// </summary>
        /// <param name="role">Роль (user/bot)</param>
        /// <param name="text">Текст сообщения</param>
        public Message(string role, string text) 
        { 
            Role = role; 
            Text = text;
            Time = DateTime.Now;
        }
    }
}
