using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CardGame13.Game.Tests
{
    [TestClass]
    public class RuleHandlerTests
    {
        [TestMethod]
        public void Has3OfSpades_Using3OfSpades_ReturnsTrue()
        {
            var sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0)
            };

            bool result = sut.Has3OfSpades(cards);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Has3OfSpades_UsingInvalidPlay_ReturnsFalse()
        {
            var sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Diamonds", 2)
            };

            bool result = sut.Has3OfSpades(cards);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FindCategory_UsingSingle_ReturnsSingle()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Single, result);
        }

        [TestMethod]
        public void FindCategory_UsingDouble_ReturnsDouble()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Double, result);
        }

        [TestMethod]
        public void FindCategory_UsingInvalidDouble_ReturnsInvalid()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Invalid, result);
        }

        [TestMethod]
        public void FindCategory_UsingTriple_ReturnsTriple()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1),
                new Card("3", 0, "Diamonds", 2)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Triple, result);
        }

        [TestMethod]
        public void FindCategory_UsingInvalidTriple_ReturnsInvalid()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1),
                new Card("4", 1, "Diamonds", 2)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Invalid, result);
        }

        [TestMethod]
        public void FindCategory_UsingStraight_ReturnsStraight()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Diamonds", 2)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Straight, result);
        }

        [TestMethod]
        public void FindCategory_UsingSevenCardStraight_ReturnsStraight()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Diamonds", 2),
                new Card("6", 3, "Diamonds", 2),
                new Card("7", 4, "Diamonds", 2),
                new Card("8", 5, "Diamonds", 2),
                new Card("9", 6, "Diamonds", 2)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Straight, result);
        }

        [TestMethod]
        public void FindCategory_UsingInvalidStraight_ReturnsInvalid()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Diamonds", 2),
                new Card("6", 3, "Diamonds", 2),
                new Card("7", 4, "Diamonds", 2),
                new Card("8", 5, "Diamonds", 2),
                new Card("10", 7, "Diamonds", 2)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Invalid, result);
        }

        [TestMethod]
        public void FindCategory_UsingStraightFlush_ReturnsStraightFlush()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Spades", 0),
                new Card("5", 2, "Spades", 0)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.StraightFlush, result);
        }

        [TestMethod]
        public void FindCategory_Using4OfAKind_ReturnsBomb()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1),
                new Card("3", 0, "Diamonds", 2),
                new Card("3", 0, "Hearts", 3)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Bomb, result);
        }

        [TestMethod]
        public void FindCategory_Using3ConsecutivePair_ReturnsBomb()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1),
                new Card("4", 1, "Diamonds", 2),
                new Card("4", 1, "Hearts", 3),
                new Card("5", 2, "Diamonds", 2),
                new Card("5", 2, "Hearts", 3)
            };

            RuleHandler.Category result = sut.FindCategory(cards);

            Assert.AreEqual(RuleHandler.Category.Bomb, result);
        }

        [TestMethod]
        public void IsValidPlay_UsingInvalidPlay_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Spades", 0)
            };

            bool result = sut.IsValidPlay(cards, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingInvalidValidSingleOnFirstPlay_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("4", 1, "Spades", 0)
            };

            bool result = sut.IsValidPlay(cards, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingValidSingleOnFirstPlay_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0)
            };

            bool result = sut.IsValidPlay(cards, null);

            Assert.IsTrue(result);
            Assert.AreEqual(RuleHandler.Category.Single, sut.CurrentCategory);
        }

        [TestMethod]
        public void IsValidPlay_Using4OfAKindBomb_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1),
                new Card("3", 0, "Diamonds", 2),
                new Card("3", 0, "Hearts", 3)
            };

            bool result = sut.IsValidPlay(cards, null);

            Assert.IsTrue(result);
            Assert.AreEqual(RuleHandler.Category.Bomb, sut.CurrentCategory);
        }

        [TestMethod]
        public void IsValidPlay_Using3ConsecutivePairBomb_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1),
                new Card("4", 1, "Diamonds", 2),
                new Card("4", 1, "Hearts", 3),
                new Card("5", 2, "Diamonds", 2),
                new Card("5", 2, "Hearts", 3)
            };

            bool result = sut.IsValidPlay(cards, null);

            Assert.IsTrue(result);
            Assert.AreEqual(RuleHandler.Category.Bomb, sut.CurrentCategory);
        }

        [TestMethod]
        public void IsValidPlay_UsingLowerSingle_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var pile = new List<Card>
            {
                new Card("4", 1, "Spades", 0)
            };

            bool result = sut.IsValidPlay(cards, pile);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingEqualSingle_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("3", 0, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingHigherSingle_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Spades", 0)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingEqualSingleHigherSuit_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>();
            sut.FreeTurn();
            cards.Add(new Card("J", 8, "Spades", 0));
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("J", 8, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingEqualSingleLowerSuit_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>();
            sut.FreeTurn();
            cards.Add(new Card("J", 8, "Clubs", 1));
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("J", 8, "Spades", 0)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingLowerDouble_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1)
            };
            sut.IsValidPlay(cards, null);
            var pile = new List<Card>
            {
                new Card("4", 1, "Spades", 0),
                new Card("4", 1, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards, pile);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingHigherDouble_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("3", 0, "Clubs", 1)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Spades", 0),
                new Card("4", 1, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingLowerStraight_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var pile = new List<Card>
            {
                new Card("4", 1, "Spades", 0),
                new Card("5", 2, "Clubs", 1),
                new Card("6", 3, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards, pile);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingHigherStraight_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Spades", 0),
                new Card("5", 2, "Clubs", 1),
                new Card("6", 3, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingStraightFlush_Success()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Clubs", 1),
                new Card("6", 3, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsTrue(result);
            Assert.AreEqual(RuleHandler.Category.StraightFlush, sut.CurrentCategory);
        }

        [TestMethod]
        public void IsValidPlay_UsingDifferentLengthStraight_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Clubs", 1),
                new Card("6", 3, "Spades", 0),
                new Card("7", 4, "Spades", 0)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingDifferentLengthStraightFlushOnStraight_Fail()
        {
            RuleHandler sut = new RuleHandler();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0),
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Clubs", 1),
                new Card("5", 2, "Clubs", 1),
                new Card("6", 3, "Clubs", 1),
                new Card("7", 4, "Clubs", 1)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingTripleOnSingle_Fail()
        {
            RuleHandler sut = new RuleHandler();
            sut.FreeTurn();
            List<Card> cards = new List<Card>
            {
                new Card("3", 0, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Clubs", 1),
                new Card("4", 1, "Spades", 0),
                new Card("4", 1, "Diamonds", 2)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPlay_UsingTripleOnSingleAce_Success()
        {
            RuleHandler sut = new RuleHandler();
            sut.FreeTurn();
            List<Card> cards = new List<Card>
            {
                new Card("A", 11, "Spades", 0)
            };
            sut.IsValidPlay(cards, null);
            var cards1 = new List<Card>
            {
                new Card("4", 1, "Clubs", 1),
                new Card("4", 1, "Spades", 0),
                new Card("4", 1, "Diamonds", 2)
            };

            bool result = sut.IsValidPlay(cards1, cards);

            Assert.IsTrue(result);
        }
    }
}
