using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WPFPokerTexas.model.pokerhands
{
    public class HighCardHand : PokerHand
    {

 
        public PokerHand.HandType Type => PokerHand.HandType.HIGH_CARD;

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
