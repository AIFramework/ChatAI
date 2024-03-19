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
        OpenFileDialog _openFile = new OpenFileDialog();
        SaveFileDialog _saveFile = new SaveFileDialog();
        /// <summary>
        /// Объект, с каким работаем
        /// </summary>
        List<QASample> qASamples = new List<QASample>();

        /// <summary>
        /// 
        /// </summary>
        public OpenFileDialog OpenFile
        {
            get => _openFile;
            set => _openFile = value;
        }

        public SaveFileDialog SaveFile
        {
            get => _saveFile;
            set => _saveFile = value;
        }


        QAUtil utilData;

        public Form1()
        {
            InitializeComponent();
            _saveFile.Filter = "(JSON)|*.json";
            utilData = new QAUtil(dataGrid);
        }

        



        #region Меню
        // Открыть файл
        private void _openBtn_Click(object sender, EventArgs e)
        {
            dataGrid.Rows.Clear();

            if (_openFile.ShowDialog() == DialogResult.OK) 
            {
                qASamples = QAManager.LoadData(_openFile.FileName);
                utilData.AddQASamples(qASamples);
            }
        }

        // Сохранить файл
        private void _saveBtn_Click(object sender, EventArgs e)
        {
            if(_saveFile.ShowDialog() == DialogResult.OK) 
            {
                var data = utilData.GetData();
                QAManager.SaveToJson(_saveFile.FileName, data);
            }
        }
        #endregion

    }
}
