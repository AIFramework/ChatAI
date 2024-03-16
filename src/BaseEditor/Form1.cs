using AI.ChatBotLib.Utilites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseEditor
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFile = new OpenFileDialog();
        SaveFileDialog saveFile = new SaveFileDialog();
        /// <summary>
        /// Объект, с каким работаем
        /// </summary>
        List<QASample> qASamples = new List<QASample>();

        public Form1()
        {
            InitializeComponent();
            saveFile.Filter = "(JSON)|*.json";
        }

       

        #region Меню
        

        private void openBtn_Click(object sender, EventArgs e)
        {
            dataGrid.Rows.Clear();

            if (openFile.ShowDialog() == DialogResult.OK) 
            {
                qASamples = QAManager.LoadData(openFile.FileName);
                QAUtil.AddQASamples(qASamples, dataGrid);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(saveFile.ShowDialog() == DialogResult.OK) 
            {
                var data = QAUtil.GetData(dataGrid);
                QAManager.SaveToJson(saveFile.FileName, data);
            }
        }
        #endregion
    }
}
