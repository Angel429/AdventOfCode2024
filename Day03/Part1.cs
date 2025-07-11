using System.Text.RegularExpressions;

namespace Day03
{
    static internal class Part1
    {
        public static void Execute()
        {
            var memory = File.ReadAllText("input.txt");

            var regex = new Regex(@"mul\((?<firstNumber>\d+),(?<secondNumber>\d+)\)");

            Console.WriteLine(regex.Matches(memory).Sum(x => int.Parse(x.Groups["firstNumber"].ValueSpan) * int.Parse(x.Groups["secondNumber"].ValueSpan)));
        }
    }
}
