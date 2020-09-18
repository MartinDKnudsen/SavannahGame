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

        public int CountData()
        {

          return gl.AllAnimals.Count();
        }
        public int lionsKilled()
        {

            return gl.lionKilled();
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

            return gl.NumberOfBornCubs();

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
            return gl.RabbitsKilled();

        }

        public int rabbitCubs()
        {
          return  gl.numberOfNewrabbits();

        }

        public int lionCubs()
        {
            return gl.numberOfNewLions();
        }


        public int GrassEat()
        {

            return gl.GrassEated();
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
