using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardGame13.Network
{
    public class NetworkHelper
    {
        public enum MessageType
        {
            Unassigned, Initial, ValidPlay, Pass
        }

        public static byte[] Encode(NetworkMessage message)
        {
            var serializer = new XmlSerializer(typeof(NetworkMessage));
            var stringBuilder = new StringBuilder();
            var stringWriter = new StringWriter(stringBuilder);
            serializer.Serialize(stringWriter, message);
            return Encoding.UTF8.GetBytes(stringBuilder.ToString());
        }

        public static NetworkMessage Decode(byte[] buffer)
        {
            var stringReader = new StringReader(Encoding.UTF8.GetString(buffer));
            var serializer = new XmlSerializer(typeof(NetworkMessage));
            return (NetworkMessage)serializer.Deserialize(stringReader);
        }

        public static int GetMessageSize(byte[] buffer) => IPAddress.NetworkToHostOrder(BitConverter.ToInt32(buffer));

        public static void SendMessage(NetworkStream stream, NetworkMessage message)
        {
            var body = Encode(message);
            var header = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(body.Length));
            stream.Write(header, 0, header.Length);
            stream.Write(body, 0, body.Length);
        }

        public static async Task<NetworkMessage> ReceiveMessage(NetworkStream stream)
        {
            //read header
            var header = new byte[4];
            await stream.ReadAsync(header, 0, 4).ConfigureAwait(false);
            int messageSize = GetMessageSize(header);

            //read bytes according to header
            var buffer = new byte[messageSize];
            await stream.ReadAsync(buffer, 0, messageSize).ConfigureAwait(false);

            //return the decoded message
            return Decode(buffer);
        }
    }
}
