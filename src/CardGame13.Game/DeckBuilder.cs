using System.Collections.Generic;

namespace CardGame13.Game
{
    public static class DeckBuilder
    {
        public static List<Card> BuildDeck13()
        {
            var ranks = new List<string>() { "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A", "2" };
            var suits = new List<string> { "Spades", "Clubs", "Diamonds", "Hearts" };
            var deck = new List<Card>(52);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    deck.Add(new Card(ranks[j], j, suits[i], i));
                }
            }
            return deck;
        }
    }
}
