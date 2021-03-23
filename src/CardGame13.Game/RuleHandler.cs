using System.Collections.Generic;

namespace CardGame13.Game
{
    public class RuleHandler
    {
        public enum Category
        {
            First, Free, Single, Double, Triple, Straight, StraightFlush, Bomb, Invalid
        }

        public Category CurrentCategory { get; private set; }

        public RuleHandler()
        {
            CurrentCategory = Category.First;
        }

        public bool IsValidPlay(List<Card> cards, List<Card>? pile)
        {
            Category category = FindCategory(cards);

            if (category == Category.Invalid) return false;
            if (CurrentCategory == Category.First && !Has3OfSpades(cards)) return false;

            if (CurrentCategory == Category.First || CurrentCategory == Category.Free) return SetCategory(category);

            if (category == Category.Bomb) return SetCategory(category);

            if (category == Category.Triple && CurrentCategory == Category.Single && pile![0].RankValue == 11) return SetCategory(category);

            if ((HigherRank(cards, pile!) || (EqualRank(cards, pile!) && HigherSuit(cards, pile!))) && cards.Count == pile!.Count)
            {
                if (category == Category.StraightFlush && CurrentCategory == Category.Straight) return SetCategory(category);
                if (CurrentCategory == category) return true;
            }
            return false;
        }

        private bool SetCategory(Category category)
        {
            CurrentCategory = category;
            return true;
        }

        private bool EqualRank(List<Card> cards, List<Card> pile) => cards[^1].RankValue == pile[^1].RankValue;

        private bool HigherRank(List<Card> cards, List<Card> pile) => cards[^1].RankValue > pile[^1].RankValue;

        private bool HigherSuit(List<Card> cards, List<Card> pile) => (pile[^1].RankValue < 8) || cards[^1].SuitValue > pile[^1].SuitValue;

        public Category FindCategory(List<Card> cards)
        {
            if (cards.Count == 1) return Category.Single;
            if (cards.Count == 2 && HasMatchingRank(cards)) return Category.Double;
            if (cards.Count == 3 && HasMatchingRank(cards)) return Category.Triple;
            if (cards.Count >= 3 && IsConsecutive(cards)) return HasMatchingSuit(cards) ? Category.StraightFlush : Category.Straight;
            if (IsBomb(cards)) return Category.Bomb;

            return Category.Invalid;
        }

        private bool IsBomb(List<Card> cards)
        {
            if (cards.Count == 4 && HasMatchingRank(cards)) return true;
            if (cards.Count == 6)
            {
                var firstPair = new List<Card> { cards[0], cards[1] };
                var secondPair = new List<Card> { cards[2], cards[3] };
                var thirdPair = new List<Card> { cards[4], cards[5] };
                var consecutive = new List<Card> { cards[0], cards[2], cards[4] };

                if (HasMatchingRank(firstPair) && HasMatchingRank(secondPair) && HasMatchingRank(thirdPair) && IsConsecutive(consecutive)) return true;
            }

            return false;
        }

        private bool HasMatchingRank(List<Card> cards)
        {
            for (int i = 1; i < cards.Count; i++)
            {
                if (cards[0].RankValue != cards[i].RankValue) return false;
            }
            return true;
        }

        private bool HasMatchingSuit(List<Card> cards)
        {
            for (int i = 1; i < cards.Count; i++)
            {
                if (cards[0].SuitValue != cards[i].SuitValue) return false;
            }
            return true;
        }

        private bool IsConsecutive(List<Card> cards)
        {
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].RankValue != (cards[i + 1].RankValue - 1)) return false;
            }
            return cards[^1].RankValue != 12;
        }

        public bool Has3OfSpades(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                if (card.RankValue == 0 && card.SuitValue == 0) return true;
            }
            return false;
        }

        public void FreeTurn() => CurrentCategory = Category.Free;
    }
}
