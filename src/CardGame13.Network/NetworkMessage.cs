using CardGame13.Game;
using System.Collections.Generic;

namespace CardGame13.Network
{
    public class NetworkMessage
    {
        public NetworkHelper.MessageType MessageType { get; set; } = NetworkHelper.MessageType.Unassigned;
        public Player? Player { get; set; }
        public int LastPlayBy { get; set; }
        public List<Card>? Hand { get; set; }
        public List<Player>? Players { get; set; }
    }
}
