using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RoverApplication.RoverApplicationTests
{
    [TestClass()]
    public class RoverTests
    {
        [TestMethod()]
        public void RoverIntialisesCorrectly()
        {
            Rover TestRover = new Rover("0", "0", "N");

            Assert.AreEqual(new Position(0, 0), TestRover.GetCurrentPosition());
            Assert.AreEqual(Direction.N, TestRover.GetCurrentDirection());

            TestRover = new Rover("5000000", "5000000", "S");

            Assert.AreEqual(new Position(5000000, 5000000), TestRover.GetCurrentPosition());
            Assert.AreEqual(Direction.S, TestRover.GetCurrentDirection());
        }

        [TestMethod()]
        public void InvalidInitialPositionsThrowExceptions()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover("-1", "0", "N"));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover("0", "-1", "N"));

            Assert.ThrowsException<FormatException>(() => new Rover(" ", null, "N"));
        }

        [TestMethod()]
        public void InvalidInitialDirectionsThrowExceptions()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Rover("0", "0", "-1"));

            Assert.ThrowsException<ArgumentException>(() => new Rover("0", "0", "}"));

            Assert.ThrowsException<ArgumentException>(() => new Rover("0", "0", null));
        }

        [TestMethod()]
        public void RoverTurnsWhenCorrectCommandsAreSpecified()
        {
            Rover TestRover = new Rover("0", "0", "N");

            TestRover.DoTurn(Command.L);
            Assert.AreEqual(Direction.W, TestRover.GetCurrentDirection());

            TestRover.DoTurn(Command.R);
            Assert.AreEqual(Direction.N, TestRover.GetCurrentDirection());
        }

        [TestMethod()]
        public void RoverThrowsExceptionsWhenInvalidTurnCommandsAreSpecified()
        {
            Rover TestRover = new Rover("0", "0", "N");

            Assert.ThrowsException<ArgumentException>(() => TestRover.DoTurn(Command.M));
        }

        [TestMethod()]
        public void RoverPerformsMoveCommandsCorrectly()
        {
            Rover TestRover = new Rover("0", "0", "N");

            TestRover.DoMove(Command.M);
            Assert.AreEqual(new Position(0,1), TestRover.GetCurrentPosition());
            Assert.AreEqual(Direction.N, TestRover.GetCurrentDirection());

            TestRover.DoMove(Command.M);
            Assert.AreEqual(new Position(0, 2), TestRover.GetCurrentPosition());
            Assert.AreEqual(Direction.N, TestRover.GetCurrentDirection());
        }

        [TestMethod()]
        public void RoverThrowsExceptionsWhenInvalidMoveCommandsAreSpecified()
        {
            Rover TestRover = new Rover("0", "0", "N");

            Assert.ThrowsException<ArgumentException>(() => TestRover.DoMove(Command.L));
            Assert.ThrowsException<ArgumentException>(() => TestRover.DoMove(Command.R));
        }
    }
}