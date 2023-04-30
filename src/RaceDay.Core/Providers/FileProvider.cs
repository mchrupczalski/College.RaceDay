namespace RaceDay.Core.Providers;

public static class FileProvider
{
    public static string ReadFile(string filePath) => !File.Exists(filePath) ? string.Empty : File.ReadAllText(filePath);
    public static void WriteFile(string filePath, string content) => File.WriteAllText(filePath, content);
}