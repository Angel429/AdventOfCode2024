namespace Day02
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var safeReports = 0;
            foreach (var line in lines)
            {
                var levels = Array.ConvertAll(line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), int.Parse);
                if (levels.Length >= 2)
                {
                    var isIncreasing = levels[1] > levels[0];
                    var isValid = true;
                    for (var i = 1; i < levels.Length; i++)
                    {
                        var distance = Math.Abs(levels[i - 1] - levels[i]);
                        if (distance >= 1 && distance <= 3)
                        {
                            if (isIncreasing)
                            {
                                if (levels[i] < levels[i - 1])
                                {
                                    isValid = false;
                                    break;
                                }
                            } else
                            {
                                if (levels[i] > levels[i - 1])
                                {
                                    isValid = false;
                                    break;
                                }
                            }
                        } else
                        {
                            isValid = false;
                            break;
                        }
                    }

                    if (isValid)
                    {
                        safeReports++;
                    }
                }
            }
            Console.WriteLine(safeReports);
        }
    }
}
