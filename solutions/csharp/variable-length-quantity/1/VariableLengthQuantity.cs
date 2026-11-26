
using System;
using System.Collections.Generic;

public static class VariableLengthQuantity
{
    public static uint[] Encode(uint[] numbers)
    {
        var result = new List<uint>();

        foreach (uint number in numbers)
        {
            var bytes = new List<uint>();
            uint value = number;

            bytes.Add(value & 0x7F);
            value >>= 7;

            while (value > 0)
            {
                bytes.Insert(0, (value & 0x7F) | 0x80);
                value >>= 7;
            }

            result.AddRange(bytes);
        }

        return result.ToArray();
    }

    public static uint[] Decode(uint[] bytes)
    {
        var result = new List<uint>();
        uint value = 0;

        foreach (uint currentByte in bytes)
        {
            value = (value << 7) | (currentByte & 0x7F);

            if ((currentByte & 0x80) == 0)
            {
                result.Add(value);
                value = 0;
            }
        }

        if (bytes.Length > 0 && (bytes[bytes.Length - 1] & 0x80) != 0)
        {
            throw new InvalidOperationException();
        }

        return result.ToArray();
    }
}
