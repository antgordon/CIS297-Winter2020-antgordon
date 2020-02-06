using System;
using System.Collections.Generic;
using System.Text;

namespace WPFYahtzee.model
{
    public class ScoreCard
    {

       
        public ScoreSlot SLOT_ONES { get; }
        public ScoreSlot SLOT_TWOS { get; }
        public ScoreSlot SLOT_THREES { get; }
        public ScoreSlot SLOT_FOURS { get; }
        public ScoreSlot SLOT_FIVES { get; }
        public ScoreSlot SLOT_SIXES { get; }
        public ScoreSlot SLOT_THREE_OF_KIND { get; }
        public ScoreSlot SLOT_FOUR_OF_KIND { get; }
        public ScoreSlot SLOT_FULL_HOUSE { get; }
        public ScoreSlot SLOT_SMALL_STRAIGHT { get; }
        public ScoreSlot SLOT_LARGE_STRAIGHT { get; }
        public ScoreSlot SLOT_CHANCE { get; }
        public ScoreSlot SLOT_YAHTZEE { get; }


        public ScoreCard() {

            SLOT_ONES = createUpperSlot(1,1);
            SLOT_TWOS = createUpperSlot(2,2);
            SLOT_THREES = createUpperSlot(3,3);
            SLOT_FOURS = createUpperSlot(4,4);
            SLOT_FIVES = createUpperSlot(5,5);
            SLOT_SIXES = createUpperSlot(6,6);

            SLOT_THREE_OF_KIND = new ScoreSlot(11)
            {
                Qualifier = (diceset) =>
                {
                    int[] count = new int[DiceSet.DICE_FACE + 1];
                    foreach (GameDie die in diceset)
                    {

                        if (die.Number >= 1 && die.Number <= DiceSet.DICE_FACE)
                            count[die.Number] += 1;
                    }

                    for (int index = 1; index < DiceSet.DICE_FACE + 1; index += 1)
                    {
                        if (count[index] >= 3)
                        {

                            return true;
                        }
                    }

                    return false;
                },
                ScorePotential = (diceset) => calcDiceSum(diceset)
            };

            SLOT_FOUR_OF_KIND = new ScoreSlot(12)
            {
                Qualifier = (diceset) =>
                {
                    int[] count = new int[DiceSet.DICE_FACE + 1];
                    foreach (GameDie die in diceset)
                    {

                        if (die.Number >= 1 && die.Number <= DiceSet.DICE_FACE)
                            count[die.Number] += 1;
                    }

                    for (int index = 1; index < DiceSet.DICE_FACE + 1; index += 1)
                    {
                        if (count[index] >= 4)
                        {

                            return true;
                        }
                    }

                    return false;
                },

                ScorePotential = (diceset) => calcDiceSum(diceset)

            };

            SLOT_FULL_HOUSE = new ScoreSlot(13)
            {
                Qualifier = (diceset) =>
                {
                    int[] count = new int[DiceSet.DICE_FACE + 1];
                    foreach (GameDie die in diceset)
                    {
                        if (die.Number >= 1 && die.Number <= DiceSet.DICE_FACE)
                            count[die.Number] += 1;

                    }

                    bool two = false;
                    bool three = false;

                    for (int index = 1; index < DiceSet.DICE_FACE + 1; index += 1)
                    {
                        if (count[index] == 2)
                        {
                            two = true;
                        }
                        else if (count[index] == 3)
                        {
                            three = true;
                        }
                    }

                    return two && three;
                },

                ScorePotential = (diceset) => 25

            };

            SLOT_SMALL_STRAIGHT = new ScoreSlot(14)
            {
                Qualifier = (diceset) =>
                {
                    int[] count = new int[DiceSet.DICE_FACE + 1];
                    foreach (GameDie die in diceset)
                    {
                        if (die.Number >= 1 && die.Number <= DiceSet.DICE_FACE)
                            count[die.Number] += 1;

                    }


                    int inSeq = 0;


                    for (int index = 1; index < DiceSet.DICE_FACE + 1; index += 1)
                    {
                        if (count[index] == 0)
                        {
                            inSeq = 0;
                        }
                        else {
                            inSeq += 1;

                            if (inSeq >= 4)
                                return true;
                        }

                    }

                    return inSeq >= 3;
                },

                ScorePotential = (diceset) => 30
            };

            SLOT_LARGE_STRAIGHT = new ScoreSlot(15)
            {
                Qualifier = (diceset) =>
                {
                    int[] count = new int[DiceSet.DICE_FACE + 1];
                    foreach (GameDie die in diceset)
                    {
                        if (die.Number >= 1 && die.Number <= DiceSet.DICE_FACE)
                            count[die.Number] += 1;

                    }


                    int inSeq = 0;


                    for (int index = 1; index < DiceSet.DICE_FACE + 1; index += 1)
                    {
                        if (count[index] == 0)
                        {
                            inSeq = 0;
                        }
                        else
                        {
                            inSeq += 1;

                            if (inSeq >= 5)
                                return true;
                        }

                    }

                    return inSeq >= 3;
                },

                ScorePotential = (diceset) => 40

            };

            SLOT_CHANCE = new ScoreSlot(16)
            {
                Qualifier = (diceset) => true,

                ScorePotential = (diceset) => calcDiceSum(diceset)
            };

            SLOT_YAHTZEE = new ScoreSlot(17)
            {
                Qualifier = (diceset) =>
                {
                    int[] count = new int[DiceSet.DICE_FACE + 1];
                    foreach (GameDie die in diceset)
                    {
                        if (die.Number >= 1 && die.Number <= DiceSet.DICE_FACE)
                            count[die.Number] += 1;

                    }


                    for (int index = 1; index < DiceSet.DICE_FACE + 1; index += 1)
                    {
                        if (count[index] == 5)
                        {
                            return true;
                        }

                    }

                    return false;
                },

                ScorePotential = (diceset) => 50
            };
        }


