using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WPFYahtzee.model;

namespace YahtzeeUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPlayerInitialPermission()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();
            PlayerState player = new PlayerState(set, scoreCard);
            Assert.IsTrue(player.Throws == 0, "Failed 0 throws");
            Assert.IsFalse(player.CanHoldDice(), "Has permission to hold dice");
            Assert.IsTrue(player.CanThrow(), "Cannot throw dice");
            Assert.IsFalse(player.CanScore(), "Has permission to score");

        }

        [TestMethod]
        public void TestPlayerMidPermission()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();
            PlayerState player = new PlayerState(set, scoreCard);
            player.ThrowDice();
            Assert.IsTrue(player.Throws == 1, "Failed 1 throws");
            Assert.IsTrue(player.CanHoldDice(), "no permission to hold dice");
            Assert.IsTrue(player.CanThrow(), "Cannot throw dice");
            Assert.IsTrue(player.CanScore(), "no permission to score");

        }


        [TestMethod]
        public void TestPlayerEndPermission()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();
            PlayerState player = new PlayerState(set, scoreCard);
            player.ThrowDice();
            player.ThrowDice();
            player.ThrowDice();
            Assert.IsTrue(player.Throws == 3, "Failed 1 throws");
            Assert.IsTrue(player.CanHoldDice(), "no permission to hold dice");
            Assert.IsFalse(player.CanThrow(), "Can throw dice");
            Assert.IsTrue(player.CanScore(), "no permission to score");

        }

        [TestMethod]
        public void TestUpperSection()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();
            TestUpperSectionSingle(set, scoreCard.SLOT_ONES, 1);
            TestUpperSectionSingle(set, scoreCard.SLOT_TWOS, 2);
            TestUpperSectionSingle(set, scoreCard.SLOT_THREES, 3);
            TestUpperSectionSingle(set, scoreCard.SLOT_FOURS, 4);
            TestUpperSectionSingle(set, scoreCard.SLOT_FIVES, 5);
            TestUpperSectionSingle(set, scoreCard.SLOT_SIXES, 6);


        }

        [TestMethod]
        public void TestUpperSectionBonus()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();
      

            {
                scoreCard.SLOT_ONES.Score = 62;
                int expected = 0;
                int actual = scoreCard.GetBonus();

                Assert.AreEqual(expected, actual, "Failed upper test not bonus");
            }

            {

                scoreCard.SLOT_ONES.Score = 63;

                int expected = 35;
                int actual = scoreCard.GetBonus();

                Assert.AreEqual(expected, actual, "Failed upper test bonus");
            }
        }

        [TestMethod]
        public void TestLowerThreeOfKind()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();


            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 2;
                set[4].Number = 3;


                Assert.IsTrue(scoreCard.SLOT_THREE_OF_KIND.Qualifier(set), "Failed to qualifiy exactly 3");
                int expected = 1 * 3 + 2 + 3;
                int actual = scoreCard.SLOT_THREE_OF_KIND.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact 3");
            }

            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 1;
                set[4].Number = 3;


                Assert.IsTrue(scoreCard.SLOT_THREE_OF_KIND.Qualifier(set), "Failed to qualifiy exactly 4");
                int expected = 1 * 4 + 3;
                int actual = scoreCard.SLOT_THREE_OF_KIND.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact 4");
            }

            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 1;
                set[3].Number = 3;
                set[4].Number = 3;


                Assert.IsFalse(scoreCard.SLOT_THREE_OF_KIND.Qualifier(set), "Is qualifiy on exactly 2");
              
            }
        }


        [TestMethod]
        public void TestLowerFourOfKind()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();


            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 1;
                set[4].Number = 3;


                Assert.IsTrue(scoreCard.SLOT_FOUR_OF_KIND.Qualifier(set), "Failed to qualifiy exactly 4");
                int expected = 1 * 4 + 3;
                int actual = scoreCard.SLOT_FOUR_OF_KIND.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact 4");
            }

            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 1;
                set[4].Number = 1;


                Assert.IsTrue(scoreCard.SLOT_FOUR_OF_KIND.Qualifier(set), "Failed to qualifiy exactly 5");
                int expected = 1 * 5;
                int actual = scoreCard.SLOT_FOUR_OF_KIND.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact 5");
            }

            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 3;
                set[4].Number = 3;


                Assert.IsFalse(scoreCard.SLOT_FOUR_OF_KIND.Qualifier(set), "Is qualifiy on exactly 3");

            }
        }


        [TestMethod]
        public void TestLowerFullHouse()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();


            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 2;
                set[4].Number = 2;


                Assert.IsTrue(scoreCard.SLOT_FULL_HOUSE.Qualifier(set), "Failed to qualifiy");
                int expected = 25;
                int actual = scoreCard.SLOT_FULL_HOUSE.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact");
            }

            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 3;
                set[3].Number = 2;
                set[4].Number = 2;


                Assert.IsFalse(scoreCard.SLOT_FULL_HOUSE.Qualifier(set), "Failed to qualifiy");
              
            }
        }



        [TestMethod]
        public void TestLowerSmallStraight()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();


            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 3;
                set[3].Number = 4;
                set[4].Number = 2;


                Assert.IsTrue(scoreCard.SLOT_SMALL_STRAIGHT.Qualifier(set), "Failed to qualifiy");
                int expected = 30;
                int actual = scoreCard.SLOT_SMALL_STRAIGHT.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact");
            }

            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 3;
                set[3].Number = 4;
                set[4].Number = 5;


                Assert.IsTrue(scoreCard.SLOT_SMALL_STRAIGHT.Qualifier(set), "Failed to qualifiy 5");
                int expected = 30;
                int actual = scoreCard.SLOT_SMALL_STRAIGHT.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact 5");
            }
            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 2;
                set[3].Number = 3;
                set[4].Number = 5;


                Assert.IsFalse(scoreCard.SLOT_SMALL_STRAIGHT.Qualifier(set), "Qualified gap");
             
            }

            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 2;
                set[3].Number = 2;
                set[4].Number = 2;


                Assert.IsFalse(scoreCard.SLOT_SMALL_STRAIGHT.Qualifier(set), " qualified bad data");

            }

        }


        public void TestLowerLargeStraight()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();


            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 3;
                set[3].Number = 4;
                set[4].Number = 5;


                Assert.IsTrue(scoreCard.SLOT_LARGE_STRAIGHT.Qualifier(set), "Failed to qualifiy");
                int expected = 40;
                int actual = scoreCard.SLOT_LARGE_STRAIGHT.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact");
            }

           
            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 2;
                set[3].Number = 3;
                set[4].Number = 5;


                Assert.IsFalse(scoreCard.SLOT_LARGE_STRAIGHT.Qualifier(set), "Qualified gap");

            }

            {
                set[0].Number = 1;
                set[1].Number = 2;
                set[2].Number = 2;
                set[3].Number = 2;
                set[4].Number = 2;


                Assert.IsFalse(scoreCard.SLOT_LARGE_STRAIGHT.Qualifier(set), " qualified bad data");

            }

        }
        private void  TestUpperSectionSingle(DiceSet set, ScoreSlot slot, int digit) {
          
            {
                int expected = 4 * digit;
                set[0].Number = digit;
                set[1].Number = digit;
                set[2].Number = digit;
                set[3].Number = digit;
                set[4].Number = 0;
                int actual = slot.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Failed upper test for " + digit);
            }
        }

        [TestMethod]
        public void TestLowerYahtzee()
        {

            DiceSet set = new DiceSet(new Random());
            ScoreCard scoreCard = new ScoreCard();


            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 1;
                set[4].Number = 1;


                Assert.IsTrue(scoreCard.SLOT_YAHTZEE.Qualifier(set), "Failed to qualifiy exactly 5");
                int expected = 50;
                int actual = scoreCard.SLOT_YAHTZEE.ScorePotential(set);
                Assert.AreEqual(expected, actual, "Gave wrong score for exact 5");
            }

            {
                set[0].Number = 1;
                set[1].Number = 1;
                set[2].Number = 1;
                set[3].Number = 3;
                set[4].Number = 3;


                Assert.IsFalse(scoreCard.SLOT_YAHTZEE.Qualifier(set), "Is qualifiy on exactly 3");

            }
        }
    }
}
