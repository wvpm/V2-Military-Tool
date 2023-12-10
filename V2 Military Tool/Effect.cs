using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2_Military_Tool
{
    public class Effect
    {
        public string Target { get; private set; }
        public decimal Organisation { get; private set; }
        public decimal Attack { get; private set; }
        public decimal Defence { get; private set; }
        public decimal Support { get; private set; }
        public UInt16 Maneuver { get; private set; }
        public decimal Supply_Consumption { get; private set; }

        public Effect(string target, decimal organisation = 0, decimal attack = 0, decimal defence = 0, decimal support = 0, UInt16 maneuver = 0, decimal supply_consumption = 0)
        {
            Target = target;
            Organisation = organisation;
            Attack = attack;
            Defence = defence;
            Support = support;
            Maneuver = maneuver;
            Supply_Consumption = supply_consumption;
        }
    }
}
