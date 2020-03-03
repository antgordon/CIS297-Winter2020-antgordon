using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class FourKindHand : PokerHand
    {
        public PokerHand.HandType Type => PokerHand.HandType.FOUR_KIND;


        public FourKindHand(int fourCardRank, int kicker) {
            FourCardRank = fourCardRank;
            Kicker = kicker;
        }

        public int FourCardRank { get; }

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
                if ( compare == 0)
                {
                   FourKindHand otherHand = other as FourKindHand;
                    if (other == null)
                    {
                        throw new ArgumentException("Not valid hand");
                    }

                    compare = this.FourCardRank - otherHand.FourCardRank;
                    if (compare == 0) {
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
