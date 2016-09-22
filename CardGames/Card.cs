using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Card
    {
        Suit cardSuit = new Suit();
        Rank cardRank = new Rank();

        public Card(Suit suit, Rank rank)
        {
            cardSuit = suit;
            cardRank = rank;
        }

        public int CompareTo(Card OtherCardObject)
        {
            if (this.rank == OtherRankObject.rank && this.cardSuit == OtherCardObject.suit)
            {
                return 0;
            }
            else if(this.cardRank == OtherCardObject.cardRank && this.cardSuit < OtherCardObject.cardSuit)
            {
                return -1;
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

        public Rank GetRank()
        {
            return this.cardRank;
        }

        public Suit GetSuit()
        {
            return this.cardSuit;
        }

        public String ToString()
        {
            return this.cardRank + " of " + this.cardSuit;
        }
    }
}