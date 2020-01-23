using System;
using System.Collections.Generic;
using System.Text;

namespace TexBracketWPF
{
    public class TaxBracketSet
    {
        private readonly TaxBracket[] _brackets;

        public TaxBracketSet(params TaxBracket[] brackets) {
            _brackets = brackets;
    
        }

        public TaxBracket[] TaxBrackets {
           get =>  _brackets;
        } 
    }

    public class TaxBracket {

        public TaxBracket(double upBound, double percent) {
            this.UpperBound = upBound;
            this.Percent = percent;
        }
        public double UpperBound { get; private set; }
        public double Percent { get; private set; }
    }
}
