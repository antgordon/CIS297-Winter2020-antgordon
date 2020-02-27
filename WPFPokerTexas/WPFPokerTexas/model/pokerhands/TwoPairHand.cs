using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class TwoPairHand : PokerHand

    {
        public static PokerHand.HandValidator TwoPairValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.TWO_PAIR;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();


        public TwoPairHand(PlayingCard pairHigh, PlayingCard pairLow, PlayingCard kicker) {
            PairHigh = pairHigh;
            PairLow = pairLow;
            Kicker = kicker;
        }

        public PlayingCard PairHigh { get; }
        public PlayingCard PairLow { get; }
        public PlayingCard Kicker { get; }

        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
