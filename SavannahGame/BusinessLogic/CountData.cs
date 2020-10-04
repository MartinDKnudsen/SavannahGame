using System.Linq;

namespace BusinessLogic
{
    public class CountData
    {

        private readonly GameLogic _gl = GameLogic.Getinstance();

        private static CountData _cD = null;

        private CountData()
        {
            
        }
        public static CountData Getinstance()
        {
            return _cD ?? (_cD = new CountData());
        }

        //Count all animals on the Savannah
        public int CountAnimals()
        {
            return _gl.Territories.SelectMany(c => c).Count(c => c.animal != null);
        }

        //Count Greenfields
        public int CountGreenField()
        {
            return _gl.Territories.SelectMany(e => e).Count(e => e.GreenField);
        }

        //Count number of Fields
        public int CountField()
        {
            //Tæller felter
            return _gl.Territories.SelectMany(f => f).ToList().Count();
        }

        //Count number of selected animal on Territories
        public int CountAllSpecAnimalOnTheTerritories<T>()
        {
            return _gl.Territories.SelectMany(c => c).ToList().Count(c => c.animal is T);
          
        }

        //Show the weight of all animals of a specific type on the Tavannah
        public double WeightOfAllAnimalsOfAType<T>()
        {
            var xAnimalWeigth = _gl.FlatList().Where(c => c is T).ToList();
                double total = xAnimalWeigth.Sum(item => item.Weight);
                return total;
        }
      

    }
}
