using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class FullHouseHand : PokerHand
    {

        public static PokerHand.HandValidator FullHouseValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.FULL_HOUSE;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();

        public FullHouseHand(PlayingCard trippleHigh, PlayingCard pairLow)
        {
            TrippleHigh = trippleHigh;
            PairLow = pairLow;
        }

        public PlayingCard TrippleHigh { get; }
        public PlayingCard PairLow { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
