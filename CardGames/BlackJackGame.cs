using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    class BlackJackGame
    {
        public static void PlayGame()
        {
            List<Card> multDeck = new List<Card>();

            BlackJackHand player = new BlackJackHand();
            BlackJackHand dealer = new BlackJackHand();

            for (int x = 0; x < 6; x++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j <= 12; j++)
                    {
                        Card cards = new Card(Suit.VALUES[i], Rank.VALUES[j]);
                        multDeck.Add(cards);
                    }
                }
            }

            Deck deck = new Deck();

            foreach (Card card in multDeck)
            {
                deck.AddCard(card);
            }

            deck.Shuffle();

            Console.WriteLine("Welcome to Blackjack!");

            string input = "";
            var score = 0;

            player.AddCard(deck.DealOne());
            player.AddCard(deck.DealOne());
            score = player.EvaluateHand();
            Console.WriteLine("You have been dealt the {0}", deck.DealOne());
            do
            {
                Console.WriteLine("You have been dealt the {0}", deck.DealOne());
                Console.WriteLine("Hand score: {0}", score);
                if (score > 21)
                {
                    Console.WriteLine("Bust!");
                    break;
                }
                Console.WriteLine("Hit or Stay (H/S)");
                input = Console.ReadLine();
                score = player.EvaluateHand();
               
            }
            while (input == "H" || input == "h" || input == "Hit" || input == "hit");

        }
    }
}
