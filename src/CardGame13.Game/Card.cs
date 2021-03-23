
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
            Rank = rank;
            RankValue = rankValue;
            Suit = suit;
            SuitValue = suitValue;
        }

        public Card() { }

        public override string ToString()
        {
            return $"{Rank}\nof\n{Suit}";
        }
    }
}
