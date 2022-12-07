using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeDay3
{
    class Program
    {
        private const int k_lowercaseOffset = 'a' - 1;
        private const int k_uppercaseOffset = 'A' - 27;
        private const int k_groupSize = 3;

        static void Main(string[] args)
        {
            var sum = 0;

            var lines = File.ReadAllLines("input.txt");

            var currentGroup = new List<string>();
            foreach (var line in lines)
            {
                currentGroup.Add(line);

                if (currentGroup.Count < k_groupSize)
                    continue;

                var intersections = FindIntersectionsForGroup(currentGroup);
                currentGroup.Clear();

                foreach (var intersection in intersections)
                {
                    var count = intersection - (char.IsUpper(intersection) ? k_uppercaseOffset : k_lowercaseOffset);
                    sum += count;
                }
            }

            Console.WriteLine($"Sum: {sum}");
        }

        private static List<char> FindIntersectionsForGroup(List<string> currentGroup)
        {
            var uniqueList = currentGroup[0].ToList();
            
            for(var i = 1; i < currentGroup.Count; ++i)
            {
                var list = currentGroup[i].ToList();

                uniqueList = uniqueList.Intersect(list).ToList();
            }

            return uniqueList;
        }
    }
}