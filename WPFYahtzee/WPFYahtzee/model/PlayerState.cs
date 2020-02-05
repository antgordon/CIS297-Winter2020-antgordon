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


        public bool CanThrow() { return Throws < 3; }
        public bool CanHoldDice()
        {
            return Throws > 0;
        }

        public bool CanScore()
        {
            return Throws > 0;
        }


        public void ThrowDice() 
        {
            DiceSet.RollDice();
            Throws += 1;
        }


        public void Reset() 
        {
            Throws = 0;
            DiceSet.ResetHeld();
           
        }
    }
}
