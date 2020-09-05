using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavannahGame;

namespace BetaTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var m = new GameLogic();
            // m.AddFields();
         // m.AddAnimal("Lion",10);
            m.AddAnimal(10,10);

            Console.ReadLine();
        }
    }
}
