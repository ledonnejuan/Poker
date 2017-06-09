using Don.Poker.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Don.Poker.Engine
{
    public class Deck
    {
        #region Fields
        private List<Card> _deck; 
        #endregion

        #region Ctor
        public Deck()
        {
            var deck = new List<Card>();
            for (var suit = 0; suit < 4; suit++)
            {
                for (var i = 2; i <= 14; i++)
                {
                    deck.Add(new Card((Suit)suit, (Face)i));
                }
            }

            _deck = deck;

        } 
        #endregion

        #region Properties
        public List<Card> DeckOfCards { get { return _deck; } }
        #endregion

        #region Public Methods
        /// <summary>
        /// Simple Shuffling of decks
        /// </summary>
        public void Shuffle()
        {
            //used simple shuffling
            _deck = _deck.OrderBy(c => Guid.NewGuid()).ToList();
        }

        /// <summary>
        /// Deal one card from the top of the deck
        /// </summary>
        /// <returns></returns>
        public Card Deal()
        {
            var card = _deck[0];
            _deck.RemoveAt(0);
            return card;
        } 
        #endregion

    }
}
