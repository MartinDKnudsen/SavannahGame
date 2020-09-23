using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SavannahGame
{
    public class CountData
    {

        private GameLogic gl = GameLogic.Getinstance();

        private static CountData cD = null;

        private CountData()
        {
            
        }
        public static CountData Getinstance()
        {
            return cD ?? (cD = new CountData());
        }

        //Count all animals on the Savannah
        public int CountAnimals()
        {
            return gl.Territories.SelectMany(c => c).Count(c => c.animal != null);
        }

        //Count Greenfields
        public int CountGreenField()
        {
            return gl.Territories.SelectMany(e => e).ToList().Count(e => e.GreenField);
        }

        //Count number of Fields
        public int CountField()
        {
            //Tæller felter
            return gl.Territories.SelectMany(f => f).ToList().Count();
        }

        //Count number of selected animal on Territories
        public int CountAllSpecAnimalOnTheTerritories<T>()
        {
            return gl.Territories.SelectMany(c => c).Count(c => c.animal is T);
          
        }

        //Show the weigth of all animals of a specific type on the Tavannah
        public double WeigthOfAllAnimalsOfAType<T>()
        {
            var XAnimalWeigth = gl.FlatList().Where(c => c is T).ToList();
                double total = XAnimalWeigth.Sum(item => item.Weight);
                return total;
        }
      

    }
}
