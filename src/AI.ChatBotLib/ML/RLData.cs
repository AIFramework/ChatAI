using AI.DataStructs.Algebraic;
using System;
using System.Text.Json.Serialization;

namespace AI.ChatBotLib.ML
{
    /// <summary>
    /// Данные для обучения с подкреплением
    /// </summary>
    [Serializable]
    public class RLData
    {
        /// <summary>
        /// Сетка влияния реварда на историю
        /// </summary>
        [JsonPropertyName("dialog_history_grid")]
        public Vector DialoHistoryCoef { get; set; }

        /// <summary>
        /// Данные для обучения с подкреплением
        /// </summary>
        /// <param name="historySize">Размер контекста</param>
        /// <param name="lambda">Коэф. для уменьшения</param>
        public RLData(int historySize, double lambda = 0.9) 
        {
            CalcDialoHistoryCoef(historySize, lambda);
        }

        /// <summary>
        /// Расчет сетки влияния шагов контексте диалога
        /// </summary>
        /// <param name="historySize">Размер контекста</param>
        /// <param name="lambda">Коэф. для уменьшения</param>
        public void CalcDialoHistoryCoef(int historySize, double lambda = 0.9) 
        {
            DialoHistoryCoef = new Vector(historySize);
            DialoHistoryCoef[0] = 1;

            for (int i = 1; i < historySize; i++)
                DialoHistoryCoef[i] = DialoHistoryCoef[i - 1] * lambda; 
        }
    }
}
