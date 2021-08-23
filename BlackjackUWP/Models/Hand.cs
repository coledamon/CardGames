using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public abstract class Hand
    {
        public List<Card> cardsInHand;
        public List<Card> usedCards = new List<Card>();

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
            //if (cardsInHand.Contains(card.ToString()))
            if (cardsInHand.Contains(card))
                return true;
            else
                return false;
        }

        public void DiscardHand()
        {
            cardsInHand.Clear();
        }

        public int FindCard(Card card)
        {
            //if (cardsInHand.Contains(card.ToString()))
            //    return cardsInHand.IndexOf(card.ToString());
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

        public List<Card> FindCardsByValue(string value)
        {
            return cardsInHand.FindAll(c => c.GetRank().ToString() == value);
        }
        
        public Card GetCardAtIndex(int index)
        {
            if (index >= cardsInHand.Count) return null;
            return cardsInHand[index];
        }

        public int GetNumberOfCards()
        {
            return cardsInHand.Count;
        }

        public bool IsEmpty()
        {
            if (!cardsInHand.Any())
                return true;
            else
                return false;
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

        string toStringOutput = "";
        public override string ToString()
        {
            foreach (Card cards in cardsInHand)
            {
                toStringOutput = toStringOutput + cards;
            }
            return toStringOutput;
        }
    }
}