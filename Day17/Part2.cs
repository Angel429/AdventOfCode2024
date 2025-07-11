using System.Numerics;
using System.Text.RegularExpressions;

namespace Day17
{
    static internal class Part2
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var program = lines[^1]["Program: ".Length..].Split(',');

            Console.WriteLine(FindBestValueForNOutput(program, program.Length - 1, 0));
        }

        private static BigInteger FindBestValueForNOutput(string[] program, int n, BigInteger currentAValue)
        {
            if (n < 0)
            {
                return currentAValue;
            }

            var lowerBound = currentAValue * 8;
            var upperBound = currentAValue * 8 + 8;

            for (var testingAValue = lowerBound; testingAValue < upperBound; testingAValue++)
            {
                var output = RunProgram(program, testingAValue);

                if (output[0] == BigInteger.Parse(program[n]))
                {
                    var bestValueForNPlus1 = FindBestValueForNOutput(program, n - 1, testingAValue);

                    if (bestValueForNPlus1 != -1)
                    {
                        return bestValueForNPlus1;
                    }
                }
            }

            return -1;
        }

        private static BigInteger[] RunProgram(string[] program, BigInteger testingAValue)
        {
            string[] comboInstructions = ["0", "2", "5", "6", "7"];
            var registers = new Dictionary<char, BigInteger>
            {
                ['A'] = testingAValue,
                ['B'] = 0,
                ['C'] = 0
            };
            var output = new List<BigInteger>();

            for (int line = 0; line < program.Length;)
            {
                var isJump = false;
                var literalValue = int.Parse(program[line + 1]);
                var isComboValueNeeded = comboInstructions.Contains(program[line]);
                var comboValue = !isComboValueNeeded ? -1 : literalValue switch
                {
                    0 or 1 or 2 or 3 => literalValue,
                    4 => registers['A'],
                    5 => registers['B'],
                    6 => registers['C'],
                    _ => throw new NotImplementedException()
                };
                switch (program[line])
                {
                    case "0":
                        for (BigInteger i = 0; i < comboValue; i++)
                        {
                            registers['A'] /= 2;
                        }
                        break;

                    case "1":
                        registers['B'] ^= literalValue;
                        break;

                    case "2":
                        registers['B'] = comboValue % 8;
                        break;

                    case "3":
                        if (registers['A'] != 0)
                        {
                            line = literalValue;
                            isJump = true;
                        }
                        break;

                    case "4":
                        registers['B'] ^= registers['C'];
                        break;

                    case "5":
                        output.Add(comboValue % 8);
                        break;

                    case "6":
                        registers['B'] = registers['A'];
                        for (BigInteger i = 0; i < comboValue; i++)
                        {
                            registers['B'] /= 2;
                        }
                        break;

                    case "7":
                        registers['C'] = registers['A'];
                        for (BigInteger i = 0; i < comboValue; i++)
                        {
                            registers['C'] /= 2;
                        }
                        break;

                    default:
                        throw new NotImplementedException();
                }

                if (!isJump)
                {
                    line += 2;
                }
            }

            return [.. output];
        }
    }
}
