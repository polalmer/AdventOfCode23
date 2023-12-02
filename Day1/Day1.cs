using System.Linq;
using System.Runtime.ExceptionServices;

namespace AdventOfCode23;

public static class Day1
{
    public static void Part1()
    {
        string[] input = FileReader.ReadFile(@"\Day1\Part1.txt");
        //string[] input = ["1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet"];

        int result = 0;
        foreach (string line in input)
        {
            char first = line.First(c => c == '0' || c == '1' || c == '2' || c == '3'
                                || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9');

            char last = line.Last(c => c == '0' || c == '1' || c == '2' || c == '3'
                                || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9');

            int number = Convert.ToInt32(string.Concat(first, last));

            Console.Write(number + "\n");
            result += number;
        }
        Console.WriteLine(result);
    }

    public static void Part2()
    {
        string[] input = FileReader.ReadFile(@"\Day1\Part2.txt");
        //string[] input = ["two1nine", "eightwothree", "abcone2threexyz", "xtwone3four", "4nineeightseven2", "zoneight234", "7pqrstsixteen"];

        int result = 0;
        foreach (string line in input)
        {
            string first = string.Empty;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '0' || c == '1' || c == '2' || c == '3'
                    || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                {
                    first = line[i].ToString();
                    break;
                }
                string part = line[i..];
                part = StartsWithNumber(part);
                if (part == string.Empty) continue;

                first = part;
                break;
            }

            string last = string.Empty;
            for (int i = line.Length - 1; i >= 0; i--)
            {
                char c = line[i];
                if (c == '0' || c == '1' || c == '2' || c == '3'
                    || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                {
                    last = line[i].ToString();
                    break;
                }
                string part = line[i..];
                part = StartsWithNumber(part);
                if (part == string.Empty) continue;

                last = part;
                break;
            }

            int number = Convert.ToInt32(first + last);
            Console.Write(number + "\n");
            result += number;
        }
        Console.WriteLine(result);
    }

    private static string StartsWithNumber(string part)
    {
        if (part.StartsWith("one"))
        {
            return "1";
        }
        if (part.StartsWith("two"))
        {
            return "2";
        }
        if (part.StartsWith("three"))
        {
            return "3";
        }
        if (part.StartsWith("four"))
        {
            return "4";
        }
        if (part.StartsWith("five"))
        {
            return "5";
        }
        if (part.StartsWith("six"))
        {
            return "6";
        }
        if (part.StartsWith("seven"))
        {
            return "7";
        }
        if (part.StartsWith("eight"))
        {
            return "8";
        }
        if (part.StartsWith("nine"))
        {
            return "9";
        }

        return string.Empty;
    }
}