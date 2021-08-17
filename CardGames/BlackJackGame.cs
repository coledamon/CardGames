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
        static bool gameOver = false;

        static BlackJackGame()
        {
            List<Card> multDeck = new List<Card>();

            //creates 6 decks
            for (int x = 0; x < 6; x++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j <= 12; j++)
                    {
                        Card card = new Card(Suit.VALUES[i], Rank.VALUES[j]);
                        deck.AddCard(card);
                    }
                }
            }

            ////adds all 6 decks to the current deck
            //foreach (Card card in multDeck)
            //{
            //    deck.AddCard(card);
            //}

            InstantiateGame(500);
            


        }

        public static void PlayGame()
        {
            //string playerInput = "";

            //find the player's current score
            playerScore = player.EvaluateHand();


            if (playerScore > 21) //player's starting hand was over 21
            {
                gameOver = true;
            }
            else
            {
                //Console.WriteLine("Hit or Stay (H/S)");
                //playerInput = Console.ReadLine();

                //while (!gameOver) //run while game 
                //{
                    player.cardsInHand.Add(deck.DealOne());
                    //Console.WriteLine("You have been dealt the {0}", player.cardsInHand.Last());
                    playerScore = player.EvaluateHand();
                    //Console.WriteLine("Hand score: {0}", playerScore);
                    if(playerScore > 21)
                    {
                        gameOver = !gameOver;
                        //Console.WriteLine("Bust!");
                        //break;
                    }
                    //Console.WriteLine("Hit or Stay (H/S)");
                    //playerInput = Console.ReadLine();
                    playerScore = player.EvaluateHand();
                //}

                //else
                //{
                    //dealer = new BlackJackHand();

                    dealer.cardsInHand.Add(deck.DealOne());
                    dealer.cardsInHand.Add(deck.DealOne());
                    dealerScore = dealer.EvaluateHand();

                    foreach (Card card in dealer.cardsInHand)
                    {
                        //Console.WriteLine("Dealer hasss been dealt the {0}", card);
                    }

                    //Console.WriteLine("Dealer score: {0}", dealerScore);

                    if (dealerScore > 21)
                    {
                        //Console.WriteLine("Bust!");
                        //TODO: Implement game over function
                    }
                    else
                    {
                        while (dealerScore < 17)
                        {
                            dealer.cardsInHand.Add(deck.DealOne());
                            //Console.WriteLine("Dealer have been dealt the {0}", dealer.cardsInHand.Last());
                            dealerScore = dealer.EvaluateHand();
                            //Console.WriteLine(dealerScore);
                        }
                        
                        if (dealerScore >= 17 && dealerScore <= 21)
                        {
                            if (dealer.CompareTo(player) == 0)
                            {
                                //Console.WriteLine("Tie!");
                            }
                            else if (dealer.CompareTo(player) == 1)
                            {
                                //Console.WriteLine("Dealer wins!");
                            }
                            else
                            {
                                //Console.WriteLine("Player wins!");
                            }
                        }
                        else
                        {
                            //Console.WriteLine("Bust!");
                            //Console.WriteLine("Player wins!");
                        }
                    //}
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


        /// <summary>
        /// simulates the first turn of blackjack
        /// create player and dealer and give them both 2 cards
        /// </summary>
        /// <param name="balance"> the starting balance of the player</param>
        private static void InstantiateGame(int balance)
        {
            //shuffles the current deck
            deck.Shuffle();

            //instantiate the player
            player = new BlackJackHand(balance);

            //instantiate the dealer
            dealer = new BlackJackHand(balance);

            //deal the player and dealer two cards each
            for(int i = 0; i < 2; i++)
            {
                player.AddCard(deck.DealOne());
                dealer.AddCard(deck.DealOne());
            }
            
        }

        private static bool CheckForGameOver(BlackJackHand hand)
        {
            return hand.EvaluateHand() < 21 ? false : true;
        }

    }
}
