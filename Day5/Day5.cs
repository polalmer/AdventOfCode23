using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day5
{
    readonly string[] input = FileReader.ReadFile(@"\Day5\Test.txt");

    public void Part1()
    {
        //Setup
        string[] initialSeeds = InitalSeeds().Matches(input[0]).Select(m => m.ToString()).ToArray();
        List<Map> maps = GetAllMaps();

        Console.WriteLine();
    }

    class Map((string from, string to) categories)
    {
        public List<string> text = [];

        public readonly string From = categories.from;

        public readonly string To = categories.to;
    }

    private List<Map> GetAllMaps()
    {
        List<Map> maps = [];
        int index = 2;
        while(index < input.Length)
        {
            Map map = new(GetGroupFromNameLine(input[index]));
            index++;
            while (index < input.Length && input[index] != string.Empty)
            {
                map.text.Add(input[index]);
                index++;
            }
            maps.Add(map);
            index++;
        }

        return maps;
    }

    private (string from, string to) GetGroupFromNameLine(string line)
    {
        Match match = Categories().Matches(line).First();
        return (match.Groups[1].ToString(), match.Groups[2].ToString());
    }

    [GeneratedRegex(@"(\d)+")]
    private static partial Regex InitalSeeds();

    [GeneratedRegex(@"(\w+)-to-(\w+)")]
    private static partial Regex Categories();
}
