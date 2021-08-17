using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public abstract class Hand
    {
        public List<Card> cardsInHand;
        //public List<Card> usedCards = new List<Card>();

        int indexOfCardInHand = 0;

        public Hand()
        {
            cardsInHand = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cardsInHand.Add(card);
        }

        public abstract int CompareTo(Hand OtherHandObject);

        public bool ContainsCard(Card card)
        {
            return cardsInHand.Contains(card);

        }

        public void DiscardHand()
        {
            cardsInHand.Clear();
        }

        public int FindCard(Card card)
        {

            foreach (Card cards in cardsInHand)
            {
                if (cards.Equals(card))
                {
                    return indexOfCardInHand;
                }
                else
                    indexOfCardInHand++;
            }
            return -1;
        }
        
        public Card GetCardAtIndex(int index)
        {
            return cardsInHand[index];
        }

        public int GetNumberOfCards()
        {
            return cardsInHand.Count;
        }

        public bool IsEmpty()
        {
            return !cardsInHand.Any();
            //if (!cardsInHand.Any())
            //    return true;
            //else
            //    return false;
        }

        public Card RemoveCard(Card card)
        {
            cardsInHand.RemoveAt(FindCard(card));
            return GetCardAtIndex(FindCard(card));
        }

        public int RemoveCard(int intRemove)
        {
            cardsInHand.RemoveAt(intRemove);
            return intRemove;
        }

        public abstract int EvaluateHand();

        
        public override string ToString()
        {
            string toStringOutput = "";
            foreach (Card card in cardsInHand)
            {
                toStringOutput +=  card + " ";
            }
            return toStringOutput;
        }
    }
}