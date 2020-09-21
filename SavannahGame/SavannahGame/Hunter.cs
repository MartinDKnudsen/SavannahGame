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
        //If animal counts get too high, the hunter will kill 50% of them

        public void KillAnimal()
        {
            Bullets -= 1;
            Weight += WeigthGain;
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
