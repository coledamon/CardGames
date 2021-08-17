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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlackjackUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string maxBetString { get { return $"Max: {maxBet}"; } }
        private int maxBet = 0;

        public BlackJackHand playerHand { get; set; } = new BlackJackHand();
        public BlackJackHand dealerHand { get; set; } = new BlackJackHand();
        public MainPage()
        {
            DataContext = this;
            this.InitializeComponent();
            //MainClass.Start(new string[] { });
        }

        private void saveExit_Click(object sender, RoutedEventArgs e)
        {
            //save balance to txt file
        }

        private void betBox_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            char key = (char)e.Key;
            e.Handled = !((key >= 48 && key <= 57)/*numbers*/ || (key >=96 && key <=105)/*numpad*/ || key == 8/*backspace*/ ||key == 46/*delete*/|| (key >= 37 && key <=40)/*arrows*/);
        }

        private void betBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void doubleDownBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void stayBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void hitBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
