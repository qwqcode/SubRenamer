using System.Runtime.InteropServices;

namespace SubRenamer.Helper;

public class SystemInfo
{
    public static string GetOSArchPair()
    {
        return $"{GetOSName()}_{GetArchName()}";
    }
    
    public static string GetOSName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return "windows";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return "macos";
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return "linux";
        else return "unknown";
    }

    public static string GetArchName()
    {
        return RuntimeInformation.OSArchitecture switch
        {
            Architecture.X64 => "amd64",
            Architecture.Arm64 => "arm64",
            _ => "unknown"
        };
    }
}