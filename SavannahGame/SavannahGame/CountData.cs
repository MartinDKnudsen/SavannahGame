using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavannahGame
{
    class CountData
    {
        GameLogic Gl = new GameLogic();

        //Count all animals on the Savannah
        public int CountAnimals()
        {
            return Gl.territories.SelectMany(c => c).Count(c => c.animal != null);
        }

        //Count Greenfields
        public int CountGreenField()
        {
            return Gl.territories.SelectMany(e => e).Count(e => e.GreenField);
        }

        //Count number of Fields
        public int CountField()
        {
            //Tæller felter
            return Gl.territories.SelectMany(f => f).Count();
        }

        //Count number of selected animal on Territories
        public int CountAllSpecAnimalOnTheTerritories<T>()
        {
            int SelectedAnimalsOnTheTerritories = Gl.territories.SelectMany(c => c).Count(c => c.animal is T);
            return SelectedAnimalsOnTheTerritories;
        }

        // Count all animals in Animal List
        public int CountAnimalsByType<T>()
        {
            var AnimalsOfType = Gl.AllAnimals.Count(c => c is T);
            return AnimalsOfType;
        }


        //Show the weigth of all animals of a specific type on the Tavannah
        public double WeigthOfAllAnimalsOfAType<T>()
        {

            var XAnimalWeigth = Gl.FlatList().OrderByDescending(x => x.Weight).Where(c => c is T).ToList();

            double total = XAnimalWeigth.Sum(item => item.Weight);
            return total;

        }

        //Print all animals
        public void PrintAllAnimals()
        {
            if (Gl.AllAnimals.Count != 0)
            {
                var list = Gl.AllAnimals.Where(c => c is Lion);
                var list2 = Gl.AllAnimals.Where(c => c is Rabbit);
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
