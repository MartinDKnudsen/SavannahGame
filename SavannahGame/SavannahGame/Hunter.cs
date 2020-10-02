using System;
using System.Threading;

namespace SavannahGame
{
    public class Hunter : Animal
    {
        public Hunter()
        {
            Weight = 100;
            WeightLoss = 1;
            WeigthGain = 10;
        }

        public override void Eat()
        {
            
           Weight += WeigthGain;
        }

        public override void Move()
        {
          Weight -= WeightLoss;
        }
        
    }
}
