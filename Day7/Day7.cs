namespace AdventOfCode23;

public class Day7
{
    readonly string[] input = FileReader.ReadFile(@"\Day7\Test.txt");
    static readonly Dictionary<char, int> possibleCards = new() { { 'A', 14 }, { 'K', 13 }, { 'Q', 12 }, { 'J', 11 }, { 'T', 10 }, { '9', 9 }, { '8', 8 }, { '7', 7 }, { '6', 6 }, { '5', 5 }, { '4', 4 }, { '3', 3 }, { '2', 2 } };

    public void Part1()
    {
        IEnumerable<Game> gamesWithRank = input.Select(game => GetGame(game).SortCards().Score());
    }



    private static Game GetGame(string line)
    {
        int indexSpace = line.IndexOf(' ');
        return new Game()
        {
            Cards = line[..indexSpace],
            Bid = Convert.ToInt32(line[indexSpace..])
        };
    }

    class Game()
    {
        public string Cards;

        public int Bid;

        public HandType HandType;

        public Game SortCards()
        {
            var ordered = Cards.OrderByDescending(c => possibleCards[c]);
            Cards = string.Empty;
            foreach (var card in ordered)
            {
                Cards += card;
            }
            Console.WriteLine(Cards);
            return this;
        }

        public Game Score()
        {
            HandType = GetHandType();
            return this;    
        }

        private HandType GetHandType()
        {
            if (Cards.All(c => c == Cards[0])) return HandType.FiveOfaKind;
            if (Cards.Count(c => (c == Cards[0]) || (c == Cards[1])) == 4) return HandType.FourOfaKind;
            if (Cards.Count(c => (c == Cards[0]) || (c == Cards[1]) || (c == Cards[2])) == 3) return HandType.ThreeOfaKind;


        }
    }
}

public enum HandType
{
    FiveOfaKind,
    FourOfaKind,
    FullHouse,
    ThreeOfaKind,
    TwoPair,
    OnePair,
    None
}