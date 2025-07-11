namespace Day01
{
    static internal class Part1
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
            list2.Sort();

            var totalDistance = 0;
            foreach (var zip in list1.Zip(list2))
            {
                totalDistance += Math.Abs(zip.First - zip.Second);
            }

            Console.WriteLine(totalDistance);
        }
    }
}
