using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CardGame13.Network
{
    public class Client
    {
        private NetworkStream? Stream { get; set; }

        public bool Start(string ipAddress)
        {
            int port = 9001;
            var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Waiting to connect...");

            int attempts = 0;
            while (!socket.Connected && attempts < 5)
            {
                try { socket.Connect(endPoint); }
                catch (SocketException) 
                {
                    attempts++;
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                }
            }

            if (!socket.Connected)
            {
                Console.WriteLine("Failed to connect");
                return false;
            }

            Console.WriteLine("Connected");

            Stream = new NetworkStream(socket, true);
            return true;
        }

        public void SendMessage(NetworkMessage message) => NetworkHelper.SendMessage(Stream!, message);

        public async Task<NetworkMessage> ReceiveMessage() => await NetworkHelper.ReceiveMessage(Stream!).ConfigureAwait(false);
    }
}
