using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{
    public interface PokerPlayer
    {

        public enum PokerResponse { RAISE, FOLD}

        int Id { get; }

        int Money { get; set; }

        PokerHand Hand { get; set; }

        void NotifyOnTurn(int id);

        public OrderedCardSet HandCards { get; set; }

        public HoldemHand.HandPlayResult HandChances { get; set; }

        public bool IsTurn();
    

        public void FoldHand();

        public void RaiseHand();
    }
}
