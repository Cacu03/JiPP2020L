using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitConverter.Logic
{
    public abstract class cUnitFactory
    {
        public abstract iUnit unitBuilder(int choice);
    }

}
