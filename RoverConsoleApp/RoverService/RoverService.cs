using System;
using System.Drawing;
using System.Threading;

namespace RoverConsoleApp.RoverService
{
    public class RoverService : IRoverService
    {
        private Point UpperRightCoordinatePoint;
        private Point EndPositionPoint = new Point();
        private string CurrentDirection;

        public RoverService()
        { }

        public string ProcessMovementPlan(Point upperRightCoordinatePoint, Point startingPositionPoint,
            string startDirection, string movementPlan)
        {
            SetProperties(startDirection, startingPositionPoint, upperRightCoordinatePoint);

            string errorMessage = string.Empty;

            foreach (var navigationItem in movementPlan.ToLowerInvariant())
            {
                if (navigationItem == Constants.MoveLeft || navigationItem == Constants.MoveRight)
                {
                    GetMovementDirection(navigationItem);
                    continue;
                }

                UpdateCoordinates();

                errorMessage = CheckForInvalidMovements();
                if (!string.IsNullOrEmpty(errorMessage))
                    break;
            }

            return !string.IsNullOrEmpty(errorMessage) ? errorMessage :
                $"{EndPositionPoint.X} {EndPositionPoint.Y} {CurrentDirection.ToUpperInvariant()}";
        }

        private string CheckForInvalidMovements()
        {
            if (EndPositionPoint.X < 0)
                return ErrorMessage.LessThanZeroErrorMessage(nameof(EndPositionPoint.X), EndPositionPoint.X);
            
            if (EndPositionPoint.Y < 0)
                return ErrorMessage.LessThanZeroErrorMessage(nameof(EndPositionPoint.Y), EndPositionPoint.Y);
             
            if (EndPositionPoint.X > UpperRightCoordinatePoint.X)
                return ErrorMessage.GreaterThanUpperRightCoordinateErrorMessage(coordinateName: nameof(EndPositionPoint.X),
                    coordinateValue: EndPositionPoint.X,
                    upperRightCoordinateValue: UpperRightCoordinatePoint.X);
            
            if (EndPositionPoint.Y > UpperRightCoordinatePoint.Y)
                return ErrorMessage.GreaterThanUpperRightCoordinateErrorMessage(coordinateName: nameof(EndPositionPoint.Y),
                    coordinateValue: EndPositionPoint.Y, upperRightCoordinateValue: UpperRightCoordinatePoint.Y);

            return string.Empty;             
        }

        private void GetMovementDirection(char turnDirection)
        {
            switch (CurrentDirection.ToLowerInvariant())
            {
                case Constants.North:
                    CurrentDirection = turnDirection == Constants.MoveLeft ? Constants.West : Constants.East;
                    break;
                case Constants.South:
                    CurrentDirection = turnDirection == Constants.MoveLeft ? Constants.East : Constants.West;
                    break;
                case Constants.West:
                    CurrentDirection = turnDirection == Constants.MoveLeft ? Constants.South : Constants.North;
                    break;
                case Constants.East:
                    CurrentDirection = turnDirection == Constants.MoveLeft ? Constants.North : Constants.South;
                    break;
                default:
                    break;
            }
        }

        private void UpdateCoordinates()
        {
            if (CurrentDirection == Constants.North)
                EndPositionPoint.Y++;

            if (CurrentDirection == Constants.South)
                EndPositionPoint.Y--;

            if (CurrentDirection == Constants.East)
                EndPositionPoint.X++;

            if (CurrentDirection == Constants.West)
                EndPositionPoint.X--;
        }

        private void SetProperties(string startDirection, Point startingPositionPoint, Point upperRightCoordinatePoint)
        {
            CurrentDirection = startDirection.ToLowerInvariant();
            EndPositionPoint = startingPositionPoint;
            UpperRightCoordinatePoint = upperRightCoordinatePoint;
        }    
    }
}