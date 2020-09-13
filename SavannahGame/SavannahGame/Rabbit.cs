namespace SavannahGame
{
    public class Rabbit : Animal
    {


        public Rabbit(string gender, int counter)
        {
            this.Weight = 20;
            this.Gender = gender;
            ID = counter;
            AnimalType = "Rabbit";
            WeightLoss = 2;
        }

        public override void Eat()
        {
            Weight += 2;
        }
        public override void Move()
        {
            this.Weight -= WeightLoss;
        }

        public override string ToString()
        {
            return "Rabbit";
        }

    }
}
