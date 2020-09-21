using System.Data.SqlClient;

namespace Persistens
{
    class Database : IDatabase

    {
        //Constring
        private string connString = "server=LAPTOP-4FFA90NO\\MAINSQLSERVER; database = SavannahGame; Trusted_Connection=true";

        public void GetData()
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand c = new SqlCommand("select * from Simulations", conn);
            c.ExecuteNonQuery();
            conn.Close();
        }

        public void SaveData(int totalCubs, int AnimalsKilled, int AnimalsKilledByHunter)
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand c = new SqlCommand($"insert into Simulations(totalCubs, AnimalsKilled, AnimalKilledByHunter) values {totalCubs}, {AnimalsKilled}, {AnimalsKilledByHunter}", conn);
            c.ExecuteNonQuery();
            conn.Close();
        }
    }
}

//CREATE TABLE Simulations (
//SimulationNr int IDENTITY(1,1) PRIMARY KEY,
//totalCubs int,
//AnimalsKilled int,
//AnimalsKilledByHunter int);

//INSERT INTO Simulations (totalCubs, AnimalsKilled, AnimalsKilledByHunter)
//VALUES(30, 20, 40);

//select* from Simulations