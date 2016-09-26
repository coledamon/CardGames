using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public abstract class Hand
    {
        //private static List<String> cardsInHand = new List<String>();
        private static List<Card> cardsInHand = new List<Card>();
        public static List<Card> usedCards = new List<Card>();
        //private static Deck cardsInDeck = new Deck();
        int indexOfCardInHand = 0;

        public void AddCard(Card card)
        {
            //cardsInHand.Add(card.ToString());
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
        public string ToString()
        {
            foreach (Card cards in cardsInHand)
            {
                toStringOutput = toStringOutput + cards + "\n";
            }
            return toStringOutput;
        }
    }
}