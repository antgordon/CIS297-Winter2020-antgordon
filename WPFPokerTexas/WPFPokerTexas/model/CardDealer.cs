using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{
    public interface ICardDealer
    {

        public OrderedCardSet CommunityCards { get; }

        public OrderedCardSet RemainingDeck { get; }

        public OrderedCardSet FullDeck { get; }

        public void ShuffleAndDealDeck(PokerPlayer playerOne, PokerPlayer player2);


    }
}
