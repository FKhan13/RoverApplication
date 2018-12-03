using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace RoverApplication.RoverApplicationTests
{
    [TestClass()]
    public class MapTests
    {
        [TestMethod()]
        public void MapInitialisesCorrectly()
        {
            Map TestMap = new Map("2", "2");

            Assert.AreEqual(2, TestMap.GetMaximumX());
            Assert.AreEqual(2, TestMap.GetMaximumY());
        }

        [TestMethod()]
        public void InvalidInitialSizesThrowExceptions()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Map("0", "1"));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Map("1", "0"));

            Assert.ThrowsException<FormatException>(() => new Map(" ", null));
        }

        [TestMethod()]
        public void BasicOccupiedPositionsUpdateCorrectly()
        {
            Map TestMap = new Map("2", "2");

            Position TestPosition = new Position(0, 0);
            TestMap.AddOccupiedPosition(TestPosition);

            HashSet<Position> OccupiedPostions = TestMap.GetOccupiedPositions();

            Assert.AreEqual(1, OccupiedPostions.Count);
            Assert.IsTrue(OccupiedPostions.Contains(TestPosition));

            TestPosition.Y = TestPosition.X = 1;

            TestMap.AddOccupiedPosition(TestPosition);

            Assert.AreEqual(2, OccupiedPostions.Count);
            Assert.IsTrue(OccupiedPostions.Contains(TestPosition));
        }

        [TestMethod()]
        public void InvalidOccupiedPostionsThrowExceptions()
        {
            Map TestMap = new Map("2", "2");

            // -Negative position
            Position TestPosition = new Position(-1, 0);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestMap.AddOccupiedPosition(TestPosition));

            HashSet<Position> OccupiedPostions = TestMap.GetOccupiedPositions();
            Assert.IsFalse(OccupiedPostions.Contains(TestPosition));

            // Position that is not on the map
            TestPosition.Y = TestPosition.X = 5;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestMap.AddOccupiedPosition(TestPosition));

            OccupiedPostions = TestMap.GetOccupiedPositions();
            Assert.AreEqual(0, OccupiedPostions.Count);
            Assert.IsFalse(OccupiedPostions.Contains(TestPosition));

            // Duplicate occupied position
            TestPosition.Y = TestPosition.X = 1;

            TestMap.AddOccupiedPosition(TestPosition);

            Assert.ThrowsException<ArgumentException>(() => TestMap.AddOccupiedPosition(TestPosition));
            Assert.AreEqual(1, OccupiedPostions.Count);
            Assert.IsTrue(OccupiedPostions.Contains(TestPosition));
        }

        [TestMethod()]
        public void BasicCanDoMoveOperations()
        {
            Map TestMap = new Map("3", "3");

            Position TestPosition = new Position(2, 2);

            Assert.IsTrue(TestMap.CanDoMove(TestPosition, Direction.N));
            Assert.IsTrue(TestMap.CanDoMove(TestPosition, Direction.E));
            Assert.IsTrue(TestMap.CanDoMove(TestPosition, Direction.S));
            Assert.IsTrue(TestMap.CanDoMove(TestPosition, Direction.W));
        }

        [TestMethod()]
        public void InvalidCanDoMoveOperations()
        {
            // Negative Positions
            Map TestMap = new Map("3", "3");
            Position TestPosition = new Position(0, 0);

            Assert.IsFalse(TestMap.CanDoMove(TestPosition, Direction.S));
            Assert.IsFalse(TestMap.CanDoMove(TestPosition, Direction.W));

            // Positions greater than map size
            TestPosition = new Position(3, 3);
            Assert.IsFalse(TestMap.CanDoMove(TestPosition, Direction.N));
            Assert.IsFalse(TestMap.CanDoMove(TestPosition, Direction.E));

            // Occupied postions
            TestMap.AddOccupiedPosition(new Position(3,2));
            Assert.IsFalse(TestMap.CanDoMove(TestPosition, Direction.S));
        }
    }
}