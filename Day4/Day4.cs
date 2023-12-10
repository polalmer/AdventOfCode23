using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day4
{
    //string[] input = ["Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"];
    readonly string[] input = FileReader.ReadFile(@"\Day4\Test1.txt");

    public void Part1()
    {
        foreach (string card in input)
        {
            int colon = card.IndexOf(':');
            int split = card.IndexOf('|');

            List<int> winningNumbers = GetNumbers(card.Substring(colon, split));
            List<int> ownNumbers = GetNumbers(card[split..]);

            int winners = 0;
            foreach (int number in ownNumbers)
            {
                if (winningNumbers.Contains(number))
                {
                    Console.Write(number + "\n");
                }
            }
            Console.WriteLine();
        }
    }

    public void Part2()
    {
        List<Card> cards = [new(input[0])];
        int cardIndex = 1;
        while (cards.Count > 0)
        {
            string card = cards.First().copys.First();
            int colon = card.IndexOf(':');
            int split = card.IndexOf('|');

            List<int> winningNumbers = GetNumbers(card.Substring(colon, split));
            List<int> ownNumbers = GetNumbers(card[split..]);

            int cardsWon = 0;
            foreach (int number in ownNumbers)
            {
                if (winningNumbers.Contains(number)) cardsWon++;
            }

            //add won cards
            for (int i = 1; i <= cardsWon; i++)
            {
                if (cards.Count < i)
                {
                    cards.Add(new(input[i + cardsWon]));
                }
                else
                {
                    cards[i].copys.Add(input[i + cardsWon]);
                }
            }

            //Remove cards
            if (cards.First().copys.Count == 0)
            {
                cards.RemoveAt(0);
                cardIndex++;
            }
            else
            {
                cards.First().copys.RemoveAt(0);
            }
        }
        Console.Write(cardIndex);
    }

    List<int> GetNumbers(string text)
    {
        var matches = NumberRegex().Matches(text);
        return matches.Cast<Match>().Select(m => Convert.ToInt32(m.Value)).ToList();
    }


    class Card(string firstCard)
    {
        public List<string> copys = [firstCard];
    }

    [GeneratedRegex(@"(\d)+")]
    private static partial Regex NumberRegex();
}
