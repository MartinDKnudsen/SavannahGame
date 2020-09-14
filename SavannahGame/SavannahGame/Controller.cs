using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavannahGame
{
    class Controller
    {
    
        private void startSavannahGame()
        {
            GameLogic GM = new GameLogic();
            GM.StartGame(10,20);

        }
        //Kobling mellem methoder og forms

    }
}
