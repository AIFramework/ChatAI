using AI.DataStructs.Algebraic;
using AI.HightLevelFunctions;
using AI.Statistics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AI.ChatBotLib.Utilites
{
    /// <summary>
    /// Загружает, сохраняет и обрабатывает базу вопрос/ответ
    /// </summary>
    [Serializable]
    public static class QAManager 
    {
        /// <summary>
        /// Ответ с учетом обучения с подкреплением и внеших весов (показателей уверенности)
        /// </summary>
        /// <param name="dataQA">Данные ответа</param>
        /// <param name="random">ГПСЧ</param>
        /// <param name="w">Веса уверенности</param>
        /// <param name="minW">Минимальный вес (не 0)</param>
        /// <param name="t">Температура</param>
        /// <exception cref="Exception"></exception>
        public static string GetAnswerFromIndexes(IEnumerable<QASample> dataQA, Random random, Vector w = null, double minW = 0.01, double t = 0.6) 
        {
            double minLog = Math.Log(minW); // Получение минимального логарифма
            QASample[] data = dataQA.ToArray();
            Vector probs = new Vector(data.Length);

            // Получение оценки
            for (int i = 0; i < data.Length; i++)
                probs[i] = data[i].Score; 

            // Учет внешних весов
            if(w != null) 
            {
                if (w.Count != data.Length) throw new Exception("w.Count != data.Length");

                for (int i = 0; i < probs.Count; i++)
                    probs[i] += w[i]==0? minLog: Math.Log(w[i]);
            }

            // Применение температуры
            for (int i = 0; i < probs.Count; i++)
                probs[i] /= t;

            probs = ActivationFunctions.Softmax(probs);

            return RandomItemSelection.GetElement(probs, data, random).Answer;
        }

        /// <summary>
        /// Загрузка вопрос-ответ
        /// </summary>
        /// <param name="path">Путь до файла json</param>
        public static List<QASample> LoadFromJson(string path)
        {
            var jsonData = File.ReadAllText(path);
            var qaList = JsonSerializer.Deserialize<List<QASample>>(jsonData);
            return qaList;
        }

        /// <summary>
        /// Сохранение вопрос-ответ
        /// </summary>
        /// <param name="path">Путь до файла json</param>
        /// <param name="qaData">Данные вопрос ответ</param>
        public static void SaveToJson(string path, IEnumerable<QASample> qaData)
        {
            var json = JsonSerializer.Serialize(qaData);
            File.WriteAllText(path, json);
        }

        /// <summary>
        /// Загрузка вопрос-ответ
        /// </summary>
        /// <param name="path">Путь до файла *.txt</param>
        public static List<QASample> LoadFromTxt(string path) 
        {
            string[] lines = File.ReadAllLines(path);
            List<QASample> samples = new List<QASample>();
            QASample sample = new QASample();

            for (int i = 0; i < lines.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sample = new QASample();
                    sample.Question = lines[i];
                }
                else 
                {
                    sample.Answer = lines[i];
                    samples.Add(sample);
                }
            }

            return samples;
        }

        /// <summary>
        /// Загрузка данных с распознаванием типа файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<QASample> LoadData(string path) 
        {
            FileInfo fileInfo = new FileInfo(path);

            switch (fileInfo.Extension) 
            {
                case ".json":
                    return LoadFromJson(path);
                case ".txt":
                    return LoadFromTxt(path);
            }

            throw new Exception();
        }
    }
}
