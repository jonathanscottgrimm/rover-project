using System;
using System.Drawing;

namespace RoverConsoleApp.ConsoleHelper
{
    public interface IConsoleHelper
    {
        void Execute();
        Point DoUpperRightCoordinatePrompt();
        Tuple<Point, string> DoStartingPointPrompt(int count);
        string DoMovementPlanPrompt(int count);
    }
}
