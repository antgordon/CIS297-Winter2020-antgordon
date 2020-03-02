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
            throw new NotImplementedException();
        }
    }
}
