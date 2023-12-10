using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2_Military_Tool
{
    public class Technology : Discovery
    {
        public UInt16 Year { get; private set; }
        public Technology (string name, UInt16 year, List<Effect> effects) : base (name, effects)
        {
            Year = year;
        }
    }
}
