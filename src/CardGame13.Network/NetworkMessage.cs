using CardGame13.Game;
using System.Collections.Generic;
using static CardGame13.Game.RuleHandler;

namespace CardGame13.Network
{
    public class NetworkMessage
    {
        public NetworkHelper.MessageType MessageType { get; set; } = NetworkHelper.MessageType.Unassigned;
        public Category CurrentCategory { get; set; }
        public Player? Player { get; set; }
        public int LastPlayBy { get; set; }
        public List<Card>? Hand { get; set; }
        public List<Player>? Players { get; set; }
    }
}
