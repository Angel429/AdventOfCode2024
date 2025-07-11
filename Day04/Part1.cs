namespace Day04
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = Array.ConvertAll(File.ReadAllLines("input.txt"), x => x.ToCharArray());

            var totalSteps = new char[] { 'X', 'M', 'A', 'S' };
            var currentPositions = new HashSet<((int, int), (int, int), Direction)>();
            for (var row = 0; row < lines.Length; row++)
            {
                for (var col = 0; col < lines[row].Length; col++)
                {
                    currentPositions.Add(((row, col), (row, col), Direction.ANY));
                }
            }

            foreach (var currentStep in totalSteps)
            {
                var newPositions = new HashSet<((int, int), (int, int), Direction)>();
                for (var row = 0; row < lines.Length; row++)
                {
                    for (var col = 0; col < lines[row].Length; col++)
                    {
                        if (currentStep == lines[row][col])
                        {
                            foreach (var currentItem in currentPositions.Where(x => x.Item2 == (row, col)))
                            {
                                if (currentItem.Item3 == Direction.ANY)
                                {
                                    // Row above
                                    if (row > 0)
                                    {
                                        if (col > 0)
                                        {
                                            newPositions.Add((currentItem.Item2, (row - 1, col - 1), Direction.UP_LEFT));
                                        }

                                        newPositions.Add((currentItem.Item2, (row - 1, col), Direction.UP));

                                        if (col < lines[row].Length - 1)
                                        {
                                            newPositions.Add((currentItem.Item2, (row - 1, col + 1), Direction.UP_RIGHT));
                                        }
                                    }

                                    // Current row
                                    if (col > 0)
                                    {
                                        newPositions.Add((currentItem.Item2, (row, col - 1), Direction.LEFT));
                                    }

                                    if (col < lines[row].Length - 1)
                                    {
                                        newPositions.Add((currentItem.Item2, (row, col + 1), Direction.RIGHT));
                                    }

                                    // Row below
                                    if (row < lines.Length - 1)
                                    {
                                        if (col > 0)
                                        {
                                            newPositions.Add((currentItem.Item2, (row + 1, col - 1), Direction.DOWN_LEFT));
                                        }

                                        newPositions.Add((currentItem.Item2, (row + 1, col), Direction.DOWN));

                                        if (col < lines[row].Length - 1)
                                        {
                                            newPositions.Add((currentItem.Item2, (row + 1, col + 1), Direction.DOWN_RIGHT));
                                        }
                                    }
                                }
                                else if (GetDirectionFromCoords(currentItem.Item1, currentItem.Item2) == currentItem.Item3)
                                {
                                    switch (currentItem.Item3)
                                    {
                                        case Direction.UP_LEFT:
                                            newPositions.Add((currentItem.Item2, (row - 1, col - 1), Direction.UP_LEFT));
                                            break;
                                        case Direction.UP:
                                            newPositions.Add((currentItem.Item2, (row - 1, col), Direction.UP));
                                            break;
                                        case Direction.UP_RIGHT:
                                            newPositions.Add((currentItem.Item2, (row - 1, col + 1), Direction.UP_RIGHT));
                                            break;
                                        case Direction.LEFT:
                                            newPositions.Add((currentItem.Item2, (row, col - 1), Direction.LEFT));
                                            break;
                                        case Direction.RIGHT:
                                            newPositions.Add((currentItem.Item2, (row, col + 1), Direction.RIGHT));
                                            break;
                                        case Direction.DOWN_LEFT:
                                            newPositions.Add((currentItem.Item2, (row + 1, col - 1), Direction.DOWN_LEFT));
                                            break;
                                        case Direction.DOWN:
                                            newPositions.Add((currentItem.Item2, (row + 1, col), Direction.DOWN));
                                            break;
                                        case Direction.DOWN_RIGHT:
                                            newPositions.Add((currentItem.Item2, (row + 1, col + 1), Direction.DOWN_RIGHT));
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
                currentPositions = newPositions;
            }

            Console.WriteLine(currentPositions.Count);
        }

        private static Direction GetDirectionFromCoords((int, int) coords1, (int, int) coords2)
        {
            if (coords1 == coords2)
            {
                return Direction.ANY;
            }

            Direction[] possibleDirectionsRows;
            if (coords1.Item1 < coords2.Item1)
            {
                possibleDirectionsRows = [Direction.DOWN_LEFT, Direction.DOWN, Direction.DOWN_RIGHT];
            } else if (coords1.Item1 > coords2.Item1)
            {
                possibleDirectionsRows = [Direction.UP_LEFT, Direction.UP, Direction.UP_RIGHT];
            } else
            {
                possibleDirectionsRows = [Direction.LEFT, Direction.RIGHT];
            }

            Direction[] possibleDirectionsColumns;
            if (coords1.Item2 < coords2.Item2)
            {
                possibleDirectionsColumns = [Direction.UP_RIGHT, Direction.RIGHT, Direction.DOWN_RIGHT];
            }
            else if (coords1.Item2 > coords2.Item2)
            {
                possibleDirectionsColumns = [Direction.UP_LEFT, Direction.LEFT, Direction.DOWN_LEFT];
            }
            else
            {
                possibleDirectionsColumns = [Direction.UP, Direction.DOWN];
            }

            var intersection = possibleDirectionsRows.Intersect(possibleDirectionsColumns).ToList();
            return intersection[0];
        }

        enum Direction
        {
            ANY,
            UP_LEFT,
            UP,
            UP_RIGHT,
            LEFT,
            RIGHT,
            DOWN_LEFT,
            DOWN,
            DOWN_RIGHT
        }
    }
}
