using SavannahGame;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetaTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var m = new GameLogic();
            m.AddFields();

            m.AddAnimal(5, 5);
            m.Placement();
            //m.NewCubs(true);
            //m.RemoveAnimal(2);
            //var ekg = m.CountGreenField();
            //var ekm = m.CountField();

            //// Count all animals of a specific type on all Territories 
            //var kkk = m.CountAllSpecAnimalOnTheTerritories<Lion>();
            //var rabbit = m.CountAllSpecAnimalOnTheTerritories<Rabbit>();

            //Console.WriteLine($"Af løver er der {kkk}");
            //Console.WriteLine($"There is {rabbit} rabbits on the savannah");

            //// Console.WriteLine($"der er {tæl} løver i listen");
            ////  m.CountAnimalsByType();

            //int LøverAllAnimals = m.AllAnimals.Count(c => c is Lion);

            //var test = m.territories.SelectMany(c => c).Where(a => m.AllAnimals.Contains(a.animal)).Select(x => x.animal);
            //var list = test.OrderByDescending(x => x.Gender).ToList();
            //var listweigth = test.OrderByDescending(x => x.Weight).Where(c => c is Lion).ToList();
            //int LionsOnTheSavannah = m.territories.SelectMany(c => c).Count(c => c.animal is Lion);


            ////// Console.WriteLine(LionsOnTheSavannah);

            ////double total = listweigth.Sum(item => item.Weight);
            ////double totaltest = listweigth.Sum(item => item.Weight);

            ////Console.WriteLine();

            ////Console.WriteLine($"Total weigth for all animals on the Savannah: {total}");


            ////Console.WriteLine($"Af løver er der {kkk}");
            ////Console.WriteLine($"There is {rabbit} rabbits on the savannah");

            ////var her = m.WeigthOfAllAnimalsOfAType<Lion>();


            ////Console.WriteLine($"All Lions Weigth: {her}");

            ////double whatever; 
            ////foreach (var item in test)
            ////{
            ////    Console.WriteLine(item.Weight);

            ////}

            //foreach (var item in list)
            //{
            //    Console.WriteLine($"Type: {item} -- Gender: {item.Gender} -- weight: {item.Weight}");

            //}
            //Console.WriteLine();

            //Console.WriteLine($"I alt er der {ekm} felter");
            //Console.WriteLine($"af dem er der {ekg} grønne felter");


            //var testAnimal = m.AllAnimals[0];
            //var testAnimal2 = m.AllAnimals[4];


            //foreach (var item in m.AllAnimals)
            //{
            //    // Console.WriteLine(m.territories.SelectMany(s => s).Select(s => s).First(s => s.animal == item).Id);

            //    m.AnimalMovement(m.territories.SelectMany(s => s).Select(s => s.animal).First(s => s == item));

            //    // Console.WriteLine(m.territories.SelectMany(s => s).Select(s => s).First(s => s.animal == item).Id);
            //}


            //foreach (var item in m.AllAnimals)
            //{
            //    Console.WriteLine(m.territories.SelectMany(s => s).Select(s => s).First(s => s.animal == item).Id);

            //    m.AnimalMovement(m.territories.SelectMany(s => s).Select(s => s.animal).First(s => s == item));

            //    Console.WriteLine(m.territories.SelectMany(s => s).Select(s => s).First(s => s.animal == item).Id);
            //}

            //Console.WriteLine(m.territories.SelectMany(s => s).Select(s => s).First(s => s.animal == testAnimal).Id);

            //m.AnimalMovement(m.territories.SelectMany(s => s).Select(s => s.animal).First(s => s == testAnimal));

            //Console.WriteLine(m.territories.SelectMany(s => s).Select(s => s).First(s => s.animal == testAnimal).Id);
            //     m.PrintAllAnimals();

            //foreach (var item in m.AllAnimals.ToList())
            //{


            //    m.RemoveAnimal(m.territories.SelectMany(s => s).Select(s => s.animal).First(s => s == item));

            //}
            //m.PrintAllAnimals();

            //Console.WriteLine();


            m.PrintFelter();
            //m.RemoveAnimal(1);
            //m.RemoveAnimal(2);
            //m.RemoveAnimal(3);
            //m.RemoveAnimal(4);
            //  m.NewCubs(true);
            //    m.NewCubs(false);


            Console.ReadLine();
        }
    }
}
