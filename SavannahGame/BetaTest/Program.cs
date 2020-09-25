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

            var m = GameLogic.Getinstance();
           
            m.StartGame(100, 100, 20);
         
            Console.ReadLine();
        }
    }
}

