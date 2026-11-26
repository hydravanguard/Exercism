
using System;
using System.Linq;

public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        var words = phrase.Split(
            new char[] { ' ', '-', '_' },
            StringSplitOptions.RemoveEmptyEntries
        );

        return string.Concat(
            words.Select(word => char.ToUpper(word[0]))
        );
    }
}
