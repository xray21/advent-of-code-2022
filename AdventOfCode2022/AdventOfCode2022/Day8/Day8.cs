using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day8 : Day
    {
        public Day8() : base(8)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            var trees = new List<List<int>>();

            foreach (var item in input)
            {
                var treeRow = item.ToCharArray().ToList().Select(c => int.Parse(c.ToString())).ToList();
                trees.Add(treeRow);
            }

            for (int x = 0; x < trees.Count; x++) {
                var treeRow = trees[x];

                if (x == 0 || x == trees.Count - 1)
                {
                    output += treeRow.Count;
                    continue;
                }

                for (int y = 0; y < treeRow.Count; y++)
                {
                    if (y == 0 || y == treeRow.Count - 1)
                    {
                        output += 1;
                        continue;
                    }

                    if (checkNorth(x, y, trees))
                    {
                        Console.WriteLine($"{x},{y},{trees[x][y]} is visible from the North");
                        output += 1;
                        continue;
                    }

                    if (checkSouth(x, y, trees))
                    {
                        Console.WriteLine($"{x},{y},{trees[x][y]} is visible from the South");
                        output += 1;
                        continue;
                    }

                    if (checkWest(x, y, trees))
                    {
                        Console.WriteLine($"{x},{y},{trees[x][y]} is visible from the West");
                        output += 1;
                        continue;
                    }

                    if (checkEast(x, y, trees))
                    {
                        Console.WriteLine($"{x},{y},{trees[x][y]} is visible from the East");
                        output += 1;
                        continue;
                    }
                }
            }

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            output = 0;

            for (int x = 0; x < trees.Count; x++)
            {
                var treeRow = trees[x];

                if (x == 0 || x == trees.Count - 1)
                {
                    continue;
                }

                for (int y = 0; y < treeRow.Count; y++)
                {
                    if (y == 0 || y == treeRow.Count - 1)
                    {
                        continue;
                    }

                    var treeCountNorth = checkTreeCountNorth(x, y, trees);
                    var treeCountSouth = checkTreeCountSouth(x, y, trees);
                    var treeCountWest = checkTreeCountWest(x, y, trees);
                    var treeCountEast = checkTreeCountEast(x, y, trees);

                    var scenicScore = treeCountNorth * treeCountSouth * treeCountWest * treeCountEast;

                    if (scenicScore > output)
                    {
                        Console.WriteLine($"{x},{y},{trees[x][y]} has a better scenic score {scenicScore} than {output}.");

                        output = scenicScore;
                    }
                }
            }

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }

        public bool checkNorth(int x, int y, List<List<int>> trees)
        {
            var visible = true;

            var i = y - 1;

            var tree = trees[x][y];

            while (visible && i >= 0)
            {
                visible = tree > trees[x][i];
                i--;
            }

            return visible;
        }

        public bool checkSouth(int x, int y, List<List<int>> trees)
        {
            var visible = true;

            var i = y + 1;

            var tree = trees[x][y];

            while (visible && i < trees[0].Count)
            {
                visible = tree > trees[x][i];
                i++;
            }

            return visible;
        }

        public bool checkWest(int x, int y, List<List<int>> trees)
        {
            var visible = true;

            var i = x - 1;

            var tree = trees[x][y];

            while (visible && i >= 0)
            {
                visible = tree > trees[i][y];
                i--;
            }

            return visible;
        }

        public bool checkEast(int x, int y, List<List<int>> trees)
        {
            var visible = true;

            var i = x + 1;

            var tree = trees[x][y];

            while (visible && i < trees[0].Count)
            {
                visible = tree > trees[i][y];
                i++;
            }

            return visible;
        }

        public int checkTreeCountNorth(int x, int y, List<List<int>> trees)
        {
            var treeCount = 0;
            var i = y - 1;
            var tree = trees[x][y];

            while (i >= 0)
            {
                treeCount++;

                if (trees[x][i] >= tree)
                {
                    break;
                }

                i--;
            }

            return treeCount;
        }

        public int checkTreeCountSouth(int x, int y, List<List<int>> trees)
        {
            var treeCount = 0;
            var i = y + 1;
            var tree = trees[x][y];

            while (i < trees[0].Count)
            {
                treeCount++;

                if (trees[x][i] >= tree)
                {
                    break;
                }

                i++;
            }

            return treeCount;
        }

        public int checkTreeCountWest(int x, int y, List<List<int>> trees)
        {
            var treeCount = 0;
            var i = x - 1;
            var tree = trees[x][y];

            while (i >= 0)
            {
                treeCount++;

                if (trees[i][y] >= tree)
                {
                    break;
                }

                i--;
            }

            return treeCount;
        }

        public int checkTreeCountEast(int x, int y, List<List<int>> trees)
        {
            var treeCount = 0;

            var i = x + 1;

            var tree = trees[x][y];

            while (i < trees[0].Count)
            {
                treeCount++;

                if (trees[i][y] >= tree)
                {
                    break;
                }

                i++;
            }

            return treeCount;
        }
    }
}
