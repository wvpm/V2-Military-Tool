using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2_Military_Tool
{
    public class Invention : Discovery
    {
        public UInt16 Year { get; private set; }
        public Invention(string name, List<Effect> effects, UInt16 year) : base(name, effects)
        {
            Year = year;
        }
        public Invention(string name, List<Effect> effects, List<Technology> requirements) : base(name, effects)
        {
            Year = requirements.Max(x => x.Year);
        }
    }
}
