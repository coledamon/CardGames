using System;
using System.Collections.Generic;

namespace CardGames
{
    public abstract class Hand
    {
        List<Hand> cardsInHand = new List<Hand>();
        List<Card> usedCards = new List<Card>();
        Deck cardsInDeck;

        public void AddCard(Card card)
        {
            usedCards.Add(cardsInDeck.DealOne());
        }

        //public abstract int CompareTo(Hand OtherHandObject)
        //{
        //    if (this == OtherHandObject)
        //    {
        //        return 0;
        //    }
        //    else if (this < OtherRankObject)
        //    {
        //        return -1;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}

        //public bool ContainsCard(Card card)
        //{

        //}

    }
}