namespace RoverConsoleApp
{
    public static class ConsoleMessage
    {
        public static string StartingPointPrompt(int count) => $"Rover {count} Starting Position:";
        public static string UpperRightCoordinatePrompt => "Enter Graph Upper Right Coordinate:";
        public static string MovementPlanPrompt(int count) => $"Rover {count} Movement Plan:";
        public static string RoverOutputMessage(int count, string result) => $"Rover {count} Output: {result}";  
    }
}
