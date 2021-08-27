using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
        private List<Image> playerImages = new List<Image>();
        private List<Image> dealerImages = new List<Image>();
        private double playerBalance = 500;
        private double originalPlayerBalance;
        private double playerBet;

        private int dealerSlowdown = 500;

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
            foreach (Image image in dealerImages)
            {
                image.Source = new BitmapImage(new Uri(this.BaseUri, "./images/blank.png"));
            }
            deck = new Deck(6);

            //deck.setup();
            //instantiate the player
            playerHand = new BlackJackHand();
            //instantiate the dealer
            dealerHand = new BlackJackHand();

            
            //hide start screen
            SetStartScreenVisibility(Visibility.Collapsed);
            SetEndGameScreenVisibility(Visibility.Collapsed);
            //show bet screen
            SetBetScreenVisibility(Visibility.Visible);
        }
        private void SetStartScreenVisibility(Visibility visibility)
        {
            StartingScreen.Visibility = visibility;
        }
        private void SetBetScreenVisibility(Visibility visibility)
        {
            BettingScreen.Visibility = visibility;
        }

        private void SetGameScreenVisibility(Visibility visibility)
        {
            GameScreen.Visibility = visibility;
            if (playerBalance < playerBet || playerHand?.GetNumberOfCards() > 2)
            {
                HideDoubleDown();
            }
            else
            {
                doubleDownBtn.Visibility = visibility;
            }
            curValTxt.Visibility = visibility;
        }

        private void SetTopBalanceVisibility(Visibility visibility)
        {
            balanceTopTxt.Visibility = visibility;
        }

        private void SetEndGameScreenVisibility(Visibility visibility)
        {
            GameOver.Visibility = visibility;
        }
        private void SetshopscreenVisibility(Visibility visibility)
        {
            shopscreen.Visibility = visibility;
        }

        private void SetSettingsVisibility(Visibility visibility)
        {
            DealerSettings.Visibility = visibility;
            settingsButton.Visibility = visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
        private void SetAllVisibility(Visibility visibility)
        {
            SetStartScreenVisibility(visibility);
            SetBetScreenVisibility(visibility);
            SetBetErrorVisibility(visibility);
            SetGameScreenVisibility(visibility);
            SetshopscreenVisibility(visibility);
            GameOver.Visibility = visibility;
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
                curValTxt.Text = "Current Value: " + playerHand.EvaluateHand();
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
        private string cardback = "./images/red_back.png";
        private void PlaceCardInGUI(int player, int place, string imageString, bool faceDown = false)
        {
            if (faceDown) imageString=cardback;
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
        private async void betBtn_Click(object sender, RoutedEventArgs e)
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
                SetTopBalanceVisibility(Visibility.Visible);

                //deal the player and dealer two cards each
                await Task.Delay(250);
                DealPlayerCard(1);
                await Task.Delay(dealerSlowdown);
                DealDealerCard(1, true);
                await Task.Delay(dealerSlowdown);
                DealPlayerCard(1);
                await Task.Delay(dealerSlowdown);
                DealDealerCard(1);
                SetGameScreenVisibility(Visibility.Visible);

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
            //take bet from playerbalance, deal one card, check if hand value over, move to dealer turn
            Bet();
            DealPlayerCard(1);
            if (playerHand.EvaluateHand() > 21)
            {
                MoveToEnd(EndState.Lose);
            }
            else
            {
                DealerTurn(true);
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
            else if (playerHand.EvaluateHand() == 21)
            {
                if(playerHand.GetNumberOfCards() == 2)
                {
                    SetGameScreenVisibility(Visibility.Collapsed);
                    //blackjack
                    //flip dealer card
                    PlaceCardInGUI(1, 0, dealerHand.GetCardAtIndex(0).ToImgString());
                    //and if blackjack wait for 1s then push,
                    if (dealerHand.EvaluateHand() == 21)
                    {
                        MoveToEnd(EndState.Push);
                    }
                    else
                    {
                        MoveToEnd(EndState.BlackJackWin);
                    }
                }
                else
                {
                    DealerTurn();
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

        private async void DealerTurn(bool playerDoubledDown = false)
        {
            //hide all buttons
            SetGameScreenVisibility(Visibility.Collapsed);
            FlipDealerCard();

            int handValue = dealerHand.EvaluateHand();

            while (handValue < 17)
            {
                await Task.Delay(dealerSlowdown);
                DealDealerCard(1);
                handValue = dealerHand.EvaluateHand();
            }
            if(handValue > 21)
            {
                if(playerDoubledDown)
                {
                    MoveToEnd(EndState.DoubleDownWin);
                }
                else
                {
                    MoveToEnd(EndState.NormalWin);
                }
            }
            else
            {
                CompareHands(playerDoubledDown);
            }
        }

        /// <summary>
        /// compare dealer and player hand
        /// 
        /// </summary>
        private void CompareHands(bool playerDoubledDown = false)
        {
            switch(playerHand.CompareTo(dealerHand))
            {
                case -1:
                    MoveToEnd(EndState.Lose);
                    break;
                case 0:
                    MoveToEnd(EndState.Push);
                    break;
                case 1:
                    if (playerDoubledDown)
                    {
                        MoveToEnd(EndState.DoubleDownWin);
                    }
                    else
                    {
                        MoveToEnd(EndState.NormalWin);
                    }
                    break;
            }
        }

        private void MoveToEnd(EndState playerWon)
        {
            FlipDealerCard();
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
                    UpdatePlayerBalance(playerBalance + playerBet * 2);
                    endScreenMessage = $"You Won!\n You gained ${playerBalance - originalPlayerBalance}.";
                    break;
                case EndState.DoubleDownWin:
                    UpdatePlayerBalance(playerBalance + playerBet * 4);
                    endScreenMessage = $"You Won!\n You gained ${playerBalance - originalPlayerBalance}.";
                    break;
                case EndState.BlackJackWin:
                    UpdatePlayerBalance(playerBalance + playerBet * 2.5);
                    endScreenMessage = $"You got a Blackjack!\n You gained ${playerBalance - originalPlayerBalance}.";
                    break;
            }
            gameResultTxt.Text = endScreenMessage;
        }

        private void FlipDealerCard()
        {
            PlaceCardInGUI(1, 0, dealerHand.GetCardAtIndex(0).ToImgString());
        }

        private void playAgainBtn_Click(object sender, RoutedEventArgs e)
        {
            if (playerBalance <= 0)
                UpdatePlayerBalance(100);

            InstantiateGame();
        }

        private void shopBtn_Click(object sender, RoutedEventArgs e)
        {
            SetshopscreenVisibility(Visibility.Visible);
            SetBetScreenVisibility(Visibility.Collapsed);
            SetTopBalanceVisibility(Visibility.Visible);
        }
        private void color_Click(object sender, RoutedEventArgs e)
        {
            if (playerBalance > 100)//purposely not >=
            {
                UpdatePlayerBalance(playerBalance - 100);
                Button button = (Button)sender;
                cardback = "./images/" + button.Name + "_back.png";
                shopMessageTxt.Text = $"{button.Name.Substring(0,1).ToUpper()+button.Name.Substring(1)} Successfully Purchased.";
            }
            else
            {
                shopMessageTxt.Text = "Not enough money to complete purchase.";
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            SetTopBalanceVisibility(Visibility.Collapsed);
            SetshopscreenVisibility(Visibility.Collapsed);
            SetBetScreenVisibility(Visibility.Visible);
            shopMessageTxt.Text = "";
        }

        private Visibility startVisibility;
        private Visibility betVisibility;
        private Visibility betErrorVisibility;
        private Visibility gameVisibility;
        private Visibility gameOverVisibility;
        private Visibility shopVisibility;

        private void changeDealerBtn_Click(object sender, RoutedEventArgs e)
        {
            int curSlow = dealerSlowdown;
            if(int.TryParse(slowdownBox.Text, out dealerSlowdown))
            {
                slowdownBox.PlaceholderText = "Current Value: " + dealerSlowdown;
            }
            else
            {
                dealerSlowdown = curSlow;
            }
            slowdownBox.Text = "";
            
            SetSettingsVisibility(Visibility.Collapsed);

            SetStartScreenVisibility(startVisibility);
            SetBetScreenVisibility(betVisibility);
            SetBetErrorVisibility(betErrorVisibility);
            SetGameScreenVisibility(gameVisibility);
            SetshopscreenVisibility(shopVisibility);
            GameOver.Visibility = gameOverVisibility;
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            startVisibility = StartingScreen.Visibility;
            betVisibility = BettingScreen.Visibility;
            betErrorVisibility = betErrorTxt.Visibility;
            gameVisibility = GameScreen.Visibility;
            gameOverVisibility = GameOver.Visibility;
            shopVisibility = shopscreen.Visibility;

            SetSettingsVisibility(Visibility.Visible);
            SetAllVisibility(Visibility.Collapsed);
        }


    }

    public enum EndState { Lose, Push, NormalWin, DoubleDownWin, BlackJackWin }
}