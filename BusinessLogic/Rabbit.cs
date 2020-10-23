using System;

namespace BusinessLogic
{
    public class Rabbit : Animal
    {
        GameLogic gl = GameLogic.Getinstance();
        private int _numberOfNewRabbits = 2;
        public Rabbit(string gender, int counter)
        {
            this.Weight = 20;
            this.Gender = gender;
            ID = counter;
            AnimalType = "Rabbit";
            WeightLoss = 2;
            WeigthGain = 4;
        }

        public override void Eat()
        {
            Weight += WeigthGain;
        }


        public override void Mate()
        {

            while (_numberOfNewRabbits != 0)
            {
                gl.AddAnimal(0, 1, 0);

                _numberOfNewRabbits--;
            }

            gl.Placement();
        }
        public override void Move(Animal animal)
        {
            this.Weight -= WeightLoss;

            var x = gl.SelectAnimalOnTerritorie(animal);

            var savedAnimal = x.animal;
            var randomPos = gl.SelectetMove(savedAnimal);
            var fPos = gl.Territories[randomPos.Item1][randomPos.Item2];

            if (fPos.GreenField)
            {
                animal.Eat();
                gl.GrassEaten++;
            }
            else if (fPos.animal is Rabbit && savedAnimal.Gender != fPos.animal.Gender)
            {
                animal.Mate();
                gl.RabbitCubCounter += 2;
                gl.TotalCubCounter += 2;
                gl.RemoveAnimalFromField(fPos.animal);
                gl.Placement();
            }
            else if (fPos.animal != null && savedAnimal.Gender == fPos.animal.Gender && fPos.animal is Rabbit)
            {
                gl.RemoveAnimal(fPos.animal);
            }

            try
            {
                if (gl.AllAnimals.Contains(savedAnimal))
                {
                    gl.RemoveAnimalFromField(savedAnimal);
                }

                gl.Territories[randomPos.Item1][randomPos.Item2].animal = savedAnimal;
                gl.Dead(savedAnimal);
            }
            catch (Exception)
            {
                gl.Dead(savedAnimal);
            }
        }

        public override string ToString()
        {
            return "Rabbit";
        }
    }
}
