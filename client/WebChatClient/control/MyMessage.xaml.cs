using System.Windows.Controls;

namespace WebChatClient.control
{
    /// <summary>
    /// Interaction logic for MyMessage.xaml
    /// </summary>
    public partial class MyMessage : UserControl
    {
        public MyMessage(string time, string username, string message)
        {
            InitializeComponent();
            TimeText.Text = time;
            NameText.Text = username;
            MessageText.Text = message;
        }
    }
}
