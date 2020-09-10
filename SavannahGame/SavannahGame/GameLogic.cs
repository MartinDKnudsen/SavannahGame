using System;
using System.Collections.Generic;
using System.Linq;

namespace SavannahGame
{
    public class GameLogic
    {
        public int TheAnimalId { get; set; }
        public List<List<Field>> territories = new List<List<Field>>();
        public List<Animal> AllAnimals = new List<Animal>();

        //Placement of animals to random fields 
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

        //Count all animals on the Savannah
        public int CountAnimals()
        {
            return territories.SelectMany(c => c).Count(c => c.animal != null);
        }

        //Count greenfields
        public int CountGreenField()
        {
            return territories.SelectMany(e => e).Count(e => e.GreenField);
        }

        //Count number of fields
        public int CountField()
        {
            //Tæller felter
            return territories.SelectMany(f => f).Count();
        }

        //Count number of selected animal on territories
        public int CountAllSpecAnimalOnTheTerritories<T>()
        {
            int SelectedAnimalsOnTheTerritories = territories.SelectMany(c => c).Count(c => c.animal is T);
            return SelectedAnimalsOnTheTerritories;
        }

        // Count all animals in Animal List
        public int CountAnimalsByType<T>()
        {
            var AnimalsOfType = AllAnimals.Count(c => c is T);
            return AnimalsOfType;
        }

        // Add a field to all fields, and generate random number of greenfields
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

        //Add selected number of animals, and roll their gender 
        public void AddAnimal(int numberOfLion, int numberOfRabbits)
        {
            while (numberOfLion != 0)
            {

                AllAnimals.Add(new Lion(RandomRoll.rRoll(), TheAnimalId));
                TheAnimalId++;
                numberOfLion--;
            }

            while (numberOfRabbits != 0)
            {
                AllAnimals.Add(new Rabbit(RandomRoll.rRoll(), TheAnimalId));
                TheAnimalId++;
                numberOfRabbits--;
            }

        }

        //Show the weigth of all animals of a speficic type on the savannah
        public double WeigthOfAllAnimalsOfAType<T>()
        {

            var XAnimalWeigth = FlatList().OrderByDescending(x => x.Weight).Where(c => c is T).ToList();

            double total = XAnimalWeigth.Sum(item => item.Weight);
            return total;

        }

        //Create a FlatList of animals on territories
        public List<Animal> FlatList()
        {
            var flattenList = territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal)).Select(x => x.animal).ToList();
            return flattenList;
        }

        //Remove specific animal
        public void RemoveAnimal(Animal animal)
        {
            var tt = territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal);
            var cAnimal = tt.animal;
            territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal).animal = null;
            AllAnimals.Remove(cAnimal);

        }

        public (int, int) RandomMove()
        {

            return RandomRoll.NewPos();

        }
        public Field SelectetMove()
        {


            //Vælg en af de felter fra CheckValidMoves
            return default;
        }


        public Field SelectAnimalOnTerritorie(Animal animal)
        {
            var TheField = territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal);
            return TheField;
        }



        public void AnimalMovement(Animal animal)
        {
            (int xCon, int yCon) = RandomRoll.NewPos();
            var x = SelectAnimalOnTerritorie(animal);
            var gemt = animal;
            animal.Move();
            var cAnimal = x.animal;

            RandomMove();

            territories.SelectMany(c => c).Select(c => c).First(c => c.animal == gemt).animal = null;

            territories[xCon][yCon].animal = cAnimal;

            //CheckValidMoves 
        }

        public void PrintAllAnimals()
        {
            if (AllAnimals.Count != 0)
            {
                var list = AllAnimals.Where(c => c is Lion);
                var list2 = AllAnimals.Where(c => c is Rabbit);
                foreach (var item in list)
                {
                    Console.WriteLine($"Id of animal: {item.ID} -- Gender: {item.Gender} -- is Type: {item.typeOfAnimal}");

                }
                Console.WriteLine("_________________________");

                foreach (var item in list2)
                {
                    Console.WriteLine($"Id of animal: {item.ID} -- Gender: {item.Gender} -- Type: {item.typeOfAnimal}");
                }

                Console.WriteLine("_________________________");

            }

            else
            {
                Console.WriteLine("All animals are dead");
            }
        }

        //Add Cubs to the animal list
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
            //Placement();
        }

        public List<Field> CheckValidMoves(Animal animal)
        {

            //Start XY
            var tempInt = XandY(territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal));

            var tempInt2 = territories.SelectMany(c => c).Select(c => c)
                .Where(c => XandY(c).Item1 == 1 && XandY(c).Item2 == 2);



            //Tjek felterne omkring dyret 
            return default;
        }
        public (int, int) XandY(Field field)
        {

            var boardFields = territories.Select(s => s).First(s => s.Contains(field));

            int yCon = territories.FindIndex(c => c == boardFields);
            int xCon = boardFields.FindIndex(c => c == field);

            return (xCon, yCon);

        }

        public void PrintFelter()
        {


            foreach (var VARIABLE in territories.SelectMany(s => s).Where(s => s.animal != null))
            {
                //   Thread.Sleep(500);
                Console.WriteLine($"{VARIABLE.animal.ID} {VARIABLE.animal} stands on {XandY(VARIABLE)} weigth {VARIABLE.animal.Weight}");

            }

            foreach (var dyr in territories.SelectMany(s => s).Where(s => s.animal != null).Select(s => s.animal))
            {

                AnimalMovement(dyr);

            }

            foreach (var VARIABLE in territories.SelectMany(s => s).Where(s => s.animal != null))
            {
                //Thread.Sleep(500);
                Console.WriteLine($"{VARIABLE.animal.ID} {VARIABLE.animal} stands on {XandY(VARIABLE)} weigth {VARIABLE.animal.Weight}");

            }
        }
    }
}
