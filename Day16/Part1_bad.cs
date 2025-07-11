namespace Day16
{
    static internal class Part1_bad
    {
        public static void Execute()
        {
            var map = Array.ConvertAll(File.ReadAllLines("input.txt"), x => x.ToCharArray());

            //var a = new Location { Row = -1, Col = -1, Rotation = Rotation.East };
            //var b = a;

            //Console.WriteLine($"{a.Row} {a.Col}");
            //Console.WriteLine($"{b.Row} {b.Col}");

            //b.Row = 2;

            //Console.WriteLine($"{a.Row} {a.Col}");
            //Console.WriteLine($"{b.Row} {b.Col}");

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
                    }
                    if (map[row][col] == 'E')
                    {
                        endPositionDirection.Row = row;
                        endPositionDirection.Col = col;
                    }
                }
                if (reindeerPositionDirection.Row != -1)
                {
                    break;
                }
            }

            var bestPath = (Points: new List<Location>(), Score: long.MaxValue);

            var currentPaths = new PriorityQueue<(List<Location> Points, long Score), double>();
            currentPaths.Enqueue(([reindeerPositionDirection], 0), Math.Pow(reindeerPositionDirection.Row - endPositionDirection.Row, 2) + Math.Pow(reindeerPositionDirection.Col - endPositionDirection.Col, 2));

            while (currentPaths.Count > 0)
            {
                var currentPath = currentPaths.Dequeue();

                if (currentPath.Score > bestPath.Score)
                {
                    continue;
                }

                var movementVector = currentPath.Points[^1].Rotation switch
                {
                    Rotation.North => (Row: -1, Col: 0),
                    Rotation.East => (Row: 0, Col: 1),
                    Rotation.South => (Row: 1, Col: 0),
                    Rotation.West => (Row: 0, Col: -1),
                    _ => throw new NotImplementedException()
                };

                var advancePoint = new Location
                {
                    Row = currentPath.Points[^1].Row + movementVector.Row,
                    Col = currentPath.Points[^1].Col + movementVector.Col,
                    Rotation = currentPath.Points[^1].Rotation
                };

                if (currentPath.Points.FirstOrDefault(x => x.Row == advancePoint.Row && x.Col == advancePoint.Col) == null)
                {
                    if (map[advancePoint.Row][advancePoint.Col] == 'E')
                    {
                        List<Location> newPath = [.. currentPath.Points, advancePoint];
                        var newPathScore = currentPath.Score + 1;
                        if (newPathScore < bestPath.Score)
                        {
                            Console.WriteLine($"Found better path: {bestPath.Score} => {newPathScore}");
                            bestPath = (Points: newPath, Score: newPathScore);
                        }
                    }
                    else if (map[advancePoint.Row][advancePoint.Col] == '.')
                    {
                        currentPaths.Enqueue(([.. currentPath.Points, advancePoint], currentPath.Score + 1), Math.Pow(advancePoint.Row - endPositionDirection.Row, 2) + Math.Pow(advancePoint.Col - endPositionDirection.Col, 2));
                    }
                }

                if (currentPath.Points.Count(x => x.Row == currentPath.Points[^1].Row && x.Col == currentPath.Points[^1].Col) <= 1)
                {
                    foreach (var newRotation in Enum.GetValues<Rotation>())
                    {
                        if (currentPath.Points.FirstOrDefault(x => x.Row == currentPath.Points[^1].Row && x.Col == currentPath.Points[^1].Col && x.Rotation == newRotation) == null)
                        {
                            var newLocation = new Location
                            {
                                Row = currentPath.Points[^1].Row,
                                Col = currentPath.Points[^1].Col,
                                Rotation = newRotation
                            };

                            if (AreRotationNeighbors(currentPath.Points[^1].Rotation, newRotation) && !currentPath.Points.Contains(newLocation))
                            {
                                currentPaths.Enqueue(([.. currentPath.Points, newLocation], currentPath.Score + 1000), Math.Pow(newLocation.Row - endPositionDirection.Row, 2) + Math.Pow(newLocation.Col - endPositionDirection.Col, 2));
                            }
                        }
                    }
                }
            }

            Console.WriteLine(bestPath.Score);
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
        }
    }
}
