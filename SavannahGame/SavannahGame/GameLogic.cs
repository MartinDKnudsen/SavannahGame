using System.Collections.Generic;

namespace SavannahGame
{
   public class GameLogic
    {

        List<List<Field>> territories = new List<List<Field>>();

        public void Placement()
        {
            //Giv alle dyr en position 
        }
        public void AddFields()
        {
            for (int i = 0; i < 20; i++)
            {
                territories.Add(new List<Field>());
                for (int j = 0; j < 20; j++)
                {
                    territories[i].Add(new Field());
                    
                }
            }
            
        }
        public void AddAnimal(Animal a)
        {


        }

        public void RemoveAnimal(Animal a)
        {

        }



    }
}
