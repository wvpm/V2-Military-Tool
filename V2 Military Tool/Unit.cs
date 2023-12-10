using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2_Military_Tool
{
    public class Unit
    {
        public string Name { get; private set; }
        public decimal Organisation { get; private set; }
        public decimal Attack { get; private set; }
        public decimal Defence { get; private set; }
        public decimal Discipline { get; private set; }
        public decimal Support { get; private set; }
        public UInt16 Maneuver { get; private set; }
        public decimal Supply_Consumption { get; private set; }

        public Unit(string name, decimal organisation, decimal attack, decimal defence, decimal discipline, decimal support, UInt16 maneuver, decimal supply_consumption)
        {
            Name = name;
            Organisation = organisation;
            Attack = attack + 10;
            Defence = defence + 10;
            Discipline = discipline;
            Support = support;
            Maneuver = maneuver;
            Supply_Consumption = supply_consumption;
        }
    }
}
