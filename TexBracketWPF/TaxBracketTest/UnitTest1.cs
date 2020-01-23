using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TexBracketWPF;

namespace TaxBracketTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            TaxCalculator calc = new TaxCalculator(100_000.0, 0.0);
            double diff = Math.Abs(calc.TotalTaxesOwed - 18_175.0);
            Assert.IsTrue(IsClose(calc.TotalTaxesOwed, 18_175.0), $"Total taxes {calc.TotalTaxesOwed}. Expected {18_175.0}");
        }

        [TestMethod]
        public void TestBrackets()
        {

            TaxCalculator calc = new TaxCalculator(100_000.0, 0.0);

            Assert.IsTrue(IsClose(calc.TaxesOwed[(int)TaxCalculator.Backets.PER_37], 0.0));
            Assert.IsTrue(IsClose(calc.TaxesOwed[(int)TaxCalculator.Backets.PER_35], 0.0));
            Assert.IsTrue(IsClose(calc.TaxesOwed[(int)TaxCalculator.Backets.PER_32], 0.0));

            { 
                double min = calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_22].UpperBound;
                double taxable = 100_000.0 - min;
                TaxBracket tw4 = calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_24];
                double proper = taxable * tw4.Percent;
                double actual = calc.TaxesOwed[(int)TaxCalculator.Backets.PER_24];
                Assert.IsTrue(IsClose(proper, actual), $"Expected {proper}. Got {actual}");
            }

            {
               double taxable2 = calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_22].UpperBound 
                    - calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_12].UpperBound;
                double target = taxable2 * calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_22].Percent;
                double actual = calc.TaxesOwed[(int)TaxCalculator.Backets.PER_22];
                Assert.IsTrue(IsClose(target, actual), $"Expected {target}. Got {actual}");
            }

            {
                double taxable2 = calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_12].UpperBound
                     - calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_10].UpperBound;
                double target = taxable2 * calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_12].Percent;
                double actual = calc.TaxesOwed[(int)TaxCalculator.Backets.PER_12];
                Assert.IsTrue(IsClose(target, actual), $"Expected {target}. Got {actual}");
            }

            {
                double taxable2 = calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_10].UpperBound;
              
                double target = taxable2 * calc.BracketSet.TaxBrackets[(int)TaxCalculator.Backets.PER_10].Percent;
                double actual = calc.TaxesOwed[(int)TaxCalculator.Backets.PER_10];
                Assert.IsTrue(IsClose(target, actual), $"Expected {target}. Got {actual}");
            }


        }

        private bool IsClose(double actual, double target) {
            double diff = Math.Abs(actual - target);
            return diff <= 1.0;
        }
    }
}
