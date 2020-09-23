using System.Data;

namespace Persistens
{
    public interface IDatabase
    {

        void SaveData(int totalCubs, int AnimalsKilled, int AnimalsKilledByHunter);
        DataTable GetData();

    }
}
