﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitKonwerter
{
    public class KonwerterMasy : IKonwerter
    {
        public string Name => "Masy";

        public List<string> Units => new List<string>()
            {
                "kg",
                "F"
            };

        public string Convert(string unitFrom, string unitTo, string valueToConvert)
        {
            string wynik;
            if (unitFrom == "kg")
            {
                wynik = (double.Parse(valueToConvert) *2.2046).ToString();

                return wynik;
            }
            else
            {
                wynik = (double.Parse(valueToConvert) / 2.2046).ToString();
                return wynik;
            }
        }
    }
}