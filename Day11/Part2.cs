namespace Day11
{
    // From: https://javorszky.co.uk/2024/12/12/advent-of-code-2024-day-11/
    // Mi fallo fue intentar llevar la cuenta de steps de forma ascendente
    // en lugar de descendente, ups
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");
            var stones = Array.ConvertAll(lines[0].Split(' '), long.Parse).ToList();

            var blinks = 75;

            var cache = new Dictionary<(long Value, int steps), long>();
            var total = 0L;

            foreach (var stone in stones)
            {
                total += Evaluate(stone, blinks, cache);
            }

            var a = cache.Where(x => x.Key.Value == 0).ToList();

            Console.WriteLine(total);
        }

        private static long Evaluate(long stone, int steps, Dictionary<(long Value, int steps), long> cache)
        {
            if (cache.TryGetValue((stone, steps), out var value))
            {
                return value;
            }

            if (steps == 0)
            {
                return 1;
            }

            long result;
            if (stone == 0)
            {
                result = Evaluate(1, steps - 1, cache);
            }
            else
            {
                var digitCountMinus1 = (int)Math.Log10(stone);
                if (digitCountMinus1 % 2 == 1)
                {
                    var divisor = (long)Math.Pow(10, (digitCountMinus1 + 1) / 2);
                    var leftValue = stone / divisor;
                    var rightValue = stone - leftValue * divisor;

                    result = Evaluate(leftValue, steps - 1, cache) + Evaluate(rightValue, steps - 1, cache);
                }
                else
                {
                    result = Evaluate(stone * 2024, steps - 1, cache);
                }
            }

            cache[(stone, steps)] = result;

            return result;
        }
    }
}
