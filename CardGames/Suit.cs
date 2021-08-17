using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Suit
    {
        public int Value { get; set; }

        public static readonly Suit CLUBS;
        public static readonly Suit DIAMONDS;
        public static readonly Suit HEARTS;
        public static readonly Suit SPADES;

        public static readonly List<Suit> VALUES;

        static Suit()
        {
            CLUBS = new Suit(1);
            DIAMONDS = new Suit(2);
            HEARTS = new Suit(3);
            SPADES = new Suit(4);
            VALUES = new List<Suit>() { CLUBS, DIAMONDS, HEARTS, SPADES };
        }
     
        public Suit(int card)
        {
            this.Value = card;
        }

        public int CompareTo(Suit OtherSuitObject)
        {
            if (this.Value == OtherSuitObject.Value)
            {
                return 0;
            }
            else if (this.Value < OtherSuitObject.Value)
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
            switch (this.Value)
            {
                case 1:
                    return "♣"; //\u2663
                case 2:
                    return "♦"; //\u2666
                case 3:
                    return "♥"; //\u2665
                case 4:
                    return "♠"; //\u2660
                default:
                    return "?";
            }
        }

        public string GetName()
        {
            switch (this.Value)
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

            return this.GetSymbol();

        }
    }
}

