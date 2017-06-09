using Don.Poker.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Don.Poker.Engine.Rules
{
    public abstract class Rule : IRule
    {

        public PokerHands PokerHand { get; set; }

        public abstract void EvaluateHand(Player player);

        #region Helpers
        // get atleast 5 cards with the same suit
        protected List<Card> GetFlush(List<Card> cards)
        {
            var suitCount = cards.GroupBy(c => c.Suit).Where(c => c.Count() >= 5).ToList();

            if (suitCount.Count == 1)
            {
                cards = cards.Where(c => c.Suit == suitCount[0].Key).ToList();

                return cards;
            }
            return null;
        }

        // Get 5 sequential cards
        protected List<Card> GetStraight(List<Card> cards)
        {
            if (cards == null)
                return null;

            cards = cards.OrderBy(c => c.Face).ToList();

            var seq_num = 1;

            if (cards.Last().Face == Engine.Infrastructure.Face.Ace)
                cards.Insert(0, new Card(cards.Last().Suit, Engine.Infrastructure.Face.Low_Ace));

            for (var i = 1; i < cards.Count; i++)
            {
                if ((int)cards[i - 1].Face + 1 == (int)cards[i].Face)
                    seq_num++;
                else
                {
                    if (seq_num >= 5)
                    {
                        cards = cards.Take(seq_num).ToList();
                        break;
                    }
                    cards.RemoveAt(i - 1);
                    seq_num = 1;
                    i--;
                }
            }

            var hasStraight = seq_num >= 5;

            if (hasStraight)
            {
                return cards.OrderByDescending(c => c.Face).Take(5).ToList();
            }
            return null;
        }

        // Get paring cards
        protected List<Card> GetPairs(List<Card> cards)
        {
            var _pairs = cards.GroupBy(c => c.Face).Where(c => c.Count() == 2).OrderByDescending(c => c.Key).Take(2).Select(c => c.Key);

            var _cards = cards.Where(c => _pairs.Contains(c.Face)).ToList();

            return _cards;
        }

        // Get three cards with same face
        protected List<Card> GetThreeOfAKind(List<Card> cards)
        {
            var _three = cards.GroupBy(c => c.Face).Where(c => c.Count() == 3).OrderByDescending(c => c.Key).Take(2).Select(c => c.Key);

            var _cards = cards.Where(c => _three.Contains(c.Face)).ToList();

            return _cards;
        }

        // Get cards score
        protected int GetPokerHandScore(List<Card> cards)
        {
            var values = new Dictionary<Face, int>();
            values.Add(Face.Low_Ace, 1);
            values.Add(Face.Two, 2);
            values.Add(Face.Three, 4);
            values.Add(Face.Four, 8);
            values.Add(Face.Five, 16);
            values.Add(Face.Six, 32);
            values.Add(Face.Seven, 64);
            values.Add(Face.Eight, 128);
            values.Add(Face.Nine, 256);
            values.Add(Face.Ten, 512);
            values.Add(Face.Jack, 1024);
            values.Add(Face.Queen, 2048);
            values.Add(Face.King, 4096);
            values.Add(Face.Ace, 8192);

            var score = 0;

            foreach (var card in cards)
            {
                score += values[card.Face];
            }

            return score;
        }
        #endregion 

    }
}