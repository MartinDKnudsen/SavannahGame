using System.Data.SqlClient;

namespace Persistens
{
    class Database : IDatabase

    {
        //Constring
        private string connString = "server=LAPTOP-4FFA90NO\\MAINSQLSERVER; database = SavannahGame; Trusted_Connection=true";

        public void SaveData(int totalCubs, int AnimalsKilled, int AnimalsKilledByHunter)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand c = new SqlCommand($"insert into games(totalCubs, AnimalsKilled, AnimalKilledByH) values {totalCubs}, {AnimalsKilled}, {AnimalsKilledByHunter}", conn);
            c.ExecuteNonQuery();
            conn.Close();
        }
    }
}
