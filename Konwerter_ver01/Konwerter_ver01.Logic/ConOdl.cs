﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konwerter_ver01
{
    public class ConOdl : IConverter
    {
        public string Name => "Odległości";

        public List<string> Jedn => new List<string>()
        {
        "km", "mi", "m"
        };
        public string Convert(string JednZ, string JednDo, string Dane)
        {
            double Wart = double.Parse(Dane);
            if (JednZ == "km" || JednZ == "kilometr")
            {
                if (JednDo == "mi" || JednDo == "mila")
                {
                   
                    return (Wart * 0.62137).ToString();
                }
                if (JednDo == "m" || JednDo == "metr")
                {
                    return (Wart *1000).ToString();
                }
                if (JednDo == "km" || JednDo == "kilometr") { return Wart.ToString(); }
                else Console.WriteLine("Program nie obsługuje konwersji z {0} do {1}. ", JednZ, JednDo);
            }
            if (JednZ == "mi" || JednZ == "mila")
            {
                if (JednDo == "km" || JednDo == "kilometr")
                {
                    return (Wart / 0.62137).ToString();
                }
                if (JednDo == "m" || JednDo == "metr")
                {
                    return ((Wart / 0.62137)*1000).ToString();
                }
                if (JednDo == "mi" || JednDo == "mila") { return Wart.ToString(); }
                else Console.WriteLine("Program nie obsługuje konwersji z {0} do {1}. ", JednZ, JednDo);
            }
            if (JednZ == "m" || JednZ == "metr")
            {
                if (JednDo == "km" || JednDo == "kilometr")
                {
                    return (Wart /1000).ToString();
                }
                if (JednDo == "mi" || JednDo == "mila")
                {
                    return ((Wart /1000) * 0.62137).ToString();
                }
                if (JednDo == "m" || JednDo == "metr") { return Wart.ToString(); }
                else Console.WriteLine("Program nie obsługuje konwersji z {0} do {1}. ", JednZ, JednDo);
            }
            else Console.WriteLine("Program nie obsługuje konwersji z {0} do {1}. ", JednZ, JednDo); return "";
        }
    }
}