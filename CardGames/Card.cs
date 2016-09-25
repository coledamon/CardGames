using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Card
    {
        Suit cardSuit; //Suit
        Rank cardRank; //Rank
        //string cardRank;

        public Card(Suit suit, Rank rank)
        {
            cardSuit = suit;
            cardRank = rank;
        }

        public int CompareTo(Card OtherCardObject)
        {
            if (this.cardRank.CompareTo(OtherCardObject.cardRank) == 0 &&  this.cardSuit.CompareTo(OtherCardObject.cardSuit) == 0)
            {
                return 0;
            }
            else if(this.cardRank.CompareTo(OtherCardObject.cardRank) == 0 && this.cardSuit.CompareTo(OtherCardObject.cardSuit) == -1)
            {
                return -1;
            }
            else if (this.cardRank.CompareTo(OtherCardObject.cardRank) == -1)
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

        public override string ToString()
        {
            return this.cardRank.ToString() + this.cardSuit.ToString();
        }
    }
}