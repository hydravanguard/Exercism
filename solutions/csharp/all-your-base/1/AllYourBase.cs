
using System;
using System.Collections.Generic;
using System.Numerics;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] digits, int outputBase)
    {
        if (inputBase < 2 || outputBase < 2)
            throw new ArgumentException();

        BigInteger number = 0;

        foreach (int digit in digits)
        {
            if (digit < 0 || digit >= inputBase)
                throw new ArgumentException();

            number = number * inputBase + digit;
        }

        if (number == 0)
            return new int[] { 0 };

        var result = new List<int>();

        while (number > 0)
        {
            result.Add((int)(number % outputBase));
            number /= outputBase;
        }

        result.Reverse();

        return result.ToArray();
    }
}
