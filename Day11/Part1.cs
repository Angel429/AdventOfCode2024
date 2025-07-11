namespace Day11
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");
            var stones = Array.ConvertAll(lines[0].Split(' '), long.Parse).ToList();

            var blinks = 25;

            for (var blink = 0; blink < blinks; blink++)
            {
                var newStones = new List<long>(stones.Count * 2);

                foreach (var stone in stones)
                {
                    if (stone == 0)
                    {
                        newStones.Add(1);
                    }
                    else
                    {
                        var digitCountMinus1 = (int)Math.Log10(stone);
                        if (digitCountMinus1 % 2 == 1)
                        {
                            var divisor = (long)Math.Pow(10, (digitCountMinus1 + 1) / 2);
                            var leftValue = stone / divisor;
                            var rightValue = stone - leftValue * divisor;

                            newStones.Add(leftValue);
                            newStones.Add(rightValue);
                        } else
                        {
                            newStones.Add(stone * 2024);
                        }
                    }
                }

                stones = newStones;
            }

            Console.WriteLine(stones.Count);
        }
    }
}
