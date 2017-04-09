namespace KittenFight.Models
{
    public class Player : PlayerAbstract
    {
        public string FinalSequence { get; set; }

        public Player(string units) : base(units)
        {
            SetUnits(units);
        }

        public override void GetNextUnit()
        {
            base.GetNextUnit();
            FinalSequence += CurrentUnit.Type.ToString();
        }
    }
}
