using System;

namespace SavannahGame
{
    public class BoardFields
    {
        //Number af fields generated in two dim Array

        public int[,] GenerateFields()
        {


            return new int[21, 21];

        }
        public int AddGrass ()
        {
            var m = 0;
            var p = 0;
            Random r = new Random();

            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {

                    m =+ i;
                    p =+ j;
                }
            }

            return m;

        }

    }
}
