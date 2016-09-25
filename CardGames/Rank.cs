using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Rank
    {
        string rankName;
        int value;

        public static readonly Rank TWO;
        public static readonly Rank THREE;
        public static readonly Rank FOUR;
        public static readonly Rank FIVE;
        public static readonly Rank SIX;
        public static readonly Rank SEVEN;
        public static readonly Rank EIGHT;
        public static readonly Rank NINE;
        public static readonly Rank TEN;
        public static readonly Rank JACK;
        public static readonly Rank QUEEN;
        public static readonly Rank KING;
        public static readonly Rank ACE;

        public static readonly List<Rank> VALUES;

        //public static List<Rank> VALUES = new List<Rank>() { TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE };

        static Rank()
        {
            TWO = new Rank("TWO", 2);
            THREE = new Rank("THREE", 3);
            FOUR = new Rank("FOUR", 4);
            FIVE = new Rank("FIVE", 5);
            SIX = new Rank("SIX", 6);
            SEVEN = new Rank("SEVEN", 7);
            EIGHT = new Rank("EIGHT", 8);
            NINE = new Rank("NINE", 9);
            TEN = new Rank("TEN", 10);
            JACK = new Rank("JACK", 11);
            QUEEN = new Rank("QUEEN", 12);
            KING = new Rank("KING", 13);
            ACE = new Rank("ACE", 14);

            //public static List<Rank> VALUES = new List<Rank>() { TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE };
            VALUES = new List<Rank>() { TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE };
    }

        public Rank(string name, int rank)
        {
            this.rankName = name;
            this.value = rank;
        }

        public int CompareTo(Rank OtherRankObject)
        {
            if (this.value == OtherRankObject.value)
            {
                return 0;
            }
            else if (this.value < OtherRankObject.value)
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
            if (this.value < 11)
            {
                return this.value.ToString();
            }
            else
            {
                if (this.value == 11)
                    return "J";
                else if (this.value == 12)
                    return "Q";
                else if (this.value == 13)
                    return "K";
                else
                    return "A";

            }
        }

        public string GetName()
        {
            return this.rankName;
        }

        public override string ToString()
        {
            return this.GetSymbol(); //this.rankName;
        }
    }
}