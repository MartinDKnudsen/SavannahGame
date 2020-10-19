using System.Data;

namespace Persistens
{
    public interface IDatabase
    {
        void SaveDataToDatabase(int totalCubs, int AnimalsKilled, int AnimalsKilledByHunter);
        DataTable GetData();

    }
}
