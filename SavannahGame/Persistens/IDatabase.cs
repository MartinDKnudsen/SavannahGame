using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Persistens
{
    interface IDatabase
    {

        void SaveData(int totalCubs, int AnimalsKilled, int AnimalsKilledByHunter);
        DataTable GetData();

    }
}
