using System;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class RulesTests
    {
        [TestMethod]
        public void ALivingCellWithZeroNeighborsDies()
        {
            var expected = string.Empty;
            var game = new Game(new[]
            {
                "X"
            });

            game.Tick();

            Assert.AreEqual(expected, game.ToString());
        }

        [TestMethod]
        public void ALivingCellWithTwoNeighborsLives()
        {
            var expected =
                "XX" + Environment.NewLine +
                "X ";

            var game = new Game(new[]
            {
                "XX",
                "X "
            });

            game.Tick();

            Assert.AreEqual(expected, game.ToString());
        }

        //[TestMethod]
        //public void ALivingCellWithMoreThanThreeNeighborsDies()
        //{
        //    var expected = string.Empty;

        //    var game = new Game(new[]
        //    {
        //        "XX",
        //        "XX"
        //    });

        //    game.Tick();

        //    Assert.AreEqual(expected, game.ToString());
        //}
    }
}