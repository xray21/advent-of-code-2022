using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day1 : Day
    {
        public Day1() : base(1)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            var food = new List<int>();
            var elves = new List<List<int>>();

            foreach (var l in input) 
            { 
                if (l.Length == 0)
                {
                    elves.Add(food);
                    food = new List<int>();
                    continue;
                }

                food.Add(int.Parse(l));
            }

            elves.Add(food);

            food = elves.Select(e => e.Sum()).ToList();

            output = food.Max();

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            food.Sort();
            food.Reverse();

            output = 0;
            output += food[0];
            output += food[1];
            output += food[2];

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }
    }
}
