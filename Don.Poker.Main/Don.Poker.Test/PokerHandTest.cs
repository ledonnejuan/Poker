using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Don.Poker.Engine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Don.Poker.Test
{
    [TestClass]
    public class PokerHandTest
    {
        List<Card> cards;

        [TestInitialize]
        public void InitializeCards()
        {
            
        }

        [TestMethod]
        public void Deck_GetDeckOfCardsTest()
        {
            var deck = new Deck();
            Assert.IsNotNull(deck.DeckOfCards);
            Assert.IsTrue(deck.DeckOfCards.Count == 52);
        }

        [TestMethod]
        public void Deck_GetDeckAndShuffleTest()
        {
            var deck = new Deck();
            var deckOfCards1 = deck.DeckOfCards;
            deck.Shuffle();
            var deckOfCards2 = deck.DeckOfCards;

            //Randomly check card's location
            Assert.IsFalse(deckOfCards1[0] == deckOfCards2[0]);
            Assert.IsFalse(deckOfCards1[5] == deckOfCards2[5]);
            Assert.IsFalse(deckOfCards1[34] == deckOfCards2[34]);
        }

        [TestMethod]
        public void Hand_RoyalFlushTest()
        {
            Hand hand = new Hand();
            
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Ten));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Queen));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.King));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Ace));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.RoyalFlush);
            
        }

        [TestMethod]
        public void Hand_StraightFlushTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Ten));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Queen));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.King));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Nine));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.StraightFlush);
        }

        [TestMethod]
        public void Hand_FourOfAKindTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Ten));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Nine));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.FourOfAKind);
        }

        [TestMethod]
        public void Hand_FullHouseTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Nine));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.FullHouse);
        }

        [TestMethod]
        public void Hand_FlushTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Four));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Six));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Nine));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.King));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.Flush);
        }

        [TestMethod]
        public void Hand_StraightTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Three));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ten));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Queen));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.King));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.Straight);
        }

        [TestMethod]
        public void Hand_ThreeOfAKindTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Three));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ten));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Queen));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.King));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.ThreeOfAKind);
        }

        [TestMethod]
        public void Hand_TwoPairsTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Three));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ten));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Ten));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Queen));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.King));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.TwoPair);
        }

        [TestMethod]
        public void Hand_SinglePairTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Three));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Eight));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Queen));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.King));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.Pair);
        }

        [TestMethod]
        public void Hand_NoCombinationTest()
        {
            Hand hand = new Hand();

            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ace));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Two));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Three));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Eight));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Jack));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Queen));
            hand.AddCard(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.King));

            hand.EvaluateHand();

            Assert.IsTrue(hand.PokerHandName == Engine.Infrastructure.PokerHands.HighCard);
        }
    }
}
