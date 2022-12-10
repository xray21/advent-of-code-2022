using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day10 : Day
    {
        public Day10() : base(10)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            var x = 1;
            var cycle = 0;

            var cycles = new List<int>(new int[] {20, 60, 100, 140, 180, 220});
            
            foreach(var l in input)
            {
                var commandParts = l.Split(' ');
                var command = commandParts[0];

                cycle++;

                Console.WriteLine($"{cycle}: {x}");

                if (cycles.Contains(cycle))
                {
                    output += cycle * x;
                }

                if (command == "addx")
                {
                    cycle++;
                    x += int.Parse(commandParts[1]);

                    if (cycles.Contains(cycle))
                    {
                        output += cycle * x;
                    }

                    Console.WriteLine($"{cycle}: {x}");
                }

                if (cycle > cycles.Max())
                {
                    break;
                }
            }

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            var matrix = new char[240];
            x = 1;
            cycle = 1;

            for (var i = 0; i < matrix.Length; i++)
            {
                matrix[i] = '.';
            }

            foreach (var l in input)
            {
                var commandParts = l.Split(' ');
                var command = commandParts[0];
                var mpos = cycle - 1;
                var pos = mpos % 40;

                Console.WriteLine($"{cycle}: MPos {mpos}, Pos {pos}, x {x}, Line {l}");

                if (pos == x - 1 || pos == x || pos == x + 1)
                {
                    matrix[mpos] = '#';
                }

                cycle++;

                if (command == "addx")
                {
                    mpos = cycle - 1;
                    pos = mpos % 40;

                    Console.WriteLine($"{cycle}: MPos {mpos}, Pos {pos}, x {x}, Line {l}");

                    if (pos == x - 1 || pos == x || pos == x + 1)
                    {
                        matrix[mpos] = '#';
                    }

                    x += int.Parse(commandParts[1]);

                    cycle++;
                }
            }

            var str = "";

            for (var i = 1; i <= matrix.Length; i++)
            {
                str += matrix[i - 1];

                if (i % 40 == 0)
                {
                    Console.WriteLine(str);
                    str = "";
                }
            }

            Console.WriteLine(str);
            Console.WriteLine();
        }
    }
}
