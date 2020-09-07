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
            m.NewCubs(false);
    

            var k = m.territories.SelectMany(c => c).Count(c => c.animal != null);
            var antalfelter = m.territories.SelectMany(f => f).Count();
            //Viser hvor mange grønne felter der er oprettet: 
            var isgreenfield = m.territories.SelectMany(e => e).Count(e => e.GreenField);

           Console.WriteLine($"Samlet er der {k} dyr");
            Console.WriteLine($"I alt er der {antalfelter} felter");
            Console.WriteLine($"af dem er der {isgreenfield} grønne felter");

            Console.ReadLine();
        }
    }
}
