using System.Text.RegularExpressions;

namespace Day14
{
    static internal class Part2
    {
        public static void Execute()
        {
            var sizeX = 101;
            var sizeY = 103;

            //var sizeX = 11;
            //var sizeY = 7;

            var lines = File.ReadAllLines("input.txt");
            var regex = new Regex(@"p=(?<px>\d+),(?<py>\d+) v=(?<vx>-?\d+),(?<vy>-?\d+)");
            var robots = new List<((int X, int Y) Position, (int X, int Y) Velocity)>();

            foreach (var line in lines)
            {
                var match = regex.Match(line);
                robots.Add(
                    (
                        (
                            int.Parse(match.Groups["px"].ValueSpan),
                            int.Parse(match.Groups["py"].ValueSpan)
                        ),
                        (
                            int.Parse(match.Groups["vx"].ValueSpan),
                            int.Parse(match.Groups["vy"].ValueSpan)
                        )
                    )
                );
            }

            var display = new int[sizeY,sizeX];

            for (int time = 0; time < sizeX * sizeY; time++)
            {
                List<((int X, int Y) Position, (int X, int Y) Velocity)> newRobots = robots.ConvertAll(x => {
                    var newPosX = (x.Position.X + x.Velocity.X * time) % sizeX;
                    var newPosY = (x.Position.Y + x.Velocity.Y * time) % sizeY;

                    if (newPosX < 0)
                    {
                        newPosX += sizeX;
                    }

                    if (newPosY < 0)
                    {
                        newPosY += sizeY;
                    }

                    return ((newPosX, newPosY), x.Velocity);
                });

                for (int i = 0; i < display.GetLength(0); i++)
                {
                    for (int j = 0; j < display.GetLength(1); j++)
                    {
                        display[i, j] = 0;
                    }
                }

                foreach (var robot in newRobots)
                {
                    display[robot.Position.Y, robot.Position.X]++;
                }

                var showDisplay = true;
                for (int i = 0; i < display.GetLength(0); i++)
                {
                    for (int j = 0; j < display.GetLength(1); j++)
                    {
                        if (display[i,j] == 2)
                        {
                            showDisplay = false;
                            break;
                        }
                    }
                }

                if (showDisplay)
                {
                    Console.WriteLine(time);
                    for (int i = 0; i < display.GetLength(0); i++)
                    {
                        var charArray = new char[display.GetLength(1)];
                        for (int j = 0; j < display.GetLength(1); j++)
                        {
                            charArray[j] = display[i, j] == 0 ? '.' : 'X';
                        }
                        Console.Write(charArray);
                        Console.WriteLine($" {i}");
                    }

                    Console.WriteLine();
                    Console.ReadLine();
                }
            }
        }
    }
}
