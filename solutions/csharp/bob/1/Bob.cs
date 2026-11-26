
using System;
using System.Linq;

public static class Bob
{
    public static string Response(string statement)
    {
        string trimmed = statement.Trim();

        if (trimmed.Length == 0)
        {
            return "Fine. Be that way!";
        }

        bool hasLetters = trimmed.Any(char.IsLetter);
        bool isShouting = hasLetters &&
                          trimmed.Where(char.IsLetter).All(char.IsUpper);

        bool isQuestion = trimmed.EndsWith("?");

        if (isShouting && isQuestion)
        {
            return "Calm down, I know what I'm doing!";
        }

        if (isShouting)
        {
            return "Whoa, chill out!";
        }

        if (isQuestion)
        {
            return "Sure.";
        }

        return "Whatever.";
    }
}
