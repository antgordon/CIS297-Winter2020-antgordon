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
            throw new NotImplementedException();
        }
    }
}
