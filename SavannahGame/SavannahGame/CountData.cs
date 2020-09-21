using System;
using System.Linq;

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
            return gl.Territories.SelectMany(e => e).Count(e => e.GreenField);
        }

        //Count number of Fields
        public int CountField()
        {
            //Tæller felter
            return gl.Territories.SelectMany(f => f).Count();
        }

        //Count number of selected animal on Territories
        public int CountAllSpecAnimalOnTheTerritories<T>()
        {
            int SelectedAnimalsOnTheTerritories = gl.Territories.SelectMany(c => c).Count(c => c.animal is T);
            return SelectedAnimalsOnTheTerritories;
        }

        // Count all animals in Animal List
        public int CountAnimalsByType<T>()
        {
            var AnimalsOfType = gl.AllAnimals.Count(c => c is T);
            return AnimalsOfType;
        }


        //Show the weigth of all animals of a specific type on the Tavannah
        public double WeigthOfAllAnimalsOfAType<T>()
        {
            try
            {
                var XAnimalWeigth = gl.FlatList().OrderByDescending(x => x.Weight).Where(c => c is T).ToList();
                double total = XAnimalWeigth.Sum(item => item.Weight);
                return total;
            }
            catch (Exception)
            {

                throw;
            }
           

          

        }

        //Print all animals
        public void PrintAllAnimals()
        {
            if (gl.AllAnimals.Count != 0)
            {
                var list = gl.AllAnimals.Where(c => c is Lion);
                var list2 = gl.AllAnimals.Where(c => c is Rabbit);
                foreach (var item in list)
                {
                    Console.WriteLine($"Id of animal: {item.ID} -- Gender: {item.Gender} -- is Type: {item.AnimalType}");

                }
                Console.WriteLine("_________________________");

                foreach (var item in list2)
                {
                    Console.WriteLine($"Id of animal: {item.ID} -- Gender: {item.Gender} -- Type: {item.AnimalType}");
                }

                Console.WriteLine("_________________________");
            }
            else
            {
                Console.WriteLine("All animals are dead");
            }
        }

    }
}
