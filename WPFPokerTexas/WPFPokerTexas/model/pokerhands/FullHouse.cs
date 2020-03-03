using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class FullHouseHand : PokerHand
    {

     
        public PokerHand.HandType Type => PokerHand.HandType.FULL_HOUSE;

    
        public FullHouseHand(int trippleHigh, int pairLow)
        {
            TripleHigh = trippleHigh;
            PairLow = pairLow;
        }

        public int TripleHigh { get; }
        public int  PairLow { get; }
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
                    FullHouseHand otherHand = other as FullHouseHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                    compare = this.TripleHigh - otherHand.TripleHigh;
                    if (compare == 0)
                    {
                        compare = this.PairLow - otherHand.PairLow;
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
