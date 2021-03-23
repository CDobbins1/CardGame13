using System;
using System.Collections.Generic;

namespace CardGame13.Game
{
    public class Dealer
    {
        private readonly Deck _Deck = new Deck();
        public Dealer()
        {
            InitializeDeck();
            ShuffleDeck();
        }

        private void InitializeDeck()
        {
            var deck = _Deck.Cards;
            var ranks = _Deck.Ranks;
            var suits = _Deck.Suits;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    deck[j + (i * 13)] = new Card(ranks[j], j, suits[i], i);
                }
            }
        }

        private void ShuffleDeck()
        {
            var rand = new Random();
            var deck = _Deck.Cards;
            int min = 0;
            int max = deck.Length - 1;

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < deck.Length; j++)
                {
                    int index = rand.Next(min, max);
                    var temp = deck[j];
                    deck[j] = deck[index];
                    deck[index] = temp;
                }
            }
        }

        private void Print()
        {
            var deck = _Deck.Cards;
            foreach (Card card in deck)
            {
                Console.WriteLine(card);
            }
        }

        public List<Card> DealHand(int playerIndex)
        {
            var deck = _Deck.Cards;
            List<Card> hand = new List<Card>();
            for (int i = 0; i < 13; i++)
            {
                hand.Add(deck[i + playerIndex * 13]);
            }
            hand.Sort((a, b) =>
                (a.RankValue < b.RankValue || (a.RankValue == b.RankValue && a.SuitValue < b.SuitValue)) ? -1 : 1);
            return hand;
        }
    }
}
