using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SavannahGame
{
    public class GameLogic
    {
        private static GameLogic gl = null;

        private GameLogic()
        {

        }
        public static GameLogic Getinstance()
        {
            return gl ?? (gl = new GameLogic());
        }

        public int AnimalId { get; set; }
        public List<List<Field>> Territories = new List<List<Field>>();
        public List<Animal> AllAnimals = new List<Animal>();
        public List<Thread> Threads = new List<Thread>();

        public int TotalCubCounter { get; set; }
        public int RabbitCubCounter { get; set; }
        public int LionCubCounter { get; set; }
        public int LionsKilled { get; set; }
        public int LionsRabbitKillsCounter { get; set; }
        public int GrassEaten { get; set; }
        public int HunterKillCount { get; set; }

        //Placement of animals to random fields - make private
        public void Placement()
        {
            // Flatter min liste og tjekker hvilke dyr der ikke er på felt    
            var existingAnimals = Territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal)).Select(m => m.animal);
            if (Territories.SelectMany(c => c).Count(c => c.animal != null) >= 398)
            {
                GlobalWarming();
            }
            foreach (var animal in AllAnimals.Except(existingAnimals))
            {
                var r = RandomRoll.AnimalRandomPlacement();
                var r1 = RandomRoll.AnimalRandomPlacement();

                while (Territories[r][r1].animal != null)
                {
                    r = RandomRoll.AnimalRandomPlacement();
                    r1 = RandomRoll.AnimalRandomPlacement();

                }

                Territories[r][r1].animal = animal;
            }
        }

        public void StartGame(int aLions, int aRabbits)
        {
            //Code to start everything

            AddAnimal(aLions, aRabbits);
            AddFields();
            Placement();
            GameRunning();
        }

        //Do the full game here 
        public void GameRunning()
        {

            while (Territories.SelectMany(c => c).Count(c => c.animal != null) != 0)
            {

                NonSurvivers();
                GlobalWarming();

                foreach (var animal in Territories.SelectMany(s => s).Where(s => s.animal != null).Select(s => s.animal))
                {

                    Dead(animal);
                    AnimalMovement(animal);
                    Thread.Sleep(100);
                }
            }
        }


        // Add a field to all fields, and generate random number of greenfields
        public void AddFields()
        {
            for (int i = 0; i <= 19; i++)
            {
                Territories.Add(new List<Field>());

                for (int j = 0; j <= 19; j++)
                {
                    int greenfield = RandomRoll.Greenfield();
                    if (greenfield < 5)
                    {
                        Territories[i].Add(new Field(true));
                    }
                    else
                    {
                        Territories[i].Add(new Field(false));
                    }
                }
            }
        }

        //Add selected number of animals, and roll their gender 
        public void AddAnimal(int numberOfLion, int numberOfRabbits)
        {
            //Add one hunter to the field
            AllAnimals.Add(new Hunter());
            AnimalId = 1;

            while (numberOfLion != 0)
            {

                AllAnimals.Add(new Lion(RandomRoll.genderRoll(), AnimalId));
                AnimalId++;
                numberOfLion--;
            }

            while (numberOfRabbits != 0)
            {
                AllAnimals.Add(new Rabbit(RandomRoll.genderRoll(), AnimalId));
                AnimalId++;
                numberOfRabbits--;
            }


        }

        //Create a FlatList of animals on territories
        public List<Animal> FlatList()
        {
            var flattenList = Territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal)).Select(x => x.animal).ToList();
            return flattenList;
        }

        //Remove specific animal
        private void RemoveAnimal(Animal animal)
        {
            try
            {
                var tt = Territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal);
                var cAnimal = tt.animal;
                Territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal).animal = null;
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
            var TheField = Territories.SelectMany(c => c).ToList().Select(c => c).First(c => c.animal == animal);
            return TheField;
        }

        private void AnimalMovement(Animal animal)
        {

            var x = SelectAnimalOnTerritorie(animal);
            animal.Move();
            var savedAnimal = x.animal;
            var randomPos = SelectetMove(savedAnimal);
            var fPos = Territories[randomPos.Item1][randomPos.Item2];

            switch (animal)
            {
                //If rabbit move to greenfield, it eats
                case Rabbit _ when fPos.GreenField:
                    animal.Eat();
                    Console.WriteLine($"Rabbit {savedAnimal.ID} Ate some GRASS and now weigths {savedAnimal.Weight}");
                    GrassEaten++;
                    break;

                //Rabbit Mate if a rabbit is on new pos, and the rabbit gender is different
                case Rabbit _ when fPos.animal is Rabbit && animal.Gender != fPos.animal.Gender:
                    NewCubs(animal, true);
                    RabbitCubCounter += 2;
                    TotalCubCounter += 2;
                    Console.WriteLine($"Rabbit: {savedAnimal.ID} and Rabbbit {fPos.animal.ID} is now parents :D");
                    break;

                //If lion move to a field with a rabbit, it eats it.
                case Lion _ when fPos.animal is Rabbit:
                    var soonToDieRabbit = fPos.animal;
                    Console.WriteLine($"Lion number {savedAnimal.ID} eat Rabbit: {soonToDieRabbit.ID} and gained {soonToDieRabbit.Weight} more kg");
                    RemoveAnimal(soonToDieRabbit);
                    LionsRabbitKillsCounter++;
                    animal.Eat();
                    break;

                //Lion Mate if a Lion is on new pos, and the Lion gender is different
                case Lion _ when fPos.animal is Lion && animal.Gender != fPos.animal.Gender:
                    NewCubs(animal, false);
                    LionCubCounter++;
                    TotalCubCounter++;
                    Console.WriteLine($"Lion: {savedAnimal.ID} ({savedAnimal.Gender}) and Lion {fPos.animal.ID} ({fPos.animal.Gender}) are now parents :D");
                    break;
            }
            //Male Animals kill eachother of they land on same field 
            if (fPos.animal != null && animal is Lion && animal.Gender == fPos.animal.Gender && fPos.animal is Lion)
            {
                var deadLion = fPos.animal;
                var newWeigth = savedAnimal.Weight + deadLion.Weight;
                Console.WriteLine($"Lion: {fPos.animal.ID} IS DEAD because Lion: {savedAnimal.ID} landed on same field. {savedAnimal.ID} Gained {deadLion.Weight}, and now weigths {newWeigth}");
                RemoveAnimal(deadLion);
                LionsKilled++;
                // Thread.Sleep(100);
            }

            if (fPos.animal != null && animal is Rabbit && animal.Gender == fPos.animal.Gender && fPos.animal is Rabbit)
            {
                var deadRabbit = fPos.animal;
                Console.WriteLine($"Rabbit: {fPos.animal.ID} IS DEAD because Rabbit: {savedAnimal.ID} landed on same field. ");

                RemoveAnimal(deadRabbit);
                //  Thread.Sleep(100);

            }
            if (savedAnimal is Hunter && fPos.animal != null)
            {
                var deadAnimal = fPos.animal;
                animal.Eat();
                HunterKillCount++;
                RemoveAnimal(fPos.animal);

            }

            try
            {
                Territories.SelectMany(c => c).Select(c => c).First(c => c.animal == savedAnimal).animal = null;

                Territories[randomPos.Item1][randomPos.Item2].animal = savedAnimal;

            }

            catch (Exception)
            {
                Console.WriteLine();
            }

        }

        private void Dead(Animal animal)
        {
            if (animal.Weight < 0)
            {
                RemoveAnimal(animal);
                Console.WriteLine($"{animal} ({animal.ID}) is dead... :(");
                Console.WriteLine(AllAnimals.Count());
                Thread.Sleep(1000);

            }
        }

        //Add Cubs to the animal list
        private void NewCubs(Animal animal, bool rabbit, int NumberOfNewRabbits = 2, int NumberOfNewLions = 1)
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

        private List<Field> ValidMovesForAnimal(Animal animal)
        {

            int minimumToMove = 0, maximumToMove = 0, numberOfSteps = 0;
            if (animal is Lion || animal is Hunter)
            {
                minimumToMove = 0;
                maximumToMove = 18;
                numberOfSteps = 1;
            }
            else if (animal is Rabbit)
            {
                minimumToMove = 2;
                maximumToMove = 16;
                numberOfSteps = 2;
            }

            var validFields = new List<Field>();

            var tempInt = XandY(Territories.SelectMany(s => s).Select(s => s).First(s => s.animal == animal));

            var itemOne = tempInt.Item1;
            var itemTwo = tempInt.Item2;


            List<(int, int)> addList = new List<(int, int)>();
            #region !=0
            if ((animal is Lion || animal is Hunter) && itemOne != minimumToMove || animal is Rabbit && itemOne > minimumToMove)
                addList.Add((itemOne - numberOfSteps, itemTwo));
            if ((animal is Lion || animal is Hunter) && itemTwo != minimumToMove || animal is Rabbit && itemTwo > minimumToMove)
                addList.Add((itemOne, itemTwo - numberOfSteps));
            #endregion
            #region <18
            if ((animal is Lion || animal is Hunter) && itemOne < maximumToMove || animal is Rabbit && itemOne < maximumToMove)
                addList.Add((itemOne + numberOfSteps, itemTwo));
            if ((animal is Lion || animal is Hunter) && itemTwo < maximumToMove || animal is Rabbit && itemTwo < maximumToMove)
                addList.Add((itemOne, itemTwo + numberOfSteps));
            #endregion
            #region twoField

            if (animal is Lion || animal is Hunter)
            {
                addList.Add((itemOne != minimumToMove ? itemOne - numberOfSteps : itemOne, itemTwo != minimumToMove ? itemTwo - 1 : itemTwo));
                addList.Add((itemOne < maximumToMove ? itemOne + numberOfSteps : itemOne, itemTwo < maximumToMove ? itemTwo + numberOfSteps : itemTwo));
                #endregion
                addList.ForEach(d =>
                {
                    if (Territories[d.Item1][d.Item2] != null)
                        validFields.Add(Territories[d.Item1][d.Item2]);
                });

            }
            else if (animal is Rabbit)
            {
                addList.Add((itemOne > minimumToMove ? itemOne - numberOfSteps : itemOne, itemTwo > numberOfSteps ? itemTwo - numberOfSteps : itemTwo));
                addList.Add((itemOne < maximumToMove ? itemOne + numberOfSteps : itemOne, itemTwo < maximumToMove ? itemTwo + numberOfSteps : itemTwo));

                addList.ForEach(s =>
                {
                    if (Territories[s.Item1][s.Item2] != null)
                        validFields.Add(Territories[s.Item1][s.Item2]);
                });
            }

            return validFields;

        }

        private (int, int) XandY(Field field)
        {
            //Take a field and find the cordinates of it 

            var boardFields = Territories.Select(s => s).First(s => s.Contains(field));

            int yCon = Territories.FindIndex(c => c == boardFields);
            int xCon = boardFields.FindIndex(c => c == field);

            return (xCon, yCon);

        }

        private void GlobalWarming()
        {
            if (Territories.SelectMany(c => c).Count(c => c.animal != null) >= 390)
            {
                Console.WriteLine("All animals are dying because of global warming ");
                foreach (var item in Territories.SelectMany(c => c))
                {
                    item.animal = null;
                }
                Console.WriteLine("ALL ANIMALS ARE DEAD GOOD FUCKING JOB IDIOTS");
                Console.WriteLine($"All in all {TotalCubCounter} babies where born");
                Console.WriteLine($"Lions killed {LionsRabbitKillsCounter} Rabbits");
            }
        }

        private void NonSurvivers()
        {
            if (Territories.SelectMany(c => c).Count(c => c.animal != null) < 1)
            {
                Console.WriteLine("All animals killed eachother :( ");
                Console.WriteLine($"All in all {TotalCubCounter} babies where born");
                Console.WriteLine($"Lions killed {LionsRabbitKillsCounter} Rabbits");
            }

        }
    }
}

