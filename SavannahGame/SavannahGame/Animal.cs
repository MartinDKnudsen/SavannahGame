﻿using System;
using System.Collections.Generic;

namespace SavannahGame
{
  public abstract class Animal
    {
        public string Gender { get; set; }
        public double Weight { get; set; }
        public double WeigthGain { get; set; }
        public double WeightLoss { get; set; }

        public abstract void Eat();

        public abstract int Move();

        public abstract void Mate(int NumberOfCubs);

    }
}
