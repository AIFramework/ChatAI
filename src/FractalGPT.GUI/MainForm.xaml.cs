using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using FractalGPT.GUI.Models;
using AI.ChatBotLib.BaseLogic.GenerativeBot;
using System.IO;
using System.Collections.Generic;
using AI.ChatBotLib.RetrievalBot.BaseLogic.TextRetri;
using AI.ChatBotLib;
using AI.ChatBotLib.MainLogic;
using AI.ChatBotLib.BaseLogic.RetrievalBot;

namespace FractalGPT.GUI
{
    /// <summary>
    /// Логика взаимодействия для основной формы приложения.
    /// </summary>
    
    public partial class MainForm : Window
    {
        
        private readonly OpenFileDialog _openFileDialog; // Диалог для выбора файлов
        private string _filePath; // Путь к выбранному файлу
        PersonaChatGPT GBot;
        ChatBot _bot;

        /// <summary>
        /// Конструктор, инициализирующий компоненты формы и API клиента.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            _openFileDialog = new OpenFileDialog();

            List<string> facts = new List<string>()
            {
                "Я Ваш личный помощник",
            };

            GBot = new PersonaChatGPT(File.ReadAllText("key.txt"), proxyPath: "proxy.json");
            GBot.BaseFacts = facts;
            Settings.Load("data.json");
            var searchBot = new NgramJaccardTextSearchBot();

            _bot = new ChatBot(new List<IRetryBot>() { searchBot }, GBot);
            _bot.GenerativeAnswer += Bot_GenerativeAnswer;
        }

        // Прием сообщений бота
        private void Bot_GenerativeAnswer(string obj)
        {
            // Создаем новое сообщение и добавляем его в список сообщений
            MessagesList.Items.Add(new Message(obj, Sender.Bot, filePath: _filePath));
            MessagesList.ScrollIntoView(MessagesList.Items[^1]);
        }





        // Обработчик события клика по кнопке отправки сообщения
        private void SendButton_Click(object sender, RoutedEventArgs e) => Send();

        // Обработчик события для перемещения окна приложения
        private void MovingForm(object sender, RoutedEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        // Обработчик события нажатия клавиши в текстовом поле сообщения
        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Send();
        }

        // Обработчик события выбора файла
        private void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            if (_openFileDialog.ShowDialog() == true)
                _filePath = _openFileDialog.FileName;
        }

        // Обработчик события закрытия приложения
        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        // Обработчик события сворачивания окна приложения
        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        // Обработчик события для возможности перемещения окна
        private void MoveWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }





        /// <summary>
        /// Отправляет текстовое сообщение в ChatGpt и выводит ответ.
        /// </summary>
        private async void Send()
        {
            // Проверяем, не пуст ли текст сообщения
            if (string.IsNullOrEmpty(TextMessage.Text))
                return;

            // Получаем текст вопроса из текстового поля
            string question = TextMessage.Text;
            _bot.GetGenerariveAnswer(question);

            // Создаем новое сообщение и добавляем его в список сообщений
            MessagesList.Items.Add(new Message(question, Sender.User, filePath: _filePath));
            // Прокручиваем список сообщений до последнего элемента
            MessagesList.ScrollIntoView(MessagesList.Items[^1]);
            // Очищаем путь к файлу, если он был использован
            _filePath = string.Empty;
            // Очищаем текстовое поле после отправки сообщения
            TextMessage.Clear();
        }


       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //_chatGptApi.Dispose();
        }
    }
}