using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Suit
    {
        int suit;

        static int CLUBS = 1;
        static int DIAMONDS = 2;
        static int HEARTS = 3;
        static int SPADES = 4;
        static int VALUES; //not sure what values is supposed to be set to

        public Suit(int card)
        {
            suit = card;
        }

        public int CompareTo(Suit OtherSuitObject)
        {
            if (this.suit == OtherSuitObject.suit)
            {
                return 0;
            }
            else if (this.suit < OtherSuitObject.suit)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public string GetSymbol()
        {
            switch (this.suit)
            {
                case 1:
                    return "♣";
                case 2:
                    return "♦";
                case 3:
                    return "♥";
                case 4:
                    return "♠";
                default:
                    return "?";
            }
        }

        public string GetName()
        {
            switch (this.suit)
            {
                case 1:
                    return "CLUBS";
                case 2:
                    return "DIAMONDS";
                case 3:
                    return "HEARTS";
                case 4:
                    return "SPADES";
                default:
                    return "?";
            }
        }

        public string ToString()
        {
            return "";
        }
        /*
        static void Main()
        {
            List<Suit> suits = new List<Suit>();

            suits.Add(new Suit(CLUBS));
            suits.Add(new Suit(DIAMONDS));
            suits.Add(new Suit(HEARTS));
            suits.Add(new Suit(SPADES));

            foreach (Suit card in suits)
            {
                Console.WriteLine(card.suit);
            }
        }*/
    }
}

