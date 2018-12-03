using System;
using System.Collections.Generic;

namespace RoverApplication
{
    public class Map
    {
        private int MaximumX;
        private int MaximumY;
        private HashSet<Position> OccupiedPositions;

        /// <summary>
        /// Initialise a map object
        /// </summary>
        /// <param name="maximumX"></param>
        /// <param name="maximumY"></param>
        public Map(string maximumX, string maximumY)
        {
            int MaxX, MaxY;

            SetMaximumX(int.Parse(maximumX));
            SetMaximumY(int.Parse(maximumY));

            if (GetMaximumX() <= 0 || GetMaximumY() <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            OccupiedPositions = new HashSet<Position>();
        }

        /// <summary>
        /// GetMaximumX
        /// </summary>
        /// <returns></returns>
        public int GetMaximumX()
        {
            return MaximumX;
        }

        /// <summary>
        /// SetMaximumX
        /// </summary>
        /// <param name="value"></param>
        public void SetMaximumX(int value)
        {
            MaximumX = value;
        }

        /// <summary>
        /// GetMaximumY
        /// </summary>
        /// <returns></returns>
        public int GetMaximumY()
        {
            return MaximumY;
        }

        /// <summary>
        /// SetMaximumY
        /// </summary>
        /// <param name="value"></param>
        public void SetMaximumY(int value)
        {
            MaximumY = value;
        }

        /// <summary>
        /// Determine if a move in the specified direction from the specified position can be made
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="directionToMove"></param>
        /// <returns></returns>
        public bool CanDoMove(Position startPosition, Direction directionToMove)
        {
            startPosition = Utilities.MovePosition(startPosition, directionToMove);

            return startPosition.Y <= GetMaximumY() && startPosition.Y >= 0 &&
                   startPosition.X <= GetMaximumX() && startPosition.X >= 0 &&
                   !GetOccupiedPositions().Contains(startPosition);
        }

        /// <summary>
        /// GetOccupiedPositions
        /// </summary>
        /// <returns></returns>
        public HashSet<Position> GetOccupiedPositions()
        {
            return OccupiedPositions;
        }

        /// <summary>
        /// Set a position on the map as occupied
        /// </summary>
        /// <param name="occupiedPosition"></param>
        public void AddOccupiedPosition(Position occupiedPosition)
        {
            if (occupiedPosition.Y > GetMaximumY() || occupiedPosition.Y < 0 ||
                   occupiedPosition.X > GetMaximumX() || occupiedPosition.X < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if(!OccupiedPositions.Add(occupiedPosition))
            {
                throw new ArgumentException();
            }
        }
    }
}
