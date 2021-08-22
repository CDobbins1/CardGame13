using CardGame13.Game;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CardGame13.Network
{
    public class Server
    {
        private List<NetworkStream> ClientStreams { get; } = new();

        private RuleHandler RuleHandler { get; } = new();

        private Dealer Dealer { get; } = new(DeckBuilder.BuildDeck13());

        private List<Player> Players { get; } = new();

        private List<Card>? Pile { get; set; } = null;

        private int LastPlayerPlayed { get; set; }

        public async void Start()
        {
            int port = 9001;
            var endPoint = new IPEndPoint(IPAddress.Any, port);
            var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);
            socket.Listen(128);

            Dealer.ShuffleDeck();
            await WaitForConnectionsAsync(socket).ConfigureAwait(false);

            Console.WriteLine("All players connected");
            foreach (var stream in ClientStreams)
            {
                WaitForMessage(stream);
            }
        }

        private async void WaitForMessage(NetworkStream stream)
        {
            while (true)
            {
                var message = await NetworkHelper.ReceiveMessage(stream).ConfigureAwait(false);
                message.LastPlayBy = LastPlayerPlayed;
                if (message.MessageType == NetworkHelper.MessageType.Pass)
                {
                    if ((message.Player!.PlayerNumber + 1) % 4 == LastPlayerPlayed) RuleHandler.FreeTurn();
                    foreach (var clientStream in ClientStreams) NetworkHelper.SendMessage(clientStream, message);
                }
                else
                {
                    var validPlay = RuleHandler.IsValidPlay(message.Hand!, Pile);
                    if (validPlay)
                    {
                        Pile = message.Hand;
                        foreach (var clientStream in ClientStreams) NetworkHelper.SendMessage(clientStream, message);
                        LastPlayerPlayed = message.Player!.PlayerNumber;
                    }
                }
            }
        }

        private async Task WaitForConnectionsAsync(Socket socket)
        {
            Console.WriteLine("Waiting for players...");
            while (ClientStreams.Count < 4)
            {
                var clientSocket = await Task.Factory.FromAsync(
                    new Func<AsyncCallback, object?, IAsyncResult>(socket.BeginAccept),
                    new Func<IAsyncResult, Socket>(socket.EndAccept),
                    null).ConfigureAwait(false);

                Console.WriteLine("User connected");
                NetworkStream clientStream = new(clientSocket, true);
                ClientStreams.Add(clientStream);

                NetworkMessage message = await NetworkHelper.ReceiveMessage(clientStream).ConfigureAwait(false);
                if (message.Player is null) throw new InvalidOperationException($"Initial message cannot have null player!");
                message.Player.PlayerNumber = Players.Count;
                message.Hand = Dealer.DealHand(Players.Count);
                Players.Add(message.Player);

                NetworkHelper.SendMessage(clientStream, message);

                var playerUpdateMessage = new NetworkMessage { Players = Players };
                foreach (var stream in ClientStreams) NetworkHelper.SendMessage(stream, playerUpdateMessage);
            }
        }
    }
}
