using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day6 : Day
    {
        public Day6() : base(6)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput)[0];

            Console.WriteLine("Part 1");

            for (var i = 0; i < input.Length - 4; i++)
            {
                var substr = input.Substring(i, Math.Min(input.Length - i, 4));

                if (substr.Length == substr.ToCharArray().ToList().Distinct().Count())
                {
                    output = i + Math.Min(input.Length - i, 4);
                    break;
                }
            }

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            output = 0;

            for (var i = 0; i < input.Length - 14; i++)
            {
                var substr = input.Substring(i, Math.Min(input.Length - i, 14));

                // Console.WriteLine($"{substr} {substr.Length} {substr.ToCharArray().ToList().Distinct().Count()}");

                if (substr.Length == substr.ToCharArray().ToList().Distinct().Count())
                {
                    output = i + Math.Min(input.Length - i, 14);
                    break;
                }
            }

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }
    }
}
