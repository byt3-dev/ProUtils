using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            PrintUsage();
            return;
        }

        string action = args[0].ToLower();
        string folder = Path.GetFullPath(args[1]); // Resolves relative paths to absolute
        
        // We target the User level to avoid requiring Admin privileges
        var target = EnvironmentVariableTarget.User;
        string currentPath = Environment.GetEnvironmentVariable("Path", target) ?? "";
        
        // Split into list, removing empty entries and normalizing separators
        var pathList = currentPath.Split(';', StringSplitOptions.RemoveEmptyEntries)
                                  .Select(p => p.TrimEnd(Path.DirectorySeparatorChar))
                                  .ToList();

        if (action == "--add")
        {
            if (pathList.Contains(folder, StringComparer.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Folder is already in PATH.");
            }
            else
            {
                pathList.Add(folder);
                SavePath(pathList, target);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Successfully added: {folder}");
            }
        }
        else if (action == "--remove")
        {
            if (pathList.Contains(folder, StringComparer.OrdinalIgnoreCase))
            {
                pathList = pathList.Where(p => !p.Equals(folder, StringComparison.OrdinalIgnoreCase)).ToList();
                SavePath(pathList, target);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Successfully removed: {folder}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Folder not found in PATH.");
            }
        }
        else
        {
            PrintUsage();
        }
        
        Console.ResetColor();
    }

    static void SavePath(System.Collections.Generic.List<string> paths, EnvironmentVariableTarget target)
    {
        string newPathString = string.Join(";", paths);
        Environment.SetEnvironmentVariable("Path", newPathString, target);
    }

    static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  editpath --add <folder>");
        Console.WriteLine("  editpath --remove <folder>");
    }
}