using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Suit
    {
        int value;

        public static readonly Suit CLUBS;
        public static readonly Suit DIAMONDS;
        public static readonly Suit HEARTS;
        public static readonly Suit SPADES;
        //public static readonly List<Suit> VALUES;
        public static List<Suit> VALUES = new List<Suit>() { CLUBS, DIAMONDS, HEARTS, SPADES };

        static Suit()
        {
            CLUBS = new Suit(1);
            DIAMONDS = new Suit(2);
            HEARTS = new Suit(3);
            SPADES = new Suit(4);
            //VALUES = new List<Suit>() { CLUBS, DIAMONDS, HEARTS, SPADES };
        }
     
        public Suit(int card)
        {
            this.value = card;
        }

        public int CompareTo(Suit OtherSuitObject)
        {
            if (this.value == OtherSuitObject.value)
            {
                return 0;
            }
            else if (this.value < OtherSuitObject.value)
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
            switch (this.value)
            {
                case 1:
                    return "a"; //\u2663
                case 2:
                    return "b"; //u2666
                case 3:
                    return "c"; //\u2665
                case 4:
                    return "d"; //\u2660
                default:
                    return "?";
            }
        }

        public string GetName()
        {
            switch (this.value)
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

        public override string ToString()
        {
            switch (this.value)
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

