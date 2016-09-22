using System;
using System.Collections.Generic;

namespace CardGames
{
    public abstract class Hand
    {
        List<Hand> cardsInHand = new List<Hand>();
        List<Hand> usedCards = new List<Hand>();

        public void AddCard(Card card)
        {
            card.DealOne();
            usedCards.Add(card.DealOne());
        }

        public abstract int CompareTo(Hand OtherHandObject)
        {
            if (this == OtherHandObject)
            {
                return 0;
            }
            else if (this < OtherRankObject)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

    }
}