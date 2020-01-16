using System;

namespace TaxBracket
{
    class Program
    {
        //Use single 2019 tax bracket
       // http://www.moneychimp.com/features/tax_brackets.htm
        static Bracket[] brackets = {
            new Bracket(0,9_700,0.10),
            new Bracket(9_700,39_475, 0.12),
            new Bracket(39_475,84_200, 0.22),
            new Bracket(84_200,160_725, 0.24),
            new Bracket(160_725,204_100, 0.32),
            new Bracket(204_100, 510_300, 0.35),
            new Bracket(510_300, int.MaxValue, 0.37),
        };


        static void Main(string[] args)
        {
            int income = 0;

            {
                int reported = 0;
                do
                {
                    Console.Write("Enter your income or 0 to stop entering income:");
                    string text = Console.ReadLine();


                    if (int.TryParse(text, out reported))
                    {
                        income += reported;
                    }
                    else {
                        Console.WriteLine("Invalid Number. Try again.");
                        reported = 1;
                    }



                } while (reported != 0);
            }

            int deductions = 0;
            bool itemizeDeducts = false;
            Console.Write("Do you want to use the standard deduction $12,200 (Y/N):");
            char ch = Console.ReadKey().KeyChar;
            Console.WriteLine();

            {

                bool repeat = false;

                do
                {
                    switch (ch)
                    {
                        case 'Y':
                        case 'y':
                            itemizeDeducts = false;
                            repeat = false;
                            break;
                        case 'N':
                        case 'n':
                            itemizeDeducts = true;
                            repeat = false;
                            break;

                        default:
                            Console.WriteLine("Invalid input! type Y or N");
                            repeat = true;
                            break;


                    }
                } while (repeat);
            }


            if (itemizeDeducts) {
                {
                    int reported = 0;
                    do
                    {
                        Console.Write("Enter your deduction or 0 to stop entering deductions:");
                        string text = Console.ReadLine();


                        if (int.TryParse(text, out reported))
                        {
                            deductions += reported;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Number. Try again.");
                            reported = 1;
                        }



                    } while (reported != 0);
                }
            } else {
                deductions = 12_200;
            }


            int adjustedIncome = income - deductions;
            Console.WriteLine($"Adjusted income {adjustedIncome}={income}-{deductions}");
            int totalTaxes = 0;
           // int[] taxtBreakdown = new int[brackets.Length];//Store the results from each tax bracket

            foreach (Bracket brack in brackets)
            {
                int tax = brack.CalcTaxes(adjustedIncome);
                totalTaxes += tax;
                //USe string format witth interpolation. Fixed point
                //https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings#FFormatString
                Console.WriteLine($"Taxes at {brack.GetPercent() * 100.0:F0}%: {tax}");

            }

            Console.WriteLine($"Total Taxes {totalTaxes}");
            Console.WriteLine($"Taxes as a percentage of Adjusted income { Math.Floor((double)totalTaxes/(double)adjustedIncome * 100.0):F0}%");
            Console.WriteLine($"Taxes as a percentage of Gross income { Math.Floor((double)totalTaxes / (double)income * 100.0) :F0}%");

            Console.ReadKey();
        }
    }

    //Use unction object to manage brackets
    class Bracket
    {
        private readonly int _min;
        private readonly int _max;
        private readonly double _percent;

        public Bracket(int min, int max, double percent) 
        {
            _min = min;
            _max = max;
            _percent = percent;
        
        }


        public double GetPercent() {
            return _percent;
        }

        public int CalcTaxes(int income) 
        {

            if (income > _min)
            {
        

                if (income >= _max) {
                    return (int)Math.Floor((_max - _min) * _percent);
                } else {
                    int working = income - _min;
                    return (int)Math.Floor((working) * _percent);
                }
        

            }
            else
            {
                return 0;
            }
        }
        
    }
}
