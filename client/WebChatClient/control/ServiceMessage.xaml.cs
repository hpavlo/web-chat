using System.Windows.Controls;

namespace WebChatClient.control
{
    /// <summary>
    /// Interaction logic for ServiceMessage.xaml
    /// </summary>
    public partial class ServiceMessage : UserControl
    {
        public ServiceMessage(string text)
        {
            InitializeComponent();
            MessageText.Text = text;
        }
    }
}
