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
            throw new NotImplementedException();
        }
    }
}
