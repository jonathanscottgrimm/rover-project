using System.Linq;
using System.Text.RegularExpressions;

namespace RoverConsoleApp
{
    public static class ExtensionMethods
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");

        public static string ReplaceWhitespace(this string input) =>
            sWhitespace.Replace(input, string.Empty);

        public static bool ValidateUpperRightCoordinate(this string input) =>
            input.Length == 2 && int.TryParse(input, out int num);

        public static bool ValidateStartingPosition(this string input)
        {
            char[] chars = { Constants.North[0], Constants.South[0], Constants.West[0], Constants.East[0] };

            return input.Length == 3 &&
                 int.TryParse(input.Substring(0, 2), out int num) &&
                 input.Substring(2).ToLowerInvariant().IndexOfAny(chars) != -1;
        }

        public static bool ValidateMovementPlan(this string input) =>
            input.ToLowerInvariant().All(x => Constants.AllowedMovements.Contains(x));
    }
}