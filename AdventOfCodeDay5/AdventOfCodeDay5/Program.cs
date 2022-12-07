using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCodeDay5
{
    class Program
    {
        private struct Stack
        {
            public List<string> Crates;
        }

        private const int k_tabSize = 4;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var stacks = new List<Stack>();

            foreach (var line in lines)
            {
                if (line.StartsWith("move"))
                {
                    ProcessMoveRequest(line, ref stacks);
                }
                else
                {
                    ProcessStackMap(line, ref stacks);
                }
            }

            for(var i = 0; i < stacks.Count; ++i)
            {
                Console.Write(stacks[i].Crates[0]);
            }
        }

        private static void ProcessMoveRequest(string instruction, ref List<Stack> stacks)
        {
            var expression = new System.Text.RegularExpressions.Regex(
                @"move (?<Count>[^,]+) from (?<From>[^,]+) to (?<To>[^,]+)");
            
            var match = expression.Match(instruction);

            var count = int.Parse(match.Groups["Count"].Value);
            var from = int.Parse(match.Groups["From"].Value) - 1;
            var to = int.Parse(match.Groups["To"].Value) - 1;

            var fromStack = stacks[from].Crates;
            var toStack = stacks[to].Crates;

            var take = fromStack.Take(count).ToList();
            fromStack.RemoveRange(0, count);
            
            toStack.InsertRange(0, take);
        }

        private static void ProcessStackMap(string line, ref List<Stack> stacks)
        {
            var lineEntries = GetStacksInLine(line);

            if (stacks.Count == 0)
            {
                CreateBaseStacks(lineEntries.Length, ref stacks);
            }

            for (var i = 0; i < lineEntries.Length; ++i)
            {
                var index = lineEntries[i].IndexOf('[');
                if (index == -1)
                    continue;

                var letter = lineEntries[i].Substring(index + 1, 1);
                
                stacks[i].Crates.Add(letter);
            }
        }

        private static void CreateBaseStacks(int count, ref List<Stack> stacks)
        {
            for (var i = 0; i < count; ++i)
            {
                stacks.Add(new Stack()
                {
                    Crates = new List<string>()
                });
            }
        }

        private static string[] GetStacksInLine(string line)
        {
            var entries = new string[(line.Length + 1) / k_tabSize];
            var index = 0;

            for (int i = 0; i < line.Length; i += k_tabSize)
            {
                entries[index] = line.Substring(i, Math.Min(k_tabSize, line.Length-i));
                index++;
            }

            return entries;
        }
    }
}