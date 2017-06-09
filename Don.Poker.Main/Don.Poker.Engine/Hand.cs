using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Don.Poker.Engine.Infrastructure;

namespace Don.Poker.Engine
{
    public class Hand
    {
        #region Properties
        public List<Card> Cards { get; private set; }
        public List<Card> PokerHand { get; private set; }
        public PokerHands PokerHandName { get; private set; }
        public int HighCardScore { get; private set; }
        public int KickerScore { get; private set; } 
        #endregion

        #region Public Methods
        public void AddCard(Card card)
        {
            if (Cards != null)
            {
                if (Cards.Count == 7)
                {
                    throw new Exception("Cannot add more card");
                }
                if (Cards.Exists(c => c.Face == card.Face && c.Suit == card.Suit))
                {
                    throw new Exception("Duplicate Card");
                }
                Cards.Add(card);
            }
            else
            {
                Cards = new List<Card>();
                Cards.Add(card);
            }
        }

        public void EvaluateHand()
        {
            if (HasRoyalFlush())
            {
                PokerHandName = PokerHands.RoyalFlush;
            }
            else if (HasStraightFlush())
            {
                PokerHandName = PokerHands.StraightFlush;
            }
            else if (HasFourOfAKind())
            {
                PokerHandName = PokerHands.FourOfAKind;
            }
            else if (HasFullHouse())
            {
                PokerHandName = PokerHands.FullHouse;
            }
            else if (HasFlush())
            {
                PokerHandName = PokerHands.Flush;
            }
            else if (HasStraight())
            {
                PokerHandName = PokerHands.Straight;
            }
            else if (HasThreeOfAKind())
            {
                PokerHandName = PokerHands.ThreeOfAKind;
            }
            else if (HasTwoPairs())
            {
                PokerHandName = PokerHands.TwoPair;
            }
            else if (HasSinglePair())
            {
                PokerHandName = PokerHands.Pair;
            }
            else
            {
                PokerHandName = PokerHands.HighCard;
                PokerHand = this.Cards.OrderByDescending(c => c.Face).Take(5).ToList();
                HighCardScore = GetPokerHandScore(PokerHand);
                KickerScore = 0;
            }
        }

        public override string ToString()
        {
            return string.Join(" ", PokerHand.OrderBy(c => c.Face).Select(c => "[" + c.Face + "-" + c.Suit + "]"));
        } 
        #endregion

        #region Card Evaluation Rules
        #region Royal Flush/Straight Flush/Flush/Straight
        private bool HasRoyalFlush()
        {
            var fCards = GetFlush();
            var sCards = GetStraight(fCards);

            if (sCards != null && sCards.First().Face == Face.Ace)
            {
                PokerHand = sCards;
                HighCardScore = GetPokerHandScore(sCards.Take(1).ToList());
                KickerScore = 0;
                return true;

            }
            return false;
        }

        private bool HasStraightFlush()
        {
            var fCards = GetFlush();
            var sCards = GetStraight(fCards);

            if (sCards != null)
            {
                PokerHand = sCards;
                HighCardScore = GetPokerHandScore(sCards.Take(1).ToList());
                KickerScore = 0;
                return true;
            }
            return false;
        }

        private bool HasFlush()
        {
            var cards = GetFlush();
            if (cards != null && cards.Count >= 5)
            {
                cards = cards.OrderByDescending(c => c.Face).Take(5).ToList();

                PokerHand = cards;
                HighCardScore = GetPokerHandScore(cards);
                KickerScore = 0;
                return true;
            }
            return false;
        }

        private bool HasStraight()
        {
            var cards = GetStraight(this.Cards);

            if (cards != null)
            {
                PokerHand = cards;
                HighCardScore = GetPokerHandScore(cards.Take(1).ToList());
                KickerScore = 0;
                return true;
            }
            return false;
        }

        #endregion

        #region Four of a Kind
        private bool HasFourOfAKind()
        {
            var tmpCards = this.Cards;
            var _foakNominee = tmpCards.GroupBy(c => c.Face).Where(c => c.Count() == 4).ToList();

            if (_foakNominee.Count > 0)
            {
                var cards = tmpCards.Where(c => c.Face == _foakNominee[0].Key).ToList();

                var kicker = tmpCards.Where(c => !cards.Contains(c)).OrderByDescending(c => c.Face).FirstOrDefault();

                HighCardScore = GetPokerHandScore(cards.Take(1).ToList());
                KickerScore = GetPokerHandScore(new List<Card>() { kicker });

                cards.Add(kicker);

                PokerHand = cards;

                return true;
            }
            return false;
        }
        #endregion

