using System.Collections.Generic;

namespace Day15
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var map = new List<List<char>>();
            var robotPosition = (Row: -1, Col: -1);

            var currentLine = 0;
            while (lines[currentLine].Length > 0)
            {
                map.Add([.. lines[currentLine].Replace("#", "##").Replace("O", "[]").Replace(".", "..").Replace("@", "@.")]);
                if (map[^1].Contains('@'))
                {
                    robotPosition = (currentLine, map[^1].IndexOf('@'));
                }
                currentLine++;
            }

            var movements = lines[currentLine..].Aggregate((acum, next) => acum + next);

            foreach (var movement in movements)
            {
                //for (int i = 0; i < map.Count; i++)
                //{
                //    Console.WriteLine(new string([.. map[i]]));
                //}
                //Console.WriteLine();
                //Console.WriteLine(movement);

                var movementVector = movement switch
                {
                    '^' => (Row: -1, Col: 0),
                    '>' => (Row: 0, Col: 1),
                    'v' => (Row: 1, Col: 0),
                    '<' => (Row: 0, Col: -1),
                    _ => throw new NotImplementedException()
                };

                var affectedPoints = new List<(int Row, int Col)>
                {
                    (robotPosition.Row, robotPosition.Col)
                };
                var pointsOfInterest = new Queue<(int Row, int Col)>();
                pointsOfInterest.Enqueue((robotPosition.Row + movementVector.Row, robotPosition.Col + movementVector.Col));

                while (pointsOfInterest.Count > 0)
                {
                    var currentPointOfInterest = pointsOfInterest.Dequeue();
                    switch(map[currentPointOfInterest.Row][currentPointOfInterest.Col])
                    {
                        case '#':
                            affectedPoints = [];
                            pointsOfInterest = [];
                            break;
                        case '.':
                            break;
                        case '[':
                        case ']':
                            var boxPoints = new List<(int Row, int Col)>
                            {
                                (currentPointOfInterest.Row, currentPointOfInterest.Col),
                                (currentPointOfInterest.Row, currentPointOfInterest.Col + (map[currentPointOfInterest.Row][currentPointOfInterest.Col] == '[' ? 1 : -1)),
                            };

                            var newBoxPoints = new List<(int Row, int Col)>();
                            foreach (var point in boxPoints)
                            {
                                if (!affectedPoints.Contains(point))
                                {
                                    newBoxPoints.Add(point);
                                    affectedPoints.Add(point);
                                }
                                
                            }
                            boxPoints = newBoxPoints;

                            foreach (var boxPoint in boxPoints)
                            {
                                var newPoint = (boxPoint.Row + movementVector.Row, boxPoint.Col + movementVector.Col);
                                if (!boxPoints.Contains(newPoint))
                                {
                                    pointsOfInterest.Enqueue(newPoint);
                                }
                            }
                            break;
                    }
                }

                if (affectedPoints.Count > 0)
                {
                    affectedPoints.Reverse();
                    foreach (var affectedPoint in affectedPoints)
                    {
                        map[affectedPoint.Row + movementVector.Row][affectedPoint.Col + movementVector.Col] = map[affectedPoint.Row][affectedPoint.Col];
                        map[affectedPoint.Row][affectedPoint.Col] = '.';
                    }
                    map[affectedPoints[^1].Row][affectedPoints[^1].Col] = '.';
                    robotPosition = (robotPosition.Row + movementVector.Row, robotPosition.Col + movementVector.Col);
                }
            }

            //Console.WriteLine();
            //Console.WriteLine(movements[^1]);
            //for (int i = 0; i < map.Count; i++)
            //{
            //    Console.WriteLine(new string([.. map[i]]));
            //}

            var totalGps = 0L;
            for (var row = 0; row < map.Count; row++)
            {
                for (var col = 0; col < map[row].Count; col++)
                {
                    if (map[row][col] == '[')
                    {
                        // The problem suggests that this is the way to calculate the GPS, but the examples and solution say otherwise
                        //totalGps += 100 * Math.Min(row, map.Count - row - 1) + Math.Min(col, map[row].Count - col - 2);
                        totalGps += 100 * row + col;
                    }
                }
            }
            Console.WriteLine(totalGps);
        }
    }
}
