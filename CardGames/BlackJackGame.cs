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
            string playerInput = "";
            dealer = new BlackJackHand();

            Console.WriteLine("Welcome to Blackjack!");

            foreach (Card card in player.cardsInHand)
                Console.WriteLine("You have been dealt the {0}", card);

            playerScore = player.EvaluateHand();

            Console.WriteLine("Hand score: {0}", playerScore);
            if (playerScore > 21)
            {
                Console.WriteLine("Bust!");
            }
            else
            {
                Console.WriteLine("Hit or Stay (H/S)");
                playerInput = Console.ReadLine();

                while (playerInput.ToLower()[0] == 'h' && playerScore <= 21)
                {
                    player.cardsInHand.Add(deck.DealOne());
                    Console.WriteLine("You have been dealt the {0}", player.cardsInHand.Last());
                    playerScore = player.EvaluateHand();
                    Console.WriteLine("Hand score: {0}", playerScore);
                    if(playerScore > 21)
                    {
                        Console.WriteLine("Bust!");
                        break;
                    }
                    Console.WriteLine("Hit or Stay (H/S)");
                    playerInput = Console.ReadLine();
                    playerScore = player.EvaluateHand();
                }

            
                if (playerScore > 21)
                {
                    ;
                }

                else
                {
                    //dealer = new BlackJackHand();

                    dealer.cardsInHand.Add(deck.DealOne());
                    dealer.cardsInHand.Add(deck.DealOne());
                    dealerScore = dealer.EvaluateHand();

                    foreach (Card card in dealer.cardsInHand)
                    {
                        Console.WriteLine("Dealer hasss been dealt the {0}", card);
                    }

                    Console.WriteLine("Dealer score: {0}", dealerScore);

                    if (dealerScore > 21)
                    {
                        Console.WriteLine("Bust!");
                    }
                    else
                    {
                        while (dealerScore < 17)
                        {
                            dealer.cardsInHand.Add(deck.DealOne());
                            Console.WriteLine("Dealer have been dealt the {0}", dealer.cardsInHand.Last());
                            dealerScore = dealer.EvaluateHand();
                            Console.WriteLine(dealerScore);
                        }
                        
                        if (dealerScore >= 17 && dealerScore <= 21)
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
                        else
                        {
                            Console.WriteLine("Bust!");
                            Console.WriteLine("Player wins!");
                        }
                    }
                }
            }

            deck.RestoreDeck();
            deck.Shuffle();
            player.DiscardHand();
            if (!dealer.IsEmpty())
            {
                dealer.DiscardHand();
            }
        }
    }
}
