
public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        shiftKey %= 26;

        char[] result = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (c >= 'a' && c <= 'z')
            {
                result[i] = (char)('a' + (c - 'a' + shiftKey) % 26);
            }
            else if (c >= 'A' && c <= 'Z')
            {
                result[i] = (char)('A' + (c - 'A' + shiftKey) % 26);
            }
            else
            {
                result[i] = c;
            }
        }

        return new string(result);
    }
}
