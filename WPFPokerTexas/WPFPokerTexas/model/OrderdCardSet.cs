using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{
    public class OrderedCardSet: IEnumerable<PlayingCard>
    {

        private IReadOnlyList<PlayingCard> cards;
        public OrderedCardSet(ICollection<PlayingCard> cards) {

            List<PlayingCard> cardSort = new List<PlayingCard>(cards);
            cardSort.Sort(SortCards);
            cards = cardSort;
        }

        public IEnumerator<PlayingCard> GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cards.GetEnumerator();
        }

        public IReadOnlyList<PlayingCard> asList() {
            return cards;

        }

        public int Count { get => cards.Count; }

        public PlayingCard this[int index] {
            get => cards[index];
                
        }
        public static int SortCards(PlayingCard firstCard, PlayingCard secondCard)
        {

            int compare = firstCard.CompareTo(secondCard);

            if (compare == 0)
            {
                compare = (int)firstCard.CardSuit - (int)secondCard.CardSuit;
            }


            return compare;

        }
    }


 
}
