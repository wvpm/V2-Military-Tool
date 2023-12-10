using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2_Military_Tool
{
    public abstract class Discovery
    {
        public string Name { get; private set; }
        public List<Effect> Effects { get; private set; }

        public Discovery (string name, List<Effect> effects)
        {
            Name = name;
            Effects = effects;
        }
    }
}
