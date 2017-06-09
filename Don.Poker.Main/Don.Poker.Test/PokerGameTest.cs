using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Don.Poker.Engine;

namespace Don.Poker.Test
{
    [TestClass]
    public class PokerGameTest
    {
        [TestMethod]
        public void PokerGame_RandomScenarioTest()
        {

            var poker = new PokerGame();
            poker.RegisterPlayer(new Player(1, "Don"));
            poker.RegisterPlayer(new Player(2, "Juan"));

            poker.StartGame();
            poker.Flop();
            poker.Turn();
            poker.River();
            poker.CheckPlayersHand();
            var winner = poker.ShowWinner();

            Assert.IsTrue(winner.Count > 0);
        }

        [TestMethod]
        public void PokerGame_Scenario1Test()
        {
            var poker = new PokerFixedGame();

            var player1 = new Player(1, "Joe");
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Three));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Six));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Eight));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Jack));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.King));

            var player2 = new Player(2, "Jen");
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Three));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Three));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Three));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Eight));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Ten));

            var player3 = new Player(3, "Bob");
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Two));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Five));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Seven));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Ten));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Ace));

            poker.RegisterPlayer(player1);
            poker.RegisterPlayer(player2);
            poker.RegisterPlayer(player3);
            poker.StartGame();
            poker.CheckPlayersHand();
            var winner = poker.ShowWinner();
        }

        [TestMethod]
        public void PokerGame_Scenario2Test()
        {
            var poker = new PokerFixedGame();

            var player1 = new Player(1, "Joe");
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Three));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Four));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Nine));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Nine));
            player1.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Queen));

            var player2 = new Player(2, "Jen");
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Five));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Diamond, Engine.Infrastructure.Face.Seven));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Nine));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Nine));
            player2.AddCardToHand(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Queen));

            var player3 = new Player(3, "Bob");
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Two));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Two));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Spade, Engine.Infrastructure.Face.Five));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Club, Engine.Infrastructure.Face.Ten));
            player3.AddCardToHand(new Card(Engine.Infrastructure.Suit.Heart, Engine.Infrastructure.Face.Ace));

            poker.RegisterPlayer(player1);
            poker.RegisterPlayer(player2);
            poker.RegisterPlayer(player3);
            poker.CheckPlayersHand();
            var winner = poker.ShowWinner();
        }
    }
}
