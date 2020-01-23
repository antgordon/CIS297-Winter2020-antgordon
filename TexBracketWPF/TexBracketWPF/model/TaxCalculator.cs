using System;
using System.Collections.Generic;
using System.Text;

namespace TexBracketWPF
{


   public class TaxCalculator
    {

        /**
         *  new Bracket(0,9_700,0.10),
            new Bracket(9_700,39_475, 0.12),
            new Bracket(39_475,84_200, 0.22),
            new Bracket(84_200,160_725, 0.24),
            new Bracket(160_725,204_100, 0.32),
            new Bracket(204_100, 510_300, 0.35),
            new Bracket(510_300, int.MaxValue, 0.37),
         * **/
        private static TaxBracketSet TAX_BRACKETS = new TaxBracketSet(
            new TaxBracket(9_700.0, 0.10),
            new TaxBracket(39_475.0, 0.12),
            new TaxBracket(84_200.0, 0.22),
            new TaxBracket(160_725.0, 0.24),
            new TaxBracket(204_100.0, 0.32),
            new TaxBracket(510_300.0, 0.35),
            new TaxBracket(double.MaxValue, 0.37)
            );

       public enum Backets { 
            PER_10, PER_12, PER_22, PER_24, PER_32, PER_35, PER_37,

        }

        private double[] taxesOwed;
        private double totalTaxesOwed;
        private double taxPercentGross;
        private double taxPercentAdjust;

        public TaxCalculator( double grossIncome, double deductions) 
        {
            BracketSet = TAX_BRACKETS;

            double adjustedIncome = grossIncome - deductions;
            int length = BracketSet.TaxBrackets.Length;
            taxesOwed = new double[length];

            double min = 0.0;

            for (int bracketIndex = 0; bracketIndex < length; ++bracketIndex) 
            {
                TaxBracket taxBracket = BracketSet.TaxBrackets[bracketIndex];

                double max = taxBracket.UpperBound;

                double owed;
                if (adjustedIncome > min)
                {


                    if (adjustedIncome > max)
                    {
                        owed = Math.Floor((max - min) * taxBracket.Percent);
                    }
                    else
                    {
                        double working = adjustedIncome - min;
                        owed = Math.Floor((working) * taxBracket.Percent);
                    }


                }
                else
                {
                    owed = 0.0;
                }

                min = max;
                taxesOwed[bracketIndex] = owed;
                totalTaxesOwed += owed;
            }


            taxPercentGross = totalTaxesOwed / grossIncome;
            taxPercentAdjust = totalTaxesOwed / adjustedIncome;
        }


        public TaxBracketSet BracketSet { get; private set; }

        public double[] TaxesOwed { get => taxesOwed;  }

        public double TotalTaxesOwed { get => totalTaxesOwed;}

        public double TaxesPecentOfGrossIncome { get => taxPercentGross;  }
        public double TaxesPecentOfAdjustedIncome { get => taxPercentAdjust; }


    }
}
