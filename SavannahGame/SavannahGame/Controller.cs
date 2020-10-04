using System.Data;
using System.Linq;
using Persistens;
using BusinessLogic;
using System.Security.Policy;

namespace SavannahGame
{
    public class Controller
    {
        private Database db = new Database();

        IDatabase DB;

        private GameLogic gl = GameLogic.Getinstance();

        private CountData cD = CountData.Getinstance();

        public Controller()
        {
            DB = db;
        }

        
        public int CountArrows()
        {
            return gl.Arrows;

        }
        public string HunterWon()
        {
            return gl.NonSurvivers();

        }

        public string FullSavanna()
        {
            return gl.NoMoreRoom();
        }

        public void StartSavannahGame(int rabbits, int lions, int hunters)
        {
            gl.StartGame(lions, rabbits, hunters);
        }
        public void SaveData(int totalCubs, int AnimalsKilled, int AnimalsKilledByHunter)
        {
            DB.SaveDataToDatabase(totalCubs, AnimalsKilled, AnimalsKilledByHunter);
        }
        public DataTable Dt()
        {
            return DB.GetData();
        }

        public void PrintDataTable(string name, DataTable datatable)
        {

            TxtPrinter.Write(name, datatable);
        }
    
        public void OpenSavedTxt()
        {

            TxtOpener.SupportTxt();
        }
        public int LionsKilled()
        {

            return gl.LionsKilled;
        }
        public int HunterKills()
        {

            return gl.HunterKillCount;

        }
          
        public int CountRabbits()
        {

            return cD.CountAllSpecAnimalOnTheTerritories<Rabbit>();

        }
        public int CountGreenField()
        {

            return cD.CountGreenField();
        }

        public int CountCubsBorn()
        {

            return gl.TotalCubCounter;

        }
        public double TotalWeigthOfLions()
        {

            return cD.WeightOfAllAnimalsOfAType<Lion>();
        }

        public double TotalWeigthOfRabbits()
        {

            return cD.WeightOfAllAnimalsOfAType<Rabbit>();
        }
        public int KilledRabbits()
        {
            return gl.LionsRabbitKillsCounter;

        }
    
        public int RabbitCubs()
        {
          return  gl.RabbitCubCounter;

        }

        public int LionCubs()
        {
            return gl.LionCubCounter;
        }


        public int GrassEat()
        {

            return gl.GrassEaten;
        }

        public int CountLions()
        {
            return cD.CountAllSpecAnimalOnTheTerritories<Lion>();
        }
      
        public int TotalAnimals()
        {
            return cD.CountAnimals();

        }

    }
}
