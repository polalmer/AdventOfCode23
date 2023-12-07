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


    List<Gear> Gears = [];

    public void Part2()
    {
        foreach (string line in input)
        {
            FindGears(line);
            lineCount++;
        }
        int ratios = 0;
        foreach (Gear gear in Gears)
        {
            if (gear.numbers.Count == 1)
            {
                Console.Write("Single Gear value: \n");
            }
            else
            {
                ratios += gear.numbers[0] * gear.numbers[1];
            }
        }
        Console.WriteLine(ratios);
    }

    private void FindGears(string line)
    {
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
                try
                {
                    var GearPos = CheckGear(pos.start, pos.end);
                    var gear = Gears.FirstOrDefault(g => g.line == GearPos.line && g.column == GearPos.col);
                    int part = Convert.ToInt32(line[pos.start..(pos.end + 1)]);

                    if (gear is null)
                    {
                        gear = new() { column = GearPos.col, line = GearPos.line, numbers = [part] };
                        Gears.Add(gear);
                    }
                    else
                    {
                        gear.numbers.Add(part);
                    }
                    Console.Write(part + "\n");
                }
                catch { }
            }
        }
    }

    private (int line, int col) CheckGear(int start, int end)
    {
        for (int row = lineCount - 1; row <= lineCount + 1; row++)
        {
            if (row < 0 || row >= input.Length) continue;
            string line = input[row];
            for (int col = start - 1; col <= end + 1; col++)
            {
                if (col < 0 || col >= line.Length) continue;
                if (line[col] == '*')
                {
                    return (row, col);
                }
            }
        }
        throw new Exception("Not Gear");
    }

    class Gear
    {
        public int line;
        public int column;
        public List<int> numbers = [];
    }
}
