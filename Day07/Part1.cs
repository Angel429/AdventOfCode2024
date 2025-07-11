namespace Day07
{
    static internal class Part1
    {
        public static void Execute()
        {
            var result = 0L;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var lineSplit = line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var testValue = long.Parse(lineSplit[0]);
                var rightSideNumbers = Array.ConvertAll(lineSplit[1].Split(' '), long.Parse);

                var isValid = false;
                var operands = new Operator[rightSideNumbers.Length];
                Array.Fill(operands, Operator.SUM);
                while (!isValid)
                {
                    var currentResult = CalculateResult(rightSideNumbers, operands);
                    if (currentResult == testValue)
                    {
                        isValid = true;
                    } else
                    {
                        var currentIndex = operands.Length - 1;
                        var changedOperand = false;
                        while (!changedOperand && currentIndex >= 0)
                        {
                            if (operands[currentIndex] == Operator.SUM)
                            {
                                operands[currentIndex] = Operator.MULTIPLY;
                                changedOperand = true;
                            } else
                            {
                                operands[currentIndex] = Operator.SUM;
                                currentIndex--;
                            }
                        }

                        if (!changedOperand)
                        {
                            break;
                        }
                    }
                }

                if (isValid)
                {
                    result += testValue;
                }
            }

            Console.WriteLine(result);
        }

        private static long CalculateResult(long[] rightSideNumbers, Operator[] operands)
        {
            var result = rightSideNumbers[0];
            for (var i = 1; i < rightSideNumbers.Length; i++)
            {
                if (operands[i - 1] == Operator.SUM)
                {
                    result += rightSideNumbers[i];
                } else if (operands[i - 1] == Operator.MULTIPLY)
                {
                    result *= rightSideNumbers[i];
                }
            }
            return result;
        }

        enum Operator
        {
            SUM,
            MULTIPLY
        }
    }
}
