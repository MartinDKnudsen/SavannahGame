using System;
using System.Collections.Generic;
using System.Text;

namespace Persistens
{
    interface IDatabase
    {

        void SaveData(int totalCubs, int AnimalsKilled, int AnimalsKilledByHunter);

    }
}
