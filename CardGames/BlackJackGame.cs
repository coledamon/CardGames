using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    class BlackJackGame
    {
        static BlackJackHand player;
        static BlackJackHand dealer;
        static Deck deck = new Deck();
        public static int dealerScore = 0;
        public static int playerScore = 0;
        static string input = "";
        //public static int i = 0;

        static BlackJackGame()
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

            foreach (Card card in multDeck)
            {
                deck.AddCard(card);
            }

            deck.Shuffle();

            player = new BlackJackHand();
            //dealer = new BlackJackHand();

            player.cardsInHand.Add(deck.DealOne());
            player.cardsInHand.Add(deck.DealOne());
            playerScore = player.EvaluateHand();

        }

        public static void PlayGame()
        {
            Console.WriteLine("Welcome to Blackjack!");
            foreach (Card card in player.cardsInHand)
                Console.WriteLine("You have been dealt the {0}", card);

            if (player.EvaluateHand() <= 21 || input.ToLower()[0] == 'h')
            {
                do
                {
                    Console.WriteLine("Hand score: {0}", playerScore);
                    if (playerScore > 21)
                    {
                        Console.WriteLine("Bust!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Hit or Stay (H/S)");
                        input = Console.ReadLine();
                        playerScore = player.EvaluateHand();
                        if (input == "H" || input == "h" || input == "Hit" || input == "hit")
                        {
                            player.cardsInHand.Add(deck.DealOne());
                            Console.WriteLine("You have been dealt the {0}", player.cardsInHand.Last());
                            playerScore = player.EvaluateHand();
                        }
                        else
                        {
                            dealer = new BlackJackHand();

                            dealer.cardsInHand.Add(deck.DealOne());
                            dealer.cardsInHand.Add(deck.DealOne());
                            dealerScore = dealer.EvaluateHand();

                            foreach (Card card in dealer.cardsInHand)
                                Console.WriteLine("You have been dealt the {0}", card);

                            Console.WriteLine("Dealer score: {0}", dealerScore);
                            if (dealerScore > 21)
                            {
                                Console.WriteLine("Bust!");
                            }
                            else
                            {
                                //score = dealer.EvaluateHand();
                                if (dealerScore < 17)
                                {
                                    dealer.cardsInHand.Add(deck.DealOne());
                                    Console.WriteLine("You have been dealt the {0}", dealer.cardsInHand.Last());
                                    dealerScore = dealer.EvaluateHand();
                                }
                                else
                                {
                                    if (dealerScore > 21)
                                    {
                                        Console.WriteLine("Bust!");
                                        Console.WriteLine("Player wins!");
                                    }
                                    else
                                    {
                                        if (dealer.CompareTo(player) == 0)
                                        {
                                            Console.WriteLine("Tie!");
                                        }
                                        else if (dealer.CompareTo(player) == 1)
                                        {
                                            Console.WriteLine("Dealer wins!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Player wins!");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                while (input.ToLower()[0] == 'h');
            }
            else
            {
                dealer = new BlackJackHand();

                dealer.cardsInHand.Add(deck.DealOne());
                dealer.cardsInHand.Add(deck.DealOne());
                dealerScore = dealer.EvaluateHand();

                foreach (Card card in dealer.cardsInHand)
                    Console.WriteLine("You have been dealt the {0}", card);

                Console.WriteLine("Dealer score: {0}", dealerScore);
                if (dealerScore > 21)
                {
                    Console.WriteLine("Bust!");
                }
                else
                {
                    //score = dealer.EvaluateHand();
                    if (dealerScore < 17)
                    {
                        dealer.cardsInHand.Add(deck.DealOne());
                        Console.WriteLine("You have been dealt the {0}", dealer.cardsInHand.Last());
                        dealerScore = dealer.EvaluateHand();
                    }
                    else
                    {
                        if (dealerScore > 21)
                        {
                            Console.WriteLine("Bust!");
                            Console.WriteLine("Player wins!");
                        }
                        else
                        {
                            if (dealer.CompareTo(player) == 0)
                            {
                                Console.WriteLine("Tie!");
                            }
                            else if (dealer.CompareTo(player) == 1)
                            {
                                Console.WriteLine("Dealer wins!");
                            }
                            else
                            {
                                Console.WriteLine("Player wins!");
                            }
                        }
                    }
                }
            }
        }
    }
}
