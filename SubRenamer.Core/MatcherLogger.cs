namespace SubRenamer.Core;

public static class MatcherLogger
{
    public static TextWriter Out { get; private set; } = Console.Out;
    public static void SetWriter(TextWriter writer) => Out = writer;
}