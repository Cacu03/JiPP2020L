﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitFile.Logic
{
    public class Parallelogram : IFigure
    {
        public string Nazwa => "Równoległobok";

        public double Calculate(double a, double b, double h)
        {
            double sideA = a;
            double sideB = b;
            double sideH = h;

            return (sideA * sideH);
        }
    }
}
