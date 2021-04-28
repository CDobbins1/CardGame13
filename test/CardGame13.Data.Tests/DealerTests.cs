using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CardGame13.Game.Tests
{
    [TestClass]
    public class DealerTests
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void DealHand_DealsCorrect13Cards_Success(int playerNumber)
        {
            var deck = DeckBuilder.BuildDeck13();
            var dealer = new Dealer(deck);

            var hand = dealer.DealHand(playerNumber);

            Assert.AreEqual(13, hand.Count);
            for (int i = 0; i < hand.Count; i++)
            {
                Assert.AreEqual(deck[i + (playerNumber * 13)], hand[i]);
            }
        }

        [TestMethod]
        public void ShuffleDeck_DoesNotCreateDuplicates_Success()
        {
            var dealer = new Dealer(DeckBuilder.BuildDeck13());

            dealer.ShuffleDeck();

            Assert.AreEqual(52, dealer.Deck.Distinct().ToList().Count);
        }
    }
}
