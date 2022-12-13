using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day11 : Day
    {
        public Day11() : base(11)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            bool debug = false;
            long output = 0;

            var parts = new List<int> { 20, 10000 };

            foreach (var part in parts)
            {
                Console.WriteLine($"Rounds: {part}");

                var monkeys = new List<Monkey>();

                if (useTestInput)
                {
                    monkeys = new List<Monkey>
                    {
                        new Monkey(0, 23, 2, 3, new List<long> { 79, 98 }, 'm', 19),
                        new Monkey(1, 19, 2, 0, new List<long> { 54, 65, 75, 74 }, 'a', 6),
                        new Monkey(2, 13, 1, 3, new List<long> { 79, 60, 97 }, 'm', -1),
                        new Monkey(3, 17, 0, 1, new List<long> { 74 }, 'a', 3)
                    };
                }
                else
                {
                    monkeys = new List<Monkey>
                    {
                        new Monkey(0, 11, 4, 7, new List<long> { 98, 97, 98, 55, 56, 72 }, 'm', 13),
                        new Monkey(1, 17, 2, 6, new List<long> { 73, 99, 55, 54, 88, 50, 55 }, 'a', 4),
                        new Monkey(2, 5,  6, 5, new List<long> { 67, 98 }, 'm', 11),
                        new Monkey(3, 13, 1, 2, new List<long> { 82, 91, 92, 53, 99 }, 'a', 8),
                        new Monkey(4, 19, 3, 1, new List<long> { 52, 62, 94, 96, 52, 87, 53, 60 }, 'm', -1),
                        new Monkey(5, 2,  7, 0, new List<long> { 94, 80, 84, 79 }, 'a', 5),
                        new Monkey(6, 3,  0, 5, new List<long> { 89 }, 'a', 1),
                        new Monkey(7, 7,  4, 3, new List<long> { 70, 59, 63 }, 'a', 3)
                    };
                }
                
                long worryBound = monkeys.Select(m => m.test).Aggregate(1, (m1, m2) => m1 * m2);

                for (int i = 1; i <= part; i++)
                {
                    if (debug)
                    {
                        Console.WriteLine($"Round {i}: FIGHT");
                    }

                    for (var mi = 0; mi < monkeys.Count; mi++)
                    {
                        var monkey = monkeys.Find(m => m.id == mi);
                        if (monkey == null)
                        {
                            throw new Exception($"{mi} is not a real Monkey");
                        }

                        foreach (var item in monkey.items)
                        {
                            var newWorry = item;

                            // inspect
                            monkey.inspectionCount++;

                            // modify worry level
                            var worryModifier = monkey.opAmount == -1 ? item : monkey.opAmount;

                            switch (monkey.op)
                            {
                                case 'a':
                                    newWorry = item + worryModifier; break;
                                case 'm':
                                    newWorry = item * worryModifier; break;
                                default:
                                    throw new Exception("you done goof'd");
                            }

                            // modify worry level for bored
                            if (part == 20)
                            {
                                newWorry = (long)Math.Floor(newWorry / 3.0);
                            }
                            else
                            {
                                newWorry = newWorry % worryBound;
                            }
                           
                            // perform test and pass to next Monkey
                            Monkey targetMonkey = null;

                            if (newWorry % monkey.test == 0)
                            {
                                targetMonkey = monkeys.Find(m => m.id == monkey.trueTarget);
                            }
                            else
                            {
                                targetMonkey = monkeys.Find(m => m.id == monkey.falseTarget);
                            }

                            targetMonkey.items.Add(newWorry);
                        }

                        // Monkey should end with no items
                        monkey.items = new List<long>();
                    }

                    if (debug)
                    {
                        for (int mi = 0; mi < monkeys.Count; mi++)
                        {
                            var monkey = monkeys.Find(m => m.id == mi);

                            Console.WriteLine($"Monkey {mi}: {monkey.getItems()}");
                        }

                        Console.WriteLine();
                    }
                }

                var orderedMonkeys = monkeys.OrderBy(m => m.inspectionCount).Reverse().ToList();

                var m1 = orderedMonkeys[0];
                var m2 = orderedMonkeys[1];

                output = m1.inspectionCount * m2.inspectionCount;

                Console.WriteLine($"This is the Round {part} Output: {output}");
                Console.WriteLine();
            }
        }
    }

    public class Monkey
    {
        public Monkey() { }

        public Monkey(int id, int test, int trueTarget, int falseTarget, List<long> items, char op, int opAmount)
        {
            this.id = id;
            this.test = test;
            this.trueTarget = trueTarget;
            this.falseTarget = falseTarget;
            this.items = items;
            this.op = op;
            this.opAmount = opAmount;
        }

        public int id { get; set; }
        public long inspectionCount { get; set; }
        public int test { get; set; }
        public int trueTarget { get; set; }
        public int falseTarget { get; set; }

        public List<long> items { get; set; }

        public char op { get; set; }

        public int opAmount { get; set; }

        public string getItems()
        {
            return String.Join(", ", items);
        }
    }
}
