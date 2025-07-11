namespace Day10
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var currentTrails = new List<((int Row, int Col) Coords, int TrailId)>();
            var currentTrailId = 0;
            for (var row = 0; row < lines.Length; row++)
            {
                for (var col = 0; col < lines[row].Length; col++)
                {
                    if (lines[row][col] == '0')
                    {
                        currentTrails.Add(((row, col), currentTrailId));
                        currentTrailId++;
                    }
                }
            }

            
            for (var currentValue = '1'; currentValue <= '9'; currentValue++)
            {
                var newTrails = new List<((int Row, int Col) Coords, int TrailId)>();
                foreach (var (trailCoords, trailId) in currentTrails)
                {
                    if (trailCoords.Row > 0 && lines[trailCoords.Row - 1][trailCoords.Col] == currentValue)
                    {
                        newTrails.Add(((trailCoords.Row - 1, trailCoords.Col), trailId));
                    }

                    if (trailCoords.Row < lines.Length - 1 && lines[trailCoords.Row + 1][trailCoords.Col] == currentValue)
                    {
                        newTrails.Add(((trailCoords.Row + 1, trailCoords.Col), trailId));
                    }

                    if (trailCoords.Col > 0 && lines[trailCoords.Row][trailCoords.Col - 1] == currentValue)
                    {
                        newTrails.Add(((trailCoords.Row, trailCoords.Col - 1), trailId));
                    }

                    if (trailCoords.Col < lines[0].Length - 1 && lines[trailCoords.Row][trailCoords.Col + 1] == currentValue)
                    {
                        newTrails.Add(((trailCoords.Row, trailCoords.Col + 1), trailId));
                    }
                }
                currentTrails = newTrails;
            }

            Console.WriteLine(currentTrails.Distinct().Count());
        }
    }
}
