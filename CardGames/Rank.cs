using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Rank
    {
        string rankName;
        int value;

        static Rank TWO = new Rank("TWO", 2);
        static Rank THREE = new Rank("THREE", 3);
        static Rank FOUR = new Rank("FOUR", 4);
        static Rank FIVE = new Rank("FIVE", 5);
        static Rank SIX = new Rank("SIX", 6);
        static Rank SEVEN = new Rank("SEVEN", 7);
        static Rank EIGHT = new Rank("EIGHT", 8);
        static Rank NINE = new Rank("NINE", 9);
        static Rank TEN = new Rank("TEN", 10);
        static Rank JACK = new Rank("JACK", 11);
        static Rank QUEEN = new Rank("QUEEN", 12);
        static Rank KING = new Rank("KING", 13);
        static Rank ACE = new Rank("ACE", 14);

        static List<Rank> VALUES = new List<Rank>()
        {
            TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
        };


        public Rank(string name, int rank)
        {
            rankName = name;
            value = rank;
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
                return value.ToString();
            }
            else
            {
                if (value == 11)
                    return "J";
                else if (value == 12)
                    return "Q";
                else if (value == 13)
                    return "K";
                else
                    return "A";

            }
        }

        public string GetName()
        {
            return rankName;
        }

        public string ToString()
        {
            return rankName;
        }
    }
}