﻿namespace UnitConverter.UnitUtil
{
    /// <summary>
    /// Klasa dziedzicząca klasę {Unit}. Służy ona do przechowywania danych odnośnie jednostki bazowej.
    /// </summary>
    /// <see cref="Unit"/>
    class BaseUnit : Unit
    {
        public BaseUnit(string name) : base(name, value => value, value => value) { }
    }
}
