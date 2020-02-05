using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFYahtzee.model
{
    public class DiceSet: IEnumerable<GameDie>
    {
        public static readonly int DICE_COUNT = 5;
        public static readonly int DICE_FACE = 6;
        private GameDie[] dice;
        private Random random;
        public DiceSet(Random random) {

            this.random = random;
            this.dice = new GameDie[DICE_COUNT];
            for (int index = 0; index < DICE_COUNT; index += 1)
            {
                GameDie die = new GameDie(DICE_FACE, random);
                dice[index] = die;
                die.Randomize();

            }
 

        }

        public void RollDice() {
            for (int index = 0; index < DICE_COUNT; index += 1) {
                GameDie die = dice[index];
                if (!die.IsHeld) {
                    die.Randomize();
                }
            }
        }

        public IEnumerator<GameDie> GetEnumerator()
        {
            int index = 0;

            while (index < dice.Length) {
                yield return dice[index];
                index += 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int index = 0;

            while (index < dice.Length)
            {
                yield return dice[index];
                index += 1;
            }
        }

        public GameDie this[int die] { get {

                if (die < 0 || die > DICE_COUNT) {
                    throw new IndexOutOfRangeException("No die at index " + die);
                }

                return dice[die];
            }

        }


        public void ResetHeld() {
            foreach (GameDie die in DiceSet)
            {
                die.IsHeld = false;
            }
        }

    }
}
