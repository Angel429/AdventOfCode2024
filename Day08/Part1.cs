namespace Day08
{
    static internal class Part1
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
                        } else
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

                        var firstAntinode = (antenna1Row + rowDiff, antenna1Col + colDiff);

                        if (firstAntinode.Item1 >= 0 &&
                            firstAntinode.Item1 < map.Length &&
                            firstAntinode.Item2 >= 0 &&
                            firstAntinode.Item2 < map[0].Length &&
                            !antinodesCoords.Contains(firstAntinode))
                        {
                            antinodesCoords.Add(firstAntinode);
                        }

                        var secondAntinode = (antenna2Row - rowDiff, antenna2Col - colDiff);

                        if (secondAntinode.Item1 >= 0 &&
                            secondAntinode.Item1 < map.Length &&
                            secondAntinode.Item2 >= 0 &&
                            secondAntinode.Item2 < map[0].Length &&
                            !antinodesCoords.Contains(secondAntinode))
                        {
                            antinodesCoords.Add(secondAntinode);
                        }
                    }
                }
            }

            Console.WriteLine(antinodesCoords.Count);
        }
    }
}
