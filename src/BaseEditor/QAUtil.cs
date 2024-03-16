using AI.ChatBotLib.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseEditor
{
    public static class QAUtil
    {
        public static void AddQASample(QASample qASample, DataGridView dataGridView)
        {
            int id = qASample.ID;
            string q = qASample.Question;
            string a = qASample.Answer;
            double reward = qASample.Reward;
            int count = qASample.Count;

            dataGridView.Rows.Add(new object[] { id, q, a, reward, count});
        }

        public static void AddQASamples(IEnumerable<QASample> qASamples, DataGridView dataGridView)
        {
            foreach (var item in qASamples)
                AddQASample(item, dataGridView);
            
        }

        public static QASample Row2QAData(DataGridViewCellCollection dataRow)
        {
            int id = int.Parse(dataRow[0].Value.ToString());
            string q = (string)dataRow[1].Value;
            string a = (string)dataRow[2].Value;
            double reward = double.Parse(dataRow[3].Value.ToString());
            int count = int.Parse(dataRow[4].Value.ToString());

            QASample sample = new QASample(q, a);
            sample.Reward = reward;
            sample.Count = count;
            sample.ID = id;
            return sample;
        }

        public static List<QASample> GetData(DataGridView dataGridView)
        {
            List<QASample> qASamples = new List<QASample>();

            foreach (DataGridViewRow item in dataGridView.Rows)
                if (item.Cells[0].Value != null)
                    qASamples.Add(Row2QAData(item.Cells));

            return qASamples;
        }


       
    }
}
