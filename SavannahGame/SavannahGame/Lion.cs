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
        }

        

        public override void Eat()
        {
            Weight += 20;

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

