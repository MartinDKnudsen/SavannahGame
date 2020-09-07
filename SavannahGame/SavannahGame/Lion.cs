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

        public override void Mate(int NumberOfCubs)
        {
            var GL = new GameLogic();
            GL.AddAnimal(NumberOfCubs, 0); 

        }

        public override string ToString()
        {
            return "Lion";
        }
    }
}
