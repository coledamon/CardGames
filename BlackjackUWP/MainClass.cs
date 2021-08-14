using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    class MainClass
    {
        public static void Start(string[] args)
        {
            
            Console.WriteLine("Homework 2 Output");

            // instantiate some cards
            Card c1 = new Card(Suit.HEARTS, Rank.TWO);
            Card c2 = new Card(Suit.HEARTS, Rank.TWO);
            Card c3 = new Card(Suit.CLUBS, Rank.FOUR);
            Card c4 = new Card(Suit.CLUBS, Rank.JACK);
            Card c5 = new Card(Suit.DIAMONDS, Rank.ACE);

            Card c6 = new Card(Suit.HEARTS, Rank.QUEEN);

            // testing ToString functionality
            Console.WriteLine(c1.GetRank());
            Console.WriteLine(c1.GetSuit());
            Console.WriteLine(c1);

            // printing all ranks
            foreach (Rank r in Rank.VALUES)
                Console.WriteLine(r.GetSymbol() + " " + r.GetName() + " " + r);
            Console.WriteLine("Number of ranks: " + Rank.VALUES.Count);

            // printing all suits
            foreach (Suit s in Suit.VALUES)
                Console.WriteLine(s.GetSymbol());
            Console.WriteLine("Number of suits: " + Suit.VALUES.Count);

            // testing CompareTo functionality
            Console.WriteLine("Comparing {0} and {1}: ---> {2}", c1, c2, c1.CompareTo(c2));
            Console.WriteLine("Comparing {0} and {1}: ---> {2}", c1, c3, c1.CompareTo(c3));
            Console.WriteLine("Comparing {0} and {1}: ---> {2}", c3, c1, c3.CompareTo(c1));
            Console.WriteLine("Comparing {0} and {1}: ---> {2}", c1, c4, c1.CompareTo(c4));
            Console.WriteLine("Comparing {0} and {1}: ---> {2}", c4, c5, c4.CompareTo(c5));

            // play a CardCountGame
            CardCountGame.PlayGame();
            BlackJackGame.PlayGame();
        }
    }
}
