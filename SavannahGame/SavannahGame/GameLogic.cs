using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SavannahGame
{
    public class GameLogic
    {
        public int TheAnimalId { get; set; }
        public List<List<Field>> territories = new List<List<Field>>();
        public List<Animal> AllAnimals = new List<Animal>();

        public int Cubcounter { get; set; }
        public int lionsRabbitKillsCounter { get; set; }

        //Placement of animals to random fields - make private
        public void Placement()
        {
            // Flatter min liste og tjekker hvilke dyr der ikke er på felt    
            var existingAnimals = territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal)).Select(m => m.animal);
            if (territories.SelectMany(c => c).Count(c => c.animal != null) >= 398)
            {
                GlobalWarming();
            }
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

        public void StartGame(int aLions, int aRabbits)
        {
            //Code to start everything

            AddAnimal(aLions, aRabbits);
            AddFields();
            Placement();

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
        private List<Animal> FlatList()
        {
            var flattenList = territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal)).Select(x => x.animal).ToList();
            return flattenList;
        }

        //Remove specific animal
        private void RemoveAnimal(Animal animal)
        {
            try
            {
                var tt = territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal);
                var cAnimal = tt.animal;
                territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal).animal = null;
                AllAnimals.Remove(cAnimal);
            }
            catch (Exception)
            {
                Console.WriteLine();
            }
        }

        private (int, int) SelectetMove(Animal animal)
        {

            var validFields = ValidMovesForAnimal(animal);
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

        private void AnimalMovement(Animal animal)
        {

            var x = SelectAnimalOnTerritorie(animal);
            //var tempSaved = animal;
            animal.Move();
            var savedAnimal = x.animal;
            var randomPos = SelectetMove(savedAnimal);
            var fPos = territories[randomPos.Item1][randomPos.Item2];

            switch (animal)
            {
                //If rabbit move to greenfield, it eats
                case Rabbit _ when fPos.GreenField:
                    animal.Eat();
                    Console.WriteLine($"Rabbit {savedAnimal.ID} Ate some GRASS and now weigths {savedAnimal.Weight}");
                    break;

                //Rabbit Mate if a rabbit is on new pos, and the rabbit gender is different
                case Rabbit _ when fPos.animal is Rabbit && animal.Gender != fPos.animal.Gender:
                    NewCubs(animal, true);
                    Console.WriteLine($"Rabbit: {savedAnimal.ID} and Rabbbit {fPos.animal.ID} is now parents :D");
                    break;

                //If lion move to a field with a rabbit, it eats it.
                case Lion _ when fPos.animal is Rabbit:
                    var soonToDieRabbit = fPos.animal;
                    Console.WriteLine($"Lion number {savedAnimal.ID} eat Rabbit: {soonToDieRabbit.ID} and gained {soonToDieRabbit.Weight} more kg");
                    RemoveAnimal(soonToDieRabbit);
                    lionsRabbitKillsCounter++;
                    animal.Eat();
                    break;

                //Lion Mate if a Lion is on new pos, and the Lion gender is different
                case Lion _ when fPos.animal is Lion && animal.Gender != fPos.animal.Gender:
                    NewCubs(animal, false);
                    Console.WriteLine($"Lion: {savedAnimal.ID} ({savedAnimal.Gender}) and Lion {fPos.animal.ID} ({fPos.animal.Gender}) are now parents :D");
                    break;
            }
            //Male Animals kill eachother of they land on same field 
            if (fPos.animal != null && animal is Lion && animal.Gender == fPos.animal.Gender && fPos.animal is Lion)
            {
                var DeadLion = fPos.animal;
                var newWeigth = savedAnimal.Weight + DeadLion.Weight;
                Console.WriteLine($"Lion: {fPos.animal.ID} IS DEAD because Lion: {savedAnimal.ID} landed on same field. {savedAnimal.ID} Gained {DeadLion.Weight}, and now weigths {newWeigth}");
                RemoveAnimal(DeadLion);
               // Thread.Sleep(100);
            }

            if (fPos.animal != null && animal is Rabbit && animal.Gender == fPos.animal.Gender && fPos.animal is Rabbit)
            {
                var DeadRabbit = fPos.animal;
                Console.WriteLine($"Rabbit: {fPos.animal.ID} IS DEAD because Rabbit: {savedAnimal.ID} landed on same field. ");

                RemoveAnimal(DeadRabbit);
              //  Thread.Sleep(100);

            }
            try
            {
                territories.SelectMany(c => c).Select(c => c).First(c => c.animal == savedAnimal).animal = null;

                territories[randomPos.Item1][randomPos.Item2].animal = savedAnimal;

            }
            catch (Exception)
            {
                Console.WriteLine();

            }

            Dead(savedAnimal);
        }

        private void Dead(Animal animal)
        {
            if (animal.Weight < 0)
            {
                RemoveAnimal(animal);
                Console.WriteLine($"{animal} ({animal.ID}) is dead... :(");
                Console.WriteLine(AllAnimals.Count());
                //   Thread.Sleep(1000

            }

            NonSurvivers();
            
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
        private void NewCubs(Animal animal, bool rabbit, int NumberOfNewRabbits = 2, int NumberOfNewLions = 1)
        {

            if (rabbit)
            {
                Cubcounter += 2;
                while (NumberOfNewRabbits != 0)
                {
                    AddAnimal(0, 1);

                    NumberOfNewRabbits--;
                }

            }
            else if (rabbit == false)
            {
                Cubcounter += 1;
                while (NumberOfNewLions != 0)
                {
                    AddAnimal(1, 0);
                    NumberOfNewLions--;
                }
            }
            Placement();
        }

        private List<Field> ValidMovesForAnimal(Animal animal)
        {

            //FINSIH THIS SHIT 
            int minimumToMove = 0, maximumToMove = 0, numberOfSteps = 0;
            if (animal is Lion)
            {
                minimumToMove = 0;
                maximumToMove = 20;
                numberOfSteps = 1;
            }
            else if (animal is Rabbit)
            {
                minimumToMove = 2;
                maximumToMove = 18;
                numberOfSteps = 2;
            }

            var validFields = new List<Field>();

            var tempInt = XandY(territories.SelectMany(s => s).Select(s => s).First(s => s.animal == animal));

            var itemOne = tempInt.Item1;
            var itemTwo = tempInt.Item2;


            List<(int, int)> addList = new List<(int, int)>();
            #region !=0
            if (animal is Lion && itemOne != minimumToMove || animal is Rabbit && itemOne > minimumToMove)
                addList.Add((itemOne - numberOfSteps, itemTwo));
            if (animal is Lion && itemTwo != minimumToMove || animal is Rabbit && itemTwo > minimumToMove)
                addList.Add((itemOne, itemTwo - numberOfSteps));
            #endregion
            #region <18
            if (animal is Lion && itemOne < maximumToMove || animal is Rabbit && itemOne < maximumToMove)
                addList.Add((itemOne + numberOfSteps, itemTwo));
            if (animal is Lion && itemTwo < maximumToMove || animal is Rabbit && itemTwo < maximumToMove)
                addList.Add((itemOne, itemTwo + numberOfSteps));
            #endregion
            #region twoField

            if (animal is Lion)
            {
                addList.Add((itemOne != minimumToMove ? itemOne - numberOfSteps : itemOne, itemTwo != minimumToMove ? itemTwo - 1 : itemTwo));
                addList.Add((itemOne < maximumToMove ? itemOne + numberOfSteps : itemOne, itemTwo < maximumToMove ? itemTwo + numberOfSteps : itemTwo));
                #endregion
                addList.ForEach(d =>
                {
                    if (territories[d.Item1][d.Item2] != null)
                        validFields.Add(territories[d.Item1][d.Item2]);
                });

            }
            else if (animal is Rabbit)
            {
                addList.Add((itemOne > minimumToMove ? itemOne - numberOfSteps : itemOne, itemTwo > numberOfSteps ? itemTwo - numberOfSteps : itemTwo));
                addList.Add((itemOne < maximumToMove ? itemOne + numberOfSteps : itemOne, itemTwo < maximumToMove ? itemTwo + numberOfSteps : itemTwo));

                addList.ForEach(s =>
                {
                    if (territories[s.Item1][s.Item2] != null)
                        validFields.Add(territories[s.Item1][s.Item2]);
                });
            }

            return validFields;

        }

        private (int, int) XandY(Field field)
        {
            //Take a field and find the cordinates of it 

            var boardFields = territories.Select(s => s).First(s => s.Contains(field));

            int yCon = territories.FindIndex(c => c == boardFields);
            int xCon = boardFields.FindIndex(c => c == field);

            return (xCon, yCon);

        }

        private void GlobalWarming()
        {
            if (territories.SelectMany(c => c).Count(c => c.animal != null) >= 390)
            {
                Console.WriteLine("All animals are dying because of global warming ");
                foreach (var item in territories.SelectMany(c => c))
                {
                    item.animal = null;
                }
                Console.WriteLine("ALL ANIMALS ARE DEAD GOOD FUCKING JOB IDIOTS");
                Console.WriteLine($"All in all {Cubcounter} babies where born");
                Console.WriteLine($"Lions killed {lionsRabbitKillsCounter} Rabbits");
            }
        }
        private void NonSurvivers()
        {
            if (territories.SelectMany(c => c).Count(c => c.animal != null) < 1)
            {
                Console.WriteLine("All animals killed eachother :( ");
                Console.WriteLine($"All in all {Cubcounter} babies where born");
                Console.WriteLine($"Lions killed {lionsRabbitKillsCounter} Rabbits");
            }

        }

        //Prints all field with animals and their cordinates
        public void PrintFelter()
        {

            while (territories.SelectMany(c => c).Count(c => c.animal != null) != 0)
            {
                GlobalWarming();

                foreach (var animal in territories.SelectMany(s => s).Where(s => s.animal != null).Select(s => s.animal))
                {
                    Dead(animal);

                    AnimalMovement(animal);

                }
                foreach (var animal in territories.SelectMany(s => s).Where(s => s.animal != null))
                {
                    Console.WriteLine("---------------NEW POS---------------");
                    //   Thread.Sleep(200);
                    Console.WriteLine($"{animal.animal.ID} {animal.animal} stands on {XandY(animal)} weigth {animal.animal.Weight} and is {animal.animal.Gender}");
                  

                }
            }

        }
    }
}

