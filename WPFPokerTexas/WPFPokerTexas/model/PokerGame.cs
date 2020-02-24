using System;
using System.Collections.Generic;
using System.Text;

namespace WPFPokerTexas.model
{


    public class PokerGame
    {
        private IReadOnlyCollection<PlayingCard> cardDeck;
        private GameStage stage;
        public PokerPlayer HumanPlayer { get; }
        public PokerPlayer RoboPlayer { get; }

        public ICardDealer GameDealer { get; }

        public int Pot { get; private set; }

        public int GameTurns { get; private set; }



        public PokerGame(ICollection<PlayingCard> cardDeck, ICardDealer dealer) {
            this.cardDeck = new SortedSet<PlayingCard>(cardDeck);
            this.GameDealer = dealer

        }




        public void NotifyOnResponse(PokerPlayer player, PokerPlayer.PokerResponse responce) {

        }


        public enum GameStage {
            SETUP, PLAYER_TURN, OPPONENT_TURN, DISTRIBUTE_POT, GAME_END,

        }

        public void moveStage(GameStage gameStage) {

            switch (gameStage) {

                case GameStage.GAME_END: { } break;
                case GameStage.SETUP: {
                        GameTurns += 1;

                        if (HumanPlayer.Money < 0 || RoboPlayer.Money < 0) {
                            moveStage(GameStage.GAME_END)
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

                        int compareHand = HumanPlayer.Hand.CompareTo(RoboPlayer.Hand); // make more complext to handle folds- shoudl return the integer or enum of who won

                        if (compareHand > 0)
                        {
                            HumanPlayer.Money += Pot;
                            moveStage(GameStage.SETUP);

                        }
                        else if (compareHand < 0)
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
