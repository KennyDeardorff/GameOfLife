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
                "XX";

            var game = new Game(new[]
            {
                "XX",
                "X "
            });

            game.Tick();

            Assert.AreEqual(expected, game.ToString());
        }

        [TestMethod]
        public void ALivingCellWithMoreThanThreeNeighborsDies()
        {
            var expected =
                "X X" + Environment.NewLine +
                "X X" + Environment.NewLine +
                " X ";

            var game = new Game(new[]
            {
                "XX ",
                "XXX"
            });

            game.Tick();

            Assert.AreEqual(expected, game.ToString());
        }

        [TestMethod]
        public void Bug1()
        {
            var expected =
                "XXX" + Environment.NewLine +
                "XXX" + Environment.NewLine +
                " X ";

            var game = new Game(new[]
            {
                " X ",
                "XXX"
            });

            game.Tick();

            Assert.AreEqual(expected, game.ToString());
        }

        [TestMethod]
        public void Bug2()
        {
            var expected =
                "XXX";

            var game = new Game(new[]
            {
                "X",
                "X",
                "X",
            });

            game.Tick();

            Assert.AreEqual(expected, game.ToString());
        }

        [TestMethod]
        public void ADeadCellWithExactlyThreeNeighborsComesAlive()
        {
            var expected =
                "X" + Environment.NewLine +
                "X";

            var game = new Game(new[]
            {
                " X ",
                "X X"
            });

            game.Tick();

            Assert.AreEqual(expected, game.ToString());
        }
    }
}