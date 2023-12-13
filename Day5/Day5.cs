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
        maps.ForEach(map => map.FillDictionary());

        int? lowestLocation = null;
        foreach(string seed in initialSeeds)
        {
            int loc = GetLocationValue(Convert.ToInt32(seed));
            if (lowestLocation is null)
            {
                lowestLocation = loc;
            }
            else if(lowestLocation > loc)
            {
                lowestLocation = loc;
            }
            Console.WriteLine(loc);
        }
        Console.WriteLine("\n"+lowestLocation);
    }

    class Map((string from, string to) categories)
    {
        public List<string> text = [];

        /// <summary>
        /// Key = From, Value = To
        /// </summary>
        public Dictionary<int,int> MappedValues = [];

        public readonly string From = categories.from;

        public readonly string To = categories.to;

        public void FillDictionary()
        {
            foreach (string line in text)
            {
                Match match = Mappings().Match(line);
                (int destinationRangeStart, int sourceRangestart, int rangeLength) =
                    (Convert.ToInt32(match.Groups[1].Value), Convert.ToInt32(match.Groups[2].Value), Convert.ToInt32(match.Groups[3].Value));

                while (rangeLength > 0)
                {
                    MappedValues.Add(sourceRangestart,destinationRangeStart);
                    destinationRangeStart++;
                    sourceRangestart++;
                    rangeLength--;
                }
            }
        }
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

    private int GetLocationValue(int key)
    {
        foreach(Map map in maps)
        {
            if (map.MappedValues.TryGetValue(key, out int value))
            {
                key = value;
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
