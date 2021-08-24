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
using Windows.Storage;
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
        private double playerBalance = 500;
        private double originalPlayerBalance;
        private double playerBet;
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
        private async void startBtn_Click(object sender, RoutedEventArgs e)
        {
            int num = 500;
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            if (await storageFolder.TryGetItemAsync("balance.txt") != null)
            {
                StorageFile storageFile = await storageFolder.GetFileAsync("balance.txt");
                num = int.Parse(await FileIO.ReadTextAsync(storageFile));
            }
            UpdatePlayerBalance(num);//read in player balance
            InstantiateGame();
        }
        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdatePlayerBalance(500);
            InstantiateGame();
        }
        private void UpdatePlayerBalance(double newBalance)
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
            foreach (Image image in playerImages)
            {
                image.Source = new BitmapImage(new Uri(this.BaseUri, "./images/blank.png"));
            }
            //instantiate the player
            playerHand = new BlackJackHand(/*playerBalance*/);
            //instantiate the dealer
            dealerHand = new BlackJackHand(/*playerBalance*/);

            
            //hide start screen
            SetStartScreenVisibility(Visibility.Collapsed);
            SetEndGameScreenVisibility(Visibility.Collapsed);
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
            doubleDownBtn.Visibility = visibility;
            hitBtn.Visibility = visibility;
            stayBtn.Visibility = visibility;
            curValTxt.Visibility = visibility;
        }

        private void SetTopBalanceVisibility(Visibility visibility)
        {
            balanceTopTxt.Visibility = visibility;
        }

        private void SetEndGameScreenVisibility(Visibility visibility)
        {
            gameResultTxt.Visibility = visibility;
            playAgainBtn.Visibility = visibility;
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
                PlaceCardInGUI(0, playerHand.GetNumberOfCards() - 1, card.ToImgString());
                curValTxt.Text = playerHand.EvaluateHand().ToString();
            }
        }
        private void DealDealerCard(int numOfCards, bool faceDown = false)
        {
            for (int i = 0; i < numOfCards; i++)
            {
                Card card = deck.DealOne();
                dealerHand.AddCard(card);
                PlaceCardInGUI(1, dealerHand.GetNumberOfCards() - 1, card.ToImgString(), faceDown);
            }
        }

        private void PlaceCardInGUI(int player, int place, string imageString, bool faceDown = false)
        {
            if (faceDown) imageString = "./images/red_back.png";
            BitmapImage bitmapImage = new BitmapImage(new Uri(this.BaseUri, imageString));
            if (player == 0)//player hand
            {
                playerImages[place].Source = bitmapImage;
            }
            else //dealer hand
            {
                dealerImages[place].Source = bitmapImage;
            }
        }

        private async void saveExit_Click(object sender, RoutedEventArgs e)
        {
            //save balance to txt file
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            if (await storageFolder.TryGetItemAsync("balance.txt") == null)
            {
                await storageFolder.CreateFileAsync("balance.txt");
            }
            StorageFile storageFile = await storageFolder.GetFileAsync("balance.txt");
            await FileIO.WriteTextAsync(storageFile, playerBalance.ToString());
            Application.Current.Exit();
        }
        private void betBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            char key = (char)e.Key;
            e.Handled = !((key >= 48 && key <= 57)/*numbers*/ || (key >= 96 && key <= 105)/*numpad*/ || key == 8/*backspace*/ || key == 46/*delete*/|| (key >= 37 && key <= 40)/*arrows*/);
        }
        private void betBtn_Click(object sender, RoutedEventArgs e)
        {
            //validate bet amt
            originalPlayerBalance = playerBalance;
            if (double.TryParse(betBox.Text, out playerBet) && Bet())
            {
                //user value
                //hide error message
                SetBetErrorVisibility(Visibility.Collapsed);
                //hide bet screen
                SetBetScreenVisibility(Visibility.Collapsed);
                //show game screen
                SetGameScreenVisibility(Visibility.Visible);
                SetTopBalanceVisibility(Visibility.Visible);

                //deal the player and dealer two cards each
                DealPlayerCard(1);
                DealDealerCard(1, true);
                DealPlayerCard(1);
                DealDealerCard(1);

                //check for blackjack (if so move to dealer turn)
                CheckPlayerTotal();
            }
            else
            {
                SetBetErrorVisibility(Visibility.Visible, "Bet error invalid, please try again.");
            }
        }
        private void SetBetErrorVisibility(Visibility visibility, string message = "")
        {
            betErrorTxt.Text = message;
            betErrorTxt.Visibility = visibility;
        }

        private bool Bet()
        {
            if (playerBet > 0 && playerBet <= playerBalance) //make sure user is within min and max bet values
            {
                //playerBalance -= (betValue - playerBet); //subtract the bet value from the user's balance
                UpdatePlayerBalance(playerBalance - playerBet); //update all elements when updates playerbalance
                return true;
            }
            return false;
        }

        /// <summary>
        /// allows player to double down on their original bet
        /// assumes it is the first turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doubleDownBtn_Click(object sender, RoutedEventArgs e)
        {
            //take bet from playerbalance, deal one card, check if hand value over, move to dealerturn
            Bet();
            DealPlayerCard(1);
            if (playerHand.EvaluateHand() > 21)
            {
                MoveToEnd(EndState.Lose);
            }
            else
            {
                DealerTurn();
            }
        }

        private void CheckPlayerTotal()
        {
            if (playerHand.EvaluateHand() > 21)
            {
                //bust, move to end screen
                SetGameScreenVisibility(Visibility.Collapsed);
                MoveToEnd(EndState.Lose);
            }
            else if (playerHand.EvaluateHand() == 21 && playerHand.GetNumberOfCards() == 2)
            {
                SetGameScreenVisibility(Visibility.Collapsed);
                //blackjack
                //flip dealer card
                PlaceCardInGUI(1, 0, dealerHand.GetCardAtIndex(0).ToImgString());
                //and if blackjack wait for 1s then push,
                if (dealerHand.EvaluateHand() == 21)
                {
                    //wait 1s
                    //Thread.Sleep(1000);
                    MoveToEnd(EndState.Push);
                }
                else
                {
                    MoveToEnd(EndState.BlackJackWin);
                }
            }
        }

        private void stayBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDoubleDown();
            //move to dealer turn
            DealerTurn();
        }

        private void hitBtn_Click(object sender, RoutedEventArgs e)
        {
            HideDoubleDown();
            //add card
            DealPlayerCard(1);
            //check if hand value over
            CheckPlayerTotal();
        }

        private void DealerTurn()
        {
            //hide all buttons
            SetGameScreenVisibility(Visibility.Collapsed);
            PlaceCardInGUI(1, 0, dealerHand.GetCardAtIndex(0).ToImgString());
            //original code (maybe try to use so we can say it was modified at least)

            //while (dealerScore < 17)
            //{
            //    dealerHand.cardsInHand.Add(deck.DealOne());
            //    //Console.WriteLine("Dealer have been dealt the {0}", dealer.cardsInHand.Last());
            //    dealerScore = dealerHand.EvaluateHand();
            //    //Console.WriteLine(dealerScore);
            //}

            //Thread.Sleep(3000);
            int handValue = dealerHand.EvaluateHand();

            while (handValue < 17)
            {
                DealDealerCard(1);
            }
        }

        /// <summary>
        /// compare dealer and player hand
        /// 
        /// </summary>
        private void CompareHands()
        {
            int playerHandValue = playerHand.EvaluateHand();
            int dealerHandValue = dealerHand.EvaluateHand();
            playerHand.CompareTo(dealerHand);

            if (playerHandValue == 21) //player has blackjack
            {
                if (dealerHandValue != 21)
                {
                    playerBalance += (int)(playerBet * 2.5);
                }
            }
            //player and dealer have the same value
            if (playerHandValue == dealerHandValue)
            {
                playerBalance += playerBet; //return players bet
            }
            else if (playerHandValue > dealerHandValue)
            {
                playerBalance += playerBet * 2; //return players bet * 2
            }
        }

        private void MoveToEnd(EndState playerWon)
        {
            SetEndGameScreenVisibility(Visibility.Visible);
            SetGameScreenVisibility(Visibility.Collapsed);
            string endScreenMessage = "";
            switch (playerWon)
            {
                case EndState.Lose:
                    endScreenMessage = $"You Lost ${originalPlayerBalance - playerBalance}!\nTry Again!";
                    break;
                case EndState.Push:
                    endScreenMessage = "You Pushed.\nYou didn't lose any money.";
                    break;
                case EndState.NormalWin:
                    endScreenMessage = $"You Won!\n You gained ${playerBalance - originalPlayerBalance}.";
                    UpdatePlayerBalance(playerBalance + playerBet * 2);
                    break;
                case EndState.DoubleDownWin:
                    endScreenMessage = $"You Won!\n You gained ${playerBalance - originalPlayerBalance}.";
                    UpdatePlayerBalance(playerBalance + playerBet * 3);
                    break;
                case EndState.BlackJackWin:
                    endScreenMessage = $"You got a Blackjack!\n You gained ${playerBalance - originalPlayerBalance}.";
                    UpdatePlayerBalance(playerBalance + playerBet * 2.5);
                    break;
            }
            gameResultTxt.Text = endScreenMessage;
        }

        //public void PlayGame()
        //{
        //    //string playerInput = "";
        //    //find the player's current score
        //    playerScore = playerHand.EvaluateHand();
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
        //        playerHand.cardsInHand.Add(deck.DealOne());
        //        //Console.WriteLine("You have been dealt the {0}", player.cardsInHand.Last());
        //        playerScore = playerHand.EvaluateHand();
        //        //Console.WriteLine("Hand score: {0}", playerScore);
        //        if (playerScore > 21)
        //        {
        //            gameOver = !gameOver;
        //            //Console.WriteLine("Bust!");
        //            //break;
        //        }
        //        //Console.WriteLine("Hit or Stay (H/S)");
        //        //playerInput = Console.ReadLine();
        //        playerScore = playerHand.EvaluateHand();
        //        //}
        //        //else
        //        //{
        //        //dealer = new BlackJackHand();
        //        dealerHand.cardsInHand.Add(deck.DealOne());
        //        dealerHand.cardsInHand.Add(deck.DealOne());
        //        dealerScore = dealerHand.EvaluateHand();
        //        foreach (Card card in dealerHand.cardsInHand)
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
        //                dealerHand.cardsInHand.Add(deck.DealOne());
        //                //Console.WriteLine("Dealer have been dealt the {0}", dealer.cardsInHand.Last());
        //                dealerScore = dealerHand.EvaluateHand();
        //                //Console.WriteLine(dealerScore);
        //            }
        //            if (dealerScore >= 17 && dealerScore <= 21)
        //            {
        //                if (dealerHand.CompareTo(playerHand) == 0)
        //                {
        //                    //Console.WriteLine("Tie!");
        //                }
        //                else if (dealerHand.CompareTo(playerHand) == 1)
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
        //    playerHand.DiscardHand();
        //    if (!dealerHand.IsEmpty())
        //    {
        //        dealerHand.DiscardHand();
        //    }
        //}
        private void playAgainBtn_Click(object sender, RoutedEventArgs e)
        {
            if (playerBalance <= 0)
                UpdatePlayerBalance(500);

            InstantiateGame();
        }
    }

    public enum EndState { Lose, Push, NormalWin, DoubleDownWin, BlackJackWin }
}