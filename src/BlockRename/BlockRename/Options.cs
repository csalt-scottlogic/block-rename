using CommandLine;

namespace BlockRename;

public class Options
{
    [Option('f', "folder", Required = true, HelpText = "Folder to rename the contents of.")]
    public string? Folder { get; set; }

    [Option('b', "base", Required = true, HelpText = "File name prefix.")]
    public string? NameBase { get; set; }
}
