using System.Data;
using System.Linq;
using Persistens;

namespace SavannahGame
{
    public class Controller
    {
        private Database DB = new Database();

        private GameLogic gl = GameLogic.Getinstance();

        private CountData cD = SavannahGame.CountData.Getinstance();

        
        public void StartSavannahGame(int rabbits, int lions)
        {

            gl.StartGame(lions, rabbits);
    
        }

        public int CountData()
        {

          return gl.AllAnimals.Count();
        }
        public int lionsKilled()
        {

            return gl.LionsKilled;
        }
        public int hunterKills()
        {

            return gl.HunterKillCount;

        }
          
        public int CountRabbits()
        {

            return cD.CountAllSpecAnimalOnTheTerritories<Rabbit>();

        }
        public int countGreenField()
        {

            return cD.CountGreenField();
        }

        public int CountCubsBorn()
        {

            return gl.TotalCubCounter;

        }
        public double TotalWeigthOfLions()
        {

            return cD.WeigthOfAllAnimalsOfAType<Lion>();
        }

        public double TotalWeigthOfRabbits()
        {

            return cD.WeigthOfAllAnimalsOfAType<Rabbit>();
        }
        public int KilledRabbits()
        {
            return gl.LionsRabbitKillsCounter;

        }
        public DataTable DT()
        {
          return DB.GetData();
        }
        public int rabbitCubs()
        {
          return  gl.RabbitCubCounter;

        }

        public int lionCubs()
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
      //  Kobling mellem methoder og forms

      public void testafgame()
      {
            gl.GameRunning();
      }
        public int TotalAnimals()
        {
            return cD.CountAnimals();

        }
    }
}
