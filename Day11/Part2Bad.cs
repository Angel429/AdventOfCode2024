namespace Day11
{
    static internal class Part2Bad
    {
        static List<long> values = [
            809,
809600,
400,
400481,
400481269760,
197866240,
97760,
3113397760,
1538240,
760,
485760,
240,
240419,
240419852288,
118784512,
58688,
2920858688,
1443112,
713,
394713,
394713290752,
195016448,
96352,
2875796352,
1420848,
702,
702328,
347,
347258,
347258554368,
171570432,
84768,
3248584768,
1605032,
793,
793408,
392,
392222,
392222572544,
193785856,
95744,
3355095744,
1657656,
819,
819720,
405,
405855,
405855977472,
200521728,
99072,
2752899072,
1360128,
672,
75450672,
37278,
3727884160,
1841840,
910,
355910,
355910522880,
175845120,
86880,
3604986880,
1781120,
880,
880440,
435,
435160,
215,
215643,
215643760640,
106543360,
52640,
3645952640,
1801360,
890];
        public static void Execute()
        {
            var lines = File.ReadAllLines("input.txt");
            var stones = Array.ConvertAll(lines[0].Split(' '), long.Parse);

            var stonesAndBlinks = Array.ConvertAll(stones, x => new StoneAndBlinksDto { Value = x, Children = null}).ToList();
            var blinks = 75;
            var cache = new Dictionary<long, StoneAndBlinksDto>();

            foreach (var stoneAndBlink in stonesAndBlinks)
            {
                cache.Add(stoneAndBlink.Value, stoneAndBlink);
            }

            foreach (var stoneAndBlink in stonesAndBlinks)
            {
                EvaluateChildren(stoneAndBlink, cache, 0, blinks);
            }

            var stoneCount = 0L;
            foreach (var stoneAndBlink in stonesAndBlinks)
            {
                var currentNodes = new List<(int Multiplier, StoneAndBlinksDto Node)>
                {
                    (1, stoneAndBlink)
                };

                for (var blink = 0; blink < blinks; blink++)
                {
                    var newNodes = new List<(int Multiplier, StoneAndBlinksDto Node)>();
                    foreach (var currentNode in currentNodes)
                    {
                        if (values.Contains(currentNode.Node.Value))
                        {
                            Console.WriteLine(currentNode.Node.Value);
                        }
                        foreach (var child in currentNode.Node.Children!)
                        {
                            var foundDuplicate = false;
                            for (var j = 0; j < newNodes.Count; j++)
                            {
                                if (newNodes[j].Node == child)
                                {
                                    newNodes[j] = (newNodes[j].Multiplier + currentNode.Multiplier, newNodes[j].Node);
                                    foundDuplicate = true;
                                    break;
                                }
                            }

                            if (!foundDuplicate)
                            {
                                newNodes.Add((currentNode.Multiplier, child));
                            }
                        }
                    }
                    currentNodes = newNodes;
                }
                stoneCount += currentNodes.Select(x => x.Multiplier).Sum();
            }

            Console.WriteLine("a");
        }

        private static bool EvaluateChildren(StoneAndBlinksDto stoneAndBlink, Dictionary<long, StoneAndBlinksDto> cache, int currentDepth, int requiredDepth)
        {
            if (stoneAndBlink.Value == 809)
            {
                //Console.WriteLine(stoneAndBlink.Value);
                return true;
            }

            if (currentDepth == requiredDepth)
            {
                return false;
            }

            //if (cache.TryGetValue(stoneAndBlink.Value, out var cachedValue) && cachedValue.Children != null)
            //{
            //    return false;
            //}

            if (stoneAndBlink.Value == 0)
            {
                var newValue = 1;
                if (!cache.TryGetValue(newValue, out var newNode))
                {
                    newNode = new StoneAndBlinksDto { Value = newValue, Children = null };
                    cache.Add(newNode.Value, newNode);
                }

                stoneAndBlink.Children = [newNode];

                var found = EvaluateChildren(newNode, cache, currentDepth + 1, requiredDepth);
                if (found)
                {
                    //Console.WriteLine(stoneAndBlink.Value);
                }
                return found;
            }
            else
            {
                var digitCountMinus1 = (int)Math.Log10(stoneAndBlink.Value);
                if (digitCountMinus1 % 2 == 1)
                {
                    var divisor = (long)Math.Pow(10, (digitCountMinus1 + 1) / 2);
                    var leftValue = stoneAndBlink.Value / divisor;
                    var rightValue = stoneAndBlink.Value - leftValue * divisor;

                    if (!cache.TryGetValue(leftValue, out var newNode))
                    {
                        newNode = new StoneAndBlinksDto { Value = leftValue, Children = null };
                        cache.Add(newNode.Value, newNode);
                    }

                    if (!cache.TryGetValue(rightValue, out var newNode2))
                    {
                        newNode2 = new StoneAndBlinksDto { Value = rightValue, Children = null };
                        cache.Add(newNode2.Value, newNode2);
                    }

                    stoneAndBlink.Children = [newNode, newNode2];

                    var found1 = EvaluateChildren(newNode, cache, currentDepth + 1, requiredDepth);
                    var found2 = EvaluateChildren(newNode2, cache, currentDepth + 1, requiredDepth);

                    if (found1 || found2)
                    {
                        //Console.WriteLine(stoneAndBlink.Value);
                    }

                    return found1 || found2;
                }
                else
                {
                    var newValue = stoneAndBlink.Value * 2024;
                    if (!cache.TryGetValue(newValue, out var newNode))
                    {
                        newNode = new StoneAndBlinksDto { Value = newValue, Children = null };
                        cache.Add(newNode.Value, newNode);
                    }

                    stoneAndBlink.Children = [newNode];

                    var found = EvaluateChildren(newNode, cache, currentDepth + 1, requiredDepth);
                    if (found)
                    {
                        //Console.WriteLine(stoneAndBlink.Value);
                    }

                    return found;
                }
            }
        }
    }

    class StoneAndBlinksDto
    {
        public required long Value { get; set; }
        public List<StoneAndBlinksDto>? Children { get; set; }
    }
}
