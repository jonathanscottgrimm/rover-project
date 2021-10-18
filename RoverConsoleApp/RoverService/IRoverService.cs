using System.Drawing;

namespace RoverConsoleApp.RoverService
{
    public interface IRoverService
    {
        string ProcessMovementPlan(
            Point upperRightCoordinatePoint,
            Point startingPositionPoint,
            string startDirection,
            string movementPlan);
     }
}
