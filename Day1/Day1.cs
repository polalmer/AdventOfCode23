namespace AdventOfCode23;

public static class Day1
{
    public static void Part1()
    {
        //string[] input = FileReader.ReadFile(@"\Day1\Part1.txt");
        string[] input = ["1abc2", "pqr3stu8vwx", "a1b2c3d4e5f", "treb7uchet"];

        int result = 0;
        foreach (string line in input)
        {
            char first = line.First(c => c == '0' || c == '1' || c == '2' || c == '3'
                                || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9');
            char last = line.Last(c => c == '0' || c == '1' || c == '2' || c == '3'
                                || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9');

            int number = first + last;


            Console.Write(number + "\n");
            result += number;
        }
    }
}