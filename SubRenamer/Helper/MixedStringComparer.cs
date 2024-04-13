using System.Collections.Generic;

namespace SubRenamer.Helper;

public class MixedStringComparer : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        if (x is null || y is null) return 0;
        return NaturalCompare(x, y);
    }

    private int NaturalCompare(string str1, string str2)
    {
        int length1 = str1.Length;
        int length2 = str2.Length;
        int index1 = 0;
        int index2 = 0;

        while (index1 < length1 && index2 < length2)
        {
            if (char.IsDigit(str1[index1]) && char.IsDigit(str2[index2]))
            {
                int num1 = 0;
                int num2 = 0;

                while (index1 < length1 && char.IsDigit(str1[index1]))
                {
                    num1 = num1 * 10 + (str1[index1] - '0');
                    index1++;
                }

                while (index2 < length2 && char.IsDigit(str2[index2]))
                {
                    num2 = num2 * 10 + (str2[index2] - '0');
                    index2++;
                }

                if (num1 != num2)
                {
                    return num1.CompareTo(num2);
                }
            }
            else
            {
                if (str1[index1] != str2[index2])
                {
                    return str1[index1].CompareTo(str2[index2]);
                }

                index1++;
                index2++;
            }
        }

        return length1.CompareTo(length2);
    }
}