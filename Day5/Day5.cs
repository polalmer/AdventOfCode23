using System.Text.RegularExpressions;

namespace AdventOfCode23;

public partial class Day5
{
    readonly string[] input = FileReader.ReadFile(@"\Day5\Puzzle.txt");
    List<Map> maps = [];

    public void Part1()
    {
        string[] initialSeeds = InitalSeeds().Matches(input[0]).Select(m => m.ToString()).ToArray();
        maps = GetAllMaps();
        maps.ForEach(map => map.FillMappings());

        long? lowestLocation = null;
        foreach (string seed in initialSeeds)
        {
            long loc = GetLocationValue(Convert.ToInt64(seed));
            if (lowestLocation is null)
            {
                lowestLocation = loc;
            }
            else if (lowestLocation > loc)
            {
                lowestLocation = loc;
            }
            Console.WriteLine(loc);
        }
        Console.WriteLine("\n" + lowestLocation);
    }

    public void Part2()
    {
        string[] initialSeeds = InitalSeeds().Matches(input[0]).Select(m => m.ToString()).ToArray();
        List<(long start, int range)> seeds = [];
        for (int i = 0; i < initialSeeds.Length; i += 2)
        {
            seeds.Add((Convert.ToInt64(initialSeeds[i]), Convert.ToInt32(initialSeeds[i + 1])));
        }
        maps = GetAllMaps();
        maps.ForEach(map => map.FillMappings());
        List<long> lowestLocation = [];

        Parallel.ForEach(seeds, seed => { lowestLocation.Add(FindSmallest(seed.start, seed.range)); });

        Console.WriteLine("\n" + lowestLocation.Min());
    }

    private long FindSmallest(long start, int range)
    {
        long? lowestLocation = null;
        for (int i = 0; i < range; i++)
        {
            long seed = start + i;

            long loc = GetLocationValue(seed);
            if (lowestLocation is null)
            {
                lowestLocation = loc;
            }
            else if (lowestLocation > loc)
            {
                lowestLocation = loc;
            }
            Console.WriteLine(loc);
        }
        return lowestLocation ?? throw new Exception();
    }

    class Map((string from, string to) categories)
    {
        public List<string> text = [];

        public List<(long from, long to, long maxMapped)> mappings = [];

        public readonly string From = categories.from;

        public readonly string To = categories.to;

        public void FillMappings()
        {
            foreach (string line in text)
            {
                Match match = Mappings().Match(line);
                (long destinationRangeStart, long sourceRangestart, long rangeLength) =
                    (Convert.ToInt64(match.Groups[1].Value), Convert.ToInt64(match.Groups[2].Value), Convert.ToInt64(match.Groups[3].Value));
                mappings.Add((sourceRangestart, destinationRangeStart, rangeLength));
            }
        }
    }

    private List<Map> GetAllMaps()
    {
        List<Map> maps = [];
        int index = 2;
        while (index < input.Length)
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

    private long GetLocationValue(long key)
    {
        foreach (Map map in maps)
        {
            foreach (var (from, to, maxMapped) in map.mappings)
            {
                if (key >= from && key < (from + maxMapped))
                {
                    long diff = key - from;
                    key = to + diff;
                    break;
                }
            }
        }
        return key;
    }

    [GeneratedRegex(@"(\d)+")]
    private static partial Regex InitalSeeds();

    [GeneratedRegex(@"(\w+)-to-(\w+)")]
    private static partial Regex Categories();

    [GeneratedRegex(@"(\w+) (\w+) (\w+)")]
    private static partial Regex Mappings();
}
