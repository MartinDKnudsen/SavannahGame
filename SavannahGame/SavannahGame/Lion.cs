namespace SavannahGame
{
   public class Lion : Animal
    {
      
        public Lion(string gender, int counter)
        {
            this.Weight = 250;
            this.Gender = gender;
            ID = counter;
            typeOfAnimal = "Lion";
            WeightLoss = 10;
        }

        public override void Eat()
        {
            Weight = Weight + 20;
        }

        public override void Move()
        {
            this.Weight -= WeightLoss;

        }

        public override void Mate(int NumberOfCubs = 1)
        {
            var MateLion = new GameLogic();

            while (NumberOfCubs != 0)
            {
                MateLion.AddAnimal(1, 0);
                NumberOfCubs--;
            }
            MateLion.Placement();
        }

        public override string ToString()
        {
            return $"Lion";
        }
    }
}

