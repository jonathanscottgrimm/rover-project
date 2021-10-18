using Xunit;
using RoverConsoleApp;
using System.Drawing;
using FluentAssertions;
using System.Collections.Generic;
using RoverConsoleApp.RoverService;

namespace RoverConsoleAppTest
{
    public class RoverServiceTest
    {  
        [Theory]
        [MemberData(nameof(GetValidDataForProcessMovementPlan))]
        public void ProcessMovementPlan_WithValidData_ReturnsString(Point upperRightCoordinatePoint, Point startingPositionPoint, string startDirection, string movementPlan, string expectedResult)
        {
            var result = new RoverService()
                            .ProcessMovementPlan(upperRightCoordinatePoint, startingPositionPoint, startDirection, movementPlan);

            result.Should().Be(expectedResult);
        }

        [Theory]
        [MemberData(nameof(GetInvalidStartingPositionDataForProcessMovementPlan))]
        public void ProcessMovementPlan_InvalidStartingPositionData_ReturnsGreaterThanUpperRightCoordinateErrorMessage(Point upperRightCoordinatePoint, Point startingPositionPoint, string startDirection, string movementPlan, string expectedResult)
        {
            var result = new RoverService()
                            .ProcessMovementPlan(upperRightCoordinatePoint, startingPositionPoint, startDirection, movementPlan);

            result.Should().Be(expectedResult); 
        }

        [Theory]
        [MemberData(nameof(GetInvalidStartingPositionDataForProcessMovementPlan))]
        public void ProcessMovementPlan_LessThanZeroStartingPositionData_ReturnsLessThanErrorMessage(Point upperRightCoordinatePoint, Point startingPositionPoint, string startDirection, string movementPlan, string expectedResult)
        {
            var result = new RoverService()
                            .ProcessMovementPlan(upperRightCoordinatePoint, startingPositionPoint, startDirection, movementPlan);

            result.Should().Be(expectedResult);
        }

        public static IEnumerable<object[]> GetValidDataForProcessMovementPlan() => new List<object[]>
            {
            new object[] { new Point(5,5), new Point(3,3), "E", "MMRMMRMRRM", "5 1 E" },
            new object[] { new Point(5,5), new Point(1,2), "N", "LMLMLMLMM", "1 3 N" }
            };

        public static IEnumerable<object[]> GetInvalidStartingPositionDataForProcessMovementPlan() => new List<object[]>
            {
            new object[] { new Point(5,5), new Point(5,300), "E", "MMRMMRMRRM", ErrorMessage.GreaterThanUpperRightCoordinateErrorMessage("X", 6, 5)},
            new object[] { new Point(5,5), new Point(200,5), "N", "LMLMLMLMM", ErrorMessage.GreaterThanUpperRightCoordinateErrorMessage("X", 199, 5)}
            };


        public static IEnumerable<object[]> GetLessThanZeroStartingPositionDataForProcessMovementPlan() => new List<object[]>
            {
            new object[] { new Point(5,5), new Point(-2,300), "E", "MMRMMRMRRM", ErrorMessage.LessThanZeroErrorMessage("X", -2)},
            new object[] { new Point(5,5), new Point(200,-4), "N", "LMLMLMLMM", ErrorMessage.LessThanZeroErrorMessage("Y", -4)}
            };
    }
}