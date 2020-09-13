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
            for (int i = 0; i <= 20; i++)
            {
                territories.Add(new List<Field>());

                for (int j = 0; j <= 20; j++)
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

        public (int, int) SelectetMove(Animal animal)
        {


            var validFields = CheckValidMoves(animal);
            var randomfield = RandomRoll.RField(validFields);
            var selectedRamdomFeld = validFields[randomfield];
            var posOfRField = XandY(selectedRamdomFeld);

            return posOfRField;
        }

        public Field SelectAnimalOnTerritorie(Animal animal)
        {
            var TheField = territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal);
            return TheField;
        }

        public List<Field> Test(Animal animal)
        {
            List<Field> fieldWithRabbits = new List<Field>();

            var k = CheckValidMoves(animal);
            foreach (var Animal in k)
            {
                if (Animal.animal is Rabbit)
                {
                    fieldWithRabbits.Add(Animal);

                }
            }
            return fieldWithRabbits;
        }



        public void AnimalMovement(Animal animal)
        {

            var x = SelectAnimalOnTerritorie(animal);
            var tempSaved = animal;
            animal.Move();
            var savedAnimal = x.animal;
            var newRandomPos = SelectetMove(savedAnimal);

            territories.SelectMany(c => c).Select(c => c).First(c => c.animal == tempSaved).animal = null;
            territories[newRandomPos.Item1][newRandomPos.Item2].animal = savedAnimal;
            Dead(animal);
        }

        public void Dead(Animal animal)
        {
            if (animal.Weight < 0)
            {
                RemoveAnimal(animal);
            }

        }
        public void PrintAllAnimals()
        {
            if (AllAnimals.Count != 0)
            {
                var list = AllAnimals.Where(c => c is Lion);
                var list2 = AllAnimals.Where(c => c is Rabbit);
                foreach (var item in list)
                {
                    Console.WriteLine($"Id of animal: {item.ID} -- Gender: {item.Gender} -- is Type: {item.AnimalType}");

                }
                Console.WriteLine("_________________________");

                foreach (var item in list2)
                {
                    Console.WriteLine($"Id of animal: {item.ID} -- Gender: {item.Gender} -- Type: {item.AnimalType}");
                }

                Console.WriteLine("_________________________");
            }
            else
            {
                Console.WriteLine("All animals are dead");
            }
        }

        //Add Cubs to the animal list
        public void NewCubs(bool rabbit, int NumberOfNewRabbits = 1, int NumberOfNewLions = 1)
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

        public List<Field> CheckValidMoves(Animal animal)
        {

            var validFields = new List<Field>();

            var tempInt = XandY(territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal));

            var itemOne = tempInt.Item1;
            var itemTwo = tempInt.Item2;


            if (itemTwo != 0 && territories[itemOne][itemTwo - 1] != null)
            {
                validFields.Add(territories[itemOne][itemTwo - 1]);
            }

            if (itemOne < 18 && (territories[itemOne + 1][itemTwo] != null))
            {
                validFields.Add(territories[itemOne + 1][itemTwo]);
            }

            if (itemOne != 0 && territories[itemOne - 1][itemTwo] != null)
            {
                validFields.Add(territories[itemOne - 1][itemTwo]);
            }

            if (itemTwo < 18 && (territories[itemOne][itemTwo + 1] != null))
            {
                validFields.Add(territories[itemOne][itemTwo + 1]);
            }

            if (itemOne < 18 && itemTwo < 18 && (territories[itemOne + 1][itemTwo + 1] != null))
            {
                validFields.Add(territories[itemOne + 1][itemTwo + 1]);
            }

            if (itemTwo != 0 && (itemOne != 0 && (territories[itemOne - 1][itemTwo - 1] != null)))
            {
                validFields.Add(territories[itemOne - 1][itemTwo - 1]);
            }

            if (itemOne < 18 && itemTwo != 0 && (territories[itemOne + 1][itemTwo - 1] != null))
            {
                validFields.Add(territories[itemOne + 1][itemTwo - 1]);
            }

            if (itemOne != 0 && itemTwo < 18 && territories[itemOne - 1][itemTwo + 1] != null)
            {
                validFields.Add(territories[itemOne - 1][itemTwo + 1]);
            }


            return validFields;
        }

        public List<Field> ValiMovesForRabbits()
        {
            return default;
        }

        public (int, int) XandY(Field field)
        {
            //Take a field and find the cordinates of it 

            var boardFields = territories.Select(s => s).First(s => s.Contains(field));

            int yCon = territories.FindIndex(c => c == boardFields);
            int xCon = boardFields.FindIndex(c => c == field);

            return (xCon, yCon);

        }

        //Prints all field with animals and their cordinates
        public void PrintFelter()
        {


            while (AllAnimals.Count != 0)
            {

                foreach (var VARIABLE in territories.SelectMany(s => s).Where(s => s.animal != null))
                {
                    // Thread.Sleep(500);
                    Console.WriteLine($"{VARIABLE.animal.ID} {VARIABLE.animal} stands on {XandY(VARIABLE)} weigth {VARIABLE.animal.Weight}");

                }

                foreach (var dyr in territories.SelectMany(s => s).Where(s => s.animal != null).Select(s => s.animal))
                {
                    Dead(dyr);
                    AnimalMovement(dyr);

                }

                NewCubs(false);
                foreach (var VARIABLE in territories.SelectMany(s => s).Where(s => s.animal != null))
                {
                    Console.WriteLine("---------------NEW POS---------------");
                    //    Thread.Sleep(200);
                    Console.WriteLine($"{VARIABLE.animal.ID} {VARIABLE.animal} stands on {XandY(VARIABLE)} weigth {VARIABLE.animal.Weight}");

                }
            }
        }
    }
}

