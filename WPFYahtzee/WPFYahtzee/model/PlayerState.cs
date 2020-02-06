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
            this.ScoreCard = scoreCard;
        }
        public int Throws { get; private set; }


        public bool CanThrow() { return !ScoreCard.IsFullCard() && Throws < 3; }
        public bool CanHoldDice()
        {
            return !ScoreCard.IsFullCard() && Throws > 0;
        }

        public bool CanScore()
        {
            return !ScoreCard.IsFullCard() &&  Throws > 0;
        }


        public void ThrowDice() 
        {
            DiceSet.RollDice();
            Throws += 1;
        }



        public void UseScore(ScoreSlot slot, DiceSet set)
        {

            ScoreCard.UseScore(slot, set);
            Reset();
        }
        public void Reset() 
        {
            Throws = 0;
            DiceSet.ResetHeld();
           
        }
    }
}
