using System;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class PrintToStringTests
    {
        [TestMethod]
        public void PrintsTheMinimumSquareToDisplayBoardInMonospacedFont()
        {
            var expected = string.Join(
                Environment.NewLine,
                "X X",
                " X ");

            var actual = new Game(new []
            {
                "    ",
                " X X",
                "  X "
            }).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
