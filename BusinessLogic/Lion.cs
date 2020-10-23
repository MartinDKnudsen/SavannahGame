using System;
using System.Threading;

namespace BusinessLogic
{
   public class Lion : Animal
   {
       private int _numberOfNewLions = 1;

      GameLogic gl = GameLogic.Getinstance();
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

        public override void Mate()
        {
            while (_numberOfNewLions != 0)
            {
                gl.AddAnimal(1, 0, 0);
                _numberOfNewLions--;
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

            if (fPos.animal is Rabbit)
            {
                gl.RemoveAnimal(fPos.animal);
                gl.LionsRabbitKillsCounter++;
                savedAnimal.Eat();

            }
            else if (fPos.animal is Lion && savedAnimal.Gender != fPos.animal.Gender)
            {
                animal.Mate();
                gl.LionCubCounter++;
                gl.TotalCubCounter++;
                gl.RemoveAnimal(fPos.animal);
            }
            else if (fPos.animal is Lion && savedAnimal.Gender == fPos.animal.Gender)
            {
                gl.RemoveAnimal(fPos.animal);
                gl.LionsKilled++;
                Thread.Sleep(100);
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
            return "Lion";
        }


    }
}

