using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class FourKindHand : PokerHand
    {

        public static PokerHand.HandValidator FourKindValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.FOUR_KIND;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();


        public FourKindHand(PlayingCard fourCard, PlayingCard kicker) {
            FourCard = fourCard;
            Kicker = kicker;
        }

        public PlayingCard FourCard { get; }

        public PlayingCard Kicker { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
