using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CardGame13.Network
{
    public class Client
    {
        private NetworkStream? Stream { get; set; }

        public void Start(string ipAddress)
        {
            int port = 9001;
            var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Waiting to connect...");

            while (!socket.Connected)
            {
                try { socket.Connect(endPoint); }
                catch (SocketException) { }
            }
            Console.WriteLine("Connected");

            Stream = new NetworkStream(socket, true);
        }

        public void SendMessage(NetworkMessage message) => NetworkHelper.SendMessage(Stream!, message);

        public async Task<NetworkMessage> ReceiveMessage() => await NetworkHelper.ReceiveMessage(Stream!).ConfigureAwait(false);
    }
}
