using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class ThreeKindHand : PokerHand
    {
        public PokerHand.HandType Type => PokerHand.HandType.THREE_KIND;

        public ThreeKindHand(int threeCard, int kickerHigh, int kickerLow, OrderedCardSet cards)
        {
            CardSet = cards;
            ThreeCard = threeCard;
            KickerLow = kickerLow;
            KickerHigh = KickerHigh;
     
        }

        public OrderedCardSet CardSet { get;  }

        public int ThreeCard { get; }

        public int KickerLow { get; }

        public int KickerHigh { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                int compare = PokerHand.compareHandRank(this, other);
                if ( compare == 0)
                {
                    ThreeKindHand otherHand = other as ThreeKindHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                     compare = this.ThreeCard - otherHand.ThreeCard;
                    if (compare == 0)
                    {
                        compare = this.KickerHigh - otherHand.KickerHigh;
                    }


                    if (compare == 0)
                    {
                        compare = this.KickerLow - otherHand.KickerLow;
                    }



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
