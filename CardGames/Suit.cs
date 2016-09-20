using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Suit
    {
        int suit;

        static Suit CLUBS = new Suit(1);
        static Suit DIAMONDS = new Suit(2);
        static Suit HEARTS = new Suit(3);
        static Suit SPADES = new Suit(4);
        List<Suit> VALUES = new List<Suit>()
        {
            CLUBS, DIAMONDS, HEARTS, SPADES
        };

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
    }
}

