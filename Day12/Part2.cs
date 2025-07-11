namespace Day12
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var currentRegion = 0;
            var currentChar = '\0';
            var pointsAndRegions = new Dictionary<(int Row, int Col), (int Region, List<Direction> Directions)>();

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
                                ((int Row, int Col) Point, Direction Direction)[] nextPoints = [
                                    ((currentPoint.Row - 1, currentPoint.Col), Direction.Up),
                                    ((currentPoint.Row, currentPoint.Col - 1), Direction.Left),
                                    ((currentPoint.Row, currentPoint.Col + 1), Direction.Right),
                                    ((currentPoint.Row + 1, currentPoint.Col), Direction.Down)
                                ];
                                nextPoints = [.. nextPoints.Where(x => x.Point.Row >= 0 && x.Point.Row <= lines.Length - 1 && x.Point.Col >= 0 && x.Point.Col <= lines[0].Length - 1 && lines[x.Point.Row][x.Point.Col] == currentChar)];
                                var perimeter = 4 - nextPoints.Length;

                                foreach (var nextPoint in nextPoints)
                                {
                                    queue.Enqueue(nextPoint.Point);
                                }

                                pointsAndRegions.Add((currentPoint.Row, currentPoint.Col), (currentRegion, [.. Array.FindAll(Enum.GetValues<Direction>(), x => !nextPoints.Select(y => y.Direction).Contains(x))]));
                            }
                        }
                        currentRegion++;
                    }
                }
            }

            var area = 0L;
            foreach (var region in pointsAndRegions.GroupBy(x => x.Value.Region, x => (Point: x.Key, x.Value.Directions)))
            {
                var regionBorderCount = 0L;

                //var currentPoint = region.First();
                while (region.Any(x => x.Directions.Count > 0))
                {
                    var startingPoint = region.First(x => x.Directions.Count > 0);
                    var currentDirection = startingPoint.Directions.First();
                    var currentPoints = new Queue<((int Row, int Col) Point, List<Direction> Directions)>();
                    currentPoints.Enqueue(startingPoint);

                    while (currentPoints.Count > 0)
                    {
                        var currentPoint = currentPoints.Dequeue();
                        var newPotentialPoints = new List<(int Row, int Col)>
                        {
                            (currentPoint.Point.Row - 1, currentPoint.Point.Col),
                            (currentPoint.Point.Row, currentPoint.Point.Col - 1),
                            (currentPoint.Point.Row, currentPoint.Point.Col + 1),
                            (currentPoint.Point.Row + 1, currentPoint.Point.Col)
                        };

                        var newPoints = newPotentialPoints
                            .Where(x => region.Any(y => x == y.Point))
                            .Where(x => region.First(y => x == y.Point).Directions.Contains(currentDirection))
                            .Select(x => region.First(y => x == y.Point));

                        List<((int Row, int Col) Point, List<Direction> Directions)> newPoints2 =
                        [..
                            region.Where(x =>
                                newPotentialPoints.Contains(x.Point) &&
                                x.Directions.Contains(currentDirection)
                            )
                        ];

                        foreach (var newPoint in newPoints)
                        {
                            currentPoints.Enqueue(newPoint);
                        }

                        currentPoint.Directions.Remove(currentDirection);
                    }

                    regionBorderCount++;
                }

                area += regionBorderCount * region.Count();
            }

            Console.WriteLine(area);
        }
    }

    enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
