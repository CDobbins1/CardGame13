using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame13.Game.Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("")]
        [DataRow(null)]
        public void CreateCard_UsingInvalidRank_ThrowsArgumentException(string testValue)
        {
            _ = new Card(testValue, 0, "Clubs", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-1)]
        [DataRow(13)]
        public void CreateCard_UsingInvalidRankValue_ThrowsArgumentOutOfRangeException(int testValue)
        {
            _ = new Card("Three", testValue, "Clubs", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("")]
        [DataRow(null)]
        public void CreateCard_UsingInvalidSuit_ThrowsArgumentException(string testValue)
        {
            _ = new Card("Three", 0, testValue, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-1)]
        [DataRow(4)]
        public void CreateCard_UsingInvalidSuitValue_ThrowsArgumentOutOfRangeException(int testValue)
        {
            _ = new Card("Three", 0, "Clubs", testValue);
        }

        [TestMethod]
        public void CreateCard_UsingValidArguments_Success()
        {
            _ = new Card("Three", 0, "Clubs", 0);
        }
    }
}
