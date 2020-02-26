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

        public void NotifyOnTurn(int id)
        {

            if (game.Stage == PokerGame.GameStage.OPPONENT_TURN) {
                CardChances.HandPlayResult result =  CardChances.TestHandChances(game.GameDealer.CommunityCards, Hand);
                if (result.Wins * 2 > result.SampleCount)
                {
                    game.NotifyOnResponse(this, PokerPlayer.PokerResponse.RAISE);
                }
                else {
                    game.NotifyOnResponse(this, PokerPlayer.PokerResponse.FOLD);
                }
            
            }
           
        }
    }

}

