using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavannahGame
{
    class Lion : Animal
    {
        public Lion()
        {
            Weight = 80;
            Gender = DesideGender();

        }
        public string DesideGender()
        {
            int mOrf;
            Random r = new Random();
            mOrf = r.Next(0, 100);
            if (mOrf > 50)
            {
                Gender = "Female";
            }
            else if (mOrf < 50)
            {
                Gender = "Male";
            }
            return Gender;

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
    }
}
