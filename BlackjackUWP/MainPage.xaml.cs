using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlackjackUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private BlackJackHand playerHand;
        private BlackJackHand dealerHand;
        private Deck deck = new Deck(6);
        private int dealerScore = 0;
        private int playerScore = 0;
        private bool gameOver = false;
        private List<Image> playerImages = new List<Image>();
        private List<Image> dealerImages = new List<Image>();
        private int playerBalance;
        private int playerBet;

        public MainPage()
        {
            DataContext = this;
            this.InitializeComponent();

            playerImages.Add(playerCard1Img);
            playerImages.Add(playerCard2Img);
            playerImages.Add(playerCard3Img);
            playerImages.Add(playerCard4Img);
            playerImages.Add(playerCard5Img);
            playerImages.Add(playerCard6Img);
            playerImages.Add(playerCard7Img);

            dealerImages.Add(dealerCard1Img);
            dealerImages.Add(dealerCard2Img);
            dealerImages.Add(dealerCard3Img);
            dealerImages.Add(dealerCard4Img);
            dealerImages.Add(dealerCard5Img);
            dealerImages.Add(dealerCard6Img);
            dealerImages.Add(dealerCard7Img);
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdatePlayerBalance(500);//read in player balance
            InstantiateGame();
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdatePlayerBalance(500);
            InstantiateGame();
        }

        private void UpdatePlayerBalance(int newBalance)
        {
            playerBalance = newBalance;
            balanceMidTxt.Text = $"Balance: {playerBalance}";
            balanceTopTxt.Text = $"Balance: {playerBalance}";
            maxBetTxt.Text = $"Max: {playerBalance}";
        }

        /// <summary>
        /// simulates the first turn of blackjack
        /// create player and dealer and give them both 2 cards
        /// </summary>
        private void InstantiateGame()
        {
            //instantiate the player
            playerHand = new BlackJackHand(/*playerBalance*/);

            //instantiate the dealer
            dealerHand = new BlackJackHand(/*playerBalance*/);

            //hide start screen
            SetStartScreenVisibility(Visibility.Collapsed);
            //show bet screen
            SetBetScreenVisibility(Visibility.Visible);
        }

        private void SetStartScreenVisibility(Visibility visibility)
        {
            startBtn.Visibility = visibility;
            newGameBtn.Visibility = visibility;
        }

        private void SetBetScreenVisibility(Visibility visibility)
        {
            balanceMidTxt.Visibility = visibility;
            betTxt.Visibility = visibility;
            betBox.Visibility = visibility;
            minBetTxt.Visibility = visibility;
            maxBetTxt.Visibility = visibility;
            betBtn.Visibility = visibility;
        }

        private void SetGameScreenVisibility(Visibility visibility)
        {
            balanceTopTxt.Visibility = visibility;
            doubleDownBtn.Visibility = visibility;
            hitBtn.Visibility = visibility;
            stayBtn.Visibility = visibility;
            curValTxt.Visibility = visibility;
        }
        private void HideDoubleDown()
        {
            doubleDownBtn.Visibility = Visibility.Collapsed;
        }

        private void DealPlayerCard(int numOfCards)
        {
            for (int i = 0; i < numOfCards; i++)
            {
                Card card = deck.DealOne();
                playerHand.AddCard(card);
                PlaceCard(0, card.ToImgString());
                curValTxt.Text = playerHand.EvaluateHand().ToString();
            }
        }
        private void DealDealerCard(int numOfCards, bool faceDown = false)
        {
            for (int i = 0; i < numOfCards; i++)
            {
                Card card = deck.DealOne();
                dealerHand.AddCard(card);
                PlaceCard(1, card.ToImgString(), faceDown);
            }
        }

        private void PlaceCard(int player, string imageString, bool faceDown = false)
        {
            if (faceDown) imageString = "./images/red_back.png";
            BitmapImage bitmapImage = new BitmapImage(new Uri(this.BaseUri, imageString));
            if (player == 0)//player hand
            {
                playerImages[playerHand.GetNumberOfCards() - 1].Source = bitmapImage;
            }
            else //dealer hand
            {
                dealerImages[dealerHand.GetNumberOfCards() - 1].Source = bitmapImage;
            }
        }

        private void saveExit_Click(object sender, RoutedEventArgs e)
        {
            //save balance to txt file
            //i have tested it in a diffrent project and it works but something is stoping it in this one, most likely git, if i can get it to work here i will add it to where it is needed
            //if you want to test it just uncomment
            //File.WriteAllText("balance.txt", playerBalance+"");
        }

        private void betBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            char key = (char)e.Key;
            e.Handled = !((key >= 48 && key <= 57)/*numbers*/ || (key >= 96 && key <= 105)/*numpad*/ || key == 8/*backspace*/ || key == 46/*delete*/|| (key >= 37 && key <= 40)/*arrows*/);
        }

        private void betBtn_Click(object sender, RoutedEventArgs e)
        {
            //validate bet amt
            int betValue = int.Parse(betBox.Text); //user value
            Bet(betValue);


            //hide bet screen
            SetBetScreenVisibility(Visibility.Collapsed);
            //show game screen
            SetGameScreenVisibility(Visibility.Visible);

            //deal the player and dealer two cards each
            DealPlayerCard(2);
            DealDealerCard(1, true);
            DealDealerCard(1);

            //check for blackjack (if so move to dealer turn)
        }

        /// <summary>
        /// allows player to double down on their original bet
        /// assumes it is the first turn and players current hand equals 9,10,11
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleDownBtn_Click(object sender, RoutedEventArgs e)
        {
            //take bet from playerbalance, deal one card, check if hand value over, move to dealerturn
            Bet(playerBet);
            DealPlayerCard(1);

        }

        private void stayBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDoubleDown();
            //move to dealer turn
            //hide all buttons
        }

        private void hitBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDoubleDown();
            //add card, check if hand value over 
        }

        private void Bet(int betValue)
        {
            if (betValue >= 0 && betValue <= playerBalance) //make sure user is within min and max bet values
            {
                playerBalance -= (betValue - playerBet); //subtract the bet value from the user's balance
                playerBet = betValue;
                betBox.Text = playerBet.ToString();
            }
        }

        public void PlayGame()
        {
            //string playerInput = "";

            //find the player's current score
            playerScore = playerHand.EvaluateHand();


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
                playerHand.cardsInHand.Add(deck.DealOne());
                //Console.WriteLine("You have been dealt the {0}", player.cardsInHand.Last());
                playerScore = playerHand.EvaluateHand();
                //Console.WriteLine("Hand score: {0}", playerScore);
                if (playerScore > 21)
                {
                    gameOver = !gameOver;
                    //Console.WriteLine("Bust!");
                    //break;
                }
                //Console.WriteLine("Hit or Stay (H/S)");
                //playerInput = Console.ReadLine();
                playerScore = playerHand.EvaluateHand();
                //}

                //else
                //{
                //dealer = new BlackJackHand();

                dealerHand.cardsInHand.Add(deck.DealOne());
                dealerHand.cardsInHand.Add(deck.DealOne());
                dealerScore = dealerHand.EvaluateHand();

                foreach (Card card in dealerHand.cardsInHand)
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
                        dealerHand.cardsInHand.Add(deck.DealOne());
                        //Console.WriteLine("Dealer have been dealt the {0}", dealer.cardsInHand.Last());
                        dealerScore = dealerHand.EvaluateHand();
                        //Console.WriteLine(dealerScore);
                    }

                    if (dealerScore >= 17 && dealerScore <= 21)
                    {
                        if (dealerHand.CompareTo(playerHand) == 0)
                        {
                            //Console.WriteLine("Tie!");
                        }
                        else if (dealerHand.CompareTo(playerHand) == 1)
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
            playerHand.DiscardHand();
            if (!dealerHand.IsEmpty())
            {
                dealerHand.DiscardHand();
            }
        }

        private static bool CheckForGameOver(BlackJackHand hand)
        {
            return hand.EvaluateHand() < 21 ? false : true;
        }

        
    }
}
