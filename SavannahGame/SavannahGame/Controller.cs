using System.Linq;

namespace SavannahGame
{
    public class Controller
    {


        private GameLogic gl = GameLogic.Getinstance();

        private CountData cD = SavannahGame.CountData.Getinstance();

        
        public void StartSavannahGame(int rabbits, int lions)
        {

            gl.StartGame(lions, rabbits);
    
        }

        public void CountData()
        {
           

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
    }
}
