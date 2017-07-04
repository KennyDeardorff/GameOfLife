using System.Collections.Generic;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void Equality()
        {
            var cell1 = new Cell(3, 1);
            var cell2 = new Cell(3, 1);

            Assert.AreEqual(cell1, cell2);
        }

        [TestMethod]
        public void ListContains()
        {
            var cell1 = new Cell(3, 1);
            var cell2 = new Cell(3, 1);

            var list = new List<Cell> {cell1};

            Assert.IsTrue(list.Contains(cell2));
        }

        [TestMethod]
        public void Neighbors()
        {
            Cell
                tl = new Cell(3, 3), tc = new Cell(4, 3), tr = new Cell(5, 3),
                cl = new Cell(3, 2), cc = new Cell(4, 2), cr = new Cell(5, 2),
                bl = new Cell(3, 1), bc = new Cell(4, 1), br = new Cell(5, 1);

            var expected = new List<Cell>
            {
                tl, tc, tr,
                cl,     cr,
                bl, bc, br
            };

            Assert.AreEqual(expected.Count, cc.Neighbors.Count);

            foreach (var cell in expected)
                Assert.IsTrue(cc.Neighbors.Contains(cell));
        }
    }
}