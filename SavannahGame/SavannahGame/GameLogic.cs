using System.Collections.Generic;
using System.Linq;

namespace SavannahGame
{
    public class GameLogic
    {
        public List<List<Field>> territories = new List<List<Field>>();
        public List<Animal> AllAnimals = new List<Animal>();

        //Placerer dyrnene 
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


        public int CountAnimals()
        {
            //Tæller Dyr
            return territories.SelectMany(c => c).Count(c => c.animal != null);

        }
        public int CountGreenField()
        {
            //Tæller grønne felter 
            return territories.SelectMany(e => e).Count(e => e.GreenField);
        }
        public int CountField()
        {
            //Tæller felter
            return territories.SelectMany(f => f).Count();

        }

        public int CountSpecAnimalOnTheTerritories(bool LonS)
        {
            //Count a specific number of animals in the territories 
            if (LonS)
            {
                int LionsOnTheSavannah = territories.SelectMany(c => c).Count(c => c.animal is Lion);
                return LionsOnTheSavannah;
            }
            else if (LonS == false)
            {
                int RabbitsOnTheSavannah = territories.SelectMany(c => c).Count(c => c.animal is Rabbit);
                return RabbitsOnTheSavannah;
            }

            return default;
        }
        public int CountAnimalsByType(bool countLionsTrue)
        {
            if (countLionsTrue)
            {
                var LionsInAllAnimals = AllAnimals.Count(c => c is Lion);
                return LionsInAllAnimals;
            }
            else if (countLionsTrue == false)
            {
                var RabbitsInAllAnimals = AllAnimals.Count(c => c is Rabbit);
                return RabbitsInAllAnimals;

            }

            return default;

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
