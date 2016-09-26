using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public class Deck
    {
        List<Card> usedDeck = new List<Card>();
        List<Card> deckOfCards;
        Card dealtCard;
        //Deck emptyDeck;

        public Deck()
        { //need to decide what to do with this. Instructions : There will be one constructor: Deck(), which creates an empty deck of cards.
            deckOfCards = new List<Card>();
            //emptyDeck = new Deck();
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
            return usedDeck.Last();
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