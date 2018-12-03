using System;

namespace RoverApplication
{
    public class Rover
    {
        private Position currentPosition;
        private Direction CurrentDirection;

        /// <summary>
        /// Initialise Rover object
        /// </summary>
        /// <param name="startX">Starting X position</param>
        /// <param name="startY">Starting Y position</param>
        /// <param name="startDirection">Start Direction</param>
        public Rover(string startX, string startY, string startDirection)
        {
            int StartX, StartY;

            StartX = int.Parse(startX);
            StartY = int.Parse(startY);

            if(StartX < 0 || StartY < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            SetCurrentPosition(new Position(StartX, StartY));

            if (Enum.TryParse(startDirection, out Direction proposedDirection))
            {
                if(Enum.IsDefined(typeof(Direction), proposedDirection))
                {
                    SetCurrentDirection(proposedDirection);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

            }
            else
            {
                throw new ArgumentException();
            }
            
        }

        /// <summary>
        /// GetCurrentPosition
        /// </summary>
        /// <returns></returns>
        public Position GetCurrentPosition()
        {
            return currentPosition;
        }

        /// <summary>
        /// SetCurrentPosition
        /// </summary>
        /// <param name="value"></param>
        public void SetCurrentPosition(Position value)
        {
            currentPosition = value;
        }

        /// <summary>
        /// SetCurrentDirection
        /// </summary>
        /// <returns></returns>
        public Direction GetCurrentDirection()
        {
            return CurrentDirection;
        }

        /// <summary>
        /// SetCurrentDirection
        /// </summary>
        /// <param name="value"></param>
        public void SetCurrentDirection(Direction value)
        {
            CurrentDirection = value;
        }
        
        /// <summary>
        /// Turn Rover in direction specified
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool DoTurn(Command command)
        {
            switch (command)
            {
                case Command.L:
                    ChangeDirectionLeft();
                    break;
                case Command.R:
                    ChangeDirectionRight();
                    break;
                default:
                    throw new ArgumentException();
            }

            return true;
        }

        /// <summary>
        /// Move Rover one space in the direction its currently facing
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool DoMove(Command command)
        {
            if (command != Command.M)
            {
                throw new ArgumentException("Unrecognised Command");
            }

            SetCurrentPosition(Utilities.MovePosition(GetCurrentPosition(), GetCurrentDirection()));

            return true;
        }

        /// <summary>
        /// Change rover direction for a R command
        /// </summary>
        private void ChangeDirectionRight()
        {
            switch (GetCurrentDirection())
            {
                case Direction.N:
                    SetCurrentDirection(Direction.E);
                    break;
                case Direction.E:
                    SetCurrentDirection(Direction.S);
                    break;
                case Direction.S:
                    SetCurrentDirection(Direction.W);
                    break;
                case Direction.W:
                    SetCurrentDirection(Direction.N);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Change Rover direction for a L command
        /// </summary>
        private void ChangeDirectionLeft()
        {
            switch (GetCurrentDirection())
            {
                case Direction.N:
                    SetCurrentDirection(Direction.W);
                    break;
                case Direction.E:
                    SetCurrentDirection(Direction.N);
                    break;
                case Direction.S:
                    SetCurrentDirection(Direction.E);
                    break;
                case Direction.W:
                    SetCurrentDirection(Direction.S);
                    break;
                default:
                    break;
            }
        }
    }

    public enum Direction
    {
        N,
        S,
        E,
        W
    }

    public enum Command
    {
        L,
        R,
        M
    }
}
