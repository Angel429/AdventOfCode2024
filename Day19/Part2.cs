namespace Day19
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var patterns = lines[0].Split(", ");
            var cache = new Dictionary<string, long>();

            var possibleCombinations = 0L;
            foreach (var desiredPattern in lines[2..])
            {
                possibleCombinations += DesignTowel(cache, patterns, desiredPattern, "");
            }

            Console.WriteLine(possibleCombinations);
        }

        private static long DesignTowel(Dictionary<string, long> cache, string[] patterns, string desiredPattern, string prefix)
        {
            if (cache.TryGetValue(desiredPattern[prefix.Length..], out var desiredPatternWithoutPrefix))
            {
                return desiredPatternWithoutPrefix;
            }

            if (desiredPattern == prefix)
            {
                return 1;
            }

            if (prefix.Length > desiredPattern.Length)
            {
                return 0;
            }

            var possiblePatterns = 0L;
            foreach (var pattern in patterns)
            {
                if (desiredPattern.StartsWith(prefix + pattern))
                {
                    if (cache.TryGetValue(desiredPattern[(prefix + pattern).Length..], out var count))
                    {
                        possiblePatterns += count;
                    }
                    else
                    {
                        possiblePatterns += DesignTowel(cache, patterns, desiredPattern, prefix + pattern);
                    }
                }
            }

            if (cache.ContainsKey(desiredPattern[prefix.Length..]) && cache[desiredPattern[prefix.Length..]] != possiblePatterns)
            {
                Console.WriteLine("A");
            }
            cache[desiredPattern[prefix.Length..]] = possiblePatterns;

            return possiblePatterns;
        }
    }
}
