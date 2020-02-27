using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class OnePairHand : PokerHand
    {

        public static PokerHand.HandValidator OnePairValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.ONE_PAIR;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();


        public OnePairHand(PlayingCard pair, OrderedCardSet kickers) {
            Pair = pair;
            Kickers = kickers;
        }

        public PlayingCard Pair { get; }

        public OrderedCardSet Kickers { get; }

        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
