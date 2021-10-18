namespace RoverConsoleApp
{
    public static class ErrorMessage
    {
        public static string UpperRightCoordinateError =>
            "Invalid Upper Right Coordinates: They must be two digits";

        public static string StartingPointError =>
            "Starting position must be 3 characters, the first two numeric, the last either 'N', 'S', 'E', 'W'";

        public static string MovementPlanError =>
            "Movement Plan can only consist of the following characters: 'M', 'L', 'R'";

        public static string LessThanZeroErrorMessage(string coordinateName, int coordinateValue) =>
            $"Illegal move: {coordinateName.ToUpperInvariant()} must be greater than 0. " +
            $"{coordinateName.ToUpperInvariant()} is {coordinateValue}.";

        public static string GreaterThanUpperRightCoordinateErrorMessage(string coordinateName,
                                                                         int coordinateValue,
                                                                         int upperRightCoordinateValue) =>
            $"Illegal move: {coordinateName.ToUpperInvariant()}({coordinateValue}) is outside of the "
            + $"Upper Right Coordinate({upperRightCoordinateValue}).";
    }
}
