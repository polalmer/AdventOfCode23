namespace AdventOfCode23;

public class Day6
{
    //readonly List<(int time, int distance)> Puzzle = [(7, 9), (15, 40), (30, 200)];

    readonly List<(int time, int distance)> Puzzle = [(40, 215), (92,1064),(97,1505),(90,1100)];

    public void Part1()
    {
        int result = 1;
        foreach (var (time, distance) in Puzzle)
        {
            result *= TotalWaysToWin(time, distance);
        }
        Console.WriteLine("\nResult: " + result);
    }

    private int TotalWaysToWin(int time, int distance)
    {
        int wins = 0;
        for (int speed = 1; speed < time; speed++)
        {
            int remainingTimeToTravel = time - speed;
            int drivenDisance = remainingTimeToTravel * speed;
            if (drivenDisance > distance) wins++;
        }

        Console.WriteLine(wins);
        return wins;
    }
}
