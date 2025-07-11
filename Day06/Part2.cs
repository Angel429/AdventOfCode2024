namespace Day06
{
    static internal class Part2
    {
        public static void Execute()
        {
            var map = Array.ConvertAll(File.ReadAllLines("input.txt"), x => x.ToCharArray());

            ((int Row, int Column) Position, Direction Direction) currentPosition = ((-1, -1), Direction.UP);
            for (var row = 0; row < map.Length; row++)
            {
                for (var col = 0; col < map[row].Length; col++)
                {
                    if (map[row][col] == '^')
                    {
                        currentPosition = ((row, col), Direction.UP);
                        break;
                    }
                }
            }

            var originalStaringPosition = currentPosition;
            var originalVisitedPositions = new HashSet<(int Row, int Column)>();
            while (
                currentPosition.Position.Row >= 0 &&
                currentPosition.Position.Row < map.Length &&
                currentPosition.Position.Column >= 0 &&
                currentPosition.Position.Column < map[0].Length)
            {
                originalVisitedPositions.Add(currentPosition.Position);

                var newPosition = currentPosition.Position;
                switch (currentPosition.Direction)
                {
                    case Direction.UP:
                        newPosition.Row--;
                        break;
                    case Direction.DOWN:
                        newPosition.Row++;
                        break;
                    case Direction.LEFT:
                        newPosition.Column--;
                        break;
                    case Direction.RIGHT:
                        newPosition.Column++;
                        break;
                }

                if (newPosition.Row >= 0 &&
                newPosition.Row < map.Length &&
                newPosition.Column >= 0 &&
                newPosition.Column < map[0].Length &&
                map[newPosition.Row][newPosition.Column] == '#')
                {
                    currentPosition.Direction = currentPosition.Direction switch
                    {
                        Direction.UP => Direction.RIGHT,
                        Direction.RIGHT => Direction.DOWN,
                        Direction.DOWN => Direction.LEFT,
                        Direction.LEFT => Direction.UP,
                        _ => throw new Exception(currentPosition.Direction.ToString())
                    };
                }
                else
                {
                    currentPosition.Position = newPosition;
                }
            }
            originalVisitedPositions.Remove(originalStaringPosition.Position);

            var amountOfLoops = 0;
            foreach (var (Row, Column) in originalVisitedPositions)
            {
                map[Row][Column] = '#';
                currentPosition = originalStaringPosition;
                var newVisitedPositions = new HashSet<((int Row, int Column), Direction Direction)>();
                while (
                currentPosition.Position.Row >= 0 &&
                currentPosition.Position.Row < map.Length &&
                currentPosition.Position.Column >= 0 &&
                currentPosition.Position.Column < map[0].Length &&
                newVisitedPositions.Add(currentPosition)
                )
                {
                    var newPosition = currentPosition.Position;
                    switch (currentPosition.Direction)
                    {
                        case Direction.UP:
                            newPosition.Row--;
                            break;
                        case Direction.DOWN:
                            newPosition.Row++;
                            break;
                        case Direction.LEFT:
                            newPosition.Column--;
                            break;
                        case Direction.RIGHT:
                            newPosition.Column++;
                            break;
                    }

                    if (newPosition.Row >= 0 &&
                    newPosition.Row < map.Length &&
                    newPosition.Column >= 0 &&
                    newPosition.Column < map[0].Length &&
                    map[newPosition.Row][newPosition.Column] == '#')
                    {
                        currentPosition.Direction = currentPosition.Direction switch
                        {
                            Direction.UP => Direction.RIGHT,
                            Direction.RIGHT => Direction.DOWN,
                            Direction.DOWN => Direction.LEFT,
                            Direction.LEFT => Direction.UP,
                            _ => throw new Exception(currentPosition.Direction.ToString())
                        };
                    }
                    else
                    {
                        currentPosition.Position = newPosition;
                    }
                }

                map[Row][Column] = '.';

                if (currentPosition.Position.Row >= 0 &&
                currentPosition.Position.Row < map.Length &&
                currentPosition.Position.Column >= 0 &&
                currentPosition.Position.Column < map[0].Length)
                {
                    amountOfLoops++;
                }
            }

            Console.WriteLine(amountOfLoops);
        }

        enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
        }
    }
}
