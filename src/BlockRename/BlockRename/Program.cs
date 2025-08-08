using CommandLine;

namespace BlockRename;

internal class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args).WithParsed(static (o) => Run(o));
    }

    private static void Run(Options options)
    {
        if (string.IsNullOrWhiteSpace(options.Folder))
        {
            throw new ArgumentException("Folder name must be provided", nameof(options));
        }
        if (string.IsNullOrWhiteSpace(options.NameBase))
        {
            throw new ArgumentException("Name base part must be provided", nameof(options));
        }
        if (!Directory.Exists(options.Folder))
        {
            throw new ArgumentException("Folder must exist", nameof(options));
        }

        List<string> inputFiles = GetInputFiles(options.Folder).ToList();
        List<string> outputFiles = MangleAndCheckNames(inputFiles, options.NameBase, options.StartingCount);
        if (!outputFiles.Any())
        {
            return;
        }

        for (int i = 0; i < inputFiles.Count; ++i)
        {
            Console.WriteLine($"{Path.GetFileName(inputFiles[i])} => {Path.GetFileName(outputFiles[i])}");
            File.Move(inputFiles[i], outputFiles[i]);
        }
    }

    private static IEnumerable<string> GetInputFiles(string folder)
    {
        try
        {
            var inputFiles = Directory.GetFiles(folder).ToList();
            inputFiles.Sort();
            return inputFiles;
        }
        catch (IOException ex)
        {
            Console.Error.WriteLine(ex.Message);
            return Array.Empty<string>();
        }
    }

    private static List<string> MangleAndCheckNames(IList<string> inputFiles, string newBaseName, int startingCount)
    {
        List<string> outputFiles = inputFiles.Select((n, i) => Path.Combine(Path.GetDirectoryName(n) ?? "", Path.ChangeExtension($"{newBaseName}_{i + startingCount,4:D4}", Path.GetExtension(n)))).ToList();
        if (outputFiles.Where((f, i) => inputFiles.Skip(i).Contains(f)).Any())
        {
            Console.Error.WriteLine("ERROR: Naming clash.  Proceeding would overwrite one or more files.  Please rename to a different base name first.");
            return new List<string>();
        }
        return outputFiles;
    }
}