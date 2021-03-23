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
            var xs = new XmlSerializer(typeof(NetworkMessage));
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);
            xs.Serialize(sw, message);
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public static NetworkMessage Decode(byte[] buffer)
        {
            var sr = new StringReader(Encoding.UTF8.GetString(buffer));
            var xs = new XmlSerializer(typeof(NetworkMessage));
            return (NetworkMessage)xs.Deserialize(sr);
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
            _ = await stream.ReadAsync(header, 0, 4).ConfigureAwait(false);
            int messageSize = NetworkHelper.GetMessageSize(header);

            //read bytes according to header
            var buffer = new byte[messageSize];
            _ = await stream.ReadAsync(buffer, 0, messageSize).ConfigureAwait(false);

            //return the decoded message
            return NetworkHelper.Decode(buffer);
        }
    }
}
