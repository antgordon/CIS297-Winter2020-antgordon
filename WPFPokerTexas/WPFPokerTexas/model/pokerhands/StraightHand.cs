using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class StraightHand : PokerHand
    {
        public PokerHand.HandType Type => PokerHand.HandType.STRAIGHT;

        public StraightHand(int rank)
        {
            CardRank = rank;
        }

        public int CardRank { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
