using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day2 : Day
    {
        public Day2() : base(2)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            var map = new Dictionary<string, string>();
            map["A"] = "Y";
            map["B"] = "Z";
            map["C"] = "X";

            var scores = new Dictionary<string, int>();
            scores["X"] = 1;
            scores["Y"] = 2;
            scores["Z"] = 3;

            var map2 = new Dictionary<string, string>();
            map2["A"] = "X";
            map2["B"] = "Y";
            map2["C"] = "Z";

            var totalScore = 0;

            foreach (var l in input)
            {
                var curScore = 0;
                var items = l.Split(' ');
                var theirs = items[0];
                var ours = items[1];
                var shoulda = map[theirs];

                // Check outcome
                if (ours == shoulda)
                {
                    curScore += 6;
                }
                else if (ours == map2[theirs])
                {
                    curScore += 3;
                }

                // Add base score
                curScore += scores[ours];

                Console.WriteLine(l + " " + shoulda + " " + curScore);

                totalScore += curScore;
            }

            output = totalScore;

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            totalScore = 0;

            var ties = new Dictionary<string, string>();
            ties["A"] = "X";
            ties["B"] = "Y";
            ties["C"] = "Z";

            var loss = new Dictionary<string, string>();
            loss["A"] = "Z";
            loss["B"] = "X";
            loss["C"] = "Y";

            foreach (var l in input)
            {
                var curScore = 0;
                var items = l.Split(' ');
                var theirs = items[0];
                var outcome = items[1];
                var ours = "";

                if (outcome == "Z")
                {
                    curScore += 6;
                    ours = map[theirs];
                }
                else if (outcome == "Y")
                {
                    curScore += 3;
                    ours = ties[theirs];
                }
                else
                {
                    ours = loss[theirs];
                }

                curScore += scores[ours];

                Console.WriteLine(l + " " + ours + " " + curScore);

                totalScore += curScore;
            }

            output = totalScore;

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }
    }
}
