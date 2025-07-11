using System.Text.RegularExpressions;

namespace Day13
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var buttonRegex = new Regex(@"Button \w: X\+(?<px>\d+), Y\+(?<py>\d+)");
            var prizeRegex = new Regex(@"Prize: X=(?<x>\d+), Y=(?<y>\d+)");

            var totalTokens = 0L;

            for (int i = 0; i < lines.Length; i += 4)
            {
                var buttonAMatch = buttonRegex.Match(lines[i]);
                var pax = int.Parse(buttonAMatch.Groups["px"].ValueSpan);
                var pay = int.Parse(buttonAMatch.Groups["py"].ValueSpan);

                var buttonBMatch = buttonRegex.Match(lines[i + 1]);
                var pbx = int.Parse(buttonBMatch.Groups["px"].ValueSpan);
                var pby = int.Parse(buttonBMatch.Groups["py"].ValueSpan);

                var prizeMatch = prizeRegex.Match(lines[i + 2]);
                var px = int.Parse(prizeMatch.Groups["x"].ValueSpan);
                var py = int.Parse(prizeMatch.Groups["y"].ValueSpan);

                var bNumerator = pay * px - pax * py;
                var bDemoninator = pay * pbx - pax * pby;

                if (bNumerator % bDemoninator != 0)
                {
                    continue;
                }

                var b = bNumerator / bDemoninator;

                var aNumerator = px - pbx*b;

                if (aNumerator % pax != 0)
                {
                    continue;
                }

                var a = aNumerator / pax;

                if (a >= 100 || b >= 100)
                {
                    continue;
                }

                totalTokens += a * 3 + b;
            }

            Console.WriteLine(totalTokens);
        }
    }
}
