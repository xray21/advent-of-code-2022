using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day3 : Day
    {
        public Day3() : base(3)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            foreach (var l in input)
            {
                var first = l.Substring(0, (l.Length / 2) - 1);
                var second = l.Substring(l.Length / 2);
                var thechar = ' ';

                foreach (var c in l) 
                { 
                    if (second.Contains(c))
                    {
                        thechar = c;
                        break;
                    }
                }

                var val = Char.IsUpper(thechar) ? (int)thechar - 38 : (int)thechar - 96;

                Console.WriteLine(l + " " + thechar + " " + val);

                output += val;
            }

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            output = 0;

            for (var i = 0; i < input.Count; i += 3)
            {
                var x = input[i];
                var y = input[i + 1];
                var z = input[i + 2];
                var thechar = ' ';

                foreach (var c in x)
                {
                    if (y.Contains(c) && z.Contains(c))
                    {
                        thechar = c;
                        break;
                    }
                }

                var val = Char.IsUpper(thechar) ? (int)thechar - 38 : (int)thechar - 96;

                Console.WriteLine($"{x} {y} {z} {thechar} {val}");

                output += val;
            }

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }
    }
}
