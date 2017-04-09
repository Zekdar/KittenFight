using KittenFight.Enumerations;
using System;

namespace KittenFight.Models
{
    public class Unit
    {
        public UnitType Type { get; set; }

        public bool IsAlive { get; set; }

        public Unit(char unit)
        {
            Type = (UnitType)Enum.Parse(typeof(UnitType), unit.ToString());
            IsAlive = true;
        }

        public static UnitType GetCounterUnitType(UnitType unitType)
        {
            var counterType = UnitType.E; // Don't know what to set it to by default :(

            switch (unitType)
            {
                case UnitType.F:
                    counterType = UnitType.E;
                    break;
                case UnitType.E:
                    counterType = UnitType.P;
                    break;
                case UnitType.P:
                    counterType = UnitType.F;
                    break;
            }

            return counterType;
        }
    }
}
