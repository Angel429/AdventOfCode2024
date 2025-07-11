namespace Day09
{
    static internal class Part1
    {
        public static void Execute()
        {
            var memory = File.ReadAllText("input.txt").Select(x => int.Parse(x.ToString())).ToArray();
            var uncompressedMemory = new int[memory.Sum()];
            Array.Fill(uncompressedMemory, -1);

            var currentId = 0;
            var currentMode = Mode.FILE;
            var uncompressedMemoryPointer = 0;
            for (var i = 0; i < memory.Length; i++)
            {
                if (currentMode == Mode.FILE)
                {
                    Array.Fill(uncompressedMemory, currentId, uncompressedMemoryPointer, memory[i]);
                    currentMode = Mode.FREE;
                    currentId++;
                } else
                {
                    currentMode = Mode.FILE;
                }

                uncompressedMemoryPointer += memory[i];
            }

            var freePosition = memory[0];
            var lastPositionWithFiles = uncompressedMemory.Length - 1;

            while (freePosition < lastPositionWithFiles)
            {
                if (uncompressedMemory[lastPositionWithFiles] == -1)
                {
                    lastPositionWithFiles--;
                } else if (uncompressedMemory[freePosition] != -1)
                {
                    freePosition++;
                } else
                {
                    (uncompressedMemory[freePosition], uncompressedMemory[lastPositionWithFiles]) = (uncompressedMemory[lastPositionWithFiles], uncompressedMemory[freePosition]);
                }
            }

            var checksum = 0L;
            for (var i = 0; i < uncompressedMemory.Length && uncompressedMemory[i] != -1; i++)
            {
                checksum += i * uncompressedMemory[i];
            }

            Console.WriteLine(checksum);
        }

        enum Mode
        {
            FILE,
            FREE
        }
    }
}
