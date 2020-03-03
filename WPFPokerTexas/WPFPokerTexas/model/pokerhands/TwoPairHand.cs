using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class TwoPairHand : PokerHand
    {
        public PokerHand.HandType Type => PokerHand.HandType.TWO_PAIR;
        public TwoPairHand(int pairHigh, int pairLow ,int kicker) {
            PairHigh = pairHigh;
            PairLow = pairLow;
            Kicker = kicker;
        }

        public int PairHigh { get; }
        public int PairLow { get; }
        public int Kicker { get; }

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
                    TwoPairHand otherHand = other as TwoPairHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                    compare = this.PairHigh - otherHand.PairHigh;
                    if (compare == 0)
                    {
                        compare = this.PairLow - otherHand.PairLow;
                    }

                    if (compare == 0)
                    {
                        compare = this.Kicker - otherHand.Kicker;
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
