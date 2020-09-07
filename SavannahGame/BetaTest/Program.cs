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
            m.AddAnimal(200, 100);
            m.Placement();

            var k = m.territories.SelectMany(c => c).Count(c => c.animal != null);
            var hvemder = m.territories.SelectMany(d => d).First(d => d.animal != null).animal;
            var isgreenfield = m.territories.SelectMany(e => e).Count(e => e.GreenField);



            Console.WriteLine(isgreenfield);

            Console.ReadLine();
        }
    }
}
