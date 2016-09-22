using System;
using System.Collections.Generic;

namespace CardGames
{
    public class Deck
    {
        List<Card> usedDeck = new List<Card>();

        public Deck()
        {
            List<Card> deckOfCards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            deckOfCards.Add(card);
        }
        //removed () from first
        public Card DealOne()
        {
            usedDeck.Add(deckOfCards.First);
            deckOfCards.RemoveAt(0);
            return deckOfCards.First;
        }

        public int GetDeckSize()
        {
            return deckOfCards.Count;
        }

        public bool isEmpty()
        {
            if (deckOfCards.Count == 0)
                return true;
            else
                return false;
        }

        public void Shuffle()
        {
            //var rng = new Random();
            //List<Card> shuffledDeckOfCards = deckOfCards.OrderBy(item => rng.Next());
            int n = deckOfCards.Count;
            Random rng = new Random();
            while (n > 1)
            {
                int j = (rng.Next(0, n) % n);
                n--;
                Card value = deckOfCards[j];
                deckOfCards[j] = deckOfCards[n];
                deckOfCards[n] = value;
            }
        }

        public void RestoreDeck()
        {
            deckOfCards = deckOfCards.Concat(usedDeck);
        }
    }
}