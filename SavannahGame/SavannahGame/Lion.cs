namespace SavannahGame
{
   public class Lion : Animal
    {
      
        public Lion(string gender, int counter)
        {
            this.Weight = 250;
            this.Gender = gender;
            ID = counter;
            AnimalType = "Lion";
            WeightLoss = 10;
            WeigthGain = 20;
        }

        

        public override void Eat()
        {
            Weight += WeigthGain;

        }

        public override void Move()
        {
            this.Weight -= WeightLoss;
        }

        public override string ToString()
        {
            return "Lion";
        }


    }
}

