namespace WPFPokerTexas.model
{

    public class PokerPlayerHuman : PokerPlayer {


        private PokerGame game;
        private int id;

        public PokerPlayerHuman(int id, PokerGame game) {
            this.id = id;
            this.game = game;
        }

        public int Id => id;

        public int Money { get; set; }
        public PokerHand Hand { get; set; }
        public OrderedCardSet HandCards { get; set; }

        public HoldemHand.HandPlayResult HandChances { get; set; }


        public bool IsTurn() {
            return game.Stage == PokerGame.GameStage.PLAYER_TURN;
        }

        public void FoldHand() {
            if (game.Stage == PokerGame.GameStage.PLAYER_TURN)
            {
                game.NotifyOnResponse(this, PokerPlayer.PokerResponse.FOLD);

            }
        }

        public void RaiseHand()
        {
            if (game.Stage == PokerGame.GameStage.PLAYER_TURN)
            {
                game.NotifyOnResponse(this, PokerPlayer.PokerResponse.RAISE);

            }
        }


        public void NotifyOnTurn(int id)
        {

        
           
        }
    }

}

