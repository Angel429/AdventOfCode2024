namespace Day12
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var currentRegion = 0;
            var currentChar = '\0';
            var pointsAndRegions = new Dictionary<(int Row, int Col), (int Region, int Perimeter)>();

            var queue = new Queue<(int Row, int Col)>();
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    if (!pointsAndRegions.ContainsKey((row, col)))
                    {
                        queue.Enqueue((row, col));
                        currentChar = lines[row][col];
                        while (queue.Count > 0)
                        {
                            var currentPoint = queue.Dequeue();
                            if (!pointsAndRegions.ContainsKey(currentPoint))
                            {
                                (int Row, int Col)[] nextPoints = [
                                    (currentPoint.Row - 1, currentPoint.Col),
                                    (currentPoint.Row, currentPoint.Col - 1),
                                    (currentPoint.Row, currentPoint.Col + 1),
                                    (currentPoint.Row + 1, currentPoint.Col)
                                ];
                                nextPoints = [.. nextPoints.Where(x => x.Row >= 0 && x.Row <= lines.Length - 1 && x.Col >= 0 && x.Col <= lines[0].Length - 1 && lines[x.Row][x.Col] == currentChar)];
                                var perimeter = 4 - nextPoints.Length;

                                foreach (var nextPoint in nextPoints)
                                {
                                    queue.Enqueue(nextPoint);
                                }

                                pointsAndRegions.Add((currentPoint.Row, currentPoint.Col), (currentRegion, perimeter));
                            }
                        }
                        currentRegion++;
                    }
                }
            }

            var area = 0L;
            foreach (var region in pointsAndRegions.Values.GroupBy(x => x.Region))
            {
                area += region.Count() * region.Sum(x => x.Perimeter);
            }

            Console.WriteLine(area);
        }
    }
}
