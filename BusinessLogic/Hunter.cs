namespace BusinessLogic
{
    public class Hunter : Animal
    {
        GameLogic gl = GameLogic.Getinstance();

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
        public override void Mate()
        {
        }

        public override void Move(Animal animal)
        {
            Weight -= WeightLoss;

            var x = gl.SelectAnimalOnTerritorie(animal);

            var savedAnimal = x.animal;
            var randomPos = gl.SelectetMove(savedAnimal);
            var fPos = gl.Territories[randomPos.Item1][randomPos.Item2];

            if (fPos.animal != null)
            {
                savedAnimal.Eat();
                gl.Arrows -= 1;
                gl.HunterKillCount++;
                gl.RemoveAnimal(fPos.animal);
            }
            else if (fPos.animal == null)
            {
                gl.Arrows += 1;
            }

        }

       
    }
}
