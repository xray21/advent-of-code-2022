using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day9 : Day
    {
        public Day9() : base(9)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            var hx = 0;
            var hy = 0;
            var tx = 0;
            var ty = 0;

            var locationsVisited = new Dictionary<string, int>();
            locationsVisited["0,0"] = 1;

            for (int i = 0; i < input.Count; i++)
            {
                var l = input[i];

                var parts = l.Split(' ');
                var dir = parts[0];
                var steps = int.Parse(parts[1]);

                Console.WriteLine($"Move {dir} {steps} times");

                while (steps > 0)
                {
                    switch (dir)
                    {
                        case "R":
                            hx++;
                            break;
                        case "U":
                            hy++;
                            break;
                        case "D":
                            hy--;
                            break;
                        case "L":
                            hx--;
                            break;
                    }

                    steps--;

                    if (Math.Abs(hx - tx) >= 2 || Math.Abs(hy - ty) >= 2)
                    {
                        switch (dir)
                        {
                            case "R":
                                ty = hy; tx = hx - 1; break;
                            case "L":
                                ty = hy; tx = hx + 1; break;
                            case "U":
                                ty = hy - 1; tx = hx; break;
                            case "D":
                                ty = hy + 1; tx = hx; break;
                        }

                        var key = $"{tx},{ty}";

                        if (!locationsVisited.ContainsKey(key))
                        {
                            locationsVisited[key] = 0;
                        }

                        locationsVisited[key]++;
                    }
                }
            }

            output = locationsVisited.Keys.Count;

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            var draw = false;

            var head = new Point();
            var tail = new List<Point>();
            
            for (int i = 0; i < 9; i++)
            {
                tail.Add(new Point());
            }

            locationsVisited = new Dictionary<string, int>();
            locationsVisited["0,0"] = 1;

            for (int i = 0; i < input.Count; i++)
            {
                var l = input[i];

                var parts = l.Split(' ');
                var dir = parts[0];
                var steps = int.Parse(parts[1]);

                Console.WriteLine($"Move {dir} {steps} times");

                while (steps > 0)
                {
                    switch (dir)
                    {
                        case "R":
                            head.x++; 
                            break;
                        case "U":
                            head.y++;
                            break;
                        case "D":
                            head.y--;
                            break;
                        case "L":
                            head.x--;
                            break;
                    }

                    steps--;

                   
                    if (draw)
                    {
                        Draw(head, tail);
                    }

                    for (var t = 0; t < tail.Count; t++)
                    {
                        var curHead = t == 0 ? head : tail[t - 1];
                        var curTail = tail[t];

                        if (Math.Abs(curHead.x - curTail.x) >= 2 || Math.Abs(curHead.y - curTail.y) >= 2)
                        {
                            var newDir = "";

                            if (curTail.x - curHead.x == -2 && curTail.y - curHead.y == -2)
                            {
                                curTail.x++;
                                curTail.y++;
                            }
                            else if (curTail.x - curHead.x == 2 && curTail.y - curHead.y == -2)
                            {
                                curTail.x--;
                                curTail.y++;
                            }
                            else if (curTail.x - curHead.x == -2 && curTail.y - curHead.y == 2)
                            {
                                curTail.x++;
                                curTail.y--;
                            }
                            else if (curTail.x - curHead.x == 2 && curTail.y - curHead.y == 2)
                            {
                                curTail.x--;
                                curTail.y--;
                            }
                            else
                            {
                                if (Math.Abs(curHead.x - curTail.x) >= 2)
                                {
                                    if (curHead.x > curTail.x && Math.Abs(curHead.x - curTail.x) >= 2)
                                    {
                                        newDir = "R";
                                    }
                                    else if (curHead.x < curTail.x)
                                    {
                                        newDir = "L";
                                    }
                                }
                                else if (Math.Abs(curHead.y - curTail.y) >= 2)
                                {
                                    if (curHead.y > curTail.y && Math.Abs(curHead.y - curTail.y) >= 2)
                                    {
                                        newDir = "U";
                                    }
                                    else if (curHead.y < curTail.y)
                                    {
                                        newDir = "D";
                                    }
                                }

                                var origTail = new Point(curTail.x, curTail.y);

                                switch (newDir)
                                {
                                    case "R":
                                        curTail.y = curHead.y; curTail.x = curHead.x - 1; break;
                                    case "L":
                                        curTail.y = curHead.y; curTail.x = curHead.x + 1; break;
                                    case "U":
                                        curTail.y = curHead.y - 1; curTail.x = curHead.x; break;
                                    case "D":
                                        curTail.y = curHead.y + 1; curTail.x = curHead.x; break;
                                }
                            }

                            if (draw)
                            {
                                Draw(head, tail);
                            }
                            
                            if (t == tail.Count - 1)
                            {
                                var key = $"{curTail.x},{curTail.y}";
                                if (!locationsVisited.ContainsKey(key))
                                {
                                    locationsVisited[key] = 0;
                                }

                                locationsVisited[key]++;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            output = locationsVisited.Keys.Count;

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }

        public void Draw(Point head, List<Point> tails)
        {
            var grid = new Point();
            grid.x = head.x + 20;
            grid.y = head.y + 20;

            Console.Clear();

            for (var j = -10; j < grid.y; j++)
            {
                for (var i = -10; i < grid.x; i++)
                {
                    if (i == head.x && j == head.y)
                    {
                        Console.Write("H");
                        continue;
                    }

                    var drewTail = false;

                    for (var t = 1; t <= tails.Count; t++)
                    {
                        var tail = tails[t-1];

                        if (i == tail.x && j == tail.y)
                        {
                            Console.Write(t);
                            drewTail = true;
                            break;
                        }
                    }

                    if (drewTail)
                    {
                        continue;
                    }

                    Console.Write(".");
                }

                Console.WriteLine();
            }
        }
    }

    public class Point
    {
        public Point () { }

        public Point (int x, int y) 
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; } = 0;
        public int y { get; set; } = 0;

        public string str { get { return $"{x},{y}"; }  }
    }
}
