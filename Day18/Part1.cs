namespace Day18
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var size = 70;
            var simulationSize = 1024;

            var filledPlaces = new bool[size + 1][];
            for (int i = 0; i < filledPlaces.Length; i++)
            {
                filledPlaces[i] = new bool[size + 1];
            }

            var finalNode = (Row: size, Col: size);

            var coords = Array.ConvertAll(lines[..Math.Min(lines.Length, simulationSize)], x => (Row: int.Parse(x[..x.IndexOf(',')]), Col: int.Parse(x[(x.IndexOf(',') + 1)..])));

            foreach (var (Col, Row) in coords)
            {
                filledPlaces[Row][Col] = true;
            }

            var shortestDistance = new Dictionary<(int Row, int Col), int>();
            var visitedNodes = new HashSet<(int Row, int Col)>();
            shortestDistance[(0, 0)] = 0;

            while (!visitedNodes.Contains(finalNode))
            {
                var currentNode = shortestDistance
                    .Where(x => !visitedNodes.Contains(x.Key))
                    .MinBy(x => x.Value).Key;

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

            Console.WriteLine(shortestDistance[finalNode]);
        }
    }
}
