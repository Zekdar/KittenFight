using KittenFight.Models;
using System;

namespace KittenFight
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Console.ReadLine(); // Not needed
            //var player = new Player(Console.ReadLine());
            //Console.ReadLine(); // Not needed
            //var oponent = new Oponent(Console.ReadLine());

            var player = new Player("FPFFP");
            var oponent = new Oponent("PFPFPF");
            var game = new Game(player, oponent);

            Console.WriteLine(game.GetPlayerBestSequence());
            Console.ReadKey();
        }
    }
}