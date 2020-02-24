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
            cardSort.Sort();
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

        public IReadOnlyList<PlayingCard> asCollection() {
            return cards;

        }

        public int Count { get => cards.Count; }

        public PlayingCard this[int index] {
            get => cards[index];
                
        }
    }
}
