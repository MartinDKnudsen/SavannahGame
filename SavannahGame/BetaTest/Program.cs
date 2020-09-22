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
            m.RGame = true;
            m.StartGame(100, 100);
         
            Console.ReadLine();
        }
    }
}

