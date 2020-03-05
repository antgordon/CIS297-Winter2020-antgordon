using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{


    public class PokerGame
    {

        private GameStage stage;
        public PokerGameListener GameListener {get; set;}
      
        public PokerGame(ICardDealer dealer, PokerGameListener listener) {
          
            this.GameDealer = dealer;
            Winner = WinStatus.TIE;
            Pot = 0;
            GameTurns = 0;
            HumanPlayer = new PokerPlayerHuman(0, this);
            RoboPlayer = new PokerPlayerRobo(1, this);
            GameListener = listener;
            HumanPlayer.Money = 100;
            RoboPlayer.Money = 100;
            stage = GameStage.INIT;
         
           

        }

        public GameStage Stage => stage;


        public PokerPlayerHuman HumanPlayer { get; }
        public PokerPlayerRobo RoboPlayer { get; }

        public ICardDealer GameDealer { get; }

        public WinStatus Winner { get; private set; }

        public int Pot { get; private set; }

        public int GameTurns { get; private set; }




        public void NotifyOnResponse(PokerPlayer player, PokerPlayer.PokerResponse response) {
            if (stage == GameStage.PLAYER_TURN && player == HumanPlayer)
            {
                if (response == PokerPlayer.PokerResponse.RAISE)
                {   
                    
                    player.Money -= 10;
                    Pot += 10;
                    if (GameListener != null)
                    {
                        GameListener.OnPlayerResponse(response);
                    }

                    sendRoboTurn();
                }
                else {
                    if (GameListener != null)
                    {
                        GameListener.OnPlayerResponse(response);
                    }
                    distributePot(WinStatus.PLAYER_ROBO);
                }

               
            }
            else if (stage == GameStage.OPPONENT_TURN && player == RoboPlayer) {
                if (response == PokerPlayer.PokerResponse.RAISE)
                {

                    RoboPlayer.Money -= 10;
                    Pot += 10;

                    int compare = HumanPlayer.Hand.CompareTo(RoboPlayer.Hand);
                    WinStatus win;

                    if (compare > 0)
                    {
                        win = WinStatus.PLAYER_HUMAN;
                    }
                    else if (compare < 0)
                    {
                        win = WinStatus.PLAYER_ROBO;
                    }
                    else {
                        win = WinStatus.TIE;

                    }

                    if (GameListener != null)
                    {
                        GameListener.OnOpponentResponse(response);
                    }

                    distributePot(win);
                }
                else
                {
                    if (GameListener != null)
                    {
                        GameListener.OnOpponentResponse(response);
                    }
                    distributePot(WinStatus.PLAYER_HUMAN);
                }

            }
        }


        public enum GameStage {
            INIT, SETUP, PLAYER_TURN, OPPONENT_TURN, DISTRIBUTE_POT, GAME_END,

        }

        public void distributePot(WinStatus winner) {
            stage = GameStage.DISTRIBUTE_POT;


            Winner = winner;
           
            if (winner == WinStatus.PLAYER_HUMAN)
            {
                HumanPlayer.Money += Pot;

            }
            else if (winner == WinStatus.PLAYER_ROBO)
            {
                RoboPlayer.Money += Pot;

            }
            else
            {
                RoboPlayer.Money += 20;

                HumanPlayer.Money += 20;
            }

            if (GameListener != null) {
                GameListener.PostDsitributePot();
            }
        }


        public void setupGame() {
            GameTurns += 1;
            stage = GameStage.SETUP;

            if (HumanPlayer.Money <= 0 || RoboPlayer.Money <= 0)
            {

                //Should not be needed since winner of the last round does not lose money
               /* if (RoboPlayer.Money < 0)
                {
                    Winner = WinStatus.PLAYER_HUMAN;
                }
                else if (HumanPlayer.Money < 0)
                {
                    Winner = WinStatus.PLAYER_ROBO;
                }*/

            
                endGame();
                return;
            }
            else
            {
                Pot = 20;
                HumanPlayer.Money -= 10;
                RoboPlayer.Money -= 10;

                HumanPlayer.Hand = null;
                RoboPlayer.Hand = null;
                GameDealer.ShuffleAndDealDeck(HumanPlayer, RoboPlayer);

                HumanPlayer.Hand = HoldemHand.ChooseBestHand(HumanPlayer.HandCards, GameDealer.CommunityCards);
                RoboPlayer.Hand = HoldemHand.ChooseBestHand(RoboPlayer.HandCards, GameDealer.CommunityCards);
                HumanPlayer.HandChances = HoldemHand.TestHandChances(HumanPlayer.Hand, HumanPlayer.HandCards, GameDealer);
                RoboPlayer.HandChances = HoldemHand.TestHandChances(RoboPlayer.Hand, RoboPlayer.HandCards, GameDealer);

            }

     

            if (GameListener != null)
            {
                GameListener.PostGameSetup();
            }

            sendPlayerTurn();
        }


        public void sendPlayerTurn() {

            stage = GameStage.PLAYER_TURN;
            HumanPlayer.NotifyOnTurn(GameTurns);
        }

        public void sendRoboTurn()
        {

            stage = GameStage.OPPONENT_TURN;
            RoboPlayer.NotifyOnTurn(GameTurns);
        }


        public void endGame() {
            stage = GameStage.GAME_END;
            if (GameListener != null)
            {
                GameListener.OnGameEnd();
            }
        }


       public void MoveStage() {
            switch (stage)
            {
                case GameStage.INIT:
                    setupGame();
                    break;
                case GameStage.GAME_END:
                    break;
                case GameStage.SETUP:
                    sendPlayerTurn();
                    break;
                case GameStage.PLAYER_TURN:
                    break;
                case GameStage.OPPONENT_TURN:
                    break;
                case GameStage.DISTRIBUTE_POT:
                     setupGame();
                    break;

            }
        }

    }
         
}
