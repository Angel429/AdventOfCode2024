namespace Day16
{
    static internal class Part1
    {
        public static void Execute()
        {
            var map = Array.ConvertAll(File.ReadAllLines("input.txt"), x => x.ToCharArray());

            var shortestPathToPoint = new Dictionary<Location, long>();
            var visitedPoints = new HashSet<Location>();

            var reindeerPositionDirection = new Location();
            var endPositionDirection = new Location();

            for (int row = 0; row < map.Length; row++)
            {
                for (int col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == 'S')
                    {
                        reindeerPositionDirection.Row = row;
                        reindeerPositionDirection.Col = col;

                        shortestPathToPoint.Add(new Location(row, col, Rotation.North), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.East), 0);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.South), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.West), long.MaxValue);
                    }
                    if (map[row][col] == 'E')
                    {
                        endPositionDirection.Row = row;
                        endPositionDirection.Col = col;

                        shortestPathToPoint.Add(new Location(row, col, Rotation.North), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.East), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.South), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.West), long.MaxValue);
                    }
                    if (map[row][col] == '.')
                    {
                        shortestPathToPoint.Add(new Location(row, col, Rotation.North), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.East), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.South), long.MaxValue);
                        shortestPathToPoint.Add(new Location(row, col, Rotation.West), long.MaxValue);
                    }
                }
            }

            while (visitedPoints.FirstOrDefault(x => x.Row == endPositionDirection.Row && x.Col == endPositionDirection.Col) == null)
            {
                if (visitedPoints.Count % 1000 == 0)
                {
                    Console.WriteLine($"Visited nodes: {visitedPoints.Count}");
                }
                //var currentNode = shortestPathToPoint.FirstOrDefault(x => x.Value != long.MaxValue && !visitedPoints.Contains(x.Key)).Key;
                var currentNode = shortestPathToPoint.Where(x => !visitedPoints.Contains(x.Key)).MinBy(x => x.Value).Key;

                foreach (var newRotation in Enum.GetValues<Rotation>())
                {
                    var newPoint = new Location(currentNode.Row, currentNode.Col, newRotation);
                    if (newRotation != currentNode.Rotation && AreRotationNeighbors(currentNode.Rotation, newRotation) && shortestPathToPoint[newPoint] > shortestPathToPoint[currentNode] + 1000)
                    {
                        shortestPathToPoint[newPoint] = shortestPathToPoint[currentNode] + 1000;
                    }
                }

                var movementVector = currentNode.Rotation switch
                {
                    Rotation.North => (Row: -1, Col: 0),
                    Rotation.East => (Row: 0, Col: 1),
                    Rotation.South => (Row: 1, Col: 0),
                    Rotation.West => (Row: 0, Col: -1),
                    _ => throw new NotImplementedException()
                };

                var advancePoint = new Location
                {
                    Row = currentNode.Row + movementVector.Row,
                    Col = currentNode.Col + movementVector.Col,
                    Rotation = currentNode.Rotation
                };

                if ((map[advancePoint.Row][advancePoint.Col] == '.' || map[advancePoint.Row][advancePoint.Col] == 'E') && shortestPathToPoint[advancePoint] > shortestPathToPoint[currentNode] + 1)
                {
                    shortestPathToPoint[advancePoint] = shortestPathToPoint[currentNode] + 1;
                }

                visitedPoints.Add(currentNode);
            }

            Console.WriteLine(shortestPathToPoint.Where(x => x.Key.Row == endPositionDirection.Row && x.Key.Col == endPositionDirection.Col).Select(x => x.Value).Min());
        }

        private static bool AreRotationNeighbors(Rotation rotation1, Rotation rotation2)
        {
            return rotation1 switch
            {
                Rotation.North or Rotation.South => rotation2 == Rotation.East || rotation2 == Rotation.West,
                Rotation.East or Rotation.West => rotation2 == Rotation.North || rotation2 == Rotation.South,
                _ => throw new NotImplementedException()
            };
        }

        //private static long CalculatePathScore(List<Location> path)
        //{
        //    var score = 0L;
        //    for (int i = 1; i < path.Count; i++)
        //    {
        //        var hasMoved = path[i - 1].Row != path[i].Row || path[i - 1].Col != path[i].Col;
        //        var hasRotated = path[i - 1].Rotation != path[i].Rotation;

        //        if (hasRotated && !AreRotationNeighbors(path[i - 1].Rotation, path[i].Rotation))
        //        {
        //            throw new NotImplementedException();
        //        }

        //        if (hasMoved)
        //        {
        //            if (!hasRotated)
        //            {
        //                score += 1;
        //            } else
        //            {
        //                throw new NotImplementedException();
        //            }
        //        } else
        //        {
        //            if (hasRotated)
        //            {
        //                score += 1000;
        //            } else
        //            {
        //                throw new NotImplementedException();
        //            }
        //        }
        //    }
        //    return score;
        //}

        enum Rotation
        {
            North,
            East,
            South,
            West
        }

        record Location
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public Rotation Rotation { get; set; }

            public Location()
            {
                Row = -1;
                Col = -1;
                Rotation = Rotation.East;
            }

            public Location(int row, int col, Rotation rotation)
            {
                Row = row;
                Col = col;
                Rotation = rotation;
            }
        }
    }
}
