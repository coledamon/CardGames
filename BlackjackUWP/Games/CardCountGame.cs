//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace CardGames
//{
//    class CardCountGame
//    {
//        public static void PlayGame()
//        {
//            CardCountHand h1 = new CardCountHand();
//            CardCountHand h2 = new CardCountHand();

//            var h1Eval = 0;
//            var h2Eval = 0;

//            List<Card> hand1 = new List<Card>();
//            List<Card> hand2 = new List<Card>();

//            List<Card> fullDeck = new List<Card>();

//            for (int i = 0; i <= 3; i++)
//            {
//                for (int j = 0; j <= 12; j++)
//                {
//                    Card cards = new Card(Suit.VALUES[i], Rank.VALUES[j]);
//                    fullDeck.Add(cards);
//                    //Hand.deckOfCards.AddCard(cards);
//                }
//            }

//            Deck deck = new Deck();

//            foreach (Card card in fullDeck)
//            {
//                deck.AddCard(card);
//                //Console.WriteLine(card);
//            }

//            //Console.WriteLine("");
//            //Console.WriteLine("~~~~~~~~~~~~~~~~~~");
//            //Console.WriteLine("Cards before shuffle " + deck.GetCardsRemaining());

//            deck.Shuffle();

//            //Console.WriteLine("");
//            //Console.WriteLine("~~~~~~~~~~~~~~~~~~");
//            //Console.WriteLine("Cards after shuffle " + deck.GetCardsRemaining());

//            //Console.WriteLine("");
//            //Console.WriteLine("~~~~~~~~~~~~~~~~~~");

//            for (int i = 0; i < 8; i++)
//            {
//                var dealt = deck.DealOne();
//                //CardCountHand.cards.Add(dealt);
//                h1.AddCard(dealt);
//                hand1.Add(h1.GetCardAtIndex(i));
//                //Console.WriteLine(h1.GetCardAtIndex(i));
//            }

//            h1Eval = h1.EvaluateHand();
//            h1.DiscardHand();

//            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~");

//            for (int i = 0; i < 8; i++) //i = 8; i < 16
//            {
//                var dealt = deck.DealOne();
//                h2.AddCard(dealt);
//                hand2.Add(h2.GetCardAtIndex(i));
//                //Console.WriteLine(h2.GetCardAtIndex(i));
//            }

//            h2Eval = h2.EvaluateHand();
//            h2.DiscardHand();

//            //Console.WriteLine("");
//            //Console.WriteLine("~~~~~~~~~~~~~~~~~~");
//            //Console.WriteLine("Cards after dealing " + deck.GetCardsRemaining());

//            //Console.WriteLine("");
//            //Console.WriteLine("~~~~~~~~~~~~~~~~~~");

//            Console.WriteLine("");
//            Console.WriteLine("Player 1\tPlayer2");

//            for (int i = 0; i < 8; i++)
//            {
//                Console.WriteLine("{0}\t\t{1}", hand1[i], hand2[i]); //, h1.GetCardAtIndex(i), h2.GetCardAtIndex(i + 8));
//            }
//            //Console.WriteLine("");
//            Console.WriteLine("~~~~~~~~~~~~~~~~~~");
//            Console.WriteLine("\tTOTAL");
//            Console.WriteLine("{0}\t\t{1}", h1Eval, h2Eval);
//            Console.WriteLine("");
//            Console.WriteLine("There are {0} cards left in the deck", deck.GetCardsRemaining());
//            //Console.WriteLine(h1.GetCardAtIndex(20));
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    class CardCountGame
    {
        static CardCountHand h1;
        static CardCountHand h2;
        static Deck deck = new Deck();

        static CardCountGame()
        {
            List<Card> fullDeck = new List<Card>();

            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 12; j++)
                {
                    Card cards = new Card(Suit.VALUES[i], Rank.VALUES[j]);
                    fullDeck.Add(cards);
                }
            }

            foreach (Card card in fullDeck)
            {
                deck.AddCard(card);
            }

            deck.Shuffle();

            h1 = new CardCountHand();
            for (int i = 0; i < 8; i++)
            {
                h1.cardsInHand.Add(deck.DealOne());
            }

            h2 = new CardCountHand();
            for (int i = 0; i < 8; i++)
            {
                //h2.AddCard(deck.DealOne());
                h2.cardsInHand.Add(deck.DealOne());
            }
        }
        public static void PlayGame()
        {
            Console.WriteLine("");

            //for (int i = 0; i < 8; i++)
            //{
            //    Console.WriteLine("{0}\t\t{1}", h1.ToString(), h2.GetCardAtIndex(i));
            //}
            Console.WriteLine("Hand 1: " + h1);
            Console.WriteLine("Hand 1: " + h2);
            Console.WriteLine("Hand 1: Score: " + h1.EvaluateHand());
            Console.WriteLine("Hand 2: Score: " + h2.EvaluateHand());
            Console.WriteLine("There are {0} cards left in the deck", deck.GetCardsRemaining());
        }
    }
}

