using Don.Poker.Engine;
using Don.Poker.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Don.Poker.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Welcome to Poker Game");

                Console.WriteLine("Press 1 to test Scenario");
                Console.WriteLine("Press 2 to Simulate Play");
                Console.WriteLine("Press any to exit");
                var play = Console.ReadLine();

                if (play == "1")
                {
                    var testScenario = new TestScenario();
                    testScenario.Start();
                }
                else if (play == "2")
                {
                    var simulatePlay = new SimulatePlay();
                    simulatePlay.Start();
                }
                else
                {
                    break;
                }
            }
        }


    }

    public class SimulatePlay
    {
        public void Start()
        {
            Console.WriteLine();
            var poker = new PokerGame();
            poker.RegisterPlayer(new Player(1, "Joe"));
            poker.RegisterPlayer(new Player(2, "Jen"));
            poker.RegisterPlayer(new Player(3, "Bob"));
            poker.StartGame();
            poker.Flop();
            poker.Turn();
            poker.River();
            poker.CheckPlayersHand();
            var winner = poker.ShowWinner();

            Console.WriteLine();
            Console.WriteLine("===========Showing Hands==============");

            foreach(var player in poker.Players)
            {
                Console.WriteLine("{0} ==> {1}", player.Name.ToUpper(), player.Hand.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("==========WINNER/s===============");
            foreach(var w in winner)
            {
                Console.WriteLine("{0} ==> {1}", w.Name.ToUpper(), w.Hand.PokerHandName);
            }

            Console.ReadLine();

        }
    }

    public class TestScenario
    {
        public void Start()
        {
            Console.WriteLine();
            var poker = new PokerFixedGame();

            var player1 = new Player(1, "Joe");
            var player2 = new Player(2, "Jen");
            var player3 = new Player(3, "Bob");

            Console.WriteLine("Card Value [2,3,4,5,6,7,8,10,J,Q,K,A");
            Console.WriteLine("Card Suit [C,S,H,D]");
            Console.WriteLine("Set Player's Card. (e.g: 2S,3C,4H,7D,JH)");


            Console.WriteLine("Set Joe's Card. Enter 5 cards...");
            var joeCard = Console.ReadLine();
            var jc = joeCard.Split(',');
            for (var i = 0; i < 5; i++)
            {
                player1.AddCardToHand(GetCard(jc[i]));
            }

            Console.WriteLine();
            Console.WriteLine("Set Jen's Card. Enter 5 cards...");
            var jenCard = Console.ReadLine();
            var jenc = jenCard.Split(',');
            for (var i = 0; i < 5; i++)
            {
                player2.AddCardToHand(GetCard(jenc[i]));
            }

            Console.WriteLine();
            Console.WriteLine("Set Bob's Card. Enter 5 cards...");
            var bobCard = Console.ReadLine();
            var bc = bobCard.Split(',');
            for (var i = 0; i < 5; i++)
            {
                player3.AddCardToHand(GetCard(bc[i]));
            }

            poker.RegisterPlayer(player1);
            poker.RegisterPlayer(player2);
            poker.RegisterPlayer(player3);

            poker.StartGame();
            poker.CheckPlayersHand();
            var winner = poker.ShowWinner();

            Console.WriteLine();
            Console.WriteLine("===========Showing Hands==============");

            foreach (var player in poker.Players)
            {
                Console.WriteLine("{0} ==> {1}", player.Name.ToUpper(), player.Hand.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("==========WINNER/s===============");
            foreach (var w in winner)
            {
                Console.WriteLine("{0} ==> {1}", w.Name.ToUpper(), w.Hand.PokerHandName);
            }

            Console.ReadLine();
        }

        private Card GetCard(string input)
        {
            var initial = input.Substring(0, 1);
            var suitInitial = input.Substring(1, 1);
            if(input.Length == 3)
            {
                initial = input.Substring(0, 2);
                suitInitial = input.Substring(2, 1);
            }
            if (initial == "J") initial = "11";
            if (initial == "Q") initial = "12";
            if (initial == "K") initial = "13";
            if (initial == "A") initial = "14";

            var face = (Face)int.Parse(initial);
            
            var suit = new Suit();

            switch (suitInitial)
            {
                case "C":
                    suit = Suit.Club;
                    break;
                case "S":
                    suit = Suit.Spade;
                    break;
                case "H":
                    suit = Suit.Heart;
                    break;
                case "D":
                    suit = Suit.Diamond;
                    break;
            }

            return new Card(suit, face);

        }
    }

}
