namespace SavannahGame
{
    public class Controller
    {


        private GameLogic gl = GameLogic.Getinstance();

        private CountData Cd = new CountData();


        public void StartSavannahGame(int rabbits, int lions)
        {

            gl.StartGame(lions, rabbits);

        }

        public void CountData()
        {


        }

        public int CountLions()
        {

            return Cd.CountAllSpecAnimalOnTheTerritories<Lion>();
        }
        //Kobling mellem methoder og forms

    }
}
