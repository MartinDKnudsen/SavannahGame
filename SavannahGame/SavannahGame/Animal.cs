using System.Collections.Generic;

namespace SavannahGame
{
    abstract class Animal
    {

        string Genger;

        int Number;

        double Weight;
        double WeigthGain;
        double WeightLoss;

        int xCon;
        int yCon;


        List<Animal> Animals;

        public abstract void Eat();

        public abstract void Move();



    }
}
