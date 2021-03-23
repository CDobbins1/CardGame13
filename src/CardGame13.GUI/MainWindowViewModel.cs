using CardGame13.Game;
using CardGame13.Network;
using System.Windows;
using System.Windows.Input;

namespace CardGame13.GUI
{
    public class MainWindowViewModel
    {
        public string PlayerName { get; set; } = "New Player";
        public string IPAddress { get; set; }

        private Window Window { get; }

        private Client Client { get; set; } = new Client();

        public ICommand HostCommand { get; }
        public ICommand JoinCommand { get; }

        public MainWindowViewModel(Window window)
        {
            HostCommand = new Command(OnHost);
            JoinCommand = new Command(OnJoin);
            Window = window;
        }

        private void OnHost()
        {
            //Host game then join using local host
            var server = new Server();
            server.Start();
            ConnectToServer("127.0.0.1");
        }

        private void OnJoin()
        {
            //Join using ip input
            ConnectToServer(IPAddress);
        }

        private async void ConnectToServer(string address)
        {
            Client.Start(address);
            var player = new Player { Name = PlayerName };
            var message = new NetworkMessage { MessageType = NetworkHelper.MessageType.Initial, Player = player };
            Client.SendMessage(message);
            var gameWindow = new GameWindow(Client, await Client.ReceiveMessage());
            gameWindow.Show();
            Window.Close();
        }
    }
}
