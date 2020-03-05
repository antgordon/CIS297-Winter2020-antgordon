using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{
    public interface PokerGameListener
    {

        void PostGameSetup();
        void OnPlayerResponse(PokerPlayer.PokerResponse response);
        void OnOpponentResponse(PokerPlayer.PokerResponse response);
        void PostDsitributePot();
        void OnGameEnd();

    }
}
