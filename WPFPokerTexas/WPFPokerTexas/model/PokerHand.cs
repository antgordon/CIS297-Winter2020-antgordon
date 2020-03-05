using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{
    public interface PokerHand: IComparable<PokerHand>
    {
        public enum HandType
        {
            STRAIGHT_FLUSH, FOUR_KIND, FULL_HOUSE, FLUSH, STRAIGHT, THREE_KIND, TWO_PAIR, ONE_PAIR, HIGH_CARD
        }

        public HandType Type { get; }

        public OrderedCardSet CardSet { get; }
        public static int compareHandRank(PokerHand first, PokerHand second) {

            int compare = first.Type.CompareTo(second.Type);
            if (compare != 0) {
                compare =  compare * -1; //Less is more
            }


            return compare;
        }

    
    }
}
