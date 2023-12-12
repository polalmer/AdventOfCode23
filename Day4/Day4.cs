using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day4
{
    //string[] input = ["Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"];
    readonly string[] input = FileReader.ReadFile(@"\Day4\Puzzle.txt");

    public void Part1()
    {
        foreach (string card in input)
        {
            int colon = card.IndexOf(':');
            int split = card.IndexOf('|');

            List<int> winningNumbers = GetNumbers(card[colon..split]);
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
        List<Card> cards = input.Select(l => new Card(l)).ToList();
        int totalWonCards = cards.Count;
        int cardIndex = 0;
        while (cards.FirstOrDefault() is not null)
        {
            string card = cards.First().card;
            int colon = card.IndexOf(':');
            int split = card.IndexOf('|');

            List<int> winningNumbers = GetNumbers(card[colon..split]);
            List<int> ownNumbers = GetNumbers(card[split..]);

            int cardsWon = 0;
            foreach (int number in ownNumbers)
            {
                if (winningNumbers.Contains(number))
                {
                    cardsWon++;
                }
            }

            if (cardsWon > 0)
            {
                //add won cards
                for (int i = 1; i <= cardsWon; i++)
                {
                    cards[i].Count += cards[0].Count;
                    
                }
            }

            totalWonCards += cardsWon * cards[0].Count;

            //Remove cards
            cards.RemoveAt(0);
            cardIndex++;

            Console.Write($"Won {cardsWon} this round \n");
        }
        Console.Write("Result: " + totalWonCards);
    }

    List<int> GetNumbers(string text)
    {
        var matches = NumberRegex().Matches(text);
        return matches.Cast<Match>().Select(m => Convert.ToInt32(m.Value)).ToList();
    }


    class Card(string card)
    {
        public string card = card;
        public int Count = 1;
    }

    [GeneratedRegex(@"(\d)+")]
    private static partial Regex NumberRegex();
}
