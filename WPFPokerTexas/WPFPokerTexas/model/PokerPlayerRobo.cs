namespace WPFPokerTexas.model
{

    public class PokerPlayerRobo : PokerPlayer {


        private PokerGame game;
        private int id;
        

        public PokerPlayerRobo(int id, PokerGame game) {
            this.id = id;
            this.game = game;
        }

        public int Id => id;

        public int Money { get; set; }
        public PokerHand Hand { get; set; }
        public OrderedCardSet HandCards { get; set; }
        public HoldemHand.HandPlayResult HandChances{ get; set; }

        public void NotifyOnTurn(int id)
        {

            if (game.Stage == PokerGame.GameStage.OPPONENT_TURN) {
                HoldemHand.HandPlayResult result = HandChances;
                if (result.Wins * 2 > result.SampleCount)
                {
                    RaiseHand();
                }
                else {
                    FoldHand();
                }
            
            }
           
        }
        public bool IsTurn()
        {
            return game.Stage == PokerGame.GameStage.OPPONENT_TURN;
        }

        public void FoldHand()
        {
            if (game.Stage == PokerGame.GameStage.OPPONENT_TURN)
            {
                game.NotifyOnResponse(this, PokerPlayer.PokerResponse.FOLD);

            }
        }

        public void RaiseHand()
        {
            if (game.Stage == PokerGame.GameStage.OPPONENT_TURN)
            {
                game.NotifyOnResponse(this, PokerPlayer.PokerResponse.RAISE);

            }
        }

    }

}

