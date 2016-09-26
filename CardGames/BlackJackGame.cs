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
                //Console.WriteLine(card);
            }

            deck.Shuffle();

            Console.WriteLine("Welcome to Blackjack!");


        }
    }
}
