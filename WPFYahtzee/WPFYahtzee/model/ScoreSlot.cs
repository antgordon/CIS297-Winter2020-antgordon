using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFYahtzee.model
{


    public class ScoreSlot
    {
        public delegate bool ScoreSlotQualifier(DiceSet set);
        public delegate int ScoreSlotPotential(DiceSet set);

        public int Id { get; }

        public ScoreSlot(int id)
        {
            Id = id;
        }
        public int? Score { get; set; }
        public  ScoreSlotQualifier Qualifier { get; set; }

        public ScoreSlotPotential ScorePotential{ get; set; }



    }
}
