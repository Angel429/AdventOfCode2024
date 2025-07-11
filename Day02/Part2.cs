namespace Day02
{
    static internal class Part2
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
                    var isIncreasing = IsArrayIncreasingOrDecreasing(levels);
                    if (isIncreasing != 0)
                    {
                        for (var alreadyRemovedValue = -1; alreadyRemovedValue < levels.Length; alreadyRemovedValue++)
                        {
                            var isValid = true;
                            var arrayLength = levels.Length - (alreadyRemovedValue == -1 ? 0 : 1);
                            for (var i = 1; i < arrayLength; i++)
                            {
                                var previousValue = GetValueFromArrayIndex(levels, i - 1, alreadyRemovedValue);
                                var currentValue = GetValueFromArrayIndex(levels, i, alreadyRemovedValue);
                                if (isIncreasing == 1 && previousValue > currentValue)
                                {
                                    if (alreadyRemovedValue != -1)
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }
                                else if (isIncreasing == -1 && previousValue < currentValue)
                                {
                                    if (alreadyRemovedValue != -1)
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }
                                var distance = Math.Abs(previousValue - currentValue);
                                if (distance >= 1 && distance <= 3)
                                {
                                    if (isIncreasing == 1)
                                    {
                                        if (currentValue < previousValue)
                                        {
                                            isValid = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (currentValue > previousValue)
                                        {
                                            isValid = false;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    isValid = false;
                                    break;
                                }
                            }

                            if (isValid)
                            {
                                safeReports++;
                                break;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(safeReports);
        }

        //public static void Execute()
        //{
        //    var lines = File.ReadAllLines("input.txt");

        //    var safeReports = 0;
        //    foreach (var line in lines)
        //    {
        //        var levels = Array.ConvertAll(line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries), int.Parse);
        //        if (levels.Length >= 2)
        //        {
        //            var isIncreasing = IsArrayIncreasingOrDecreasing(levels);
        //            if (isIncreasing != 0)
        //            {
        //                var alreadyRemovedValue = -1;
        //                var isValid = true;
        //                for (var i = 1; i < levels.Length; i++)
        //                {
        //                    var previousValue = GetValueFromArrayIndex(levels, i - 1, alreadyRemovedValue);
        //                    var currentValue = GetValueFromArrayIndex(levels, i, alreadyRemovedValue);
        //                    if (isIncreasing == 1 && previousValue > currentValue)
        //                    {
        //                        if (alreadyRemovedValue != -1)
        //                        {
        //                            isValid = false;
        //                            break;
        //                        }
        //                        alreadyRemovedValue = i;
        //                    }
        //                    else if (isIncreasing == -1 && previousValue < currentValue)
        //                    {
        //                        if (alreadyRemovedValue != -1)
        //                        {
        //                            isValid = false;
        //                            break;
        //                        }
        //                        alreadyRemovedValue = i - 1;
        //                    }
        //                    var distance = Math.Abs(GetValueFromArrayIndex(levels, i - 1, alreadyRemovedValue) - GetValueFromArrayIndex(levels, i, alreadyRemovedValue));
        //                    if (distance >= 1 && distance <= 3)
        //                    {
        //                        if (isIncreasing == 1)
        //                        {
        //                            if (levels[i] < levels[i - 1])
        //                            {
        //                                allowedFailures--;
        //                                if (allowedFailures < 0)
        //                                {
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (levels[i] > levels[i - 1])
        //                            {
        //                                allowedFailures--;
        //                                if (allowedFailures < 0)
        //                                {
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        isValid = false;
        //                        break;
        //                    }
        //                }

        //                if (allowedFailures >= 0)
        //                {
        //                    safeReports++;
        //                }
        //            }
        //        }
        //    }
        //    Console.WriteLine(safeReports);
        //}

        private static int IsArrayIncreasingOrDecreasing(int[] levels)
        {
            var increasingValues = 0;
            var decreasingValues = 0;

            for (var i = 1; i < levels.Length; i++)
            {
                if (levels[i] > levels[i - 1])
                {
                    increasingValues++;
                }
                else if (levels[i] < levels[i - 1])
                {
                    decreasingValues++;
                }
            }

            if (((increasingValues >= 1) ^ (decreasingValues >= 1)) || increasingValues + decreasingValues - levels.Length < 2)
            {
                if (increasingValues > decreasingValues)
                {
                    return 1;
                }

                return -1;
            }
            else
            {
                return 0;
            }
        }

        //private static bool IsArrayIncreasingOrDecreasing(int[] levels)
        //{
        //    if (levels.Length == 2)
        //    {
        //        return levels[0] < levels[1];
        //    }

        //    var firstElement = levels[0];
        //    var secondElement = levels[1];
        //    var secondToLastElement = levels[^2];
        //    var lastElement = levels[^1];

        //    var increasingCount = 0;
        //    var decreasingCount = 0;

        //    if (firstElement < secondElement)
        //    {
        //        increasingCount++;
        //    } else if (firstElement > secondElement)
        //    {
        //        decreasingCount++;
        //    }

        //    if (secondElement < secondToLastElement)
        //    {
        //        increasingCount++;
        //    }
        //    else if (secondElement > secondToLastElement)
        //    {
        //        decreasingCount++;
        //    }

        //    if (secondToLastElement < lastElement)
        //    {
        //        increasingCount++;
        //    }
        //    else if (secondToLastElement > lastElement)
        //    {
        //        decreasingCount++;
        //    }

        //    return increasingCount > decreasingCount;
        //}

        private static int GetValueFromArrayIndex(int[] levels, int index, int alreadyRemovedValue)
        {
            if (alreadyRemovedValue == -1 || index < alreadyRemovedValue)
            {
                return levels[index];
            }

            return levels[index + 1];
        }

        //public static int GetValueToRemove(int[] levels, int currentIndex, int isAscending)
        //{
        //    if 
        //}
    }
}
