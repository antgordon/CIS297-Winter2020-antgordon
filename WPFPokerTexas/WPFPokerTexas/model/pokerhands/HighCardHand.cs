using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class HighCardHand : PokerHand
    {

        public static PokerHand.HandValidator HighCardValidator = (OrderedCardSet cards, out PokerHand hand) => { hand = null; return false; };
        public PokerHand.HandType Type => PokerHand.HandType.HIGH_CARD;

        public PokerHand.HandValidator Validator => throw new NotImplementedException();


        public HighCardHand(OrderedCardSet cards) {
            CardSet = cards;
        
        }


        public OrderedCardSet CardSet { get; }
        public int CompareTo([AllowNull] PokerHand other)
        {
            throw new NotImplementedException();
        }
    }
}
