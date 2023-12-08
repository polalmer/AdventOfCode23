using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day4
{
    string[] input = ["Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"];
    //readonly string[] input = FileReader.ReadFile(@"\Day4\Test1.txt");

    public void Part1()
    {
        foreach (string card in input)
        {
            int colon = card.IndexOf(':');
            int split = card.IndexOf('|');

            List<int> winningNumbers = GetNumbers(card.Substring(colon, split));
            List<int> ownNumbers = GetNumbers(card[split..]);

            int winners = 0;
            foreach(int number in ownNumbers)
            {
                if (winningNumbers.Contains(number))
                {
                    Console.Write(number + "\n");
                }
            }
            Console.WriteLine();
        }
    }

    List<int> GetNumbers(string text)
    {
        var matches = NumberRegex().Matches(text);
        return matches.Cast<Match>().Select(m => Convert.ToInt32(m.Value)).ToList();
    }

    [GeneratedRegex(@"(\d)+")]
    private static partial Regex NumberRegex();
}
