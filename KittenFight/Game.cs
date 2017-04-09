using KittenFight.Enumerations;
using KittenFight.Models;
using KittenFight.StaticFunctions;
using System.Collections.Generic;

namespace KittenFight
{
    public class Game
    {
        public Player Player { get; set; }
        public Oponent Oponent { get; set; }
        private Stack<string> Sequences { get; set; }

        public Game(Player player, Oponent oponent)
        {
            Player = player;
            Oponent = oponent;
            Sequences = new Stack<string>(Swapper.GetAllPermutations(Player.GenuineSequence));
        }

        private DuelResult Duel()
        {
            while (Player.HasUnits && Oponent.HasUnits
                || Player.CurrentUnit.IsAlive && Oponent.HasUnits
                || Oponent.CurrentUnit.IsAlive && Player.HasUnits)
            {
                if (Oponent.CurrentUnit == null || !Oponent.CurrentUnit.IsAlive)
                    Oponent.GetNextUnit();
                if (Player.CurrentUnit == null || !Player.CurrentUnit.IsAlive)
                    Player.GetNextUnit();

                Fight(Player.CurrentUnit, Oponent.CurrentUnit);
            }

            return GetResult();
        }

        private DuelResult GetResult()
        {
            if ((Player.CurrentUnit.IsAlive || Player.HasUnits) && !Oponent.CurrentUnit.IsAlive && !Oponent.HasUnits)
            {
                // In case of a victory, we need to get all of the remaining units to complete the player's final sequence
                while (Player.HasUnits)
                    Player.GetNextUnit();
                return DuelResult.Victory;
            }

            if ((Oponent.CurrentUnit.IsAlive || Oponent.HasUnits) && !Player.CurrentUnit.IsAlive && !Player.HasUnits)
                return DuelResult.Defeat;

            return DuelResult.Draw;
        }

        private static void Fight(Unit playerUnit, Unit oponentUnit)
        {
            if (playerUnit.Type == oponentUnit.Type)
            {
                playerUnit.IsAlive = false;
                oponentUnit.IsAlive = false;
                return;
            }

            if (Unit.GetCounterUnitType(oponentUnit.Type) == playerUnit.Type)
            {
                oponentUnit.IsAlive = false;
                return;
            }

            playerUnit.IsAlive = false;
        }

        private string GetNextSequence()
        {
            return Sequences.Count == 0 ? null : Sequences.Pop();
        }

        public string GetPlayerBestSequence()
        {
            string sequence = GetNextSequence();
            var drawSequence = string.Empty;
            var victorySequence = string.Empty;

            while (!string.IsNullOrEmpty(sequence))
            {
                Player.SetUnits(sequence);
                Player.FinalSequence = string.Empty;
                Oponent.SetUnits(Oponent.GenuineSequence); // Resets the stack of units

                DuelResult duelResult = Duel();

                if (duelResult == DuelResult.Victory)
                {
                    victorySequence = Player.FinalSequence;
                    break;
                }

                if (duelResult == DuelResult.Draw && string.IsNullOrEmpty(drawSequence))
                    drawSequence = Player.FinalSequence;

                sequence = GetNextSequence();
            }

            if (!string.IsNullOrEmpty(victorySequence))
                return string.Concat("+", victorySequence);

            return !string.IsNullOrEmpty(drawSequence)
                ? string.Concat("=", drawSequence)
                : string.Concat("-", Player.GenuineSequence);
        }
    }
}
