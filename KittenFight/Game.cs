using KittenFight.Enumerations;
using KittenFight.Models;
using KittenFight.StaticFunctions;
using System.Collections.Generic;

namespace KittenFight
{
    public class Game
    {
        private readonly Player _player;
        private readonly Oponent _oponent;
        private readonly Stack<string> _sequences;

        public Game(Player player, Oponent oponent)
        {
            _player = player;
            _oponent = oponent;
            _sequences = new Stack<string>(Swapper.GetAllPermutations(_player.OriginalSequence));
        }

        private DuelResult Duel(Player player, Oponent oponent)
        {
            while (player.HasUnits && oponent.HasUnits
                || player.CurrentUnit.IsAlive && oponent.HasUnits
                || oponent.CurrentUnit.IsAlive && player.HasUnits)
            {
                if (oponent.CurrentUnit == null || !oponent.CurrentUnit.IsAlive)
                    oponent.GetNextUnit();
                if (player.CurrentUnit == null || !player.CurrentUnit.IsAlive)
                    player.GetNextUnit();

                Fight(player.CurrentUnit, oponent.CurrentUnit);
            }

            return GetResult();
        }

        private DuelResult GetResult()
        {
            if ((_player.CurrentUnit.IsAlive || _player.HasUnits) && !_oponent.CurrentUnit.IsAlive && !_oponent.HasUnits)
            {
                // In case of a victory, we need to get all of the remaining units to complete the player's final sequence
                while (_player.HasUnits)
                    _player.GetNextUnit();
                return DuelResult.Victory;
            }

            if ((_oponent.CurrentUnit.IsAlive || _oponent.HasUnits) && !_player.CurrentUnit.IsAlive && !_player.HasUnits)
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
            return _sequences.Count == 0 ? null : _sequences.Pop();
        }

        public string GetPlayerBestSequence()
        {
            string sequence = GetNextSequence();
            var drawSequence = string.Empty;
            var victorySequence = string.Empty;

            while (!string.IsNullOrEmpty(sequence))
            {
                _player.SetUnits(sequence);
                _player.FinalSequence = string.Empty;
                _oponent.SetUnits(_oponent.OriginalSequence); // Resets the stack of units

                DuelResult duelResult = Duel(_player, _oponent);

                if (duelResult == DuelResult.Victory)
                {
                    victorySequence = _player.FinalSequence;
                    break;
                }

                if (duelResult == DuelResult.Draw && string.IsNullOrEmpty(drawSequence))
                    drawSequence = _player.FinalSequence;

                sequence = GetNextSequence();
            }

            if (!string.IsNullOrEmpty(victorySequence))
                return string.Concat("+", victorySequence);

            return !string.IsNullOrEmpty(drawSequence)
                ? string.Concat("=", drawSequence)
                : string.Concat("-", _player.OriginalSequence);
        }
    }
}
