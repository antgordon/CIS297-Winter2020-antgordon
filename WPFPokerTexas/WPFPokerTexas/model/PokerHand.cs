using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{
    public interface PokerHand: IComparable<PokerHand>
    {
        public delegate bool HandValidator(OrderedCardSet cards, out PokerHand pHand);

        public enum HandType
        {
            STRAIGHT_FLUSH, FOUR_KIND, FULL_HOUSE, FLUSH, STRAIGHT, THREE_KIND, TWO_PAIR, ONE_PAIR, HIGH_CARD
        }

        public HandType Type { get; }

        public HandValidator Validator { get; }

    
    }
}
