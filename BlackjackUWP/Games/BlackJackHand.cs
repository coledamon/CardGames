using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGames
{
    public class BlackJackHand : Hand
    {
        public override int CompareTo(Hand OtherHandObject)
        {
            if (this.EvaluateHand() == OtherHandObject.EvaluateHand())
                return 0;
            else if (this.EvaluateHand() < OtherHandObject.EvaluateHand())
                return -1;
            else
                return 1;
        }

        public override int EvaluateHand()
        {
            int handTotal = CalculateHandTotal();

            if(handTotal > 21)
            {
                List<Card> cards = FindCardsByValue("A");
                foreach(Card card in cards)
                {
                    if(handTotal > 21)
                    {
                        card.GetRank().Value = 1;
                        handTotal = CalculateHandTotal();
                    }
                }
            }

            return handTotal;
        }

        private int CalculateHandTotal()
        {
            int handTotal = 0;
            for (int i = 0; i < this.GetNumberOfCards(); i++)
            {
                Card card = this.GetCardAtIndex(i);
                if (int.TryParse(card.GetRank().ToString(), out int num))
                {
                    handTotal += num;
                }
                else if (card.GetRank().ToString() == "10" || card.GetRank().ToString() == "J" || card.GetRank().ToString() == "Q" || card.GetRank().ToString() == "K")
                {
                    handTotal += 10;
                }
                else if (card.GetRank().ToString() == "A")
                {
                    handTotal += card.GetRank().Value == 1 ? 1 : 11;
                }
            }
            return handTotal;
        }

        public override string ToString()
        {
            string toStringOutput = "";
            foreach (Card cards in cardsInHand)
            {
                toStringOutput += cards + " ";
            }
            return toStringOutput;
        }

    }
}
