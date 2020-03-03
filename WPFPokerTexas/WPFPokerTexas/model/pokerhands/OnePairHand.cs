using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class OnePairHand : PokerHand
    {

        public PokerHand.HandType Type => PokerHand.HandType.ONE_PAIR;



        public OnePairHand(int pair, int kickerHigh, int kickerMed, int kickerLow) {
            Pair = pair;
            KickerHigh = kickerHigh;
            KickerMed = kickerMed;
            KickerLow = kickerLow;
        }

        public int Pair { get; }
        public int KickerHigh { get; }
        public int KickerMed{ get; }
        public int KickerLow { get; }


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
                    OnePairHand otherHand = other as OnePairHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                     compare = this.Pair - otherHand.Pair;
                    if (compare == 0)
                    {
                        compare = this.KickerHigh - otherHand.KickerHigh;
                    }

                    if (compare == 0)
                    {
                        compare = this.KickerMed - otherHand.KickerMed;
                    }


                    if (compare == 0)
                    {
                        compare = this.KickerLow- otherHand.KickerLow;
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
