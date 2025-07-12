namespace Day19
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var patterns = lines[0].Split(", ");

            var possiblePatterns = 0;
            foreach (var desiredPattern in lines[2..])
            {
                if (DesignTowel(patterns, desiredPattern, ""))
                {
                    possiblePatterns++;
                }
            }

            Console.WriteLine(possiblePatterns);
        }

        private static bool DesignTowel(string[] patterns, string desiredPattern, string prefix)
        {
            if (desiredPattern == prefix)
            {
                return true;
            }

            if (prefix.Length > desiredPattern.Length)
            {
                return false;
            }

            foreach (var pattern in patterns)
            {
                if (desiredPattern.StartsWith(prefix + pattern) && DesignTowel(patterns, desiredPattern, prefix + pattern))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
