using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCodeDay4
{
    class Program
    {
        private struct Data
        {
            public int Min;
            public int Max;
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var containCount = 0;
            foreach (var line in lines)
            {
                var sectionDatas = GetSections(line);
                for (var i = 0; i < sectionDatas.Count - 1; ++i)
                {
                    if (CheckContained(sectionDatas[i], sectionDatas[i + 1]))
                    {
                        containCount++;
                    }
                }
            }

            Console.WriteLine($"Contain count: {containCount}");
        }

        private static List<Data> GetSections(string line)
        {
            var list = new List<Data>();

            var sections = line.Split(",");
            foreach (var section in sections)
            {
                var nums = section.Split('-');
                var data = new Data();
                data.Min = int.Parse(nums[0]);
                data.Max = int.Parse(nums[1]);

                list.Add(data);
            }

            return list;
        }

        private static bool CheckContained(Data left, Data right)
        {
            return left.Min <= right.Min && left.Max >= right.Max
                   || right.Min <= left.Min && right.Max >= left.Max;
        }
    }
}