using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SubRenamer.Matcher;

public static class Diff
{
    public record DiffResult(string Prefix, string Suffix)
    {
        public override string ToString() =>
            $"DiffResult {{ Prefix = \"{Prefix}\", Suffix = \"{Suffix}\" }}";
    }

    public static DiffResult? GetDiffResult(List<string> names)
    {
        // Note: the names is without extension
        names = names.Distinct().ToList();
        if (names.Count < 2) return null;

        for (var i = 0; i < names.Count - 1; i++)
        {
            for (var j = names.Count - 1; j > i; j--) // Start from the end to avoid two names too similar
            {
                var prefix = FindCommonPrefix(names[i], names[j]);
                var suffix = FindCommonSuffix(names[i][prefix.Length..], names[j][prefix.Length..]);

                if (!string.IsNullOrEmpty(prefix))
                {
                    return new DiffResult(prefix, suffix);
                }
            }
        }

        return null;
    }

    private static string FindCommonPrefix(string a, string b)
    {
        var minLength = Math.Min(a.Length, b.Length);
        var prefix = a.Substring(0, minLength);

        for (var i = 0; i < minLength; i++)
        {
            if (char.ToLower(a[i]) != char.ToLower(b[i]))
            {
                prefix = a.Substring(0, i);
                break;
            }
        }

        // Trim end number
        prefix = Regex.Replace(prefix, "\\d+$", "");

        return prefix;
    }

    private static string FindCommonSuffix(string a, string b)
    {
        var i = 0;
        var j = 0;

        while (i < a.Length && j < b.Length)
        {
            // Skip characters
            while (i < a.Length && Skip(a[i])) i++;
            while (j < b.Length && Skip(b[j])) j++;

            // If both are still in valid range, compare the current character
            if (i < a.Length && j < b.Length && char.ToLower(a[i]) == char.ToLower(b[j]))
            {
                return a[i].ToString();
            }

            i++;
            j++;
        }

        return "";

        // Skip [a-z], [A-Z], [0-9], and whitespace. Which is not allowed as a suffix.
        // Because it may be a part of the `Key` (Episode Number).
        // Such as "file [01A] end" and "file [01B] end".
        //
        // But allows Chinese character as a suffix.
        // Such as "file 01 話" and "file 02 話".
        // @see https://github.com/qwqcode/SubRenamer/pull/45
        bool Skip(char c) => char.IsAsciiLetterOrDigit(c) || c == ' ';
    }

    public static string ExtractMatchKeyByDiff(DiffResult? diff, string filename)
    {
        string pattern;
        if (diff is null)
        {
            // if matchData is null then fail down to simple number match
            // (in case that filename sample less than 2)
            pattern = "(\\d+)(?!.*\\d)"; // @link https://stackoverflow.com/questions/5320525/regular-expression-to-match-last-number-in-a-string
        }
        else
        {
            pattern = string.IsNullOrEmpty(diff.Suffix)
                ? $"{Regex.Escape(diff.Prefix)}(\\d+)"
                : $"{Regex.Escape(diff.Prefix)}(.+?){Regex.Escape(diff.Suffix)}";
        }

        var match = Regex.Match(filename, pattern, RegexOptions.IgnoreCase);
        if (!match.Success || match.Groups.Count == 0) return "";

        var key = match.Groups[1].Value.Trim();

        return key;
    }
}