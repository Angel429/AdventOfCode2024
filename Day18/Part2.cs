namespace Day18
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var size = 70;

            var finalNode = (Row: size, Col: size);
            var filledPlaces = new bool[size + 1][];
            for (int i = 0; i < filledPlaces.Length; i++)
            {
                filledPlaces[i] = new bool[size + 1];
            }

            var lowerBound = 0;
            var upperBound = lines.Length - 1;

            while (upperBound - lowerBound > 1)
            {
                //Console.WriteLine($"{lowerBound} {upperBound}");
                var simulationSize = (lowerBound + upperBound) / 2;
                for (var row = 0; row < size + 1; row++)
                {
                    for (var col = 0; col < size + 1; col++)
                    {
                        filledPlaces[row][col] = false;
                    }
                }

                var coords = Array.ConvertAll(lines[..Math.Min(lines.Length, simulationSize)], x => (Row: int.Parse(x[..x.IndexOf(',')]), Col: int.Parse(x[(x.IndexOf(',') + 1)..])));

                foreach (var (Col, Row) in coords)
                {
                    filledPlaces[Row][Col] = true;
                }

                var shortestDistance = new Dictionary<(int Row, int Col), int>();
                var visitedNodes = new HashSet<(int Row, int Col)>();
                shortestDistance[(0, 0)] = 0;

                var foundPath = true;
                while (!visitedNodes.Contains(finalNode))
                {
                    var availableNodes = shortestDistance
                        .Where(x => !visitedNodes.Contains(x.Key))
                        .ToArray();

                    (int Row, int Col) currentNode = (-1, -1);
                    if (availableNodes.Length > 0)
                    {
                        currentNode = availableNodes.MinBy(x => x.Value).Key;
                    }
                    else
                    {
                        foundPath = false;
                        break;
                    }

                    visitedNodes.Add(currentNode);

                    foreach (var coord in new (int Row, int Col)[] { (-1, 0), (0, -1), (0, 1), (1, 0) })
                    {
                        var newPoint = (Row: currentNode.Row + coord.Row, Col: currentNode.Col + coord.Col);
                        if (newPoint.Row >= 0 && newPoint.Row <= size && newPoint.Col >= 0 && newPoint.Col <= size && !filledPlaces[newPoint.Row][newPoint.Col] && (!shortestDistance.ContainsKey(newPoint) || shortestDistance[newPoint] > shortestDistance[currentNode] + 1))
                        {
                            shortestDistance[newPoint] = shortestDistance[currentNode] + 1;
                        }
                    }
                }

                if (foundPath)
                {
                    lowerBound = simulationSize;
                } else
                {
                    upperBound = simulationSize;
                }
            }

            //Console.WriteLine($"S: {lowerBound} {upperBound}");
            //Console.WriteLine(lines[lowerBound - 1]);
            Console.WriteLine(lines[upperBound - 1]);
        }
    }
}
