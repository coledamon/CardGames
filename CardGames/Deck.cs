using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public class Deck
    {
        public static List<Card> usedDeck;
        public static List<Card> deckOfCards;
        public static Card dealtCard;

        static Deck()
        {
            deckOfCards = new List<Card>();
            usedDeck = new List<Card>();
        }

        public void AddCard(Card card)
        {
            deckOfCards.Add(card);
        }

        public Card DealOne()
        {
            dealtCard = deckOfCards[0];
            usedDeck.Add(dealtCard);
            deckOfCards.RemoveAt(0);
            return dealtCard;
        }

        public int GetCardsRemaining()
        {
            return deckOfCards.Count;
        }

        public int GetDeckSize()
        {
            return deckOfCards.Count + usedDeck.Count;
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
            foreach(var cards in usedDeck)
            {
                deckOfCards.Add(cards);
            }
        }
    }
}