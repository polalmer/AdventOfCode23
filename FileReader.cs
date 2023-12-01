namespace AdventOfCode23;

public static class FileReader
{
    public static string[] ReadFile(string folderFilePath)
    {
        return File.ReadAllLines(Directory.GetCurrentDirectory() + folderFilePath);
    }
}