        #region Pair/Two Pair/Three of A Kind/Full House

        private bool HasSinglePair()
        {
            var pairs = GetPairs(); // get all pairs
            if (pairs.Count == 2) // only single pair in hand
            {
                // get highest card as kicker
                var cards = this.Cards.Where(c => !pairs.Contains(c)).OrderByDescending(c => c.Face).Take(3).ToList();
                

                HighCardScore = GetPokerHandScore(pairs.Take(1).ToList());
                KickerScore = GetPokerHandScore(cards);

                cards.AddRange(pairs);

                PokerHand = cards;
                return true;
            }
            return false;
        }

        private bool HasTwoPairs()
        {
            var pairs = GetPairs(); // get all pairs
            if (pairs.Count == 4)
            {
                // get kicker
                var cards = this.Cards.Where(c => !pairs.Contains(c)).OrderByDescending(c => c.Face).Take(1).ToList();

                HighCardScore = GetPokerHandScore(pairs);
                KickerScore = GetPokerHandScore(cards);

                cards.AddRange(pairs);

                PokerHand = cards;
                return true;
            }

            return false;
        }

        private bool HasThreeOfAKind()
        {
            var three = GetThreeOfAKind(this.Cards);
            if (three != null && three.Count == 3)
            {
                var cards = this.Cards.Where(c => !three.Contains(c)).OrderByDescending(c => c.Face).Take(2).ToList();

                HighCardScore = GetPokerHandScore(three.Take(1).ToList());
                KickerScore = GetPokerHandScore(cards.ToList());

                cards.AddRange(three);

                PokerHand = cards;
                return true;
            }
            return false;
        }

        private bool HasFullHouse()
        {
            var three = GetThreeOfAKind(this.Cards);
            var pairs = GetPairs();
            if (three.Count > 0)
            {
                if (three.Count > 3 || pairs.Count != 0)
                {
                    pairs.AddRange(three.OrderBy(c => c.Face).Take(2).ToList());
                    pairs = pairs.OrderByDescending(c => c.Face).Take(2).ToList();

                    three = three.Take(3).ToList();

                    HighCardScore = GetPokerHandScore(three.Take(1).ToList());
                    KickerScore = GetPokerHandScore(pairs.Take(1).ToList());

                    three.AddRange(pairs);

                    PokerHand = three;
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Helpers
        // get atleast 5 cards with the same suit
        private List<Card> GetFlush()
        {
            var _cards = this.Cards;
            var suitCount = _cards.GroupBy(c => c.Suit).Where(c => c.Count() >= 5).ToList();

            if (suitCount.Count == 1)
            {
                _cards = _cards.Where(c => c.Suit == suitCount[0].Key).ToList();

                return _cards;
            }
            return null;
        }

        // Get 5 sequential cards
        private List<Card> GetStraight(List<Card> _cards)
        {
            if (_cards == null)
                return null;

            _cards = _cards.OrderBy(c => c.Face).ToList();

            var seq_num = 1;

            if (_cards.Last().Face == Engine.Infrastructure.Face.Ace)
                _cards.Insert(0, new Card(_cards.Last().Suit, Engine.Infrastructure.Face.Low_Ace));

            for (var i = 1; i < _cards.Count; i++)
            {
                if ((int)_cards[i - 1].Face + 1 == (int)_cards[i].Face)
                    seq_num++;
                else
                {
                    if (seq_num >= 5)
                    {
                        _cards = _cards.Take(seq_num).ToList();
                        break;
                    }
                    _cards.RemoveAt(i - 1);
                    seq_num = 1;
                    i--;
                }
            }

            var hasStraight = seq_num >= 5;

            if (hasStraight)
            {
                return _cards.OrderByDescending(c => c.Face).Take(5).ToList();
            }
            return null;
        }

        // Get paring cards
        private List<Card> GetPairs()
        {
            var _pairs = this.Cards.GroupBy(c => c.Face).Where(c => c.Count() == 2).OrderByDescending(c => c.Key).Take(2).Select(c => c.Key);

            var _cards = this.Cards.Where(c => _pairs.Contains(c.Face)).ToList();

            return _cards;
        }

        // Get three cards with same face
        private List<Card> GetThreeOfAKind(List<Card> cards)
        {
            var _three = cards.GroupBy(c => c.Face).Where(c => c.Count() == 3).OrderByDescending(c => c.Key).Take(2).Select(c => c.Key);

            var _cards = cards.Where(c => _three.Contains(c.Face)).ToList();

            return _cards;
        }

        // Get cards score
        private int GetPokerHandScore(List<Card> cards)
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
        #endregion
    }
}
