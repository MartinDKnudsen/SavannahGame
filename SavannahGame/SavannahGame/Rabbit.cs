namespace SavannahGame
{
    public class Rabbit : Animal
    {
   
        public int Counter { get; set; }

        public Rabbit(string gender, int counter)
        {
            Weight = 20;
            this.Gender = gender;
            ID = counter;
            typeOfAnimal = "Rabbit";
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
