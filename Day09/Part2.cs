namespace Day09
{
    static internal class Part2
    {
        public static void Execute()
        {
            var memory = File.ReadAllText("input.txt").Select(x => int.Parse(x.ToString())).ToArray();
            var memoryStructured = new List<(int Size, int Id)>(memory.Length);
            memoryStructured.AddRange(Enumerable.Repeat((-1, -1), memory.Length));

            var currentId = 0;
            var currentMode = Mode.FILE;
            var memoryStructuredPointer = 0;
            for (var i = 0; i < memory.Length; i++)
            {
                if (currentMode == Mode.FILE)
                {
                    memoryStructured[memoryStructuredPointer] = (memory[i], currentId);
                    currentMode = Mode.FREE;
                    currentId++;
                }
                else
                {
                    memoryStructured[memoryStructuredPointer] = (memory[i], -1);
                    currentMode = Mode.FILE;
                }

                memoryStructuredPointer++;
            }

            var alreadyMovedIds = new HashSet<int>();

            for (var fileIndex = memoryStructured.Count - 1; fileIndex >= 0; fileIndex--)
            {
                var hasMoved = false;
                if (memoryStructured[fileIndex].Id != -1 && !alreadyMovedIds.Contains(memoryStructured[fileIndex].Id))
                {
                    for (var freeSpaceIndex = 0; freeSpaceIndex < fileIndex; freeSpaceIndex++)
                    {
                        if (memoryStructured[freeSpaceIndex].Id == -1 && memoryStructured[freeSpaceIndex].Size >= memoryStructured[fileIndex].Size)
                        {
                            var currentFile = memoryStructured[fileIndex];
                            var (freeSize, _) = memoryStructured[freeSpaceIndex];
                            memoryStructured.RemoveAt(fileIndex);
                            memoryStructured.Insert(fileIndex, (currentFile.Size, -1));
                            memoryStructured.RemoveAt(freeSpaceIndex);
                            memoryStructured.InsertRange(freeSpaceIndex, [currentFile, (freeSize - currentFile.Size, -1)]);
                            alreadyMovedIds.Add(currentFile.Id);
                            hasMoved = true;
                            break;
                        }
                    }

                    if (hasMoved)
                    {
                        fileIndex = memoryStructured.Count;
                    }
                }
            }

            var currentRealIndex = 0;
            var checksum = 0L;
            for (var i = 0; i < memoryStructured.Count; i++)
            {
                if (memoryStructured[i].Id != -1)
                {
                    checksum += memoryStructured[i].Id * ((long)currentRealIndex + currentRealIndex + memoryStructured[i].Size - 1) * memoryStructured[i].Size / 2;
                }
                currentRealIndex += memoryStructured[i].Size;
            }

            Console.WriteLine(checksum);

            //var uncompressedMemory = new List<int>();
            //for (var i = 0; i < memoryStructured.Count; i++)
            //{
            //    uncompressedMemory.AddRange(Enumerable.Repeat(memoryStructured[i].Id, memoryStructured[i].Size));
            //}

            //var checksum2 = 0L;
            //for (var i = 0; i < uncompressedMemory.Count; i++)
            //{
            //    if (uncompressedMemory[i] != -1)
            //    {
            //        checksum2 += i * uncompressedMemory[i];
            //    }
            //}

            //Console.WriteLine(checksum2);
        }

        enum Mode
        {
            FILE,
            FREE
        }
    }
}
