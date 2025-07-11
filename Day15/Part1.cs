namespace Day15
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var map = new List<List<char>>();
            var robotPosition = (Row: -1, Col: -1);

            var currentLine = 0;
            while (lines[currentLine].Length > 0)
            {
                if (lines[currentLine].Contains('@'))
                {
                    robotPosition = (currentLine, lines[currentLine].IndexOf('@'));
                }
                map.Add([..lines[currentLine]]);
                currentLine++;
            }

            var movements = lines[currentLine..].Aggregate((acum, next) => acum + next);

            foreach (var movement in movements)
            {
                var movementVector = movement switch
                {
                    '^' => (Row: -1, Col: 0),
                    '>' => (Row: 0, Col: 1),
                    'v' => (Row: 1, Col: 0),
                    '<' => (Row: 0, Col: -1),
                    _ => throw new NotImplementedException()
                };

                var availablePosition = robotPosition;

                while (map[availablePosition.Row][availablePosition.Col] != '.' && map[availablePosition.Row][availablePosition.Col] != '#')
                {
                    availablePosition = (availablePosition.Row + movementVector.Row, availablePosition.Col + movementVector.Col);
                }

                if (map[availablePosition.Row][availablePosition.Col] == '.')
                {
                    while (availablePosition != robotPosition)
                    {
                        map[availablePosition.Row][availablePosition.Col] = map[availablePosition.Row - movementVector.Row][availablePosition.Col - movementVector.Col];
                        map[availablePosition.Row - movementVector.Row][availablePosition.Col - movementVector.Col] = '.';
                        availablePosition = (availablePosition.Row - movementVector.Row, availablePosition.Col - movementVector.Col);
                    }

                    robotPosition = (availablePosition.Row + movementVector.Row, availablePosition.Col + movementVector.Col);
                }
            }

            var totalGps = 0L;
            for (var row = 0; row < map.Count; row++)
            {
                for (var col = 0; col < map[row].Count; col++)
                {
                    if (map[row][col] == 'O')
                    {
                        totalGps += 100 * row + col;
                    }
                }
            }
            Console.WriteLine(totalGps);
        }
    }
}
