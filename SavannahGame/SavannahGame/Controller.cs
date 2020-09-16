namespace SavannahGame
{
    public class Controller
    {
      public GameLogic Gl = new GameLogic();
      public CountData Cd = new CountData();


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
