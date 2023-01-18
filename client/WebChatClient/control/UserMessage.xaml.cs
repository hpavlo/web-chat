using System.Windows.Controls;

namespace WebChatClient.control
{
    /// <summary>
    /// Interaction logic for UserMessage.xaml
    /// </summary>
    public partial class UserMessage : UserControl
    {
        public UserMessage(string time, string username, string message)
        {
            InitializeComponent();
            TimeText.Text = time;
            NameText.Text = username;
            MessageText.Text = message;
        }
    }
}
