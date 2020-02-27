using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class StraightHand : PokerHand
    {

        public static PokerHand.HandValidator StraightValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.STRAIGHT;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();

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
