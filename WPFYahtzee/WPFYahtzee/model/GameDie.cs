using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFYahtzee.model
{
    public class GameDie
    {

      


        private Random random;
        private readonly int faces;
        public GameDie(int faces, Random random)
        {
            this.random = random;
            this.faces = faces;
    
        }

        public int GetFaces()
        {
            return faces;


        }

        public void Randomize()
        {
            Number = 1 + random.Next(faces);
        }
        public int Number { get; set; }

        public bool IsHeld { get; set; }
    }
}
