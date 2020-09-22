using System.Data;
using System.Data.SqlClient;
namespace Persistens
{
    public class Database : IDatabase
    {
        private string connString = "Server=den1.mssql8.gear.host;Database=	savannahgame1;User Id=savannahgame1;Password=Pv2GC52df-~6;";

        public DataTable GetData()
        {
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand c = new SqlCommand("select * from Simulations", conn);
            c.ExecuteNonQuery();
            SqlDataReader reader = c.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            conn.Close();
            return dt;

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