using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class StraightHand : PokerHand
    {
        public PokerHand.HandType Type => PokerHand.HandType.STRAIGHT;

        public StraightHand(int rank, OrderedCardSet cards)
        {
            CardSet = cards;
            CardRank = rank;
        }

        public OrderedCardSet CardSet { get; }
        public int CardRank { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                int compare = PokerHand.compareHandRank(this, other);
                if (compare == 0)
                {
                    StraightHand otherHand = other as StraightHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                    compare = this.CardRank - otherHand.CardRank;
               


                    return compare;

                }
                else
                {
                    return compare;
                }
            }
        }
    }
}
