using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class StraightFlushHand : PokerHand
    {

        public static PokerHand.HandValidator StraightFlushValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.STRAIGHT_FLUSH;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();



        public StraightFlushHand(int rank, PlayingCard.Suit suit) {
            CardRank = rank;
            CardSuit = suit;
        }

        public int CardRank { get; }
        public PlayingCard.Suit CardSuit { get; }

        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
