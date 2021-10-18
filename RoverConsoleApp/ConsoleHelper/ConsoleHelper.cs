using System;
using System.Drawing;
using RoverConsoleApp.RoverService;

namespace RoverConsoleApp.ConsoleHelper
{
    public class ConsoleHelper : IConsoleHelper
    {
        private readonly IRoverService _roverService;
        public ConsoleHelper(IRoverService roverService)
        {
            _roverService = roverService;
        }

        public void Execute()
        {
            int count = 1;
            Console.Clear();

            var upperRightCoordinatePoint = DoUpperRightCoordinatePrompt();

            while (true)
            {
                var startingPointAndDirection = DoStartingPointPrompt(count);
                var startingPositionPoint = startingPointAndDirection.Item1;
                var startDirection = startingPointAndDirection.Item2;

                var movementPlan = DoMovementPlanPrompt(count);

                var result = _roverService.ProcessMovementPlan(upperRightCoordinatePoint, startingPositionPoint, startDirection, movementPlan);

                Console.WriteLine(ConsoleMessage.RoverOutputMessage(count, result));

                count++;
            }
        }

        public Point DoUpperRightCoordinatePrompt()
        {
            Console.Write(ConsoleMessage.UpperRightCoordinatePrompt);
            var upperRightCoordinate = Console.ReadLine().ReplaceWhitespace();
            var isValid = upperRightCoordinate.ValidateUpperRightCoordinate();

            // Show error message until user enters the correct input ... 
            while (!isValid)
            {
                ShowValidationError(ConsoleMessage.UpperRightCoordinatePrompt, ErrorMessage.UpperRightCoordinateError);
                upperRightCoordinate = Console.ReadLine().ReplaceWhitespace();
                isValid = upperRightCoordinate.ValidateUpperRightCoordinate();
            }

            var upperRightPoint = new Point(Convert.ToInt32(upperRightCoordinate.Substring(0, 1)),
                Convert.ToInt32(upperRightCoordinate.Substring(1, 1)));

            return upperRightPoint;
        }

        public Tuple<Point, string> DoStartingPointPrompt(int count)
        {
            Console.Write(ConsoleMessage.StartingPointPrompt(count));
            var startingPosition = Console.ReadLine().ReplaceWhitespace();

            var isValid = startingPosition.ValidateStartingPosition();

            // Show error message until user enters the correct input ... 
            while (!isValid)
            {
                ShowValidationError(ConsoleMessage.StartingPointPrompt(count), ErrorMessage.StartingPointError);
                startingPosition = Console.ReadLine().ReplaceWhitespace();
                isValid = startingPosition.ValidateStartingPosition();
            }

            var startingPositionPoint = new Point(
                Convert.ToInt32(startingPosition.Substring(0, 1)),
                Convert.ToInt32(startingPosition.Substring(1, 1)));

            var startDirection = startingPosition[2];

            return new Tuple<Point, string>(startingPositionPoint, startDirection.ToString());
        }

        public string DoMovementPlanPrompt(int count)
        {
            Console.Write(ConsoleMessage.MovementPlanPrompt(count));
            var movementPlan = Console.ReadLine().ReplaceWhitespace();

            var isValid = movementPlan.ValidateMovementPlan();

            // Show error message until user enters the correct input ... 
            while (!isValid)
            {
                ShowValidationError(ConsoleMessage.MovementPlanPrompt(count), ErrorMessage.MovementPlanError);
                movementPlan = Console.ReadLine().ReplaceWhitespace();
                isValid = movementPlan.ValidateMovementPlan();
            }

            return movementPlan;
        }

        private static void ShowValidationError(string msg, string errorMsg)
        {
            Console.WriteLine(errorMsg);
            Console.Write(msg);
        }
    }
}
