using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AI.ChatBotLib.Utilites.QAEnv
{
    /// <summary>
    /// Среда для QA
    /// </summary>
    [Serializable]
    public class QAEnvironment
    {
        /// <summary>
        /// Реестр QA
        /// </summary>
        [JsonPropertyName("registry")]
        public Dictionary<int, QASample> Registry { get; set; } = new Dictionary<int, QASample>();

        /// <summary>
        /// Песочница
        /// </summary>
        [JsonPropertyName("sandbox")]
        public HashSet<int> SandBox { get; protected set; } = new HashSet<int>();

        /// <summary>
        /// Основная часть
        /// </summary>
        [JsonPropertyName("main")]
        public HashSet<int> MainPart { get; protected set; } = new HashSet<int>();

        /// <summary>
        /// Добавление QA
        /// </summary>
        /// <param name="sample">Пример ответа</param>
        public void AddQAPart(QASample sample) 
        {
            if(sample.ID == -1)
                sample.ID = Registry.Count;
            
            SandBox.Add(sample.ID);
            Registry.Add(sample.ID, sample);
        }

        /// <summary>
        /// Добавление покрепления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reward"></param>
        public void AddReward(int id, double reward) 
        {
            Registry[id].Reward += reward;
            Registry[id].Count ++;
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="id"></param>
        public void Remove(int id) 
        {
            Registry.Remove(id);
        }

        /// <summary>
        /// Перевод из песочницы в основную базу (аналог перевода из кратковременной в долговременную память)
        /// </summary>
        /// <param name="minCount">Минимальное число оценок для перевода</param>
        public void TransferToMain(int minCount = 25) 
        {
            List<int> removeList = new List<int>();
            List<int> removeAllList = new List<int>();

            foreach(int id in SandBox)
            {
                if (Registry[id].Count >= minCount && Registry[id].Reward > 0)
                {
                    MainPart.Add(id);
                    removeList.Add(id);
                }
                // Стирание мешающей информации
                else if(Registry[id].Reward <= 0)
                    removeAllList.Add(id);
            }

            foreach (int id in removeList)
                SandBox.Remove(id);

            foreach (int id in removeAllList)
            {
                SandBox.Remove(id);
                Registry.Remove(id);
            }

        }

        /// <summary>
        /// Забывание малозначительной и мешающей информации
        /// </summary>
        /// <param name="qaMax">Максимальный объем памяти</param>
        public void Update(int qaMax = 5000) 
        {
            if (MainPart.Count <= qaMax) return;

            List<QASample> data = new List<QASample>();
            foreach (var id in MainPart)
                data.Add(Registry[id]);
            
            data.Sort((x,y) => -x.Reward.CompareTo(y.Score));
            MainPart.Clear();

            for (int i = 0; i < qaMax; i++)
                MainPart.Add(data[i].ID);
        }
    }
}
