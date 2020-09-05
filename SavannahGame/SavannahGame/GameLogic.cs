using System;
using System.Collections.Generic;

namespace SavannahGame
{
    public class GameLogic
    {

        List<List<Field>> territories = new List<List<Field>>();
        List<Animal> AllAnimals = new List<Animal>();

        public void Placement()
        {
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

            foreach (var VARIABLE in AllAnimals)
            {
                Console.WriteLine(VARIABLE);
            }
        }

        public void RemoveAnimal(Animal a)
        {

        }

    }
}
