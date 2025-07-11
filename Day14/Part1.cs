using System.Text.RegularExpressions;

namespace Day14
{
    static internal class Part1
    {
        public static void Execute()
        {
            var sizeX = 101;
            var sizeY = 103;
            var time = 100;

            var quadrantSizeX = (sizeX / 2) - 1;
            var quadrantSizeY = (sizeY / 2) - 1;

            var middleX = sizeX / 2;
            var middleY = sizeY / 2;

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

            robots = robots.ConvertAll(x => {
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

            var upperLeftCount = 0L;
            var upperRightCount = 0L;
            var lowerLeftCount = 0L;
            var lowerRightCount = 0L;

            foreach (var robot in robots)
            {
                var isLeft = robot.Position.X < middleX;
                var isRight = robot.Position.X > middleX;
                var isUp = robot.Position.Y < middleY;
                var isDown = robot.Position.Y > middleY;

                if (isUp && isLeft)
                {
                    upperLeftCount++;
                }

                if (isUp && isRight)
                {
                    upperRightCount++;
                }

                if (isDown && isLeft)
                {
                    lowerLeftCount++;
                }

                if (isDown && isRight)
                {
                    lowerRightCount++;
                }
            }

            Console.WriteLine(upperLeftCount * upperRightCount * lowerLeftCount * lowerRightCount);
        }
    }
}
