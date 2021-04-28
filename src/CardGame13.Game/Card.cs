using System;

namespace CardGame13.Game
{
    public class Card
    {
        public string Rank { get; set; } = "";
        public int RankValue { get; set; }
        public string Suit { get; set; } = "";
        public int SuitValue { get; set; }

        public Card(string rank, int rankValue, string suit, int suitValue)
        {
            Rank = string.IsNullOrWhiteSpace(rank) ? throw new ArgumentException("Card must have valid Rank!", nameof(rank)) : rank;
            RankValue = (rankValue < 0 || rankValue > 12) ? throw new ArgumentOutOfRangeException(nameof(rankValue)) : rankValue;
            Suit = string.IsNullOrWhiteSpace(suit) ? throw new ArgumentException("Card must have valid Suit!", nameof(suit)) : suit;
            SuitValue = (suitValue < 0 || suitValue > 3) ? throw new ArgumentOutOfRangeException(nameof(suitValue)) : suitValue;
        }

        public Card() { }

        public override string ToString()
        {
            return $"{Rank}\nof\n{Suit}";
        }
    }
}
