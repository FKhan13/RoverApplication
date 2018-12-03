namespace RoverApplication
{
    public static class Utilities
    {
        /// <summary>
        /// Move specified position one point in the specified direction
        /// </summary>
        /// <param name="positionToMove"></param>
        /// <param name="directionToMove"></param>
        /// <returns></returns>
        public static Position MovePosition(Position positionToMove, Direction directionToMove)
        {
            switch (directionToMove)
            {
                case Direction.N:
                    positionToMove.Y += 1;
                    break;
                case Direction.E:
                    positionToMove.X += 1;
                    break;
                case Direction.S:
                    positionToMove.Y -= 1;
                    break;
                case Direction.W:
                    positionToMove.X -= 1;
                    break;
            }

            return positionToMove;
        }
    }
}
