using System.Numerics;
using System.Text.RegularExpressions;

namespace Day17
{
    static internal class Part1
    {
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");

            var registerRegex = new Regex(@"Register (?<registerName>\w): (?<registerValue>\d+)");
            var registers = new Dictionary<char, BigInteger>();
            for (int i = 0; i < lines.Length && lines[i] != string.Empty; i++)
            {
                var match = registerRegex.Match(lines[i]);
                registers[match.Groups["registerName"].Value[0]] = BigInteger.Parse(match.Groups["registerValue"].ValueSpan);
            }

            var program = lines[^1]["Program: ".Length..].Split(',');
            var output = new List<BigInteger>();

            string[] comboInstructions = ["0", "2", "5", "6", "7"];

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

            Console.WriteLine($"Register A: {registers['A']}");
            Console.WriteLine($"Register B: {registers['B']}");
            Console.WriteLine($"Register C: {registers['C']}");

            if (output.Count == 1)
            {
                Console.WriteLine(output[0]);
            }

            if (output.Count > 1)
            {
                Console.WriteLine(output.Select(x => x.ToString()).Aggregate((acum, next) => $"{acum},{next}"));
            }
        }
    }
}
