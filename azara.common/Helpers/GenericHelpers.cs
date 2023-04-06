namespace azara.common.Helpers;

public class GenericHelpers
{
    public static char cipher(char ch, int key)
    {
        if (!char.IsLetter(ch)) return ch;

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((((ch + key) - d) % 26) + d);
    }

    public static string Encipher(string input, int key)
    {
        string output = string.Empty;

        foreach (char ch in input)
            output += cipher(ch, key);

        return output;
    }

    public static string Decipher(string input, int key) => Encipher(input, 26 - key);
}