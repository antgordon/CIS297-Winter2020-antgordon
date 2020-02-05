using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFYahtzee.model
{
    public class PlayerState
    {

        public DiceSet DiceSet { get; }
        public ScoreCard ScoreCard { get; }

        public PlayerState(DiceSet set, ScoreCard scoreCard) 
        {
            this.DiceSet = set;
            this.ScoreCard = ScoreCard;
        }
        public int Throws { get; private set; }


        public bool canThrow() { return Throws < 3; }
        public bool canHoldDice()
        {
            return Throws > 0;
        }

        public bool canScore()
        {
            return Throws > 0;
        }


        public void throwDice() {
            DiceSet.RollDice();
            Throws += 1;
        }


        public void reset() {
            Throws = 0;
            DiceSet.ResetHeld();
           
        }
    }
}
