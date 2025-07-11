using System.Text.RegularExpressions;

namespace Day03
{
    static internal class Part2
    {
        public static void Execute()
        {
            var memory = File.ReadAllText("input.txt");

            var regex = new Regex(@"(mul\((?<firstNumber>\d+),(?<secondNumber>\d+)\))|do\(\)|don't\(\)");

            var matches = regex.Matches(memory);

            var result = 0;
            var enabled = true;
            foreach (var match in matches.OrderBy(x => x.Index))
            {
                var value = match.Value;
                if (value.StartsWith("mul"))
                {
                    if (enabled)
                    {
                        result += int.Parse(match.Groups["firstNumber"].ValueSpan) * int.Parse(match.Groups["secondNumber"].ValueSpan);
                    }
                }
                else if (value == "do()")
                {
                    enabled = true;
                }
                else if (value == "don't()")
                {
                    enabled = false;
                } else
                {
                    throw new Exception(match.Value);
                }
            }

            Console.WriteLine(result);
        }
    }
}
