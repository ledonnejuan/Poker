using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Don.Poker.Engine
{
    public class PokerGame
    {
        #region Fields
        private Deck _pokerGameDeck;
        private List<Player> _players; 
        #endregion

        #region Properties
        public List<Player> Players { get { return _players; } }
        public bool GameStarted { get; protected set; } 
        #endregion

        #region Public Methods

        /// <summary>
        /// Register Player
        /// </summary>
        /// <param name="player">Don.Poker.Engine.Player</param>
        public void RegisterPlayer(Player player)
        {
            if (_players == null)
                _players = new List<Player>();
            if (_players.Count > 9)
                throw new Exception("Table is full");


            _players.Add(player);
        }

        /// <summary>
        /// Start poker game
        /// </summary>
        public virtual void StartGame()
        {
            if (GameStarted)
                throw new Exception("Game is in progress");

            if (_players == null)
                throw new Exception("Table is empty");

            // create fresh deck of cards
            _pokerGameDeck = new Deck();
            
            //shuffle cards
            _pokerGameDeck.Shuffle();

            // distribute 2 cards each to players
            DealCardsToPlayers();

            // tag game as started
            GameStarted = true;

        }

        /// <summary>
        /// Flop: draw first 3 cards on the table
        /// </summary>
        public void Flop()
        {
            if (!GameStarted)
                throw new Exception("Game not yet started");

            for (var i = 0; i < 3; i++)
            {
                var card = _pokerGameDeck.Deal();
                foreach (var player in _players)
                {
                    player.AddCardToHand(card);
                }
            }
        }

        /// <summary>
        /// Turn: draw 4th card on the table
        /// </summary>
        public void Turn()
        {
            var card = _pokerGameDeck.Deal();
            foreach (var player in _players)
            {
                player.AddCardToHand(card);
            }
        }

        /// <summary>
        /// River: draw 5th card on the table
        /// </summary>
        public void River()
        {
            var card = _pokerGameDeck.Deal();
            foreach (var player in _players)
            {
                player.AddCardToHand(card);
            }
        }

        /// <summary>
        /// Check players' hands
        /// </summary>
        public void CheckPlayersHand()
        {
            if (!GameStarted)
                throw new Exception("Game not yet started");

            foreach (var player in _players)
            {
                player.Hand.EvaluateHand();
            }
        }

        /// <summary>
        /// Get Winners
        /// </summary>
        /// <returns></returns>
        public List<Player> ShowWinner()
        {
            if (!GameStarted)
                throw new Exception("Game not yet started");

            // tag game as not started
            GameStarted = false;

            // determin the highest card combination
            var highestCombination = _players.GroupBy(c => c.Hand.PokerHandName).OrderByDescending(c => c.Key).FirstOrDefault();

            // get players with the highest card combinations
            var initialWinners = _players.Where(c => c.Hand.PokerHandName == highestCombination.Key).ToList();
            if (initialWinners.Count == 1)
                return initialWinners;
            else // if more than one player has the same card combination
            {
                // determin the highest card
                var highestScore = initialWinners.GroupBy(c => c.Hand.HighCardScore).OrderByDescending(c => c.Key).FirstOrDefault();
                
                // get players with the highest card in combination
                var winners = initialWinners.Where(c => c.Hand.HighCardScore == highestScore.Key).ToList();

                if (winners.Count == 1)
                {
                    return winners;
                }
                else // if more than one player has the same card combination and highest card
                {
                    // determin highest kicker card
                    var kickerScore = winners.GroupBy(c => c.Hand.KickerScore).OrderByDescending(c => c.Key).FirstOrDefault();
                    
                    // get players with the highest card combination, highest card, highest kicker
                    var final = winners.Where(c => c.Hand.KickerScore == kickerScore.Key).ToList();
                    return final;
                }
            }
        }

        #endregion

        #region Private Methods

        private void DealCardsToPlayers()
        {
            foreach (var player in _players)
            {
                player.ClearHand();
            }
            for (var i = 0; i < 2; i++)
            {
                foreach (var player in _players)
                {
                    var card = _pokerGameDeck.Deal();
                    player.AddCardToHand(card);
                }
            }

        }

        #endregion
    }
}
