using System.Collections.Generic;
using System.Linq;

namespace KittenFight.Models
{
    public abstract class PlayerAbstract
    {
        public Queue<Unit> Units { get; protected set; }
        public Unit CurrentUnit { get; protected set; }
        public string OriginalSequence { get; protected set; }

        protected PlayerAbstract(string units)
        {
            OriginalSequence = units;
        }

        public bool HasUnits => Units.Any();

        public virtual void GetNextUnit()
        {
            CurrentUnit = Units.Dequeue();
        }

        public void SetUnits(string units)
        {
            Units = new Queue<Unit>(units.ToCharArray().Select(x => new Unit(x)));
            CurrentUnit = null;
        }
    }
}
