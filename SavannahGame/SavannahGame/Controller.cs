namespace SavannahGame
{
    public class Controller
    {
      private GameLogic Gl = new GameLogic();
      private CountData Cd = new CountData();


        public void StartSavannahGame(int rabbits, int lions)
        {
            Gl.StartGame(lions,rabbits);
        }
        

        public void CountData()
        {
           

        }
        //Kobling mellem methoder og forms

    }
}
