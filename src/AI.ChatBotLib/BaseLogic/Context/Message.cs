using System;
using System.Text.Json.Serialization;

namespace AI.ChatBotLib.BaseLogic.Context
{
    /// <summary>
    /// Сообщение
    /// </summary>
    [Serializable]
    public class Message 
    {
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
        public Message() { }

        /// <summary>
        /// Сообщение
        /// </summary>
        /// <param name="role">Роль (user/bot)</param>
        /// <param name="text">Текст сообщения</param>
        public Message(string role, string text) {  Role = role; Text = text; }
    }
}
