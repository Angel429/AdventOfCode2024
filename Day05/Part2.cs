namespace Day05
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var rules = lines.Where(x => x.Contains('|'))
                .Select(x => x.Split('|'))
                .Select(x => (x[0], x[1]))
                .ToArray();
            var updates = lines.Where(x => x.Contains(','))
                .Select(x => x.Split(','))
                .ToArray();

            var result = 0;
            foreach (var update in updates)
            {
                var mustBeCounted = false;
                for (var updateIndex = 0; updateIndex < update.Length; updateIndex++)
                {
                    for (var newUpdateIndex = updateIndex + 1; newUpdateIndex < update.Length; newUpdateIndex++)
                    {
                        if (rules.Contains((update[newUpdateIndex], update[updateIndex])))
                        {
                            (update[newUpdateIndex], update[updateIndex]) = (update[updateIndex], update[newUpdateIndex]);
                            updateIndex = 0;
                            newUpdateIndex = 0;
                            mustBeCounted = true;
                        }
                    }
                }

                if (mustBeCounted)
                {
                    result += int.Parse(update[update.Length / 2]);
                }
            }

            Console.WriteLine(result);
        }
    }
}
