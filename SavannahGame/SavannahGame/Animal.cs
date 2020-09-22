using System.Threading;

namespace SavannahGame
{
    public abstract class Animal
    {
        public string Gender { get; set; }
        public double Weight { get; set; }
        public double WeigthGain { get; set; }
        public double WeightLoss { get; set; }
        public int ID { get; set; }
        public string AnimalType { get; set; }
       
        public abstract void Eat();

        public abstract void Move();

       
    }
}
