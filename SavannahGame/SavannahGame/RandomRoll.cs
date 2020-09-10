using System;
using System.Xml.Schema;

namespace SavannahGame
{
    public static class RandomRoll
    {
        //Static fordi det altid er samme instans, eller opretter den ny random hver gang jeg kalder den.(Baseret på tid)
        //readonly = kan ikke ændre på den.
        private static readonly Random random = new Random();

        public static string rRoll()
        {
            var mOrf = RandomNumber(1, 50);

            if (mOrf >= 25)
            {
                return "Female";
            }
            else if (mOrf < 25)
            {
                return "Male";
            }

            return null;
        }
        public static int Greenfield()
        {
            var gField = RandomNumber(1, 20);
            return gField;

        }
        public static int AnimalRandomPlacement()
        {
            var ARP = RandomNumber(0, 20);

            return ARP;
        }

        public static (int, int) NewPos()
        {
            int fNumber = RandomNumber(0, 20);
            int sNumber = RandomNumber(0, 20);
            return (fNumber, sNumber);
        }

        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }

}

