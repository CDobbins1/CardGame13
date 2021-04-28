using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame13.Game.Tests
{
    [TestClass]
    public class DeckBuilderTests
    {
        List<Card> Deck { get; } = DeckBuilder.BuildDeck13();

        [TestMethod]
        public void BuildDeck13_DeckCountIs52_Success()
        {
            Assert.AreEqual(52, Deck.Count);
        }

        [TestMethod]
        public void BuildDeck13_FirstCardIs3OfSpades_Success()
        {
            var firstCard = Deck[0];

            Assert.AreEqual("3", firstCard.Rank);
            Assert.AreEqual(0, firstCard.RankValue);
            Assert.AreEqual("Spades", firstCard.Suit);
            Assert.AreEqual(0, firstCard.SuitValue);
        }

        [TestMethod]
        public void BuildDeck13_LastCardIs2OfHearts_Success()
        {
            var lastCard = Deck[51];

            Assert.AreEqual("2", lastCard.Rank);
            Assert.AreEqual(12, lastCard.RankValue);
            Assert.AreEqual("Hearts", lastCard.Suit);
            Assert.AreEqual(3, lastCard.SuitValue);
        }
    }
}
