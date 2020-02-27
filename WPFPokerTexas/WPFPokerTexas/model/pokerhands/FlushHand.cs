using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class FlushHand : PokerHand
    {

        public static PokerHand.HandValidator FlushValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.STRAIGHT_FLUSH;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();


        public FlushHand(PlayingCard.Suit suit, OrderedCardSet cards)
        {
            CardSuit = suit;
            CardSet = cards;

        }

        public PlayingCard.Suit CardSuit { get; }

        public OrderedCardSet CardSet { get; }

        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
