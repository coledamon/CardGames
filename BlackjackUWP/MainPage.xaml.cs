using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using CardGames;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlackjackUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static BlackJackHand player;
        static BlackJackHand dealer;
        static Deck deck = new Deck(6);
        public static int dealerScore = 0;
        public static int playerScore = 0;
        static bool gameOver = false;
        private int playerBalance;
        private int playerBet;



        public string maxBetString { get { return $"Max: {maxBet}"; } }
        private int maxBet = 0;
        private int minBet = 0;

        public MainPage()
        {
            DataContext = this;
            this.InitializeComponent();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            playerBalance = 500; //read in player balance
            InstantiateGame();
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            playerBalance = 500;
            InstantiateGame();
        }

        /// <summary>
        /// simulates the first turn of blackjack
        /// create player and dealer and give them both 2 cards
        /// </summary>
        /// <param name="balance"> the starting balance of the player</param>
        private void InstantiateGame()
        {
            //instantiate the player
            //player = new BlackJackHand(playerBalance);

            //instantiate the dealer
            //dealer = new BlackJackHand(playerBalance);

            //deal the player and dealer two cards each
            DealPlayerCard(2);
            DealFaceDownDealerCard(1);
            DealDealerCard(1);
        }

        private void DealPlayerCard(int numOfCards)
        {
            for (int i = 0; i < numOfCards; i++)
            {

            }
        }
        private void DealFaceDownDealerCard(int numOfCards)
        {
            for (int i = 0; i < numOfCards; i++)
            {

            }
        }
        private void DealDealerCard(int numOfCards)
        {
            for (int i = 0; i < numOfCards; i++)
            {

            }
        }

        private void saveExit_Click(object sender, RoutedEventArgs e)
        {
            //save balance to txt file
        }

        private void betBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            char key = (char)e.Key;
            e.Handled = !((key >= 48 && key <= 57)/*numbers*/ || (key >= 96 && key <= 105)/*numpad*/ || key == 8/*backspace*/ || key == 46/*delete*/|| (key >= 37 && key <= 40)/*arrows*/);
        }

        private void betBtn_Click(object sender, RoutedEventArgs e)
        {
            int betValue = int.Parse(betBox.Text); //user value
            Bet(betValue);

        }

        /// <summary>
        /// allows player to double down on their original bet
        /// assumes it is the first turn and players current hand equals 9,10,11
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleDownBtn_Click(object sender, RoutedEventArgs e)
        {
            Bet(playerBet);
            DealPlayerCard(1);

        }

        private void stayBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hitBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bet(int betValue)
        {
            //user bet must be between min and max bet values
            //user bet can not exceed player balance
            if (betValue >= minBet && betValue <= maxBet && betValue <= playerBalance)
            {
                playerBalance -= (betValue - playerBet); //subtract the bet value from the user's balance
                playerBet = betValue;
                betBox.Text = playerBet.ToString();
            }
        }

        private void DealerTurn()
        {
            //Thread.Sleep(3000);
            int handValue = dealer.EvaluateHand();

            while(handValue < 17){
                DealDealerCard(1);
            }
        }

        /// <summary>
        /// compare dealer and player hand
        /// 
        /// </summary>
        private void CompareHands()
        {
            int playerHandValue = player.EvaluateHand();
            int dealerHandValue = dealer.EvaluateHand();

            if(playerHandValue == 21) //player has blackjack
            {
                if(dealerHandValue != 21)
                {
                    playerBalance += (int)(playerBet * 2.5);
                }
            }
            //player and dealer have the same value
            if(playerHandValue == dealerHandValue) 
            {
                playerBalance += playerBet; //return players bet
            }
            else if(playerHandValue > dealerHandValue)
            {
                playerBalance += playerBet * 2; //return players bet * 2
            }
        }



        //public static void PlayGame()
        //{
        //    //string playerInput = "";

        //    //find the player's current score
        //    playerScore = player.EvaluateHand();


        //    if (playerScore > 21) //player's starting hand was over 21
        //    {
        //        gameOver = true;
        //    }
        //    else
        //    {
        //        //Console.WriteLine("Hit or Stay (H/S)");
        //        //playerInput = Console.ReadLine();

        //        //while (!gameOver) //run while game 
        //        //{
        //        player.cardsInHand.Add(deck.DealOne());
        //        //Console.WriteLine("You have been dealt the {0}", player.cardsInHand.Last());
        //        playerScore = player.EvaluateHand();
        //        //Console.WriteLine("Hand score: {0}", playerScore);
        //        if (playerScore > 21)
        //        {
        //            gameOver = !gameOver;
        //            //Console.WriteLine("Bust!");
        //            //break;
        //        }
        //        //Console.WriteLine("Hit or Stay (H/S)");
        //        //playerInput = Console.ReadLine();
        //        playerScore = player.EvaluateHand();
        //        //}

        //        //else
        //        //{
        //        //dealer = new BlackJackHand();

        //        dealer.cardsInHand.Add(deck.DealOne());
        //        dealer.cardsInHand.Add(deck.DealOne());
        //        dealerScore = dealer.EvaluateHand();

        //        foreach (Card card in dealer.cardsInHand)
        //        {
        //            //Console.WriteLine("Dealer hasss been dealt the {0}", card);
        //        }

        //        //Console.WriteLine("Dealer score: {0}", dealerScore);

        //        if (dealerScore > 21)
        //        {
        //            //Console.WriteLine("Bust!");
        //            //TODO: Implement game over function
        //        }
        //        else
        //        {
        //            while (dealerScore < 17)
        //            {
        //                dealer.cardsInHand.Add(deck.DealOne());
        //                //Console.WriteLine("Dealer have been dealt the {0}", dealer.cardsInHand.Last());
        //                dealerScore = dealer.EvaluateHand();
        //                //Console.WriteLine(dealerScore);
        //            }

        //            if (dealerScore >= 17 && dealerScore <= 21)
        //            {
        //                if (dealer.CompareTo(player) == 0)
        //                {
        //                    //Console.WriteLine("Tie!");
        //                }
        //                else if (dealer.CompareTo(player) == 1)
        //                {
        //                    //Console.WriteLine("Dealer wins!");
        //                }
        //                else
        //                {
        //                    //Console.WriteLine("Player wins!");
        //                }
        //            }
        //            else
        //            {
        //                //Console.WriteLine("Bust!");
        //                //Console.WriteLine("Player wins!");
        //            }
        //            //}
        //        }
        //    }

        //    deck.RestoreDeck();
        //    deck.Shuffle();
        //    player.DiscardHand();
        //    if (!dealer.IsEmpty())
        //    {
        //        dealer.DiscardHand();
        //    }
        //}




        //private static bool CheckForGameOver(BlackJackHand hand)
        //{
        //    return hand.EvaluateHand() < 21 ? false : true;
        //}

    }
}
