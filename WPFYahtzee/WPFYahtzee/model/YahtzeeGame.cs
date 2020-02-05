using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFYahtzee.model
{
   public  class YahtzeeGame
    {

        public DiceSet DiceSet { get; }
        public PlayerState Player1State { get;  }

        public YahtzeeGame() {
            DiceSet = new DiceSet(new Random());
            Player1State = new PlayerState(DiceSet, new ScoreCard());

        }
       

}
