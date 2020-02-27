using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class ThreeKindHand : PokerHand
    {

        public static PokerHand.HandValidator ThreeKindValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.THREE_KIND;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();



        public ThreeKindHand(PlayingCard threeCard, PlayingCard kickerHigh, PlayingCard kickerLow)
        {
            ThreeCard = threeCard;
            KickerLow = kickerLow;
            KickerHigh = KickerHigh;
     
        }

        public PlayingCard ThreeCard { get; }

        public PlayingCard KickerLow { get; }

        public PlayingCard KickerHigh { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
