namespace AdventOfCode23;

public class Day7
{
    readonly string[] input = FileReader.ReadFile(@"\Day7\Test.txt");

    public void Part1()
    {
        List<Game> games = input.Select(line => new Game(line)).ToList();


    }
}


class Game
{
    public Game(string line)
    {
        int space = line.IndexOf(' ');
        Cards = line[..space];
        Bid = Convert.ToInt32(line[space..]);
    }

    public string Cards;
    public int Bid;
}