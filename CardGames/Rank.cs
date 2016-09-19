using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Rank
    {
        int rank;

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