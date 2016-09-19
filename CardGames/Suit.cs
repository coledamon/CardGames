using System;

namespace CardGames
{
    public class Suit
    {
        int suit;

        static int TWO = 2;
        static int THREE = 3;
        static int FOUR = 4;
        static int FIVE = 5;
        static int SIX = 6;
        static int SEVEN = 7;
        static int EIGHT = 8;
        static int NINE = 9;
        static int TEN = 10;
        static int JACK = 11;
        static int QUEEN = 12;
        static int KING = 13;
        static int ACE = 14;

        public Rank(int card)
        {
            rank = card;
        }

        public int CompareTo(Rank OtherRankObject)
        {
            if (this.rank == OtherRankObject.rank)
            {
                return 0;
            }
            else if (this.rank < OtherRankObject.rank)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        public string GetSymbol(Rank rank)
        {
            switch (this.rank)
            {
                case 2:
                    return "2";
                case 3:
                    return "3";
                case 4:
                    return "4";
                case 5:
                    return "5";
                case 6:
                    return "6";
                case 7:
                    return "7";
                case 8:
                    return "8";
                case 9:
                    return "9";
                case 10:
                    return "10";
                case 11:
                    return "J";
                case 12:
                    return "Q";
                case 13:
                    return "K";
                case 14:
                    return "A";
                default:
                    return "?";
            }
        }

        public string GetName()
        {
            switch (this.rank)
            {
                case 2:
                    return "TWO";
                case 3:
                    return "THREE";
                case 4:
                    return "FOUR";
                case 5:
                    return "FIVE";
                case 6:
                    return "SIX";
                case 7:
                    return "SEVEN";
                case 8:
                    return "EIGHT";
                case 9:
                    return "NINE";
                case 10:
                    return "TEN";
                case 11:
                    return "JACK";
                case 12:
                    return "QUEEN";
                case 13:
                    return "KING";
                case 14:
                    return "ACE";
                default:
                    return "Invalid";
            }
        }

        public string ToString()
        {

        }

        static void Main()
        {
            List<Rank> ranks = new List<Rank>();

            ranks.Add(new Rank(TWO));
            ranks.Add(new Rank(THREE));
            ranks.Add(new Rank(FOUR));
            ranks.Add(new Rank(FIVE));
            ranks.Add(new Rank(SIX));
            ranks.Add(new Rank(SEVEN));
            ranks.Add(new Rank(EIGHT));
            ranks.Add(new Rank(NINE));
            ranks.Add(new Rank(TEN));
            ranks.Add(new Rank(JACK));
            ranks.Add(new Rank(QUEEN));
            ranks.Add(new Rank(KING));
            ranks.Add(new Rank(ACE));

            foreach (Rank card in ranks)
            {
                Console.WriteLine(card.rank);
            }
        }
    }
}

