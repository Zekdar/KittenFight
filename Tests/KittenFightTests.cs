using KittenFight;
using KittenFight.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class KittenFightTests
    {
        private static void StartFight(string playerUnits, string oponentUnits, string expectedResult)
        {
            var player = new Player(playerUnits);
            var oponent = new Oponent(oponentUnits);
            var game = new Game(player, oponent);
            string actualResult = game.GetPlayerBestSequence();
            Assert.AreEqual(expectedResult, expectedResult == "+" ? actualResult[0].ToString() : actualResult);
        }

        [TestMethod]
        public void Loss()
        {
            StartFight("FEPP", "FPEFPEF", "-FEPP");
        }

        [TestMethod]
        public void Draw()
        {
            StartFight("EFF", "PPPFPFFFFE", "=FFE");
        }

        [TestMethod]
        public void Win1()
        {
            StartFight("FEPP", "FPFFE", "+");
        }

        [TestMethod]
        public void Win2()
        {
            StartFight("FEEFF", "FFFFFF", "+");
        }

        [TestMethod]
        public void Win3()
        {
            StartFight("FEFFE", "FPFEPP", "+");
        }
    }
}
