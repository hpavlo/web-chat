using Newtonsoft.Json;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using WebChatClient.control;
using WebChatClient.model;
using WebSocketSharp;

namespace WebChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string DEFAULT_TEXT_AREA = "Type your message here...";
        public const string TIME_FORMAT = "HH:mm";
        private string Username => UsernameText.Text;
        private WebSocket? WebSocket;

        public MainWindow()
        {
            InitializeComponent();
            MessageArea.Text = DEFAULT_TEXT_AREA;
        }

        public void InitializeWebSocket()
        {
            WebSocket = new WebSocket(Properties.Resources.WebSocketURL);
            WebSocket.OnMessage += WebSocket_OnMessage;
            WebSocket.OnError += (s, e) => MessageBox.Show(e.Message);
            WebSocket.Connect();
            SendCreatedSessionMessage();
        }

        private void SendCreatedSessionMessage()
        {
            Message message = new(Username, DateTime.MinValue, string.Empty, MessageType.CreateSession);
            WebSocket?.Send(JsonConvert.SerializeObject(message));
        }

        private void SendClosedSessionMessage()
        {
            Message message = new(Username, DateTime.MinValue, string.Empty, MessageType.CloseSession);
            WebSocket?.Send(JsonConvert.SerializeObject(message));
        }

        private void AddServiceMessage(string text)
        {
            ServiceMessage serviceMessage = new ServiceMessage(text);
            MessagePanel.Children.Add(serviceMessage);
        }

        private void AddUserMessage(Message message)
        {
            string time = message.Time.ToString(TIME_FORMAT);
            if (message.Username.Equals(Username))
            {
                MyMessage myMessage = new(time, message.Username, message.Text);
                MessagePanel.Children.Add(myMessage);
            }
            else
            {
                UserMessage userMessage = new(time, message.Username, message.Text);
                MessagePanel.Children.Add(userMessage);
            }
        }

        private void WebSocket_OnMessage(object? sender, MessageEventArgs e)
        {
            Message? message = JsonConvert.DeserializeObject<Message>(e.Data);
            Application.Current.Dispatcher.Invoke(delegate
            {
                switch (message?.MessageType)
                {
                    case MessageType.User:
                        AddUserMessage(message);
                        break;
                    case MessageType.CreateSession:
                        AddServiceMessage($"{message.Username} has joined the chat");
                        break;
                    case MessageType.CloseSession:
                        AddServiceMessage($"{message.Username} left the chat");
                        break;
                }
                ScrollMessageViewer.ScrollToBottom();
            });
        }

        private void SendMessage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!MessageArea.Text.Equals(DEFAULT_TEXT_AREA))
            {
                Message message = new(Username, DateTime.MinValue, MessageArea.Text, MessageType.User);
                WebSocket?.Send(JsonConvert.SerializeObject(message));
            }
            Keyboard.ClearFocus();
        }

        private void MessageArea_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MessageArea.Text = string.Empty;
            MessageArea.Foreground = new SolidColorBrush(Color.FromRgb(47, 62, 70));
        }

        private void MessageArea_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MessageArea.Text = DEFAULT_TEXT_AREA;
            MessageArea.Foreground = new SolidColorBrush(Color.FromRgb(107, 144, 128));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WebSocket != null && WebSocket.IsAlive)
            {
                SendClosedSessionMessage();
                WebSocket.Close();
            }
        }
    }
}
