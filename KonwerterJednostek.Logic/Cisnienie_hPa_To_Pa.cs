﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonwerterJednostek.Logic
{
   public class Cisnienie_hPa_To_Pa : IKonwerter

    {
        public string Name => "Ciśnienie";

        public List<string> Units => new List<string>()
        {
            "Pa",
            "hPa"
        };

        public double Convert(double valueToConvert)
        {
            return valueToConvert * 100;
        }
    }
}
