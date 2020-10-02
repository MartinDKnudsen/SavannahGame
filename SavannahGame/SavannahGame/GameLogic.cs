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


        public int TotalCubCounter { get; set; }
        public int RabbitCubCounter { get; set; }
        public int LionCubCounter { get; set; }
        public int LionsKilled { get; set; }
        public int LionsRabbitKillsCounter { get; set; }
        public int GrassEaten { get; set; }
        public int HunterKillCount { get; set; }
        public int Arrows { get; set; }
        public int NumberofHunters { get; set; }

        //Placement of animals to random fields - make private
        private void Placement()
        {
            // Flatter min liste og tjekker hvilke dyr der ikke er på felt    
            var existingAnimals = Territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal)).Select(m => m.animal);

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

        //Code to start everything
        public void StartGame(int aLions, int aRabbits, int aHunter)
        {

            AddAnimal(aLions, aRabbits, aHunter);
            AddFields();
            Placement();
            GameRunning();

        }

        //Game running
        private void GameRunning()
        {
            while (AllAnimals.Count != 1)
            {
                for (int i = 0; i < AllAnimals.Count; i++)
                {
                    if (!AllAnimals.Contains(AllAnimals[i]))
                    {
                        continue;
                    }
                    Dead(AllAnimals[i]);
                    AnimalMovement(AllAnimals[i]);
                    Thread.Sleep(100);
                }
            }
        }

        // Add a field to all fields, and generate random number of greenfields
        private void AddFields()
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
        private void AddAnimal(int numberOfLion, int numberOfRabbits, int numberofHunters)
        {
            NumberofHunters = numberofHunters;
            //Add one hunter to the field
            while (numberofHunters != 0)
            {
                AllAnimals.Add(new Hunter());
                Arrows += 20;
                numberofHunters--;
            }

            while (numberOfLion != 0)
            {
                AnimalId++;
                AllAnimals.Add(new Lion(RandomRoll.GenderRoll(), AnimalId));
                numberOfLion--;
            }

            while (numberOfRabbits != 0)
            {
                AnimalId++;
                AllAnimals.Add(new Rabbit(RandomRoll.GenderRoll(), AnimalId));
                numberOfRabbits--;
            }
        }

        //Create a FlatList of animals on territories
        public List<Animal> FlatList()
        {

            var flattenList = Territories.SelectMany(c => c).Where(a => AllAnimals.Contains(a.animal))
                .Select(x => x.animal).ToList();

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
                AllAnimals.Remove(animal);
            }
        }

        //Random select move from validmoves
        private (int, int) SelectetMove(Animal animal)
        {

            var validFields = ValidMovesForAnimal(animal);
            var randomfield = RandomRoll.RField(validFields);
            var selectedRamdomFeld = validFields[randomfield];
            var posOfRField = XandY(selectedRamdomFeld);
            return posOfRField;
        }

        //Select animal on its Territories
        public Field SelectAnimalOnTerritorie(Animal animal)
        {
            var theField = Territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal);
            return theField;
        }


        //Movement for animals
        private void AnimalMovement(Animal animal)
        {
            if (AllAnimals.Contains(animal))
            {
                if (Territories.SelectMany(c => c).Select(c => c.animal).Contains(animal))
                {

                    var x = SelectAnimalOnTerritorie(animal);

                    var savedAnimal = x.animal;
                    savedAnimal.Move();
                    var randomPos = SelectetMove(savedAnimal);
                    var fPos = Territories[randomPos.Item1][randomPos.Item2];

                    switch (savedAnimal)
                    {
                        //If rabbit move to greenfield, it eats
                        case Rabbit _ when fPos.GreenField:
                            animal.Eat();
                            Console.WriteLine($"Rabbit {savedAnimal.ID} Ate some GRASS and now weigths {savedAnimal.Weight}");
                            GrassEaten++;
                            break;

                        //Rabbit Mate if a rabbit is on new pos, and the rabbit gender is different
                        case Rabbit _ when fPos.animal is Rabbit && animal.Gender != fPos.animal.Gender:
                            NewCubs(true);
                            RabbitCubCounter += 2;
                            TotalCubCounter += 2;
                            RemoveAnimalFromField(fPos.animal);
                            Placement();
                            break;

                        //If lion move to a field with a rabbit, it eats it.
                        case Lion _ when fPos.animal is Rabbit:
                            var soonToDieRabbit = fPos.animal;
                            Console.WriteLine($"Lion number {savedAnimal.ID} eat Rabbit: {soonToDieRabbit.ID} and gained {soonToDieRabbit.Weight} more kg");
                            RemoveAnimal(soonToDieRabbit);

                            LionsRabbitKillsCounter++;
                            savedAnimal.Eat();
                            break;

                        //Lion Mate if a Lion is on new pos, and the Lion gender is different
                        case Lion _ when fPos.animal is Lion && animal.Gender != fPos.animal.Gender:
                            NewCubs(false);
                            LionCubCounter++;
                            TotalCubCounter++;
                            Console.WriteLine($"Lion: {savedAnimal.ID} ({savedAnimal.Gender}) and Lion {fPos.animal.ID} ({fPos.animal.Gender}) are now parents :D");
                            RemoveAnimal(fPos.animal);
                            break;
                    }
                    //Male Animals kill eachother of they land on same field 
                    if (fPos.animal != null && animal is Lion && savedAnimal.Gender == fPos.animal.Gender && fPos.animal is Lion)
                    {

                        var newWeigth = savedAnimal.Weight + fPos.animal.Weight;
                        Console.WriteLine($"Lion: {fPos.animal.ID} IS DEAD because Lion: {savedAnimal.ID} landed on same field. {savedAnimal.ID} Gained {fPos.animal.Weight}, and now weigths {newWeigth}");
                        RemoveAnimal(fPos.animal);
                        LionsKilled++;
                        // Thread.Sleep(100);
                    }

                    if ((fPos.animal != null) && animal is Rabbit && savedAnimal.Gender == fPos.animal.Gender && fPos.animal is Rabbit)
                    {

                        Console.WriteLine($"Rabbit: {fPos.animal.ID} IS DEAD because Rabbit: {savedAnimal.ID} landed on same field. ");

                        RemoveAnimal(fPos.animal);


                    }
                    if (savedAnimal is Hunter && fPos.animal != null)
                    {
                        savedAnimal.Eat();
                        Arrows -= 1;
                        HunterKillCount++;
                        RemoveAnimal(fPos.animal);
                    }

                    if (savedAnimal is Hunter && fPos.animal == null)
                    {
                        Arrows += 1;
                    }

                    try
                    {
                        if (AllAnimals.Contains(savedAnimal))
                        {
                            RemoveAnimalFromField(savedAnimal);
                        }

                        Territories[randomPos.Item1][randomPos.Item2].animal = savedAnimal;
                        Dead(savedAnimal);
                    }
                    catch (Exception)
                    {
                        Dead(savedAnimal);
                    }
                }
            }
        }

        //Remove animal from field
        private void RemoveAnimalFromField(Animal animal)
        {
            Territories.SelectMany(c => c).Select(c => c).First(c => c.animal == animal).animal = null;
        }

        //Check if animal is dead, and remove if it it is
        private void Dead(Animal animal)
        {
            if (animal.Weight <= 0)
            {
                Console.WriteLine($"{animal} ({animal.ID}) is dead... :(");
                RemoveAnimal(animal);
                Console.WriteLine(AllAnimals.Count());
            }
            if (AllAnimals.Count() <= NumberofHunters)
            {
                NonSurvivers();
            }
        }

        //Add Cubs to the animal list
        private void NewCubs(bool rabbit, int NumberOfNewRabbits = 2, int NumberOfNewLions = 1)
        {

            if (rabbit)
            {
                while (NumberOfNewRabbits != 0)
                {
                    AddAnimal(0, 1, 0);

                    NumberOfNewRabbits--;
                }

            }
            else if (rabbit == false)
            {

                while (NumberOfNewLions != 0)
                {
                    AddAnimal(1, 0, 0);
                    NumberOfNewLions--;
                }
            }
            Placement();
        }

        //Check for valid moves that the animal can choose from
        private List<Field> ValidMovesForAnimal(Animal animal)
        {
            //Set moves for lion, hunter or rabbit
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

        //Gets the coordination of a field
        private (int, int) XandY(Field field)
        {
            //Take a field and find the cordinates of it 

            var boardFields = Territories.Select(s => s).First(s => s.Contains(field));

            int yCon = Territories.FindIndex(c => c == boardFields);
            int xCon = boardFields.FindIndex(c => c == field);

            return (xCon, yCon);

        }

        //If only Hunter is left on the savannah
        public string NonSurvivers()
        {
            return "Hunter Won";
        }

        public string NoMoreRoom()
        {
            return "Savannah full";
        }
    }
}

