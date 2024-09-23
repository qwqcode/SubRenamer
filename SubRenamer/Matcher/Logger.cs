using System;
using System.IO;

namespace SubRenamer.Matcher;

public static class Logger
{
    public static TextWriter Out { get; private set; } = Console.Out;
    public static void SetWriter(TextWriter writer) => Out = writer;
}
