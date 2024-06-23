using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SubRenamer.Matcher;

public class DiffResult
{
    public string Prefix = "";
    public string Suffix = "";
}

public static class Diff
{
    public static DiffResult? GetDiffResult(List<string> names)
    {
        // Note: the names is without extension
        names = names.Distinct().ToList();
        if (names.Count < 2) return null;

        for (var i = 0; i < names.Count - 1; i++)
        {
            for (var j = i + 1; j < names.Count; j++)
            {
                var prefix = FindCommonPrefix(names[i], names[j]);
                var suffix = FindCommonSuffix(prefix, names[i], names[j]);

                if (!string.IsNullOrEmpty(prefix) && !string.IsNullOrEmpty(suffix))
                {
                    return new DiffResult { Prefix = prefix, Suffix = suffix };
                }
            }
        }

        return null;
    }

    private static string FindCommonPrefix(string a, string b)
    {
        var minLength = Math.Min(a.Length, b.Length);
        for (var i = 0; i < minLength; i++)
        {
            if (a[i] != b[i])
            {
                var prefix = a.Substring(0, i);
                // Trim end number
                prefix = Regex.Replace(prefix, "\\d+$", "");
                return prefix;
            }
        }
        return a.Substring(0, minLength);
    }

    private static string FindCommonSuffix(string prefix, string a, string b)
    {
        a = a[prefix.Length..];
        b = b[prefix.Length..];
        var minLength = Math.Min(a.Length, b.Length);
        for (var i = 0; i < minLength; i++)
        {
            if (IsSymbol(a[i]) && IsSymbol(b[i]) && a[i] == b[i])
            {
                return a.Substring(i, 1);
            }
        }

        return "";

        bool IsSymbol(char c) => !char.IsAsciiLetterOrDigit(c) && c != ' '; // skip whitespace
    }
    
    public static string? ExtractMatchKeyByDiff(DiffResult? diff, string filename)
    {
        string pattern;
        if (diff is null)
        {
            // if matchData is null then fail down to simple number match
            // (in case that filename sample less than 2)
            pattern = "(\\d+)(?!.*\\d)"; // @link https://stackoverflow.com/questions/5320525/regular-expression-to-match-last-number-in-a-string
        } else {
            pattern = diff.Suffix is null
                ? $"{Regex.Escape(diff.Prefix)}(\\d+)"
                : $"{Regex.Escape(diff.Prefix)}(.+?){Regex.Escape(diff.Suffix)}";
        }
        
        var match = Regex.Match(filename, pattern);
        if (!match.Success || match.Groups.Count == 0) return "";
        
        var key = match.Groups[1].Value.Trim();

        // check is pure number
        if (key.All(char.IsDigit))
            key = int.Parse(key).ToString(); // '01' -> '1'
        
        return key;
    }
}