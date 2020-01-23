using System;
using System.Collections.Generic;
using System.Text;

namespace TexBracketWPF
{
    public class GuiState
    {

        public GuiState() {
            TaxResults = new TaxCalculator(0,0);
            UsingStandardDeduction = true;
            GrossIncome = 0.0;
            Deductions = 0.0;
        }
        public TaxCalculator TaxResults { get; set; }
        public double GrossIncome { get; set; }

        public double Deductions { get; set; }

        public bool UsingStandardDeduction{ get; set; }

        public const int STANDARD_DEDUCTION = 12_200;
    }
}
