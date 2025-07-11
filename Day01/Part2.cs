namespace Day01
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt").Select(x => {
                var split = x.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return (int.Parse(split[0]), int.Parse(split[1]));
            }).ToList();
            var list1 = lines.ConvertAll(x => x.Item1);
            list1.Sort();
            var list2 = lines.ConvertAll(x => x.Item2);
            var numberOfTimes = new Dictionary<int, int>();
            foreach (var item in list2)
            {
                if (!numberOfTimes.ContainsKey(item))
                {
                    numberOfTimes[item] = list2.Count(x => x == item);
                }
            }

            var totalSimilarity = 0;
            foreach (var item in list1)
            {
                totalSimilarity += numberOfTimes.ContainsKey(item) ? item * numberOfTimes[item] : 0;
            }

            Console.WriteLine(totalSimilarity);
        }
    }
}
