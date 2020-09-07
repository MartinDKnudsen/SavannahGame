using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannahGame
{
    public class GameLogic
    {
        public List<List<Field>> territories = new List<List<Field>>();
        public List<Animal> AllAnimals = new List<Animal>();

        public void Placement()
        {

        // Flatter min liste og tjekker hvilke dyr der ikke er på felt    
              var existingAnimals = territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal)).Select(m => m.animal);

              foreach (var animal in AllAnimals.Except(existingAnimals))
              {
                    var r = RandomRoll.AnimalRandomPlacement();
                    var r1 = RandomRoll.AnimalRandomPlacement();

                    while (territories[r][r1].animal != null)
                    {
                        r = RandomRoll.AnimalRandomPlacement();
                        r1 = RandomRoll.AnimalRandomPlacement();

                    }
                    territories[r][r1].animal = animal;
                }
        }

        public void AddFields()
        {

            for (int i = 0; i < 20; i++)
            {
                territories.Add(new List<Field>());

                for (int j = 0; j < 20; j++)
                {
                    int greenfield = RandomRoll.Greenfield();
                    if (greenfield < 5)
                    {
                        territories[i].Add(new Field(true));
                    }
                    else
                    {
                        territories[i].Add(new Field(false));
                    }
                }
            }
        }
        public void AddAnimal(int numberOfLion, int numberOfRabbits)
        {
            while (numberOfLion != 0)
            {
                AllAnimals.Add(new Lion(RandomRoll.rRoll()));
                numberOfLion--;
            }

            while (numberOfRabbits != 0)
            {
                AllAnimals.Add(new Rabbit(RandomRoll.rRoll()));
                numberOfRabbits--;
            }

        }

        public void RemoveAnimal(Animal a)
        {


        }
        public void NewCubs(bool rabbit, int NumberOfNewRabbits = 4, int NumberOfNewLions = 1)
        {

            if (rabbit)
            {
                while (NumberOfNewRabbits != 0)
                {
                    AddAnimal(0, 1);

                    NumberOfNewRabbits--;
                }
            }
            else if (rabbit == false)
            {
                while (NumberOfNewLions != 0)
                {
                    AddAnimal(1, 0);
                    NumberOfNewLions--;
                }
            }
            Placement();
        }

        public Field CheckPosistion(Animal animal)
        {
            return territories.SelectMany(c => c).FirstOrDefault(c => c.animal == animal);
        }

    }
}
