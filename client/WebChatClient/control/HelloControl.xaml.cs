using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WebChatClient.control
{
    /// <summary>
    /// Interaction logic for HelloControl.xaml
    /// </summary>
    public partial class HelloControl : UserControl
    {
        public MainWindow Window = (MainWindow) Application.Current.MainWindow;
        public HelloControl()
        {
            InitializeComponent();
        }

        private void Enter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Window.UsernameText.Text = UsernameText.Text;
            Window.InitializeWebSocket();
            Visibility = Visibility.Collapsed;
        }
    }
}
