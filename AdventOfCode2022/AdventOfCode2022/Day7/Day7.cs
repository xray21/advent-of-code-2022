using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode2022.Extensions;

namespace AdventOfCode2022
{
    public class Day7 : Day
    {
        public Day7() : base(7)
        {
        }

        public override void Execute(bool useTestInput = false)
        {
            int output = 0;

            var input = GetInput(useTestInput);

            Console.WriteLine("Part 1");

            var root = new DaDir();
            var curDir = root;

            foreach (var item in input)
            {
                var commandItems = item.Split(' ');

                if (commandItems.Length == 3 && commandItems[2] == "/")
                {
                    continue;
                }

                // Command
                if (commandItems[0] == "$")
                {
                    // ls, so we're going to be listing out some shit
                    if (commandItems[1] == "ls")
                    {
                        continue;
                    }

                    if (commandItems[1] == "cd")
                    {
                        var dest = commandItems[2];
                    
                        if (dest == "..")
                        {
                            curDir = curDir.parent;
                        }
                        else
                        {
                            var destDir = curDir.dirs.Find(d => d.name == dest);
                            if (destDir == null)
                            {
                                throw new Exception($"{dest} does not exist");
                            }

                            curDir = destDir;
                        }

                        continue;
                    }
                }

                if (commandItems[0] == "dir")
                {
                    var newDir = new DaDir();
                    newDir.name = commandItems[1];
                    newDir.parent = curDir;
                    curDir.dirs.Add(newDir);

                    continue;
                }

                // File line
                var file = new DaFile();
                file.size = int.Parse(commandItems[0]);
                file.name = commandItems[1];   
                curDir.files.Add(file);
            }

            output = getCount(root);

            Console.WriteLine($"This is the Part 1 Output: {output}");
            Console.WriteLine();

            Console.WriteLine("Part 2");

            var requiredSpace = 30000000 - (70000000 - root.getSize());

            output = getSmallestDir(root, requiredSpace);

            Console.WriteLine($"This is the Part 2 Output: {output}");
            Console.WriteLine();
        }

        public int getCount(DaDir dir)
        {
            var size = 0;

            var dirSize = dir.getSize();

            if (dirSize <= 100000)
            {
                size += dirSize;
            }

            return size + dir.dirs.Sum(d => getCount(d));
        }

        public int getSmallestDir (DaDir dir, int requiredSpace)
        {
            var size = dir.getSize();
            if (size < requiredSpace)
            {
                return -1;
            }

            var smallest = size;

            foreach (var subDir in dir.dirs)
            {
                var subDirSmallest = getSmallestDir(subDir, requiredSpace);
                if (subDirSmallest == -1)
                {
                    continue;
                }

                smallest = Math.Min(smallest, subDirSmallest);
            }

            return smallest;
        }

        public class DaFile
        {
            public string name { get; set; }
            public int size { get; set; }
        }

        public class DaDir
        {
            public string name { get; set; }
            public List<DaDir> dirs { get; set; } = new List<DaDir>();
            public List<DaFile> files { get; set; } = new List<DaFile>();
            public DaDir parent { get; set; }

            public int getSize()
            {
                return files.Sum(f => f.size) + dirs.Sum(d => d.getSize());
            }
        }
    }
}
