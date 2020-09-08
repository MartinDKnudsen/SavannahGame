using SavannahGame;
using System;
using System.Linq;

namespace BetaTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var m = new GameLogic();
            m.AddFields();
            // m.AddAnimal("Lion",10);
            m.AddAnimal(10, 10);
            m.Placement();
            m.NewCubs(true);
            var eks =  m.CountAnimals();
            var ekg = m.CountGreenField();
            var ekm = m.CountField();

            var tæl = m.CountAnimalsByType(true);
            var TypeAfDyrPåSavannah = m.CountSpecAnimalOnTheTerritories(false);
            Console.WriteLine($"There is {TypeAfDyrPåSavannah} rabbits on the savannah");

            Console.WriteLine($"der er {tæl} løver i listen");
            //  m.CountAnimalsByType();

            int LøverAllAnimals = m.AllAnimals.Count(c => c is Lion);
          
            var test =   m.territories.SelectMany(c => c).Where(a => m.AllAnimals.Contains(a.animal)).Select(x => x.animal);
            var list = test.OrderByDescending(x => x.Gender).ToList();

            int LionsOnTheSavannah = m.territories.SelectMany(c => c).Count(c => c.animal is Lion);

            Console.WriteLine(LionsOnTheSavannah);
            Console.WriteLine(LøverAllAnimals);

            //foreach (var item in list)
            //{
            //    Console.WriteLine($"Type: {item} -- Gender: {item.Gender} -- weight: {item.Weight}");
            //}
           
            //Console.WriteLine($"Samlet er der {eks} dyr");
            //Console.WriteLine($"I alt er der {ekm} felter");
            //Console.WriteLine($"af dem er der {ekg} grønne felter");

            Console.ReadLine();
        }
    }
}
