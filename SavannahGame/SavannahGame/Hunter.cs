using System;
using System.Threading;

namespace SavannahGame
{
    public class Hunter : Animal
    {
        public int Bullets { get; set; }


        public Hunter()
        {
            Bullets = 20;
            Weight = 100;
            WeightLoss = 5;
            WeigthGain = 10;
                       
        }

        public override void Eat()
        {
            Bullets -= 1;
            Weight += WeigthGain;
        }

        public override void Move()
        {
            Weight -= WeightLoss;
        }
    }
}
