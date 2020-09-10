namespace SavannahGame
{
   public class Lion : Animal
    {
      
        public Lion(string gender, int counter)
        {
            Weight = 250;
            this.Gender = gender;
            ID = counter;
            typeOfAnimal = "Lion";
        }

        public override void Eat()
        {
            Weight = Weight + 20;
        }

        public override int Move()
        {
            Weight = Weight - 10;
            
            return 1;
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
            return $"{ID}Lion";
        }
    }
}

