using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day5 : Day
    {
        public Day5() : base(5)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            string output = "";

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            var stacks = new List<CrateStack>();
            var moves = new List<Move>();

            // Create stacks
            foreach (var l in input)
            {
                if (l.Contains("["))
                {
                    for (var i = 0; i< l.Length; i += 4)
                    {
                        if (l[i] == ' ')
                        {
                            continue;
                        }

                        var stackIndex = (i / 4) + 1;

                        var stack = stacks.Find(cr => cr.Index == stackIndex);
                        if (stack == null)
                        {
                            stack = new CrateStack();
                            stack.Index = stackIndex;
                            stacks.Add(stack);
                        }

                        stack.Items.Add(l[i + 1]);
                    }

                    continue;
                }

                if (l.StartsWith("move"))
                {
                    var moveStr = l.Split(' ');

                    var move = new Move();
                    move.Count = int.Parse(moveStr[1]);
                    move.Source = int.Parse(moveStr[3]);
                    move.Dest = int.Parse(moveStr[5]);
                    moves.Add(move);
                }
            }

            foreach (var crateStack in stacks)
            {
                for (var i = crateStack.Items.Count; i > 0; i--)
                {
                    crateStack.Stack.Push(crateStack.Items[i - 1]);
                }
            }

            foreach (var move in moves)
            {
                var sourceStack = stacks.Find(cr => cr.Index == move.Source);
                var destStack = stacks.Find(cr => cr.Index == move.Dest);

                for (var i = 0; i < move.Count; i++)
                {
                    destStack.Stack.Push(sourceStack.Stack.Pop());
                }
            }

            foreach (var crateStack in stacks.OrderBy(cr => cr.Index))
            {
                output += crateStack.Stack.Peek();
            }

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            // Reset
            output = "";

            foreach (var crateStack in stacks)
            {
                crateStack.Stack = new Stack<char>();

                for (var i = crateStack.Items.Count; i > 0; i--)
                {
                    crateStack.Stack.Push(crateStack.Items[i - 1]);
                }
            }

            foreach (var move in moves)
            {
                var sourceStack = stacks.Find(cr => cr.Index == move.Source);
                var destStack = stacks.Find(cr => cr.Index == move.Dest);

                var crates = new Stack<char>();

                for (var i = 0; i < move.Count; i++)
                {
                    crates.Push(sourceStack.Stack.Pop());
                }

                foreach(var crate in crates)
                {
                    destStack.Stack.Push(crate);
                }
            }

            foreach (var crateStack in stacks.OrderBy(cr => cr.Index))
            {
                output += crateStack.Stack.Peek();
            }

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }

        public class CrateStack
        {
            public CrateStack() { }

            public int Index { get; set; }
            public List<char> Items { get; set; } = new List<char>();
            public Stack<char> Stack { get; set; } = new Stack<char>();
        }

        public class Move
        {
            public Move() { }

            public int Count { get; set; }
            public int Source { get; set; }
            public int Dest { get; set; }
        }
    }
}
