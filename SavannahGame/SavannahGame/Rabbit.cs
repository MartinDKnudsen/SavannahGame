using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavannahGame
{
    class Rabbit : Animal
    {
        public Rabbit(string gender)
        {
            Weight = 20;
            this.Gender = gender;
        }

        public override void Eat()
        {
            Weight = Weight + 20;
        }


        public override int Move()
        {
            Weight = Weight - 2;
            return 2;
        }

        public override void Mate(int NumberOfCubs)
        {


        }

        public override string ToString()
        {
            return "Rabbit";
        }
    }
}
