using AI.ChatBotLib.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseEditor
{
    public class QAUtil
    {
        public DataGridView DataView { get; set; }

        public QAUtil(DataGridView dataGridView) 
        {
            DataView = dataGridView;
            DataView.RowsAdded += DataView_RowsAdded;
            CreateDefaultLast();
        }

        public void AddQASample(QASample qASample)
        {
            int id = qASample.ID;
            string q = qASample.Question;
            string a = qASample.Answer;
            double reward = qASample.Reward;
            int count = qASample.Count;

            DataView.Rows.Add(new object[] { id, q, a, reward, count});
        }

        public void AddQASamples(IEnumerable<QASample> qASamples)
        {
            foreach (var item in qASamples)
                AddQASample(item);
            
        }

        public List<QASample> GetData()
        {
            List<QASample> qASamples = new List<QASample>();
            int num = 0;

            foreach (DataGridViewRow item in DataView.Rows)
            {
                num++;
                if(DataView.Rows.Count > num)
                    qASamples.Add(Row2QAData(item.Cells));
            }

            return qASamples;
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

        // Добавление параметров строки по умолчанию
        private void DataView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CreateDefaultLast();
        }

        private void CreateDefaultLast() 
        {
            DataView.Rows[DataView.Rows.Count - 1].SetValues(new object[] { -1, "", "", 0, 0 });
        }

    }
}
