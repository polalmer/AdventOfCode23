using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day3
{
    readonly string[] input = FileReader.ReadFile(@"\Day3\Part1.txt");
    int lineCount = 0;

    public void Part1()
    {
        int EngineResult = 0;
        foreach (string line in input)
        {
            EngineResult += CountParts(line);
            lineCount++;
        }
        Console.WriteLine(EngineResult);
    }

    private int CountParts(string line)
    {
        int result = 0;
        const string numbers = "1234567890";
        for (int i = 0; i < line.Length; i++)
        {
            (int start, int end) pos;
            if (numbers.Contains(line[i]))
            {
                pos.start = i;
                while (i < line.Length && numbers.Contains(line[i]))
                {
                    i++;
                }
                pos.end = i - 1;
                if (CheckIfPart(pos.start, pos.end))
                {
                    int part = Convert.ToInt32(line[pos.start..(pos.end + 1)]);
                    result += part;
                    Console.Write(part + "\n");
                }
            }
        }
        return result;
    }

    private bool CheckIfPart(int start, int end)
    {
        const string NonSymbol = "1234567890.";
        for (int row = lineCount - 1; row <= lineCount + 1; row++)
        {
            if (row < 0 || row >= input.Length) continue;
            string line = input[row];
            for (int col = start - 1; col <= end + 1; col++)
            {
                if (col < 0 || col >= line.Length) continue;
                if (!NonSymbol.Contains(line[col]))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
