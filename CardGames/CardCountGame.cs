using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    class CardCountGame
    {
        public static void Main(string[] args)
        {
            CardCountHand h1 = new CardCountHand();
            CardCountHand h2 = new CardCountHand();

            List<Card> fullDeck = new List<Card>();

            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 12; j++)
                {
                    Card cards = new Card(Suit.VALUES[i], Rank.VALUES[j]);
                    fullDeck.Add(cards);
                }
            }

            Deck deck = new Deck();

            foreach (Card card in fullDeck)
            {
                deck.AddCard(card);
                Console.WriteLine(card);
            }

            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Cards before shuffle " + deck.GetCardsRemaining());

            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");

            deck.Shuffle();

            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Cards after shuffle " + deck.GetCardsRemaining());

            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");

            for (int i = 0; i < 8; i++)
            {
                h1.AddCard(deck.DealOne());
                //Console.WriteLine(deck.GetCardsRemaining());
                Console.WriteLine(h1.GetCardAtIndex(i));

            }

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");

            for (int i = 0; i < 8; i++)
            {
                h2.AddCard(deck.DealOne());
                //Console.WriteLine(deck.GetCardsRemaining());
                Console.WriteLine(h2.GetCardAtIndex(i));
            }

            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("Cards after dealing " + deck.GetCardsRemaining());

            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");


            Console.WriteLine("Player 1\tPlayer2");

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("{0}\t\t{1}", h1.GetCardAtIndex(i), h2.GetCardAtIndex(i));
            }
            Console.WriteLine("");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\tTOTAL");
            Console.WriteLine("{0}\t\t{1}", h1.EvaluateHand(), h2.EvaluateHand());
            Console.WriteLine("");
            Console.WriteLine("There are {0} cards left in the deck", deck.GetCardsRemaining());
        }
    }
}
