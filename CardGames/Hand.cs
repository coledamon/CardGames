using System;
using System.Collections.Generic;

namespace CardGames
{
    public abstract class Hand
    {
        private static List<Hand> cardsInHand = new List<Hand>();
        private static List<Card> usedCards = new List<Card>();
        private static Deck cardsInDeck = new Deck();

        public static void AddCard(Card card)
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