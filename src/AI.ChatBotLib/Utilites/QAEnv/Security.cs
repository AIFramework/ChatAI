using System;
using System.Collections.Generic;

namespace AI.ChatBotLib.Utilites.QAEnv
{
    /// <summary>
    /// Безопасность и разграничение доступа
    /// </summary>
    [Serializable]
    public class Security
    {
        /// <summary>
        /// Уровни доступа к информации
        /// </summary>
        public Dictionary<int, AccessLevel> AccessLevels { get; set; } = new Dictionary<int, AccessLevel>();
    }


    /// <summary>
    /// Уровни доступа
    /// </summary>
    [Serializable]
    public enum AccessLevel : byte
    {
        /// <summary>
        /// На любом сервере
        /// </summary>
        Any,
        /// <summary>
        /// Только на российских серверах
        /// </summary>
        OnlyRussia,
        /// <summary>
        /// Только внутри контура
        /// </summary>
        OnlyLocal
    }
}
