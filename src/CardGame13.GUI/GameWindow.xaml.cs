using CardGame13.Network;
using System.Windows;

namespace CardGame13.GUI
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow(Client client, NetworkMessage message)
        {
            DataContext = new GameWindowViewModel(client, message);
            InitializeComponent();
        }
    }
}
