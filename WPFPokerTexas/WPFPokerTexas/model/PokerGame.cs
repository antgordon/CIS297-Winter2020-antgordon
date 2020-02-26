using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{


    public class PokerGame
    {
      
        private GameStage stage;
      
        public PokerGame(ICardDealer dealer) {
          
            this.GameDealer = dealer;
            Winner = WinStatus.TIE;
            Pot = 0;
            GameTurns = 0;
            HumanPlayer = new PokerPlayerHuman(0, this);
            RoboPlayer = new PokerPlayerRobo(1, this);
            setupGame();
        }

        public GameStage Stage => stage;


        public PokerPlayerHuman HumanPlayer { get; }
        public PokerPlayerRobo RoboPlayer { get; }

        public ICardDealer GameDealer { get; }

        public WinStatus Winner { get; private set; }

        public int Pot { get; private set; }

        public int GameTurns { get; private set; }




        public void NotifyOnResponse(PokerPlayer player, PokerPlayer.PokerResponse responce) {
            if (stage == GameStage.PLAYER_TURN && player == HumanPlayer)
            {
                if (responce == PokerPlayer.PokerResponse.RAISE)
                {   
                    sendRoboTurn();
                    player.Money -= 10;
                    Pot += 10;
                }
                else {
                    distributePot(WinStatus.PLAYER_ROBO);
                }
            }
            else if (stage == GameStage.OPPONENT_TURN && player == RoboPlayer) {
                if (responce == PokerPlayer.PokerResponse.RAISE)
                {

                    player.Money -= 10;
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

                    distributePot(win);
                }
                else
                {
                    distributePot(WinStatus.PLAYER_HUMAN);
                }

            }
        }


        public enum GameStage {
            SETUP, PLAYER_TURN, OPPONENT_TURN, DISTRIBUTE_POT, GAME_END,

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
                throw new Exception("Tie???");
            }
        }


        public void setupGame() {
            GameTurns += 1;
            stage = GameStage.SETUP;

            if (HumanPlayer.Money < 0 || RoboPlayer.Money < 0)
            {

                //Should be needed since winner of the last round does not lose money
               /* if (RoboPlayer.Money < 0)
                {
                    Winner = WinStatus.PLAYER_HUMAN;
                }
                else if (HumanPlayer.Money < 0)
                {
                    Winner = WinStatus.PLAYER_ROBO;
                }*/

            
                endGame();
            }
            else
            {
                Pot = 20;
                HumanPlayer.Money -= 10;
                RoboPlayer.Money -= 10;

                HumanPlayer.Hand = null;
                RoboPlayer.Hand = null;
                GameDealer.ShuffleAndDealDeck(HumanPlayer, RoboPlayer);

                HumanPlayer.Hand = CardChances.ChooseBestHand(HumanPlayer.HandCards, GameDealer.CommunityCards);
                RoboPlayer.Hand = CardChances.ChooseBestHand(RoboPlayer.HandCards, GameDealer.CommunityCards);
              
            }
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
        }


        private void moveStage() {
            switch (stage)
            {

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

        private void moveStage(GameStage gameStage) {

            switch (gameStage) {

                case GameStage.GAME_END: { } break;
                case GameStage.SETUP: {
                        GameTurns += 1;

                        if (HumanPlayer.Money < 0 || RoboPlayer.Money < 0) {
                            moveStage(GameStage.GAME_END);
                        } else {
                            Pot = 20;
                            HumanPlayer.Money -= 10;
                            RoboPlayer.Money -= 10;

                            HumanPlayer.Hand = null;
                            RoboPlayer.Hand = null;
                            GameDealer.ShuffleAndDealDeck(HumanPlayer, RoboPlayer);

                            HumanPlayer.Hand = CardChances.ChooseBestHand(HumanPlayer.HandCards, GameDealer.CommunityCards);
                            RoboPlayer.Hand = CardChances.ChooseBestHand(RoboPlayer.HandCards, GameDealer.CommunityCards);
                            moveStage(GameStage.PLAYER_TURN);

                        }


                    } break;
                case GameStage.PLAYER_TURN: {
                        HumanPlayer.NotifyOnTurn(GameTurns);
                    } break;
                case GameStage.OPPONENT_TURN: {

                        RoboPlayer.NotifyOnTurn(GameTurns);
                    } break;

                case GameStage.DISTRIBUTE_POT: {
         

                        if (Winner == WinStatus.PLAYER_HUMAN)
                        {
                            HumanPlayer.Money += Pot;
                            moveStage(GameStage.SETUP);

                        }
                        else if (Winner == WinStatus.PLAYER_ROBO)
                        {
                            RoboPlayer.Money += Pot;
                            moveStage(GameStage.SETUP);
                        }
                        else {
                            throw new Exception("Tie???");
                        }


                    } break;
  
            }

        }
    }
         
}
