using AI.DataPrepaire.DataLoader.NNWBlockLoader;
using AI.DataStructs.Algebraic;
using AI.ONNX.NLP.Bert;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AI.ChatBotLib.RetrievalBot.BaseLogic.VectorRetri
{
    /// <summary>
    /// Бот на базе нейросети Bert
    /// </summary>
    [Serializable]
    public class SBertBot : RetriBot<Vector>
    {
        // Модель Bert
        private BertEmbedder _embedder;

        /// <summary>
        /// Бот на базе нейросети Bert
        /// </summary>
        /// <param name="pathQA">Путь до базы вопрос-ответ</param>
        /// <param name="pathSbert">Путь до нейронной сети</param>
        public SBertBot(string pathQA, string pathSbert)
        {
            _embedder = BertEmbedder.FromPretrained(pathSbert);
            // Добавление последнего(линейного) слоя
            LinearLayerLoader linearLayer = LinearLayerLoader.LoadFromBinary($@"{pathSbert}\1_Linear\model.aifr");
            _embedder.V2VBlocks.Add(linearLayer);

            LoadData(pathQA);
        }

        /// <summary>
        /// Определение сходства между двумя векторами
        /// </summary>
        /// <param name="vect1"></param>
        /// <param name="vect2"></param>
        /// <returns></returns>
        public override double SimFunc(Vector vect1, Vector vect2) =>
            vect1.Cos(vect2);

        /// <summary>
        /// Преобразование текста в вектор
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override Vector TextTransform(string text) => 
            _embedder.ForwardSBert(text);
    }
}
