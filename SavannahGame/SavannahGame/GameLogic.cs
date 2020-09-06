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

            foreach (var animal in AllAnimals)
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

            //Giv alle dyr en position 
        }
        public void AddFields()
        {
            for (int i = 0; i < 20; i++)
            {
                territories.Add(new List<Field>());

                for (int j = 0; j < 20; j++)
                {
                    territories[i].Add(new Field());
                }
            }

            foreach (var territory in territories)
            {
                Console.Write(territory.Count);

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

            var count = AllAnimals.Count(s => s is Lion);
            var count2 = AllAnimals.Count(s => s is Rabbit);

            foreach (var animal in AllAnimals)
            {
                Console.WriteLine(animal);

            }
            Console.WriteLine($"There is {count} lions \nand {count2} rabbits");

        }

        public void RemoveAnimal(Animal a)
        {


        }


        public Field CheckPosistion(Animal animal)
        {
            return territories.SelectMany(c => c).FirstOrDefault(c => c.animal == animal);
        }

    }
}