        public bool IsFullCard() {

            bool topFull = SLOT_ONES.Score.HasValue
                && SLOT_TWOS.Score.HasValue
                && SLOT_THREES.Score.HasValue
                && SLOT_FOURS.Score.HasValue
                && SLOT_FIVES.Score.HasValue
                && SLOT_SIXES.Score.HasValue;

            bool bottomFull = SLOT_THREE_OF_KIND.Score.HasValue
                && SLOT_FOUR_OF_KIND.Score.HasValue
                && SLOT_FULL_HOUSE.Score.HasValue
                && SLOT_SMALL_STRAIGHT.Score.HasValue
                && SLOT_LARGE_STRAIGHT.Score.HasValue
                && SLOT_CHANCE.Score.HasValue
                && SLOT_YAHTZEE.Score.HasValue;

            return topFull && bottomFull;
        }



        public void UseScore(ScoreSlot slot, DiceSet set) {

            if (slot == SLOT_YAHTZEE) { } //Add bonus here

            if (!slot.Score.HasValue) {
                slot.Score = slot.ScorePotential(set);
            }
        }

        public int GetUpperSum()
        {

            int sum = 0;
            sum += SLOT_ONES.Score.GetValueOrDefault();
            sum += SLOT_TWOS.Score.GetValueOrDefault();
            sum += SLOT_THREES.Score.GetValueOrDefault();
            sum += SLOT_FOURS.Score.GetValueOrDefault();
            sum += SLOT_FIVES.Score.GetValueOrDefault();
            sum += SLOT_SIXES.Score.GetValueOrDefault();
            return sum;

        }

        public int GetLowerSum()
        {

            int sum = 0;
            sum += SLOT_THREE_OF_KIND.Score.GetValueOrDefault();
            sum += SLOT_FOUR_OF_KIND.Score.GetValueOrDefault();
            sum += SLOT_FULL_HOUSE.Score.GetValueOrDefault();
            sum += SLOT_SMALL_STRAIGHT.Score.GetValueOrDefault();
            sum += SLOT_LARGE_STRAIGHT.Score.GetValueOrDefault();
            sum += SLOT_YAHTZEE.Score.GetValueOrDefault();
            return sum;

        }


        public int GetBonus()
        {
            int sum = GetUpperSum();

            return sum >= 63 ? 35 : 0;
        }


        public int GetTotalScore() {
            int upper = GetUpperSum();
            int bonus = GetBonus();
            int lowersum = GetLowerSum();

            return upper + bonus + lowersum;
        }




        private static ScoreSlot createUpperSlot(int id, int face)
        {
            return new ScoreSlot(id)
            {
                Qualifier = (diceset) =>
                {
                    return calcUpperComboScore(diceset, face) > 0;
                },
                ScorePotential = (diceset) => {
                    return calcUpperComboScore(diceset, face);
                }
            };
        }

        private static int calcUpperComboScore(DiceSet set, int face)
        {

            int score = 0;
            foreach (GameDie die in set)
            {
                if (die.Number == face)
                {
                    score += die.Number;
                }
            }


            return score;
        }

        private static int calcDiceSum(DiceSet set)
        {

            int score = 0;
            foreach (GameDie die in set)
            {

                score += die.Number;

            }


            return score;
        }


    }
}

