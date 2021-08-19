using CardGame13.Game;
using CardGame13.GUI.Commands;
using CardGame13.GUI.Views;
using CardGame13.Network;
using System.Threading.Tasks;

namespace CardGame13.GUI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _PlayerName = "New Player";
        public string PlayerName
        {
            get => _PlayerName;
            set
            {
                if (SetProperty(ref _PlayerName, value))
                {
                    HostCommand.RaiseCanExecuteChanged();
                    JoinCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _IPAddress = "127.0.0.1";
        public string IPAddress
        {
            get => _IPAddress;
            set
            {
                if (SetProperty(ref _IPAddress, value))
                {
                    HostCommand.RaiseCanExecuteChanged();
                    JoinCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _ConnectionInfo = "";

        public string ConnectionInfo
        {
            get => _ConnectionInfo;
            set => SetProperty(ref _ConnectionInfo, value);
        }

        private IClosable Window { get; }

        public BaseCommand HostCommand { get; }
        public BaseCommand JoinCommand { get; }

        public MainWindowViewModel(IClosable window)
        {
            HostCommand = new RelayCommand(OnHost, HasRequiredFields);
            JoinCommand = new RelayCommand(OnJoin, HasRequiredFields);
            Window = window;
        }

        public bool HasRequiredFields() => !string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(IPAddress);

        private void OnHost()
        {
            //Host game then join using local host
            var server = new Server();
            server.Start();
            IPAddress = "127.0.0.1";
            Task.Run(ConnectToServer);
        }

        private void OnJoin()
        {
            //Join using ip input
            Task.Run(ConnectToServer);
        }

        private async void ConnectToServer()
        {
            await ConnectToServerAsync();
        }

        private async Task ConnectToServerAsync()
        {
            var address = IPAddress;
            ConnectionInfo = $"Connecting to: {address}";

            var client = new Client();
            var player = new Player { Name = PlayerName };
            if (client.Start(address))
            {
                var message = new NetworkMessage { MessageType = NetworkHelper.MessageType.Initial, Player = player };
                client.SendMessage(message);

                var clientMessage = await client.ReceiveMessage();
                App.Current.Dispatcher.Invoke(() =>
                {
                    var gameWindow = new GameWindow(client, clientMessage);
                    gameWindow.Show();
                    Window?.Close();
                });
            }
            else
            {
                ConnectionInfo = "Failed to connect!";
            }
        }
    }
}
