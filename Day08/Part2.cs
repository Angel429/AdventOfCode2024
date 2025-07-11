namespace Day08
{
    static internal class Part2
    {
        public static void Execute()
        {
            var map = Array.ConvertAll(File.ReadAllLines("input.txt"), x => x.ToCharArray());

            var antennaTypesAndCoords = new Dictionary<char, List<(int Row, int Col)>>();
            for (var row = 0; row < map.Length; row++)
            {
                for (var col = 0; col < map[row].Length; col++)
                {
                    if (char.IsLetter(map[row][col]) || char.IsDigit(map[row][col]))
                    {
                        if (antennaTypesAndCoords.TryGetValue(map[row][col], out var currentCoordList))
                        {
                            currentCoordList.Add((row, col));
                        }
                        else
                        {
                            antennaTypesAndCoords[map[row][col]] =
                            [
                                (row, col)
                            ];
                        }
                    }
                }
            }

            var antinodesCoords = new HashSet<(int Row, int Col)>();

            foreach (var (_, antennasCoords) in antennaTypesAndCoords)
            {
                for (var antenna1Index = 0; antenna1Index < antennasCoords.Count; antenna1Index++)
                {
                    var (antenna1Row, antenna1Col) = antennasCoords[antenna1Index];
                    for (var antenna2Index = antenna1Index + 1; antenna2Index < antennasCoords.Count; antenna2Index++)
                    {
                        var (antenna2Row, antenna2Col) = antennasCoords[antenna2Index];
                        var rowDiff = antenna1Row - antenna2Row;
                        var colDiff = antenna1Col - antenna2Col;

                        var multiplier = 0;
                        var currentAntinode = (antenna1Row, antenna1Col);

                        while (currentAntinode.antenna1Row >= 0 &&
                            currentAntinode.antenna1Row < map.Length &&
                            currentAntinode.antenna1Col >= 0 &&
                            currentAntinode.antenna1Col < map[0].Length)
                        {
                            antinodesCoords.Add(currentAntinode);
                            multiplier++;
                            currentAntinode = (antenna1Row + (multiplier * rowDiff), antenna1Col + (multiplier * colDiff));
                        }

                        multiplier = 0;
                        currentAntinode = (antenna2Row, antenna2Col);
                        while (currentAntinode.antenna1Row >= 0 &&
                            currentAntinode.antenna1Row < map.Length &&
                            currentAntinode.antenna1Col >= 0 &&
                            currentAntinode.antenna1Col < map[0].Length)
                        {
                            antinodesCoords.Add(currentAntinode);
                            multiplier++;
                            currentAntinode = (antenna2Row - (multiplier * rowDiff), antenna2Col - (multiplier * colDiff));
                        }
                    }
                }
            }

            Console.WriteLine(antinodesCoords.Count);
        }
    }
}
