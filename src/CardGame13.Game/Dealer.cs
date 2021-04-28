using System;
using System.Collections.Generic;

namespace CardGame13.Game
{
    public class Dealer
    {
        public List<Card> Deck { get; }

        public Dealer(List<Card> deck)
        {
            Deck = deck;
        }

        public void ShuffleDeck()
        {
            var rand = new Random();
            int max = Deck.Count - 1;

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < Deck.Count; j++)
                {
                    int index = rand.Next(max);
                    (Deck[j], Deck[index]) = (Deck[index], Deck[j]);
                }
            }
        }

        public List<Card> DealHand(int playerIndex)
        {
            List<Card> hand = new List<Card>();
            for (int i = 0; i < 13; i++)
            {
                hand.Add(Deck[i + playerIndex * 13]);
            }
            hand.Sort((a, b) =>
                (a.RankValue < b.RankValue || (a.RankValue == b.RankValue && a.SuitValue < b.SuitValue)) ? -1 : 1);
            return hand;
        }
    }
}
