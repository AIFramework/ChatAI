using System;
using System.Collections.Generic;
using System.Text;

namespace AI.ChatBotLib.Utilites.MaskedData
{
    /// <summary>
    /// Маскирование даты и времени
    /// </summary>
    [Serializable]
    public class DataTimeMask
    {
        /// <summary>
        /// Получить маску для нужного языка
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string GetMask(DateTime dateTime, string lang = "rus") 
        {
            long diff = DateTime.Now.Ticks - dateTime.Ticks;
            TimeSpan timeSpan = new TimeSpan(diff);

            double minutes = (double)timeSpan.TotalMinutes;
            double hours = minutes/60;
            double days = hours / 24;
            double weaks = days / 7;
            double month = (double)timeSpan.TotalDays / 30;

            Dictionary<string, string> globalMask = new Dictionary<string, string>();

            // Выбор языка
            switch (lang) 
            {
                case "rus":
                    globalMask = GlobalMaskRus;
                    break;
            }


            if (month > 2)     return globalMask["M2"];
            if (month > 1)     return globalMask["M1"];
            if (weaks > 3)     return globalMask["W3"];
            // ToDo: улучшить логику, добавить дни недели и время суток
            if (weaks > 2)     return globalMask["W2"].Replace("{{day_name}}","");
            if (weaks > 1)     return globalMask["W1"].Replace("{{day_name}}", "");
            if (days > 3)      return globalMask["W0"].Replace("{{day_name}}", "");
            if (days > 2)      return globalMask["D2"].Replace("{{time_part}}", "");
            if (days > 1)      return globalMask["D1"].Replace("{{time_part}}", "");
            if (hours > 4)     return globalMask["H4"].Replace("{{N_H}}", Math.Round(hours).ToString());
            if (hours > 1)     return globalMask["H1"].Replace("{{N_H}}", Math.Round(hours).ToString());
            if (minutes > 60)  return globalMask["H0"];
            if (minutes > 50)  return globalMask["m50"];
            if (minutes > 30)  return globalMask["m30"];
            if (minutes > 1)   return globalMask["m1"].Replace("{{N_M}}", Math.Round(minutes).ToString());

            return globalMask["m0"];


        }

        public static Dictionary<string, string> GlobalMaskRus = new Dictionary<string, string>()
        {
            { "M2", "более двух месяцев назад"},
            { "M1", "более месяца назад"},
            { "W3", "более трех недель назад"},
            { "W2", "на позапрошлой недели{{day_name}}"},
            { "W1", "на прошлой недели{{day_name}}" },
            { "W0", "на этой недели{{day_name}}"},
            { "D2", "позавчера{{time_part}}"},
            { "D1", "вчера{{time_part}}"},
            { "H4" , "{{N_H}} часов назад"},
            { "H1", "{{N_H}} часа назад"},
            { "H0", "час назад"},
            { "m50", "почти час назад"},
            { "m30", "более получаса назад" },
            { "m1", "{{N_M}} минут назад" },
            {"m0", "менее минуты назад" }
        };

        


    }
}
