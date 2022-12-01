using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private const string k_fileName = "Data.txt";
    private const int k_numberOfElves = 3;
    
    static void Main(string[] args)
    {
        var lines = File.ReadAllLines(k_fileName);

        var current = 0;
        var totals = new List<int>();
        foreach (var line in lines)
        {
            if (int.TryParse(line, out var cal))
            {
                current += cal;
            }
            else
            {
                totals.Add(current);
                current = 0;
            }
        }
        
        totals.Sort((left, right) => right.CompareTo(left));

        var top3 = 0;
        for (var i = 0; i < k_numberOfElves; ++i)
        {
            top3 += totals[i];
        }
        
        Console.WriteLine($"Highest calories was {totals[0]}");
        Console.WriteLine($"Total calories for top 3 was {top3}");
        Console.ReadLine();
    }
}