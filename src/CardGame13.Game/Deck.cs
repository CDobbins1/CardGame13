using System.Collections.ObjectModel;
using System.Linq;

namespace CardGame13.Game
{
    public class Deck
    {
        private readonly string[] _Ranks = { "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2" };
        public ReadOnlyCollection<string> Ranks { get; }
        private readonly string[] _Suits = { "Spades", "Clubs", "Diamonds", "Hearts" };
        public ReadOnlyCollection<string> Suits { get; }
        public Card[] Cards { get; } = new Card[52];

        public Deck()
        {
            Ranks = new ReadOnlyCollection<string>(_Ranks.ToList());
            Suits = new ReadOnlyCollection<string>(_Suits.ToList());
        }
    }
}
