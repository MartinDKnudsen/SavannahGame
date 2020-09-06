using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavannahGame
{
    class Lion : Animal
    {

        public Lion(string gender)
        {
            Weight = 80;
            this.Gender = gender;
        }
        
        public override void Eat()
        {
            Weight = Weight + 20;
        }

        public override int Move()
        {
            Weight = Weight - 5;
            return 1;

        }

        public override void Mate(string gender1, string gender2)
        {
            
        }

        public override string ToString()
        {
            return "Lion";
        }
    }
}
