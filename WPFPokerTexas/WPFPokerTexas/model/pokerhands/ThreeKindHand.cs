using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class ThreeKindHand : PokerHand
    {
        public PokerHand.HandType Type => PokerHand.HandType.THREE_KIND;

        public ThreeKindHand(int threeCard, int kickerHigh, int kickerLow)
        {
            ThreeCard = threeCard;
            KickerLow = kickerLow;
            KickerHigh = KickerHigh;
     
        }

        public int ThreeCard { get; }

        public int KickerLow { get; }

        public int KickerHigh { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
