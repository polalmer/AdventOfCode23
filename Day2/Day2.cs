using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day2
{
    private const int loadedReds = 12;
    private const int loadedGreens = 13;
    private const int loadedBlues = 14;

    public void Part1()
    {
        //string[] input = ["Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green", "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue", "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red", "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red", "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"];
        string[] input = FileReader.ReadFile(@"\Day2\Part1.txt");

        int result = 0;
        foreach (string line in input)
        {
            int gameCount = GetCount(line, GameRegex());
            Console.Write($"\nGame:{gameCount}\n");

            bool toMany = false;
            MatchCollection games = DivideRegex().Matches(line +';');
            foreach(Match game in games)
            {
                int blues = GetCount(game.Value, BlueRegex());
                Console.Write($"Blues:{blues}\n");
                if (blues > loadedBlues)
                {
                    toMany = true;
                    break;
                }

                int reds = GetCount(game.Value, RedRegex());
                Console.Write($"Reds:{reds}\n");
                if (reds > loadedReds)
                {
                    toMany = true;
                    break;
                }

                int greens = GetCount(game.Value, GreenRegex());
                Console.Write($"Greens:{greens}\n");
                if (greens > loadedGreens)
                {
                    toMany = true;
                    break;
                }
            }

            if (toMany) continue;

            result += gameCount;
            Console.Write("added Game!\n");
        }
        Console.WriteLine(result);
    }

    private int GetCount(string line, Regex rx)
    {
        MatchCollection collection = rx.Matches(line);
        int count = 0;
        foreach (Match match in collection.Cast<Match>())
        {
            count += Convert.ToInt32(match.Value);
        }
        return count;
    }

    [GeneratedRegex(@"(\d)+(?=:)")]
    private static partial Regex GameRegex();

    [GeneratedRegex(@"(\d)+(?= blue)")]
    private static partial Regex BlueRegex();

    [GeneratedRegex(@"(\d)+(?= red)")]
    private static partial Regex RedRegex();

    [GeneratedRegex(@"(\d)+(?= green)")]
    private static partial Regex GreenRegex();

    [GeneratedRegex(@"(.*?);")]
    private static partial Regex DivideRegex();
}