using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Schema;

namespace SavannahGame
{
    public static class RandomRoll
    {
        //Static fordi det altid er samme instans, eller opretter den ny random hver gang jeg kalder den.(Baseret på tid)
        //readonly = cant change it
        private static readonly Random random = new Random();

        //Rolls gender for animals
        public static string GenderRoll()
        {
            var mOrf = RandomNumber(1, 50);

            if (mOrf >= 25)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }
        }

        //Roll for a random of GreenFields
        public static int Greenfield()
        {
            var gField = RandomNumber(1, 20);
            return gField;

        }
        //Rolls animals placement in the GameLogic placement method
        public static int AnimalRandomPlacement()
        {
            var ARP = RandomNumber(0, 20);

            return ARP;
        }

        //Rolls random number
        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max);
        }

        //Rolls between valid fields
        public static int RField(List<Field> field)
        {
            var randomField = random.Next(field.Count);
            return randomField;

        }
    }

}

