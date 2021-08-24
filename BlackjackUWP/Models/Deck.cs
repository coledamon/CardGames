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

        public Deck(int numDecksToAdd = 1)
        {
            deckOfCards = new List<Card>();
            usedDeck = new List<Card>();

            for (int i = 0; i < numDecksToAdd; i++) AddFullDeck();
            Shuffle();
        }

        public void AddFullDeck()
        {
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 12; j++)
                {
                    Card cards = new Card(Suit.VALUES[i], new Rank(Rank.VALUES[j].RankName, Rank.VALUES[j].Value));
                    deckOfCards.Add(cards);
                }
            }
        }

        public void AddCard(Card card)
        {
            deckOfCards.Add(card);
        }

        public void setup()
        {
            deckOfCards[0] = new Card(Suit.VALUES[0], new Rank(Rank.VALUES[0].RankName, Rank.VALUES[0].Value));
            deckOfCards[1] = new Card(Suit.VALUES[0], new Rank(Rank.VALUES[12].RankName, Rank.VALUES[12].Value));
            deckOfCards[2] = new Card(Suit.VALUES[0], new Rank(Rank.VALUES[0].RankName, Rank.VALUES[0].Value));
            deckOfCards[3] = new Card(Suit.VALUES[0], new Rank(Rank.VALUES[1].RankName, Rank.VALUES[1].Value));
            deckOfCards[4] = new Card(Suit.VALUES[0], new Rank(Rank.VALUES[8].RankName, Rank.VALUES[8].Value));


            //deckOfCards[0] = new Card(Suit.VALUES[0], new Rank(Rank.VALUES[0].RankName, Rank.VALUES[0].Value));
            //deckOfCards[1] = new Card(Suit.VALUES[0], new Rank(Rank.VALUES[12].RankName, Rank.VALUES[12].Value));
            //deckOfCards[2] = new Card(Suit.VALUES[1], new Rank(Rank.VALUES[0].RankName, Rank.VALUES[0].Value));
            //deckOfCards[3] = new Card(Suit.VALUES[1], new Rank(Rank.VALUES[12].RankName, Rank.VALUES[12].Value));
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
                if (cards.GetRank().Value == 1) cards.GetRank().Value = 14;
            }
            Shuffle();
        }
    }
}