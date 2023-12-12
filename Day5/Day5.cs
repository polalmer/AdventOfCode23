using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day5
{
    readonly string[] input = FileReader.ReadFile(@"\Day5\Test.txt");

    public void Part1()
    {
        string[] initialSeeds = InitalSeeds().Matches(input[0]).Select(m => m.ToString()).ToArray();    

    }

    [GeneratedRegex(@"(\d)+")]
    private static partial Regex InitalSeeds();
}
