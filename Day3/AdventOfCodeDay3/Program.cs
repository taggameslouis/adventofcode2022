using System;
using System.IO;
using System.Linq;

namespace AdventOfCodeDay3
{
    class Program
    {
        private const int k_lowercaseOffset = 'a' - 1;
        private const int k_uppercaseOffset = 'A' - 27;
        
        static void Main(string[] args)
        {
            var sum = 0;
            
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                var length = line.Length / 2;
                var first = line.Substring(0, length);
                var second = line.Substring(length, length);

                var intersections = first.Intersect(second);

                foreach (var intersection in intersections)
                {
                    var count = intersection - (char.IsUpper(intersection) ? k_uppercaseOffset : k_lowercaseOffset);
                    sum += count;
                }
            }
            
            Console.WriteLine($"Sum: {sum}");
        }
    }
}