﻿using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Card
    {
        Suit cardSuit;
        Rank cardRank;

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
<<<<<<< HEAD
            this.cardRank;
=======
            return this.cardRank;
>>>>>>> origin/master
        }

        public Suit GetSuit()
        {
<<<<<<< HEAD
            this.cardSuit;
=======
            return this.cardSuit;
>>>>>>> origin/master
        }

        public String ToString()
        {
            return this.cardRank + " of " + this.cardSuit;
        }
    }
}