using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day4 : Day
    {
        public Day4() : base(4)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            foreach(var l in input)
            {
                var pairs = l.Split(',');
                var first = pairs[0].Split('-');
                var second = pairs[1].Split('-');
                var found = false;

                if (int.Parse(first[0]) >= int.Parse(second[0]) && int.Parse(first[1]) <= int.Parse(second[1]))
                {
                    found = true;
                }

                if (!found && int.Parse(second[0]) >= int.Parse(first[0]) && int.Parse(second[1]) <= int.Parse(first[1]))
                {
                    found = true;
                }

                if (found)
                {
                    output++;
                }

                Console.WriteLine($"{l} {found}");
            }

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            output = 0;

            foreach (var l in input)
            {
                var pairs = l.Split(',');
                var first = pairs[0].Split('-');
                var second = pairs[1].Split('-');
                var found = false;

                if (int.Parse(first[0]) <= int.Parse(second[1]) && int.Parse(first[1]) >= int.Parse(second[0]))
                {
                    found = true;
                }

                if (!found && int.Parse(second[0]) <= int.Parse(first[1]) && int.Parse(second[1]) >= int.Parse(first[0]))
                {
                    found = true;
                }

                if (found)
                {
                    output++;
                }

                Console.WriteLine($"{l} {found}");
            }


            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }
    }
}
