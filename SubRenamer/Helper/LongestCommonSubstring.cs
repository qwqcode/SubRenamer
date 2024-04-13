namespace SubRenamer.Helper;

public static class LongestCommonSubstring
{
    /**
     * Get Longest Common Substring
     */
    public static string? GetLongestCommonSubstring(string? one, string? two, bool ignoreCase = false)
    {
        if (one == null || two == null) return null;

        var lcsMatrix = CreateLongestCommonSubstringMatrix(one, two, ignoreCase);

        var length = -1;
        var index = -1;
        for (var i = 0; i <= one.Length; i++)
        {
            for (var j = 0; j <= two.Length; j++)
            {
                if (length < lcsMatrix[i, j])
                {
                    length = lcsMatrix[i, j];
                    index = i - length;
                }
            }
        }

        return length > 0 ? one.Substring(index, length) : string.Empty;
    }
        
    private static int[,] CreateLongestCommonSubstringMatrix(string one, string two, bool ignoreCase)
    {
        var lcsMatrix = new int[one.Length + 1, two.Length + 1];
            
        for (var i = 1; i <= one.Length; i++)
        {
            for (var j = 1; j <= two.Length; j++)
            {
                var characterEqual = ignoreCase
                    ? char.ToUpperInvariant(one[i - 1]) == char.ToUpperInvariant(two[j - 1])
                    : one[i - 1] == two[j - 1];
                if (characterEqual)
                {
                    lcsMatrix[i, j] = lcsMatrix[i - 1, j - 1] + 1;
                }
                else
                {
                    lcsMatrix[i, j] = 0;
                }
            }
        }
            
        return lcsMatrix;
    }
}